using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionPharmacieWinForms
{
    public partial class FormMedicamentsPerimes : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public FormMedicamentsPerimes()
        {
            InitializeComponent();
            // S'assurer que AutoGenerateColumns est activé
            dgvMedicamentsPerimes.AutoGenerateColumns = true;
            // Ouvrir en plein écran
            this.WindowState = FormWindowState.Maximized;
            ChargerMedicamentsPerimes();
        }

        private void ChargerMedicamentsPerimes()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numMedoc, Design, stock, prix_unitaire, datePeremption " +
                                   "FROM MEDICAMENT " +
                                   "WHERE datePeremption < @dateActuelle";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dateActuelle", DateTime.Now.Date);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Vérifier que toutes les colonnes attendues sont présentes dans le DataTable
                    string[] expectedColumns = { "numMedoc", "Design", "stock", "prix_unitaire", "datePeremption" };
                    foreach (string column in expectedColumns)
                    {
                        if (!dt.Columns.Contains(column))
                        {
                            MessageBox.Show($"Erreur : La colonne '{column}' est manquante dans les données récupérées. Vérifiez la table MEDICAMENT dans la base de données.", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Si aucune donnée n'est trouvée, ajouter une ligne par défaut
                    if (dt.Rows.Count == 0)
                    {
                        DataRow row = dt.NewRow();
                        row["numMedoc"] = "";
                        row["Design"] = "Aucun médicament périmé trouvé";
                        row["stock"] = DBNull.Value;
                        row["prix_unitaire"] = DBNull.Value;
                        row["datePeremption"] = DBNull.Value;
                        dt.Rows.Add(row);
                    }

                    // Vérification que dgvMedicamentsPerimes existe avant de l'utiliser
                    if (dgvMedicamentsPerimes != null)
                    {
                        // Définir le DataSource
                        dgvMedicamentsPerimes.DataSource = null; // Réinitialiser pour éviter les conflits
                        dgvMedicamentsPerimes.DataSource = dt;

                        // Configurer les colonnes dans l'événement DataBindingComplete
                        dgvMedicamentsPerimes.DataBindingComplete += (s, e) =>
                        {
                            try
                            {
                                if (dgvMedicamentsPerimes.Columns.Count > 0)
                                {
                                    // Calculer la largeur des colonnes proportionnellement à la largeur du DataGridView
                                    int totalWidth = dgvMedicamentsPerimes.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
                                    int numMedocWidth = (int)(totalWidth * 0.15); // 15%
                                    int designWidth = (int)(totalWidth * 0.35);   // 35%
                                    int stockWidth = (int)(totalWidth * 0.15);    // 15%
                                    int prixWidth = (int)(totalWidth * 0.15);     // 15%
                                    int dateWidth = (int)(totalWidth * 0.20);     // 20%

                                    if (dgvMedicamentsPerimes.Columns.Contains("numMedoc"))
                                    {
                                        dgvMedicamentsPerimes.Columns["numMedoc"].HeaderText = "Numéro Médicament";
                                        dgvMedicamentsPerimes.Columns["numMedoc"].Width = numMedocWidth;
                                    }
                                    if (dgvMedicamentsPerimes.Columns.Contains("Design"))
                                    {
                                        dgvMedicamentsPerimes.Columns["Design"].HeaderText = "Désignation";
                                        dgvMedicamentsPerimes.Columns["Design"].Width = designWidth;
                                    }
                                    if (dgvMedicamentsPerimes.Columns.Contains("stock"))
                                    {
                                        dgvMedicamentsPerimes.Columns["stock"].HeaderText = "Stock";
                                        dgvMedicamentsPerimes.Columns["stock"].Width = stockWidth;
                                    }
                                    if (dgvMedicamentsPerimes.Columns.Contains("prix_unitaire"))
                                    {
                                        dgvMedicamentsPerimes.Columns["prix_unitaire"].HeaderText = "Prix en Ar";
                                        dgvMedicamentsPerimes.Columns["prix_unitaire"].Width = prixWidth;
                                    }
                                    if (dgvMedicamentsPerimes.Columns.Contains("datePeremption"))
                                    {
                                        dgvMedicamentsPerimes.Columns["datePeremption"].HeaderText = "Date Péremption";
                                        dgvMedicamentsPerimes.Columns["datePeremption"].Width = dateWidth;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Erreur : Aucune colonne n'a été générée dans le DataGridView.", "Erreur",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erreur lors de la configuration des colonnes du DataGridView : " + ex.Message, "Erreur",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };
                    }
                    else
                    {
                        MessageBox.Show("Erreur : Le DataGridView n'est pas initialisé.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur lors de l'accès à la base de données : " + ex.Message + "\nVérifiez que la table MEDICAMENT existe et que la base de données est accessible.", "Erreur MySQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des données : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Gérer le redimensionnement du formulaire
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustControls();
        }

        private void AdjustControls()
        {
            if (this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                // Marges
                int margin = 20;

                // Ajuster lblTitre
                lblTitre.Location = new System.Drawing.Point(margin, margin);
                lblTitre.Width = this.ClientSize.Width - 2 * margin;
                lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // Ajuster dgvMedicamentsPerimes
                int gridHeight = (int)(this.ClientSize.Height * 0.7); // 70% de la hauteur
                dgvMedicamentsPerimes.Location = new System.Drawing.Point(margin, lblTitre.Bottom + margin);
                dgvMedicamentsPerimes.Size = new System.Drawing.Size(this.ClientSize.Width - 2 * margin, gridHeight);

                // Ajuster btnAnnuler
                int buttonWidth = (int)(this.ClientSize.Width * 0.15); // 15% de la largeur
                int buttonHeight = 40;
                btnAnnuler.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                btnAnnuler.Location = new System.Drawing.Point(this.ClientSize.Width - margin - buttonWidth, dgvMedicamentsPerimes.Bottom + margin);
            }
        }
    }
}