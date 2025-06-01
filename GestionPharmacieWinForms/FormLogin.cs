using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace GestionPharmacieWinForms
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";
        private System.Windows.Forms.Timer fadeTimer;
        private float opacity = 0f;
        private Label lblErrorMessage;

        public FormLogin()
        {
            InitializeComponent();
            ApplyModernStyles();
            StartFadeInAnimation();
        }

        private void ApplyModernStyles()
        {
            // Style de la fenêtre avec fond dégradé
            this.BackColor = Color.FromArgb(75, 0, 130); // Violet foncé
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 550); // Taille ajustée
            this.Text = "Connexion - PharmShop";

            // Panneau central avec bordures arrondies et bordure extérieure
            Panel pnlLogin = new Panel
            {
                Size = new Size(350, 450), // Taille du panneau
                Location = new Point((this.ClientSize.Width - 350) / 2, (this.ClientSize.Height - 450) / 2),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle // Ajout d'une bordure
            };
            pnlLogin.Paint += (sender, e) =>
            {
                GraphicsPath graphicsPath = new GraphicsPath(); // Déclarer ici pour être accessible
                try
                {
                    int radius = 20;
                    Rectangle rect = pnlLogin.ClientRectangle;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    graphicsPath.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    graphicsPath.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    graphicsPath.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                    graphicsPath.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
                    graphicsPath.CloseFigure();
                    pnlLogin.Region = new Region(graphicsPath);

                    using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 2)) // Bordure plus visible
                    {
                        e.Graphics.DrawPath(pen, graphicsPath);
                    }
                }
                finally
                {
                    graphicsPath.Dispose(); // Libérer les ressources
                }
            };
            this.Controls.Add(pnlLogin);

            // Logo circulaire agrandi
            PictureBox logoPictureBox = new PictureBox
            {
                Size = new Size(80, 80), // Taille du logo
                Location = new Point((pnlLogin.Width - 80) / 2, 20),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            try
            {
                logoPictureBox.Image = Image.FromFile(@"C:\Users\N I C K I\Pictures\Icones\logo.png");
                using (GraphicsPath graphicsPath = new GraphicsPath())
                {
                    graphicsPath.AddEllipse(0, 0, logoPictureBox.Width, logoPictureBox.Height);
                    logoPictureBox.Region = new Region(graphicsPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du logo : {ex.Message}. Le logo ne sera pas affiché.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pnlLogin.Controls.Add(logoPictureBox);

            // Titre "Connexion"
            Label lblLoginTitle = new Label
            {
                Text = "Connexion",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold), // Taille de police ajustée
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point((pnlLogin.Width - 200) / 2, logoPictureBox.Bottom + 10),
                Size = new Size(200, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlLogin.Controls.Add(lblLoginTitle);

            // Label et champ Identifiant
            Label lblIdentifiant = new Label
            {
                Text = "Identifiant",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular), // Taille de police ajustée
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(50, lblLoginTitle.Bottom + 30)
            };
            TextBox txtIdentifiant = new TextBox
            {
                Name = "txtIdentifiant", // Ajout d'un nom pour faciliter l'accès
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 12F),
                Location = new Point(50, lblIdentifiant.Bottom + 10),
                Size = new Size(250, 30)
            };
            pnlLogin.Controls.Add(lblIdentifiant);
            pnlLogin.Controls.Add(txtIdentifiant);

            // Label et champ Mot de Passe
            Label lblMotDePasse = new Label
            {
                Text = "Mot de Passe",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular), // Taille de police ajustée
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(50, txtIdentifiant.Bottom + 20)
            };
            TextBox txtMotDePasse = new TextBox
            {
                Name = "txtMotDePasse", // Ajout d'un nom pour faciliter l'accès
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 12F),
                Location = new Point(50, lblMotDePasse.Bottom + 10),
                Size = new Size(250, 30),
                PasswordChar = '*'
            };
            pnlLogin.Controls.Add(lblMotDePasse);
            pnlLogin.Controls.Add(txtMotDePasse);

            // Bouton Connexion avec dégradé et animation
            Button btnConnexion = new Button
            {
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(50, txtMotDePasse.Bottom + 20),
                Size = new Size(250, 40),
                Text = "Se Connecter"
            };
            btnConnexion.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(btnConnexion.ClientRectangle,
                    Color.FromArgb(66, 135, 245), Color.FromArgb(139, 92, 246), LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, btnConnexion.ClientRectangle);
                }
                // Dessiner le texte manuellement pour s'assurer qu'il est visible
                TextRenderer.DrawText(e.Graphics, btnConnexion.Text, btnConnexion.Font, btnConnexion.ClientRectangle,
                    btnConnexion.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            btnConnexion.MouseEnter += (sender, e) => btnConnexion.Size = new Size(260, 45);
            btnConnexion.MouseLeave += (sender, e) => btnConnexion.Size = new Size(250, 40);
            btnConnexion.Click += (sender, e) =>
            {
                // Récupérer les valeurs des champs
                string identifiant = txtIdentifiant.Text.Trim();
                string motDePasse = txtMotDePasse.Text.Trim();

                // Vérifier si les champs sont vides et afficher une alerte via MessageBox
                if (string.IsNullOrEmpty(identifiant) && string.IsNullOrEmpty(motDePasse))
                {
                    MessageBox.Show("Veuillez entrer votre identifiant et mot de passe.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(identifiant))
                {
                    MessageBox.Show("Veuillez entrer votre identifiant.", "Champ manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(motDePasse))
                {
                    MessageBox.Show("Veuillez entrer votre mot de passe.", "Champ manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Si les champs sont remplis, procéder à l'authentification
                if (AuthenticateUser(identifiant, motDePasse, out string role))
                {
                    FormDashboard dashboard = new FormDashboard(role);
                    dashboard.Show(); // Afficher le dashboard
                    this.Hide(); // Cacher le login sans fermer l'application
                }
                else
                {
                    // Afficher une alerte via MessageBox pour identifiant ou mot de passe incorrect
                    MessageBox.Show("Identifiant ou mot de passe incorrect.", "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            pnlLogin.Controls.Add(btnConnexion);

            // Liens Oubli mot de passe et Inscription
            LinkLabel lnkOubliMotDePasse = new LinkLabel
            {
                Text = "Mot de passe oublié ?",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                LinkColor = Color.FromArgb(107, 114, 128),
                Location = new Point(50, btnConnexion.Bottom + 10),
                Size = new Size(250, 20)
            };
            lnkOubliMotDePasse.Click += (sender, e) => MessageBox.Show("Fonctionnalité non implémentée.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlLogin.Controls.Add(lnkOubliMotDePasse);

            LinkLabel lnkInscription = new LinkLabel
            {
                Text = "Pas de compte ? Inscrivez-vous",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                LinkColor = Color.FromArgb(107, 114, 128),
                Location = new Point(50, lnkOubliMotDePasse.Bottom + 5),
                Size = new Size(250, 20)
            };
            lnkInscription.Click += (sender, e) => MessageBox.Show("Fonctionnalité non implémentée.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlLogin.Controls.Add(lnkInscription);

            // Message d'erreur (conservé mais non utilisé pour les alertes principales)
            lblErrorMessage = new Label
            {
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(50, lnkInscription.Bottom + 10),
                Visible = false,
                Size = new Size(250, 20)
            };
            pnlLogin.Controls.Add(lblErrorMessage);
        }

        private void StartFadeInAnimation()
        {
            fadeTimer = new System.Windows.Forms.Timer { Interval = 30 };
            fadeTimer.Tick += (sender, e) =>
            {
                if (opacity < 1.0)
                {
                    opacity += 0.05f;
                    this.Opacity = opacity;
                }
                else
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };
            fadeTimer.Start();
        }

        private void StartFadeOutAnimation()
        {
            fadeTimer = new System.Windows.Forms.Timer { Interval = 30 };
            fadeTimer.Tick += (sender, e) =>
            {
                if (opacity > 0.0)
                {
                    opacity -= 0.05f;
                    this.Opacity = opacity;
                }
                else
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                    this.Close(); // Fermer uniquement FormLogin
                }
            };
            fadeTimer.Start();
        }

        private bool AuthenticateUser(string identifiant, string motDePasse, out string role)
        {
            role = string.Empty;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT role FROM UTILISATEUR WHERE nomUtilisateur = @identifiant AND motDePasse = @motDePasse";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@identifiant", identifiant);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion à la base de données : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ne pas appeler Application.Exit() ici pour éviter de fermer toute l'application
            // Laisser l'utilisateur fermer manuellement si nécessaire
            // e.Cancel = true; // Optionnel : empêcher la fermeture si ce n'est pas voulu
        }
    }
}