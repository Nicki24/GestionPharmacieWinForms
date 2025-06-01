using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace GestionPharmacieWinForms
{
    public partial class FormDashboard : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";
        private readonly Size btnPharmacieOriginalSize;
        private readonly Size btnEntreesOriginalSize;
        private readonly Size btnAchatsOriginalSize;
        private readonly Size btnRechercherMedicamentsOriginalSize;
        private readonly Size btnVoirRupturesOriginalSize;
        private readonly Size btnMedicamentsPerimesOriginalSize;
        private Size btnDeconnexionOriginalSize;

        private List<float> barHeights;
        private List<float> targetHeights;
        private System.Windows.Forms.Timer animationTimer;
        private const int ANIMATION_STEPS = 20;
        private int animationStep = 0;
        private string userRole;

        public FormDashboard(string userRole)
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.userRole = userRole;

            btnPharmacieOriginalSize = new Size(180, 40);
            btnEntreesOriginalSize = new Size(180, 40);
            btnAchatsOriginalSize = new Size(180, 40);
            btnRechercherMedicamentsOriginalSize = new Size(180, 40);
            btnVoirRupturesOriginalSize = new Size(180, 40);
            btnMedicamentsPerimesOriginalSize = new Size(180, 40);

            cboPeriode.Items.AddRange(new string[] { "Par jour", "Par mois", "Par année" });
            cboPeriode.SelectedIndex = 1;

            ApplyModernStyles();

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 30;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void ApplyModernStyles()
        {
            pnlSidebar.BackColor = Color.FromArgb(26, 32, 44);
            lblTitre.Text = "PharmaShop";
            lblTitre.ForeColor = Color.White;
            lblTitre.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            if (pnlSidebar.Controls.ContainsKey("lblUtilisateur"))
            {
                pnlSidebar.Controls.RemoveByKey("lblUtilisateur");
            }

            PictureBox logoPictureBox = new PictureBox
            {
                Size = new Size(130, 130),
                Location = new Point((pnlSidebar.Width - 100) / 2 - 20, lblTitre.Bottom + 50),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            try
            {
                logoPictureBox.Image = Image.FromFile(@"C:\Users\N I C K I\Pictures\Icones\logo.png");
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, logoPictureBox.Width, logoPictureBox.Height);
                logoPictureBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du logo : {ex.Message}. Le logo ne sera pas affiché.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pnlSidebar.Controls.Add(logoPictureBox);

            int buttonOffset = logoPictureBox.Bottom + 20;
            int currentY = buttonOffset; // Position Y de départ pour le premier bouton
            int buttonSpacing = 10; // Espacement vertical entre les boutons

            // Liste des boutons à repositionner (sauf Déconnexion et Médicaments Périmés pour l'instant)
            var buttons = new List<Button>();
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn && btn.Name != "btnDeconnexion" && btn.Name != "btnMedicamentsPerimes")
                {
                    buttons.Add(btn);
                }
            }

            // Trier les boutons par leur position Y initiale pour respecter l'ordre du designer
            buttons.Sort((a, b) => a.Location.Y.CompareTo(b.Location.Y));

            // Repositionner chaque bouton avec un espacement uniforme
            foreach (Button btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(46, 51, 73);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.FlatStyle = FlatStyle.Flat;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.Size = new Size(180, 40);
                btn.Location = new Point(10, currentY); // Positionner à la nouvelle coordonnée Y

                if (userRole == "Simple")
                {
                    if (btn.Name == "btnGestionPharmacie" || btn.Name == "btnGestionEntrees" || btn.Name == "btnGestionAchats")
                    {
                        btn.Enabled = false;
                    }
                }

                currentY += btn.Height + buttonSpacing; // Ajouter la hauteur du bouton et l'espacement
            }

            // Positionner le bouton "Médicaments Périmés" après le dernier bouton
            Button btnMedicamentsPerimes = new Button
            {
                Name = "btnMedicamentsPerimes",
                Text = "⏳ Médicaments Périmés",
                Size = btnMedicamentsPerimesOriginalSize,
                Location = new Point(10, currentY),
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(46, 51, 73),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            btnMedicamentsPerimes.MouseEnter += BtnMedicamentsPerimes_MouseEnter;
            btnMedicamentsPerimes.MouseLeave += BtnMedicamentsPerimes_MouseLeave;
            btnMedicamentsPerimes.Click += BtnMedicamentsPerimes_Click;
            pnlSidebar.Controls.Add(btnMedicamentsPerimes);

            // Mettre à jour currentY pour le bouton Déconnexion
            currentY += btnMedicamentsPerimes.Height + buttonSpacing;

            // Positionner le bouton Déconnexion avec un espacement plus grand
            Button btnDeconnexion = new Button
            {
                Name = "btnDeconnexion",
                Text = "🔚 Déconnexion",
                Size = new Size(180, 40),
                Location = new Point(10, currentY + 140), // Ajout d'un espacement supplémentaire de 140
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            btnDeconnexionOriginalSize = btnDeconnexion.Size;
            btnDeconnexion.MouseEnter += BtnDeconnexion_MouseEnter;
            btnDeconnexion.MouseLeave += BtnDeconnexion_MouseLeave;
            btnDeconnexion.Click += BtnDeconnexion_Click;
            pnlSidebar.Controls.Add(btnDeconnexion);

            // Le reste de la méthode ApplyModernStyles reste inchangé
            StyleCard(pnlMedicamentCard, Color.FromArgb(255, 255, 255), Color.FromArgb(66, 153, 225));
            StyleCard(pnlEntreeCard, Color.FromArgb(255, 255, 255), Color.FromArgb(72, 187, 120));
            StyleCard(pnlAchatCard, Color.FromArgb(255, 255, 255), Color.FromArgb(237, 137, 54));
            StyleCard(pnlRecetteCard, Color.FromArgb(255, 255, 255), Color.FromArgb(236, 72, 153));

            foreach (Panel card in new[] { pnlMedicamentCard, pnlEntreeCard, pnlAchatCard, pnlRecetteCard })
            {
                foreach (Control ctrl in card.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        if (lbl.Name.Contains("Title"))
                        {
                            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                            lbl.ForeColor = Color.FromArgb(75, 85, 99);
                        }
                        else
                        {
                            lbl.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
                            lbl.ForeColor = Color.FromArgb(17, 24, 39);
                        }
                    }
                }
            }

            pnlTopMedicaments.BackColor = Color.White;
            pnlTopMedicaments.BorderStyle = BorderStyle.None;
            pnlTopMedicaments.Padding = new Padding(10);
            lblTopMedicamentsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTopMedicamentsTitle.ForeColor = Color.FromArgb(17, 24, 39);
            dgvTopMedicaments.BackgroundColor = Color.White;
            dgvTopMedicaments.BorderStyle = BorderStyle.None;
            dgvTopMedicaments.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvTopMedicaments.DefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            dgvTopMedicaments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            dgvTopMedicaments.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            dgvTopMedicaments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTopMedicaments.EnableHeadersVisualStyles = false;

            pnlHistogramme.BackColor = Color.White;
            lblHistogrammeTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHistogrammeTitle.ForeColor = Color.FromArgb(17, 24, 39);
            cboPeriode.BackColor = Color.FromArgb(243, 244, 246);
            cboPeriode.FlatStyle = FlatStyle.Flat;
            cboPeriode.Font = new Font("Segoe UI", 9F);

            btnLoadData.FlatStyle = FlatStyle.Flat;
            btnLoadData.BackColor = Color.FromArgb(66, 153, 225);
            btnLoadData.ForeColor = Color.White;
            btnLoadData.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLoadData.FlatAppearance.BorderSize = 0;
            btnLoadData.Size = new Size(150, 35);
            btnLoadData.Location = new Point((pnlMain.Width - btnLoadData.Width) / 2, 450);
        }

        private void StyleCard(Panel card, Color backColor, Color accentColor)
        {
            card.BackColor = backColor;
            card.BorderStyle = BorderStyle.None;
            card.Tag = accentColor;
            card.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(accentColor, 2))
                {
                    e.Graphics.DrawLine(pen, 10, 20, 10, 50);
                }
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(209, 213, 219), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(209, 213, 219), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(209, 213, 219), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(209, 213, 213), 1, ButtonBorderStyle.Solid);
            };
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            foreach (Control ctrl in card.Controls)
            {
                ctrl.MouseEnter += Card_MouseEnter;
                ctrl.MouseLeave += Card_MouseLeave;
            }
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            Panel card = sender is Panel ? (Panel)sender : (Panel)((Control)sender).Parent;
            card.BackColor = Color.FromArgb(245, 245, 245);
            card.Location = new Point(card.Location.X, card.Location.Y - 5);
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Panel card = sender is Panel ? (Panel)sender : (Panel)((Control)sender).Parent;
            card.BackColor = Color.White;
            card.Location = new Point(card.Location.X, card.Location.Y + 5);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM MEDICAMENT";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    int totalMedicaments = Convert.ToInt32(cmd.ExecuteScalar());
                    lblTotalMedicaments.Text = totalMedicaments.ToString();

                    query = "SELECT COUNT(*) FROM ENTREE";
                    cmd = new MySqlCommand(query, conn);
                    int totalEntrees = Convert.ToInt32(cmd.ExecuteScalar());
                    lblTotalEntrees.Text = totalEntrees.ToString();

                    query = "SELECT COUNT(*) FROM ACHAT";
                    cmd = new MySqlCommand(query, conn);
                    int totalAchats = Convert.ToInt32(cmd.ExecuteScalar());
                    lblTotalAchats.Text = totalAchats.ToString();

                    query = "SELECT SUM(ad.quantite * m.prix_unitaire) AS RecetteTotale " +
                            "FROM achat_details ad " +
                            "JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc";
                    cmd = new MySqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        decimal recetteTotale = Convert.ToDecimal(result);
                        lblRecetteTotale.Text = $"{recetteTotale:F0} Ar";
                    }
                    else
                    {
                        lblRecetteTotale.Text = "0 Ar";
                    }

                    query = "SELECT ad.numMedoc, m.Design, SUM(ad.quantite) AS TotalVendu " +
                            "FROM achat_details ad " +
                            "JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                            "GROUP BY ad.numMedoc, m.Design " +
                            "ORDER BY TotalVendu DESC " +
                            "LIMIT 5";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        dgvTopMedicaments.DataSource = null;
                        dgvTopMedicaments.Rows.Clear();
                        dgvTopMedicaments.Columns.Clear();
                        dgvTopMedicaments.Columns.Add("Message", "Message");
                        dgvTopMedicaments.Rows.Add("Aucune vente enregistrée.");
                    }
                    else
                    {
                        dgvTopMedicaments.DataSource = dt;
                        dgvTopMedicaments.Columns["numMedoc"].HeaderText = "ID";
                        dgvTopMedicaments.Columns["Design"].HeaderText = "Nom";
                        dgvTopMedicaments.Columns["TotalVendu"].HeaderText = "Quantité Vendue";
                    }

                    UpdateHistogramme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateHistogramme()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "";
                    string periodType = cboPeriode.SelectedItem?.ToString() ?? "Par jour";
                    DateTime today = DateTime.Today;

                    switch (periodType)
                    {
                        case "Par jour":
                            DateTime startDate = today.AddDays(-4);
                            query = "SELECT DATE(a.dateAchat) AS Periode, " +
                                    "SUM(ad.quantite * m.prix_unitaire) AS Recette " +
                                    "FROM ACHAT a " +
                                    "JOIN achat_details ad ON a.numAchat = ad.numAchat " +
                                    "JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                                    "WHERE a.dateAchat IS NOT NULL " +
                                    "AND DATE(a.dateAchat) >= @startDate " +
                                    "AND DATE(a.dateAchat) <= @today " +
                                    "GROUP BY DATE(a.dateAchat) " +
                                    "ORDER BY DATE(a.dateAchat)";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@startDate", startDate);
                            cmd.Parameters.AddWithValue("@today", today);
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            var periodLabels = new List<string>();
                            var recettes = new List<double>();
                            for (int i = 0; i < 5; i++)
                            {
                                DateTime currentDate = today.AddDays(-4 + i);
                                string dayLabel = currentDate.ToString("dddd", new CultureInfo("fr-FR"));
                                periodLabels.Add(dayLabel);

                                double recette = 0;
                                var row = dt.AsEnumerable().FirstOrDefault(r => Convert.ToDateTime(r["Periode"]).Date == currentDate.Date);
                                if (row != null && double.TryParse(row["Recette"].ToString(), out double value) && value >= 0)
                                {
                                    recette = value;
                                }
                                recettes.Add(recette);

                                Console.WriteLine($"Jour {dayLabel} ({currentDate.ToShortDateString()}) : Recette = {recette} Ar");
                            }

                            if (recettes.All(r => r == 0))
                            {
                                picHistogramme.Image = null;
                                using (Graphics g = picHistogramme.CreateGraphics())
                                {
                                    g.Clear(picHistogramme.BackColor);
                                    g.DrawString("Aucune donnée à afficher", new Font("Segoe UI", 12), Brushes.Black,
                                        new PointF(picHistogramme.Width / 2 - 100, picHistogramme.Height / 2));
                                }
                                return;
                            }

                            Console.WriteLine($"Nombre de périodes trouvées : {recettes.Count}");
                            DrawHistogram(periodLabels, recettes, periodType);
                            break;

                        case "Par mois":
                            query = "SELECT DATE_FORMAT(a.dateAchat, '%Y-%m') AS Periode, " +
                                    "SUM(ad.quantite * m.prix_unitaire) AS Recette " +
                                    "FROM ACHAT a " +
                                    "JOIN achat_details ad ON a.numAchat = ad.numAchat " +
                                    "JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                                    "WHERE a.dateAchat IS NOT NULL " +
                                    "GROUP BY YEAR(a.dateAchat), MONTH(a.dateAchat) " +
                                    "ORDER BY YEAR(a.dateAchat), MONTH(a.dateAchat) " +
                                    "LIMIT 4";
                            break;

                        case "Par année":
                            query = "SELECT YEAR(a.dateAchat) AS Periode, " +
                                    "SUM(ad.quantite * m.prix_unitaire) AS Recette " +
                                    "FROM ACHAT a " +
                                    "JOIN achat_details ad ON a.numAchat = ad.numAchat " +
                                    "JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                                    "WHERE a.dateAchat IS NOT NULL " +
                                    "GROUP BY YEAR(a.dateAchat) " +
                                    "ORDER BY YEAR(a.dateAchat) " +
                                    "LIMIT 4";
                            break;

                        default:
                            return;
                    }

                    if (periodType != "Par jour")
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            picHistogramme.Image = null;
                            using (Graphics g = picHistogramme.CreateGraphics())
                            {
                                g.Clear(picHistogramme.BackColor);
                                g.DrawString("Aucune donnée à afficher", new Font("Segoe UI", 12), Brushes.Black,
                                    new PointF(picHistogramme.Width / 2 - 100, picHistogramme.Height / 2));
                            }
                            return;
                        }

                        var periodLabels = new List<string>();
                        var recettes = new List<double>();
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Periode"] == DBNull.Value || row["Recette"] == DBNull.Value)
                                continue;

                            if (!double.TryParse(row["Recette"].ToString(), out double recette) || recette < 0)
                                continue;

                            string periode = row["Periode"].ToString();
                            switch (periodType)
                            {
                                case "Par mois":
                                    if (DateTime.TryParse(periode + "-01", out DateTime month))
                                        periodLabels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Month));
                                    break;
                                case "Par année":
                                    periodLabels.Add(periode);
                                    break;
                            }
                            recettes.Add(recette);
                        }

                        if (recettes.Count == 0)
                        {
                            recettes.Add(0);
                            periodLabels.Add("Aucune période");
                        }
                        Console.WriteLine($"Nombre de périodes trouvées : {recettes.Count}");

                        DrawHistogram(periodLabels, recettes, periodType);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de l'histogramme : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawHistogram(List<string> periodLabels, List<double> recettes, string periodType)
        {
            if (picHistogramme.Width <= 0 || picHistogramme.Height <= 0)
            {
                Console.WriteLine($"Erreur : Dimensions de picHistogramme invalides (Width={picHistogramme.Width}, Height={picHistogramme.Height}).");
                return;
            }

            Bitmap bitmap = new Bitmap(picHistogramme.Width, picHistogramme.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                int margin = 70;
                int chartWidth = picHistogramme.Width - 2 * margin;
                int chartHeight = picHistogramme.Height - 2 * margin;
                int chartBottom = picHistogramme.Height - margin;
                int chartLeft = margin;

                double maxScale = 50000;
                Console.WriteLine($"Échelle Y fixée à : {maxScale} Ar");

                int barCount = periodLabels.Count;
                if (barCount == 0) return;

                float spacing = 10;
                float totalSpacing = spacing * (barCount - 1);
                float barWidth = (chartWidth - totalSpacing) / barCount;

                g.DrawLine(new Pen(Color.FromArgb(209, 213, 219), 1), chartLeft, chartBottom, chartLeft, margin);
                g.DrawLine(new Pen(Color.FromArgb(209, 213, 219), 1), chartLeft, chartBottom, chartLeft + chartWidth, chartBottom);

                int numTicksY = 5;
                double interval = maxScale / numTicksY;
                for (int i = 0; i <= numTicksY; i++)
                {
                    float y = chartBottom - (i * chartHeight / numTicksY);
                    g.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                        chartLeft, y, chartLeft + chartWidth, y);
                    double value = interval * i;
                    SizeF size = g.MeasureString($"{value:F0} Ar", new Font("Segoe UI", 8));
                    g.DrawString($"{value:F0} Ar", new Font("Segoe UI", 8), Brushes.Gray,
                        new PointF(chartLeft - size.Width - 10, y - size.Height / 2));
                }

                for (int i = 0; i <= barCount; i++)
                {
                    float x = chartLeft + i * (barWidth + spacing);
                    g.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                        x, chartBottom, x, margin);
                }

                using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                {
                    string yAxisTitle = "Recette (Ar)";
                    SizeF titleSize = g.MeasureString(yAxisTitle, font);
                    g.RotateTransform(-90);
                    g.DrawString(yAxisTitle, font, Brushes.Gray,
                        new PointF(-(margin + chartHeight / 2 + titleSize.Width / 2), margin - 73));
                    g.RotateTransform(90);
                }

                using (Font font = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    string xAxisTitle = "Période";
                    SizeF titleSize = g.MeasureString(xAxisTitle, font);
                    g.DrawString(xAxisTitle, font, Brushes.Gray,
                        new PointF(chartLeft + chartWidth / 2 - titleSize.Width / 2, chartBottom + 30));
                }

                barHeights = new List<float>();
                targetHeights = new List<float>();
                for (int i = 0; i < barCount; i++)
                {
                    float height = (float)(recettes[i] / maxScale * chartHeight);
                    if (height < 2) height = 2;
                    if (float.IsNaN(height) || float.IsInfinity(height)) height = 2;
                    targetHeights.Add(height);
                    barHeights.Add(0);
                }

                for (int i = 0; i < barCount; i++)
                {
                    float x = chartLeft + i * (barWidth + spacing);
                    float y = chartBottom - barHeights[i];

                    Brush barBrush = (i == barCount - 1) ? new SolidBrush(Color.FromArgb(66, 153, 225)) : new SolidBrush(Color.FromArgb(96, 165, 250));
                    g.FillRectangle(barBrush, x, y, barWidth, barHeights[i]);
                    g.DrawRectangle(new Pen(Color.FromArgb(209, 213, 219)), x, y, barWidth, barHeights[i]);

                    if (recettes[i] > 0)
                    {
                        g.FillEllipse(Brushes.Black, x + barWidth / 2 - 3, y - 3, 6, 6);
                    }

                    string valueText = $"{recettes[i]:F0} Ar";
                    SizeF valueSize = g.MeasureString(valueText, new Font("Segoe UI", 7, FontStyle.Bold));
                    g.DrawString(valueText, new Font("Segoe UI", 7, FontStyle.Bold), Brushes.Black,
                        new PointF(x + barWidth / 2 - valueSize.Width / 2, y - valueSize.Height - 10));
                }

                for (int i = 0; i < barCount; i++)
                {
                    float x = chartLeft + i * (barWidth + spacing);
                    string periodLabel = periodLabels[i];
                    SizeF labelSize = g.MeasureString(periodLabel, new Font("Segoe UI", 7));
                    g.TranslateTransform(x + barWidth / 2, chartBottom + 20);
                    g.RotateTransform(-45);
                    g.DrawString(periodLabel, new Font("Segoe UI", 7), Brushes.Gray,
                        new PointF(-labelSize.Width / 2, 0));
                    g.RotateTransform(45);
                    g.TranslateTransform(-(x + barWidth / 2), -(chartBottom + 20));
                }

                g.DrawString($"Histogramme des Recettes - {periodType}", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Black,
                    new PointF(chartLeft + chartWidth / 2 - 100, margin - 50));
            }

            picHistogramme.Image = bitmap;
            picHistogramme.Tag = new Tuple<List<string>, List<double>, string>(periodLabels, recettes, periodType);

            if (animationTimer != null)
            {
                animationStep = 0;
                animationTimer.Start();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (animationStep >= ANIMATION_STEPS)
            {
                if (animationTimer != null)
                {
                    animationTimer.Stop();
                }
                return;
            }

            animationStep++;
            Bitmap bitmap = new Bitmap(picHistogramme.Width, picHistogramme.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                var (periodLabels, recettes, periodType) = (Tuple<List<string>, List<double>, string>)picHistogramme.Tag;
                int margin = 70;
                int chartWidth = picHistogramme.Width - 2 * margin;
                int chartHeight = picHistogramme.Height - 2 * margin;
                int chartBottom = picHistogramme.Height - margin;
                int chartLeft = margin;
                double maxScale = 50000;

                int barCount = periodLabels.Count;
                float spacing = 10;
                float totalSpacing = spacing * (barCount - 1);
                float barWidth = (chartWidth - totalSpacing) / barCount;

                g.DrawLine(new Pen(Color.FromArgb(209, 213, 219), 1), chartLeft, chartBottom, chartLeft, margin);
                g.DrawLine(new Pen(Color.FromArgb(209, 213, 219), 1), chartLeft, chartBottom, chartLeft + chartWidth, chartBottom);

                int numTicksY = 5;
                double interval = maxScale / numTicksY;
                for (int i = 0; i <= numTicksY; i++)
                {
                    float y = chartBottom - (i * chartHeight / numTicksY);
                    g.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                        chartLeft, y, chartLeft + chartWidth, y);
                    double value = interval * i;
                    SizeF size = g.MeasureString($"{value:F0} Ar", new Font("Segoe UI", 8));
                    g.DrawString($"{value:F0} Ar", new Font("Segoe UI", 8), Brushes.Gray,
                        new PointF(chartLeft - size.Width - 10, y - size.Height / 2));
                }

                for (int i = 0; i <= barCount; i++)
                {
                    float x = chartLeft + i * (barWidth + spacing);
                    g.DrawLine(new Pen(Color.FromArgb(229, 231, 235), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
                        x, chartBottom, x, margin);
                }

                using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
                {
                    string yAxisTitle = "Recette (Ar)";
                    SizeF titleSize = g.MeasureString(yAxisTitle, font);
                    g.RotateTransform(-90);
                    g.DrawString(yAxisTitle, font, Brushes.Gray,
                        new PointF(-(margin + chartHeight / 2 + titleSize.Width / 2), margin - 73));
                    g.RotateTransform(90);
                }

                using (Font font = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    string xAxisTitle = "Période";
                    SizeF titleSize = g.MeasureString(xAxisTitle, font);
                    g.DrawString(xAxisTitle, font, Brushes.Gray,
                        new PointF(chartLeft + chartWidth / 2 - titleSize.Width / 2, chartBottom + 30));
                }

                for (int i = 0; i < barCount; i++)
                {
                    float progress = (float)animationStep / ANIMATION_STEPS;
                    barHeights[i] = targetHeights[i] * progress;
                    float x = chartLeft + i * (barWidth + spacing);
                    float y = chartBottom - barHeights[i];

                    Brush barBrush = (i == barCount - 1) ? new SolidBrush(Color.FromArgb(66, 153, 225)) : new SolidBrush(Color.FromArgb(96, 165, 250));
                    g.FillRectangle(barBrush, x, y, barWidth, barHeights[i]);
                    g.DrawRectangle(new Pen(Color.FromArgb(209, 213, 219)), x, y, barWidth, barHeights[i]);

                    if (recettes[i] > 0)
                    {
                        g.FillEllipse(Brushes.Black, x + barWidth / 2 - 3, y - 3, 6, 6);
                    }

                    string valueText = $"{recettes[i]:F0} Ar";
                    SizeF valueSize = g.MeasureString(valueText, new Font("Segoe UI", 7, FontStyle.Bold));
                    g.DrawString(valueText, new Font("Segoe UI", 7, FontStyle.Bold), Brushes.Black,
                        new PointF(x + barWidth / 2 - valueSize.Width / 2, y - valueSize.Height - 10));
                }

                for (int i = 0; i < barCount; i++)
                {
                    float x = chartLeft + i * (barWidth + spacing);
                    string periodLabel = periodLabels[i];
                    SizeF labelSize = g.MeasureString(periodLabel, new Font("Segoe UI", 7));
                    g.TranslateTransform(x + barWidth / 2, chartBottom + 20);
                    g.RotateTransform(-45);
                    g.DrawString(periodLabel, new Font("Segoe UI", 7), Brushes.Gray,
                        new PointF(-labelSize.Width / 2, 0));
                    g.RotateTransform(45);
                    g.TranslateTransform(-(x + barWidth / 2), -(chartBottom + 20));
                }

                g.DrawString($"Histogramme des Recettes - {periodType}", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Black,
                    new PointF(chartLeft + chartWidth / 2 - 100, margin - 50));
            }

            picHistogramme.Image = bitmap;
        }

        private void cboPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHistogramme();
        }

        private void btnGestionPharmacie_Click(object sender, EventArgs e)
        {
            using (Form1 form = new Form1())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void btnGestionEntrees_Click(object sender, EventArgs e)
        {
            using (FormEntree form = new FormEntree())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void btnGestionAchats_Click(object sender, EventArgs e)
        {
            using (FormAchat form = new FormAchat())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void btnRechercherMedicaments_Click(object sender, EventArgs e)
        {
            using (FormRechercheMedicament form = new FormRechercheMedicament())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void btnVoirRuptures_Click(object sender, EventArgs e)
        {
            using (FormRuptureStock form = new FormRuptureStock())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void BtnMedicamentsPerimes_Click(object sender, EventArgs e)
        {
            using (FormMedicamentsPerimes form = new FormMedicamentsPerimes())
            {
                form.ShowDialog();
                LoadDashboardData();
            }
        }

        private void btnGestionPharmacie_MouseEnter(object sender, EventArgs e)
        {
            btnGestionPharmacie.BackColor = Color.FromArgb(66, 153, 225);
            btnGestionPharmacie.Size = new Size(btnPharmacieOriginalSize.Width + 10, btnPharmacieOriginalSize.Height + 5);
        }

        private void btnGestionPharmacie_MouseLeave(object sender, EventArgs e)
        {
            btnGestionPharmacie.BackColor = Color.FromArgb(46, 51, 73);
            btnGestionPharmacie.Size = btnPharmacieOriginalSize;
        }

        private void btnGestionEntrees_MouseEnter(object sender, EventArgs e)
        {
            btnGestionEntrees.BackColor = Color.FromArgb(66, 153, 225);
            btnGestionEntrees.Size = new Size(btnEntreesOriginalSize.Width + 10, btnEntreesOriginalSize.Height + 5);
        }

        private void btnGestionEntrees_MouseLeave(object sender, EventArgs e)
        {
            btnGestionEntrees.BackColor = Color.FromArgb(46, 51, 73);
            btnGestionEntrees.Size = btnEntreesOriginalSize;
        }

        private void btnGestionAchats_MouseEnter(object sender, EventArgs e)
        {
            btnGestionAchats.BackColor = Color.FromArgb(66, 153, 225);
            btnGestionAchats.Size = new Size(btnAchatsOriginalSize.Width + 10, btnAchatsOriginalSize.Height + 5);
        }

        private void btnGestionAchats_MouseLeave(object sender, EventArgs e)
        {
            btnGestionAchats.BackColor = Color.FromArgb(46, 51, 73);
            btnGestionAchats.Size = btnAchatsOriginalSize;
        }

        private void btnRechercherMedicaments_MouseEnter(object sender, EventArgs e)
        {
            btnRechercherMedicaments.BackColor = Color.FromArgb(66, 153, 225);
            btnRechercherMedicaments.Size = new Size(btnRechercherMedicamentsOriginalSize.Width + 10, btnRechercherMedicamentsOriginalSize.Height + 5);
        }

        private void btnRechercherMedicaments_MouseLeave(object sender, EventArgs e)
        {
            btnRechercherMedicaments.BackColor = Color.FromArgb(46, 51, 73);
            btnRechercherMedicaments.Size = btnRechercherMedicamentsOriginalSize;
        }

        private void btnVoirRuptures_MouseEnter(object sender, EventArgs e)
        {
            btnVoirRuptures.BackColor = Color.FromArgb(66, 153, 225);
            btnVoirRuptures.Size = new Size(btnVoirRupturesOriginalSize.Width + 10, btnVoirRupturesOriginalSize.Height + 5);
        }

        private void btnVoirRuptures_MouseLeave(object sender, EventArgs e)
        {
            btnVoirRuptures.BackColor = Color.FromArgb(46, 51, 73);
            btnVoirRuptures.Size = btnVoirRupturesOriginalSize;
        }

        private void BtnMedicamentsPerimes_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(66, 153, 225);
            btn.Size = new Size(btnMedicamentsPerimesOriginalSize.Width + 10, btnMedicamentsPerimesOriginalSize.Height + 5);
        }

        private void BtnMedicamentsPerimes_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(46, 51, 73);
            btn.Size = btnMedicamentsPerimesOriginalSize;
        }

        private void BtnDeconnexion_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(200, 35, 55);
            btn.Size = new Size(btnDeconnexionOriginalSize.Width + 10, btnDeconnexionOriginalSize.Height + 5);
        }

        private void BtnDeconnexion_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.FromArgb(220, 53, 69);
            btn.Size = btnDeconnexionOriginalSize;
        }

        private void BtnDeconnexion_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Close();
        }

        private void FormDashboard_Resize(object sender, EventArgs e)
        {
            try
            {
                int cardWidth = pnlMedicamentCard.Width;
                int centerX = (pnlMain.ClientSize.Width - cardWidth) / 2;

                pnlMedicamentCard.Location = new Point(centerX, 20);
                pnlEntreeCard.Location = new Point(centerX, 140);
                pnlAchatCard.Location = new Point(centerX, 260);
                pnlRecetteCard.Location = new Point(centerX, 380);

                int topMedicamentsWidth = pnlTopMedicaments.Width;
                int topMedicamentsX = pnlMain.ClientSize.Width - topMedicamentsWidth - 50;
                pnlTopMedicaments.Location = new Point(Math.Max(topMedicamentsX, centerX + cardWidth + 50), 20);

                int histogrammeWidth = pnlHistogramme.Width;
                int histogrammeX = (pnlMain.ClientSize.Width - histogrammeWidth) / 2;
                pnlHistogramme.Location = new Point(histogrammeX, 500);

                btnLoadData.Location = new Point((pnlMain.Width - btnLoadData.Width) / 2, 450);

                int maxHeight = Math.Max(pnlHistogramme.Location.Y + pnlHistogramme.Height + 50, pnlMain.ClientSize.Height);
                pnlMain.AutoScrollMinSize = new Size(0, maxHeight);

                int lastButtonBottom = 0;
                foreach (Control ctrl in pnlSidebar.Controls)
                {
                    if (ctrl is Button btn && btn.Name != "btnDeconnexion")
                    {
                        int buttonBottom = btn.Location.Y + btn.Height;
                        if (buttonBottom > lastButtonBottom)
                        {
                            lastButtonBottom = buttonBottom;
                        }
                    }
                }
                Control btnDeconnexion = pnlSidebar.Controls["btnDeconnexion"];
                if (btnDeconnexion != null)
                {
                    btnDeconnexion.Location = new Point(10, lastButtonBottom + 150);
                }

                UpdateHistogramme();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans FormDashboard_Resize : {ex.Message}");
            }
        }
    }
}