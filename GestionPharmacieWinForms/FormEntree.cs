using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace GestionPharmacieWinForms
{
    public partial class FormEntree : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public FormEntree()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Ouvre la fenêtre en plein écran
            RafraichirListeEntrees();
            this.Resize += new EventHandler(FormEntree_Resize); // Gère le centrage et l'ajustement
        }

        private void FormEntree_Resize(object sender, EventArgs e)
        {
            if (pnlButtons != null && dgvEntrees != null)
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;
                int margin = 20; // Marge globale
                int buttonPanelHeight = pnlButtons.Height; // Hauteur du panneau des boutons (50 pixels)

                // Ajuster la taille et la position de dgvEntrees
                dgvEntrees.Location = new Point(margin, margin);
                dgvEntrees.Size = new Size(
                    formWidth - 2 * margin,
                    formHeight - (margin + buttonPanelHeight + 10) // Laisser de l'espace pour pnlButtons
                );

                // Centrer horizontalement et positionner pnlButtons juste en dessous de dgvEntrees
                int panelWidth = pnlButtons.Width;
                int newX = (formWidth - panelWidth) / 2;
                int newY = dgvEntrees.Location.Y + dgvEntrees.Height + 10; // Marge de 10 pixels
                pnlButtons.Location = new Point(newX, newY);

                // Assurer que pnlButtons reste dans les limites de la fenêtre
                if (newY + pnlButtons.Height > formHeight)
                {
                    pnlButtons.Location = new Point(newX, formHeight - pnlButtons.Height - margin);
                }
            }
        }

        private void RafraichirListeEntrees()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ENTREE.numEntree, ENTREE.numMedoc, MEDICAMENT.Design, ENTREE.stockEntree, ENTREE.dateEntree " +
                                   "FROM ENTREE JOIN MEDICAMENT ON ENTREE.numMedoc = MEDICAMENT.numMedoc";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvEntrees.DataSource = dt;

                    if (dgvEntrees.Columns.Count > 0)
                    {
                        dgvEntrees.Columns["numEntree"].HeaderText = "Numéro Entrée";
                        dgvEntrees.Columns["numMedoc"].HeaderText = "Numéro Médicament";
                        dgvEntrees.Columns["Design"].HeaderText = "Désignation";
                        dgvEntrees.Columns["stockEntree"].HeaderText = "Quantité Entrée";
                        dgvEntrees.Columns["dateEntree"].HeaderText = "Date Entrée";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des entrées : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            using (FormAjouterModifierEntree form = new FormAjouterModifierEntree(true))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RafraichirListeEntrees();
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dgvEntrees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une entrée à modifier.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvEntrees.SelectedRows[0];
                string numEntree = row.Cells["numEntree"].Value?.ToString() ?? "";
                string numMedoc = row.Cells["numMedoc"].Value?.ToString() ?? "";
                string stockEntreeStr = row.Cells["stockEntree"].Value?.ToString() ?? "0";
                string dateEntreeStr = row.Cells["dateEntree"].Value?.ToString() ?? DateTime.Now.ToString("yyyy-MM-dd");

                if (!int.TryParse(stockEntreeStr, out int stockEntree)) stockEntree = 0;
                if (!DateTime.TryParse(dateEntreeStr, out DateTime dateEntree)) dateEntree = DateTime.Now;

                using (FormAjouterModifierEntree form = new FormAjouterModifierEntree(false))
                {
                    form.NumEntree = numEntree;
                    form.NumMedoc = numMedoc;
                    form.StockEntree = stockEntree;
                    form.DateEntree = dateEntree;
                    form.LoadData();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        RafraichirListeEntrees();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données de l'entrée : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgvEntrees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une entrée à supprimer.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string numEntree = dgvEntrees.SelectedRows[0].Cells["numEntree"].Value.ToString();
            string numMedoc = dgvEntrees.SelectedRows[0].Cells["numMedoc"].Value.ToString();
            int stockEntree = Convert.ToInt32(dgvEntrees.SelectedRows[0].Cells["stockEntree"].Value);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string queryStock = "SELECT stock FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                    MySqlCommand cmdStock = new MySqlCommand(queryStock, conn);
                    cmdStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                    int stockActuel = Convert.ToInt32(cmdStock.ExecuteScalar());

                    if (stockActuel < stockEntree)
                    {
                        MessageBox.Show("Impossible de supprimer cette entrée : le stock deviendrait négatif.",
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette entrée ?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No) return;

                    string queryUpdateStock = "UPDATE MEDICAMENT SET stock = stock - @stockEntree WHERE numMedoc = @numMedoc";
                    MySqlCommand cmdUpdateStock = new MySqlCommand(queryUpdateStock, conn);
                    cmdUpdateStock.Parameters.AddWithValue("@stockEntree", stockEntree);
                    cmdUpdateStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                    cmdUpdateStock.ExecuteNonQuery();

                    string queryDelete = "DELETE FROM ENTREE WHERE numEntree = @numEntree";
                    MySqlCommand cmdDelete = new MySqlCommand(queryDelete, conn);
                    cmdDelete.Parameters.AddWithValue("@numEntree", numEntree);
                    cmdDelete.ExecuteNonQuery();

                    MessageBox.Show("Entrée supprimée avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RafraichirListeEntrees();
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
            RafraichirListeEntrees();
            MessageBox.Show("Liste des entrées rafraîchie avec succès !", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}