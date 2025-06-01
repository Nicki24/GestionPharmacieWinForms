using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace GestionPharmacieWinForms
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
            RafraichirListeMedicaments();
            this.Resize += new EventHandler(Form1_Resize); // Gère le centrage et l'ajustement vertical
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Centrer le panneau des boutons horizontalement et l'ajuster verticalement sous dgvMedicaments
            if (pnlButtons != null && dgvMedicaments != null)
            {
                int formWidth = this.ClientSize.Width;
                int panelWidth = pnlButtons.Width;
                int newX = (formWidth - panelWidth) / 2;

                // Ajuster la position Y pour qu'elle soit juste sous dgvMedicaments avec une marge de 10 pixels
                int newY = dgvMedicaments.Location.Y + dgvMedicaments.Height + 10;
                pnlButtons.Location = new Point(newX, newY);

                // Assurer que le panneau reste dans les limites de la fenêtre
                if (newY + pnlButtons.Height > this.ClientSize.Height)
                {
                    pnlButtons.Location = new Point(newX, this.ClientSize.Height - pnlButtons.Height - 10);
                }
            }
        }

        private void RafraichirListeMedicaments()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM MEDICAMENT";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvMedicaments.DataSource = dt;

                    if (dgvMedicaments.Columns.Count > 0)
                    {
                        dgvMedicaments.Columns["numMedoc"].HeaderText = "Numéro Médicament";
                        dgvMedicaments.Columns["Design"].HeaderText = "Désignation";
                        dgvMedicaments.Columns["prix_unitaire"].HeaderText = "Prix Unitaire (Ar)";
                        dgvMedicaments.Columns["stock"].HeaderText = "Stock";
                        dgvMedicaments.Columns["datePeremption"].HeaderText = "Date de Péremption";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            using (FormAjouterModifier form = new FormAjouterModifier(true))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RafraichirListeMedicaments();
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvMedicaments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un médicament à modifier.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvMedicaments.SelectedRows[0];
                string numMedoc = row.Cells["numMedoc"].Value?.ToString() ?? "";
                string design = row.Cells["Design"].Value?.ToString() ?? "";
                string prixUnitaireStr = row.Cells["prix_unitaire"].Value?.ToString() ?? "0";
                string stockStr = row.Cells["stock"].Value?.ToString() ?? "0";
                string datePeremptionStr = row.Cells["datePeremption"].Value?.ToString() ?? DateTime.Now.ToString("yyyy-MM-dd");

                if (!int.TryParse(prixUnitaireStr, out int prixUnitaire)) prixUnitaire = 0;
                if (!int.TryParse(stockStr, out int stock)) stock = 0;
                if (!DateTime.TryParse(datePeremptionStr, out DateTime datePeremption)) datePeremption = DateTime.Now;

                using (FormAjouterModifier form = new FormAjouterModifier(false))
                {
                    form.NumMedoc = numMedoc;
                    form.Designation = design;
                    form.PrixUnitaire = prixUnitaire;
                    form.Stock = stock;
                    form.DatePeremption = datePeremption;
                    form.LoadData();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        RafraichirListeMedicaments();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données du médicament : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvMedicaments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un médicament à supprimer.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numMedoc = dgvMedicaments.SelectedRows[0].Cells["numMedoc"].Value.ToString();
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce médicament ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@numMedoc", numMedoc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Médicament supprimé avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RafraichirListeMedicaments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRafraichir_Click(object sender, EventArgs e)
        {
            RafraichirListeMedicaments();
            MessageBox.Show("Liste des médicaments rafraîchie avec succès !", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}