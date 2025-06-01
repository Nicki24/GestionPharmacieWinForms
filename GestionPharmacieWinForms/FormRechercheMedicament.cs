using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionPharmacieWinForms
{
    public partial class FormRechercheMedicament : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public FormRechercheMedicament()
        {
            InitializeComponent();
            // Ne pas appeler RechercherMedicaments ici, on le fera dans Form_Load
            this.Load += new EventHandler(FormRechercheMedicament_Load); // Ajouter l'événement Load
            this.Resize += new EventHandler(FormRechercheMedicament_Resize); // Gérer le redimensionnement
            FormRechercheMedicament_Resize(this, EventArgs.Empty); // Appeler manuellement Resize au démarrage
        }

        private void FormRechercheMedicament_Load(object sender, EventArgs e)
        {
            // Charger les données après que le formulaire est complètement initialisé
            RechercherMedicaments("");
        }

        private void RechercherMedicaments(string designation)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Requête SQL pour rechercher les médicaments par désignation, avec prix_unitaire
                    string query = "SELECT numMedoc, Design, stock, prix_unitaire " +
                                   "FROM MEDICAMENT " +
                                   "WHERE LOWER(Design) LIKE LOWER(@design)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Ajouter le paramètre pour la recherche partielle
                    string searchTerm = $"%{designation}%";
                    cmd.Parameters.AddWithValue("@design", searchTerm);

                    // Charger les résultats dans un DataTable
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Vérifier si des résultats ont été trouvés
                    if (dt.Rows.Count == 0)
                    {
                        // Ajouter une ligne avec un message si aucun résultat
                        DataRow row = dt.NewRow();
                        row["numMedoc"] = "";
                        row["Design"] = "Aucun médicament trouvé";
                        row["stock"] = DBNull.Value;
                        row["prix_unitaire"] = DBNull.Value;
                        dt.Rows.Add(row);
                    }

                    // Lier le DataTable au DataGridView
                    if (dgvMedicaments != null)
                    {
                        dgvMedicaments.DataSource = dt;

                        // Ajuster les en-têtes des colonnes
                        if (dgvMedicaments.Columns.Count > 0)
                        {
                            dgvMedicaments.Columns["numMedoc"].HeaderText = "Numéro Médicament";
                            dgvMedicaments.Columns["Design"].HeaderText = "Désignation";
                            dgvMedicaments.Columns["stock"].HeaderText = "Stock";
                            if (dgvMedicaments.Columns.Contains("prix_unitaire"))
                            {
                                dgvMedicaments.Columns["prix_unitaire"].HeaderText = "Prix en Ar";
                            }
                        }

                        // Ajuster la largeur des colonnes pour qu'elles soient bien visibles
                        if (dgvMedicaments.Columns.Count > 0)
                        {
                            if (dgvMedicaments.Columns.Contains("numMedoc")) dgvMedicaments.Columns["numMedoc"].Width = 120;
                            if (dgvMedicaments.Columns.Contains("Design")) dgvMedicaments.Columns["Design"].Width = 200;
                            if (dgvMedicaments.Columns.Contains("stock")) dgvMedicaments.Columns["stock"].Width = 80;
                            if (dgvMedicaments.Columns.Contains("prix_unitaire")) dgvMedicaments.Columns["prix_unitaire"].Width = 100;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erreur : Le contrôle DataGridView (dgvMedicaments) n'est pas initialisé.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche des médicaments : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            string designation = txtRechercheMedicament?.Text?.Trim() ?? "";
            RechercherMedicaments(designation);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRechercheMedicament_Resize(object sender, EventArgs e)
        {
            try
            {
                // Ajuster la taille et la position de pnlForm
                int margin = 20; // Marge globale
                int buttonHeight = 40; // Hauteur des boutons
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;

                int pnlWidth = formWidth - 2 * margin;
                int pnlHeight = formHeight - 2 * margin; // Laisser un peu d'espace en bas pour les boutons
                if (pnlForm != null)
                {
                    pnlForm.Size = new Size(pnlWidth, pnlHeight);
                    pnlForm.Location = new Point(margin, margin);
                }
                else
                {
                    MessageBox.Show("Erreur : Le contrôle pnlForm n'est pas initialisé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajuster la taille de txtRechercheMedicament avec une largeur fixe
                int searchBoxWidth = 300; // Largeur fixe pour éviter le chevauchement
                if (txtRechercheMedicament != null && lblRechercheMedicament != null)
                {
                    txtRechercheMedicament.Size = new Size(searchBoxWidth, 27);
                    txtRechercheMedicament.Location = new Point(lblRechercheMedicament.Right + 5, 3);
                }
                else
                {
                    MessageBox.Show("Erreur : txtRechercheMedicament ou lblRechercheMedicament n'est pas initialisé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajuster la position de btnRechercher
                if (btnRechercher != null && txtRechercheMedicament != null)
                {
                    btnRechercher.Location = new Point(txtRechercheMedicament.Right + 10, 0);
                }
                else
                {
                    MessageBox.Show("Erreur : btnRechercher ou txtRechercheMedicament n'est pas initialisé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajuster la taille et la position de dgvMedicaments
                int dgvY = 40; // Position initiale de dgvMedicaments
                int dgvHeight = pnlHeight - dgvY - margin - buttonHeight; // Ajuster la hauteur dynamiquement
                if (dgvMedicaments != null)
                {
                    dgvMedicaments.Size = new Size(pnlWidth - 2 * margin, dgvHeight > 0 ? dgvHeight : 200);
                    dgvMedicaments.Location = new Point(margin, dgvY);
                }
                else
                {
                    MessageBox.Show("Erreur : Le contrôle DataGridView (dgvMedicaments) n'est pas initialisé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajuster la position de btnAnnuler
                if (btnAnnuler != null)
                {
                    btnAnnuler.Location = new Point(pnlWidth - btnAnnuler.Width - margin, pnlHeight - buttonHeight - margin);
                }
                else
                {
                    MessageBox.Show("Erreur : btnAnnuler n'est pas initialisé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du redimensionnement : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}