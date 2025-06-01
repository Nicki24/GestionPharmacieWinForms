using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace GestionPharmacieWinForms
{
    public partial class FormAchat : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public FormAchat()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ChargerAchats();
            this.Resize += new EventHandler(FormAchat_Resize);
        }

        private void FormAchat_Resize(object sender, EventArgs e)
        {
            if (pnlButtons != null && dgvAchats != null)
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;
                int margin = 20;
                int buttonPanelHeight = pnlButtons.Height;

                lblAchats.Location = new Point(margin, margin);

                dgvAchats.Location = new Point(margin, lblAchats.Location.Y + lblAchats.Height + 5);
                dgvAchats.Size = new Size(
                    formWidth - 2 * margin,
                    formHeight - (dgvAchats.Location.Y + buttonPanelHeight + margin + 10)
                );

                int panelWidth = pnlButtons.Width;
                int newX = (formWidth - panelWidth) / 2;
                int newY = dgvAchats.Location.Y + dgvAchats.Height + 10;
                pnlButtons.Location = new Point(newX, newY);

                if (newY + pnlButtons.Height > formHeight)
                {
                    pnlButtons.Location = new Point(newX, formHeight - pnlButtons.Height - margin);
                }
            }
        }

        private void ChargerAchats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numAchat, nomClient, dateAchat FROM ACHAT";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvAchats.DataSource = dt;

                    if (dgvAchats.Columns["numAchat"] != null)
                        dgvAchats.Columns["numAchat"].HeaderText = "Numéro Achat";
                    if (dgvAchats.Columns["nomClient"] != null)
                        dgvAchats.Columns["nomClient"].HeaderText = "Nom Client";
                    if (dgvAchats.Columns["dateAchat"] != null)
                        dgvAchats.Columns["dateAchat"].HeaderText = "Date Achat";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des achats : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            FormAjouterModifierAchat form = new FormAjouterModifierAchat();
            form.ShowDialog();
            ChargerAchats();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvAchats.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un achat à modifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numAchat;
            try
            {
                numAchat = dgvAchats.SelectedRows[0].Cells["numAchat"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(numAchat))
                {
                    MessageBox.Show("Le numéro d'achat est vide ou non défini.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération du numéro d'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormAjouterModifierAchat form = new FormAjouterModifierAchat(numAchat);
            form.ShowDialog();
            ChargerAchats();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvAchats.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un achat à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numAchat;
            try
            {
                numAchat = dgvAchats.SelectedRows[0].Cells["numAchat"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(numAchat))
                {
                    MessageBox.Show("Le numéro d'achat est vide ou non défini.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération du numéro d'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet achat ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlTransaction transaction = conn.BeginTransaction();
                        try
                        {
                            string queryDeleteDetails = "DELETE FROM ACHAT_DETAILS WHERE numAchat = @numAchat";
                            MySqlCommand cmdDeleteDetails = new MySqlCommand(queryDeleteDetails, conn, transaction);
                            cmdDeleteDetails.Parameters.AddWithValue("@numAchat", numAchat);
                            cmdDeleteDetails.ExecuteNonQuery();

                            string queryDeleteAchat = "DELETE FROM ACHAT WHERE numAchat = @numAchat";
                            MySqlCommand cmdDeleteAchat = new MySqlCommand(queryDeleteAchat, conn, transaction);
                            cmdDeleteAchat.Parameters.AddWithValue("@numAchat", numAchat);
                            cmdDeleteAchat.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Achat supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChargerAchats();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Erreur lors de la suppression de l'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGenererFacture_Click(object sender, EventArgs e)
        {
            if (dgvAchats.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un achat pour générer une facture.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dgvAchats.Columns.Contains("numAchat") || !dgvAchats.Columns.Contains("nomClient") || !dgvAchats.Columns.Contains("dateAchat"))
            {
                MessageBox.Show("Les colonnes nécessaires (numAchat, nomClient, dateAchat) ne sont pas présentes dans le tableau.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string numAchat;
            string nomClient;
            DateTime dateAchat;

            try
            {
                numAchat = dgvAchats.SelectedRows[0].Cells["numAchat"].Value?.ToString();
                nomClient = dgvAchats.SelectedRows[0].Cells["nomClient"].Value?.ToString();
                dateAchat = Convert.ToDateTime(dgvAchats.SelectedRows[0].Cells["dateAchat"].Value);

                if (string.IsNullOrWhiteSpace(numAchat))
                {
                    MessageBox.Show("Le numéro d'achat est vide ou non défini.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données de l'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomClient))
            {
                MessageBox.Show("Le nom du client est vide ou non défini.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtDetails = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ad.numMedoc, m.Design, ad.quantite " +
                                   "FROM ACHAT_DETAILS ad " +
                                   "LEFT JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                                   "WHERE ad.numAchat = @numAchat";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@numAchat", numAchat);
                    adapter.Fill(dtDetails);
                }

                if (dtDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Aucun détail trouvé pour cet achat.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GenererFacturePDF(numAchat, nomClient, dateAchat, dtDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des détails de l'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenererFacturePDF(string numAchat, string nomClient, DateTime dateAchat, DataTable details)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Facture " + numAchat;
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont fontTitre = new XFont("Arial Bold", 16);
                XFont fontNormal = new XFont("Arial", 12);
                XFont fontTableHeader = new XFont("Arial Bold", 12);

                double yPosition = 20;
                double margin = 40;
                double tableWidth = page.Width - 2 * margin;
                double[] columnWidths = new double[] { tableWidth * 0.35, tableWidth * 0.20, tableWidth * 0.20, tableWidth * 0.25 };

                // Titre (centré)
                XStringFormat titleFormat = new XStringFormat { Alignment = XStringAlignment.Center };
                gfx.DrawString("PharmaShop", fontTitre, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 30), titleFormat);
                yPosition += 30;

                // Logo
                try
                {
                    XImage logo = XImage.FromFile(@"C:\Users\N I C K I\Pictures\Icones\logo.png");
                    double logoWidth = 100; // Largeur fixe pour le logo
                    double logoHeight = logoWidth * (logo.PixelHeight / (double)logo.PixelWidth); // Conserver les proportions
                    double logoX = (page.Width - logoWidth) / 2; // Centrer horizontalement
                    gfx.DrawImage(logo, logoX, yPosition, logoWidth, logoHeight);
                    yPosition += logoHeight + 20; // Espace après le logo
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement du logo : {ex.Message}. Le PDF sera généré sans le logo.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    yPosition += 20; // Espace par défaut si le logo échoue
                }

                // Date
                XStringFormat leftFormat = new XStringFormat { Alignment = XStringAlignment.Near };
                gfx.DrawString($"Date : {DateTime.Now:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 20), leftFormat);
                yPosition += 30;

                // Nom du client
                gfx.DrawString($"Nom du Client : {nomClient}", fontNormal, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 20), leftFormat);
                yPosition += 40;

                // Tableau
                gfx.DrawRectangle(XPens.Black, margin, yPosition, tableWidth, 20);
                XStringFormat headerFormat = new XStringFormat { Alignment = XStringAlignment.Center };
                gfx.DrawString("Désignation", fontTableHeader, XBrushes.Black, new XRect(margin, yPosition + 5, columnWidths[0], 20), headerFormat);
                gfx.DrawString("Prix Unitaire", fontTableHeader, XBrushes.Black, new XRect(margin + columnWidths[0], yPosition + 5, columnWidths[1], 20), headerFormat);
                gfx.DrawString("Nombre", fontTableHeader, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1], yPosition + 5, columnWidths[2], 20), headerFormat);
                gfx.DrawString("Total", fontTableHeader, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1] + columnWidths[2], yPosition + 5, columnWidths[3], 20), headerFormat);
                yPosition += 20;

                double prixTotalGeneral = 0;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    XStringFormat cellFormat = new XStringFormat { Alignment = XStringAlignment.Center };
                    XStringFormat designFormat = new XStringFormat { Alignment = XStringAlignment.Near };
                    foreach (DataRow row in details.Rows)
                    {
                        string design = row["Design"] != DBNull.Value ? row["Design"].ToString() : "Non défini";
                        int quantite = row["quantite"] != DBNull.Value ? Convert.ToInt32(row["quantite"]) : 0;
                        string numMedoc = row["numMedoc"].ToString();

                        string query = "SELECT prix_unitaire FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@numMedoc", numMedoc);
                        object prixUnitaireObj = cmd.ExecuteScalar();
                        int prixUnitaire = prixUnitaireObj != DBNull.Value ? Convert.ToInt32(prixUnitaireObj) : 0;
                        double prixTotalLigne = quantite * prixUnitaire;
                        prixTotalGeneral += prixTotalLigne;

                        gfx.DrawRectangle(XPens.Black, margin, yPosition, tableWidth, 20);
                        gfx.DrawString(design, fontNormal, XBrushes.Black, new XRect(margin, yPosition + 5, columnWidths[0], 20), designFormat);
                        gfx.DrawString(prixUnitaire.ToString(), fontNormal, XBrushes.Black, new XRect(margin + columnWidths[0], yPosition + 5, columnWidths[1], 20), cellFormat);
                        gfx.DrawString(quantite.ToString(), fontNormal, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1], yPosition + 5, columnWidths[2], 20), cellFormat);
                        gfx.DrawString($"{prixTotalLigne} Ar", fontNormal, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1] + columnWidths[2], yPosition + 5, columnWidths[3], 20), cellFormat);
                        yPosition += 20;
                    }
                }

                // Total général avec titre "Prix Total"
                yPosition += 10;
                XStringFormat totalLabelFormat = new XStringFormat { Alignment = XStringAlignment.Far };
                double totalLabelX = margin + columnWidths[0] + columnWidths[1];
                gfx.DrawString("Prix Total :", fontNormal, XBrushes.Black, new XRect(totalLabelX, yPosition, columnWidths[2], 20), totalLabelFormat);
                XStringFormat totalFormat = new XStringFormat { Alignment = XStringAlignment.Far };
                gfx.DrawString($"{prixTotalGeneral} Ar", fontNormal, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1] + columnWidths[2], yPosition, columnWidths[3], 20), totalFormat);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF Files|*.pdf";
                    saveFileDialog.Title = "Enregistrer la facture";
                    saveFileDialog.FileName = $"Facture_Achat_{numAchat}.pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        document.Save(saveFileDialog.FileName);
                        MessageBox.Show("Facture générée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération de la facture : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}