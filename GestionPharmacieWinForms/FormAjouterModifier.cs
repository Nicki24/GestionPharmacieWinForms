using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionPharmacieWinForms
{
    public partial class FormAjouterModifier : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";
        private bool isAjouterMode;

        public string NumMedoc { get; set; }
        public string Designation { get; set; }
        public int PrixUnitaire { get; set; }
        public int Stock { get; set; }
        public DateTime DatePeremption { get; set; }

        public FormAjouterModifier(bool isAjouterMode)
        {
            InitializeComponent();
            this.isAjouterMode = isAjouterMode;

            if (isAjouterMode)
            {
                this.Text = "Ajouter un Médicament";
            }
            else
            {
                this.Text = "Modifier un Médicament";
            }
        }

        public void LoadData()
        {
            if (!isAjouterMode)
            {
                txtNumMedoc.Text = NumMedoc ?? "";
                txtNumMedoc.ReadOnly = true;
                txtDesignation.Text = Designation ?? "";
                txtPrixUnitaire.Text = PrixUnitaire.ToString();
                txtStock.Text = Stock.ToString();
                txtDatePeremption.Text = DatePeremption.ToString("yyyy-MM-dd");
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumMedoc.Text) ||
                string.IsNullOrWhiteSpace(txtDesignation.Text) ||
                string.IsNullOrWhiteSpace(txtPrixUnitaire.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                string.IsNullOrWhiteSpace(txtDatePeremption.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string numMedoc = txtNumMedoc.Text;
                string designation = txtDesignation.Text;
                int prixUnitaire = int.Parse(txtPrixUnitaire.Text);
                int stock = int.Parse(txtStock.Text);
                DateTime datePeremption = DateTime.Parse(txtDatePeremption.Text);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    if (isAjouterMode)
                    {
                        string queryCheck = "SELECT COUNT(*) FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@numMedoc", numMedoc);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Ce numéro de médicament existe déjà.", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string query = "INSERT INTO MEDICAMENT (numMedoc, Design, prix_unitaire, stock, datePeremption) " +
                                       "VALUES (@numMedoc, @design, @prixUnitaire, @stock, @datePeremption)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmd.Parameters.AddWithValue("@design", designation);
                        cmd.Parameters.AddWithValue("@prixUnitaire", prixUnitaire);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@datePeremption", datePeremption);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string query = "UPDATE MEDICAMENT SET Design = @design, prix_unitaire = @prixUnitaire, " +
                                       "stock = @stock, datePeremption = @datePeremption WHERE numMedoc = @numMedoc";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmd.Parameters.AddWithValue("@design", designation);
                        cmd.Parameters.AddWithValue("@prixUnitaire", prixUnitaire);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@datePeremption", datePeremption);
                        cmd.ExecuteNonQuery();
                    }

                    string message = isAjouterMode ? "Médicament ajouté avec succès !" : "Médicament modifié avec succès !";
                    MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Erreur de format : assurez-vous que le prix et le stock sont des nombres, et que la date est au format YYYY-MM-DD.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Centrer pnlForm lorsque la fenêtre est redimensionnée
        private void FormAjouterModifier_Resize(object sender, EventArgs e)
        {
            pnlForm.Location = new Point(
                (ClientSize.Width - pnlForm.Width) / 2,
                (ClientSize.Height - pnlForm.Height) / 2
            );
        }

        private void txtNumMedoc_TextChanged(object sender, EventArgs e)
        {
        }
    }
}