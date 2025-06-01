using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace GestionPharmacieWinForms
{
    public partial class FormAjouterModifierEntree : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";
        private bool isAjouterMode;
        private int ancienneQuantite;

        public string NumEntree { get; set; }
        public string NumMedoc { get; set; }
        public int StockEntree { get; set; }
        public DateTime DateEntree { get; set; }

        public FormAjouterModifierEntree(bool isAjouterMode)
        {
            InitializeComponent();
            this.isAjouterMode = isAjouterMode;

            // Définir le champ Date Entrée comme non modifiable
            txtDateEntree.ReadOnly = true;

            // Pré-remplir avec la date du jour
            txtDateEntree.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (isAjouterMode)
            {
                this.Text = "Ajouter une Entrée";
            }
            else
            {
                this.Text = "Modifier une Entrée";
            }

            ChargerMedicaments();
        }

        private void ChargerMedicaments()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numMedoc, Design FROM MEDICAMENT";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string numMedoc = reader["numMedoc"].ToString();
                        string design = reader["Design"].ToString();
                        cmbNumMedoc.Items.Add(new ComboBoxItem(numMedoc, $"{numMedoc} - {design}"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class ComboBoxItem
        {
            public string Value { get; set; }
            public string Text { get; set; }

            public ComboBoxItem(string value, string text)
            {
                Value = value;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        public void LoadData()
        {
            if (!isAjouterMode)
            {
                txtNumEntree.Text = NumEntree ?? "";
                txtNumEntree.ReadOnly = true;

                foreach (ComboBoxItem item in cmbNumMedoc.Items)
                {
                    if (item.Value == NumMedoc)
                    {
                        cmbNumMedoc.SelectedItem = item;
                        break;
                    }
                }

                txtStockEntree.Text = StockEntree.ToString();
                // Note : txtDateEntree reste à la date du jour (déjà définie dans le constructeur)
                ancienneQuantite = StockEntree;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumEntree.Text) ||
                cmbNumMedoc.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtStockEntree.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Avertissement",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string numEntree = txtNumEntree.Text;
                string numMedoc = ((ComboBoxItem)cmbNumMedoc.SelectedItem).Value;
                int stockEntree = int.Parse(txtStockEntree.Text);
                // Utiliser la date du jour pour l'enregistrement
                DateTime dateEntree = DateTime.Now;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (isAjouterMode)
                    {
                        string queryCheck = "SELECT COUNT(*) FROM ENTREE WHERE numEntree = @numEntree";
                        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@numEntree", numEntree);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Ce numéro d'entrée existe déjà.", "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string queryInsert = "INSERT INTO ENTREE (numEntree, numMedoc, stockEntree, dateEntree) " +
                                            "VALUES (@numEntree, @numMedoc, @stockEntree, @dateEntree)";
                        MySqlCommand cmdInsert = new MySqlCommand(queryInsert, conn);
                        cmdInsert.Parameters.AddWithValue("@numEntree", numEntree);
                        cmdInsert.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmdInsert.Parameters.AddWithValue("@stockEntree", stockEntree);
                        cmdInsert.Parameters.AddWithValue("@dateEntree", dateEntree);
                        cmdInsert.ExecuteNonQuery();

                        string queryUpdateStock = "UPDATE MEDICAMENT SET stock = stock + @stockEntree WHERE numMedoc = @numMedoc";
                        MySqlCommand cmdUpdateStock = new MySqlCommand(queryUpdateStock, conn);
                        cmdUpdateStock.Parameters.AddWithValue("@stockEntree", stockEntree);
                        cmdUpdateStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmdUpdateStock.ExecuteNonQuery();
                    }
                    else
                    {
                        int difference = stockEntree - ancienneQuantite;

                        string queryUpdate = "UPDATE ENTREE SET numMedoc = @numMedoc, stockEntree = @stockEntree, " +
                                            "dateEntree = @dateEntree WHERE numEntree = @numEntree";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@numEntree", numEntree);
                        cmdUpdate.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmdUpdate.Parameters.AddWithValue("@stockEntree", stockEntree);
                        cmdUpdate.Parameters.AddWithValue("@dateEntree", dateEntree);
                        cmdUpdate.ExecuteNonQuery();

                        string queryUpdateStock = "UPDATE MEDICAMENT SET stock = stock + @difference WHERE numMedoc = @numMedoc";
                        MySqlCommand cmdUpdateStock = new MySqlCommand(queryUpdateStock, conn);
                        cmdUpdateStock.Parameters.AddWithValue("@difference", difference);
                        cmdUpdateStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                        cmdUpdateStock.ExecuteNonQuery();
                    }

                    string message = isAjouterMode ? "Entrée ajoutée avec succès !" : "Entrée modifiée avec succès !";
                    MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Erreur de format : assurez-vous que la quantité est un nombre.",
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
        private void FormAjouterModifierEntree_Resize(object sender, EventArgs e)
        {
            pnlForm.Location = new Point(
                (ClientSize.Width - pnlForm.Width) / 2,
                (ClientSize.Height - pnlForm.Height) / 2
            );
        }
    }
}