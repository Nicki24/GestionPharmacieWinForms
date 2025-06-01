using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionPharmacieWinForms
{
    public partial class FormRuptureStock : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";

        public FormRuptureStock()
        {
            InitializeComponent();
            ChargerMedicamentsRupture();
            this.Resize += new EventHandler(FormRuptureStock_Resize); // Gérer le redimensionnement
            FormRuptureStock_Resize(this, EventArgs.Empty); // Appeler manuellement Resize au démarrage
        }

        private void ChargerMedicamentsRupture()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numMedoc, Design, stock, datePeremption " +
                                   "FROM MEDICAMENT " +
                                   "WHERE stock < 5 " +
                                   "ORDER BY stock ASC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Aucun médicament en rupture de stock (stock < 5).", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvRuptureStock.DataSource = null;
                    }
                    else
                    {
                        dgvRuptureStock.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments en rupture : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRafraichir_Click(object sender, EventArgs e)
        {
            ChargerMedicamentsRupture();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRuptureStock_Resize(object sender, EventArgs e)
        {
            // Ajuster la taille et la position de pnlGrid
            int margin = 20; // Marge globale
            int buttonPanelHeight = 60; // Hauteur fixe pour pnlButtons
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            int pnlGridWidth = formWidth - 2 * margin;
            int pnlGridHeight = formHeight - 2 * margin - buttonPanelHeight;
            pnlGrid.Size = new Size(pnlGridWidth, pnlGridHeight > 0 ? pnlGridHeight : 0);
            pnlGrid.Location = new Point(margin, margin);

            // Ajuster la taille et la position de dgvRuptureStock
            int gridMargin = 12; // Marge interne de pnlGrid
            dgvRuptureStock.Size = new Size(pnlGridWidth - 2 * gridMargin, pnlGridHeight - 2 * gridMargin);
            dgvRuptureStock.Location = new Point(gridMargin, gridMargin);

            // Ajuster la taille et la position de pnlButtons
            pnlButtons.Size = new Size(formWidth, buttonPanelHeight);
            pnlButtons.Location = new Point(0, formHeight - buttonPanelHeight);

            // Ajuster la position des boutons dans pnlButtons
            int buttonSpacing = 10;
            btnRafraichir.Location = new Point(pnlButtons.Width - btnRafraichir.Width - margin - btnFermer.Width - buttonSpacing, (pnlButtons.Height - btnRafraichir.Height) / 2);
            btnFermer.Location = new Point(pnlButtons.Width - btnFermer.Width - margin, (pnlButtons.Height - btnFermer.Height) / 2);
        }
    }
}