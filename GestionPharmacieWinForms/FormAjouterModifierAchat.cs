using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace GestionPharmacieWinForms
{
    public partial class FormAjouterModifierAchat : Form
    {
        private string connectionString = "Server=localhost;Database=gestion_pharmacie;Uid=root;Pwd=;";
        private string numAchat; // numAchat est une chaîne
        private bool estModification;
        private List<MedicamentItem> medicamentsList; // Liste pour la recherche
        private DataTable initialDetails; // Pour stocker les détails initiaux

        private class MedicamentItem
        {
            public string NumMedoc { get; set; }
            public string Design { get; set; }
            public int PrixUnitaire { get; set; }

            public MedicamentItem(string numMedoc, string design, int prixUnitaire)
            {
                NumMedoc = numMedoc;
                Design = design;
                PrixUnitaire = prixUnitaire;
            }

            public override string ToString()
            {
                return $"{NumMedoc} - {Design} - {PrixUnitaire} Ar";
            }
        }

        public FormAjouterModifierAchat(string numAchat = null)
        {
            InitializeComponent();
            this.numAchat = numAchat;
            estModification = !string.IsNullOrWhiteSpace(numAchat);
            medicamentsList = new List<MedicamentItem>();
            initialDetails = new DataTable(); // Initialisation
            ChargerMedicaments();
            if (estModification)
            {
                ChargerAchat();
            }
        }

        private void ChargerMedicaments()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numMedoc, Design, prix_unitaire FROM MEDICAMENT";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    medicamentsList.Clear();
                    lstMedicaments.Items.Clear();

                    while (reader.Read())
                    {
                        string numMedoc = reader["numMedoc"].ToString();
                        string design = reader["Design"].ToString();
                        int prixUnitaire = reader["prix_unitaire"] != DBNull.Value ? Convert.ToInt32(reader["prix_unitaire"]) : 0;
                        var item = new MedicamentItem(numMedoc, design, prixUnitaire);
                        medicamentsList.Add(item);
                        lstMedicaments.Items.Add(item);
                    }

                    if (lstMedicaments.Items.Count == 0)
                    {
                        lstMedicaments.Items.Add("Aucun médicament disponible");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerAchat()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT nomClient, dateAchat FROM ACHAT WHERE numAchat = @numAchat";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@numAchat", numAchat);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtNomClient.Text = reader["nomClient"].ToString();
                        dtpDateAchat.Value = Convert.ToDateTime(reader["dateAchat"]);
                    }
                    reader.Close();

                    query = "SELECT ad.numMedoc, m.Design, ad.quantite " +
                            "FROM ACHAT_DETAILS ad " +
                            "LEFT JOIN MEDICAMENT m ON ad.numMedoc = m.numMedoc " +
                            "WHERE ad.numAchat = @numAchat";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@numAchat", numAchat);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("numMedoc", typeof(string));
                    dt.Columns.Add("Design", typeof(string));
                    dt.Columns.Add("quantite", typeof(int));
                    adapter.Fill(dt);
                    dgvDetailsAchat.DataSource = dt;
                    initialDetails = dt.Copy(); // Stocker une copie des détails initiaux
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de l'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRechercheMedicament_TextChanged(object sender, EventArgs e)
        {
            string recherche = txtRechercheMedicament.Text.Trim().ToLower();
            lstMedicaments.Items.Clear();

            var filteredMedicaments = medicamentsList
                .Where(m => m.Design.ToLower().Contains(recherche))
                .ToList();

            if (filteredMedicaments.Count == 0)
            {
                lstMedicaments.Items.Add("Aucun médicament trouvé");
            }
            else
            {
                foreach (var medoc in filteredMedicaments)
                {
                    lstMedicaments.Items.Add(medoc);
                }
            }
        }

        private void lstMedicaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMedicaments.SelectedItem != null && lstMedicaments.SelectedItem.ToString() != "Aucun médicament trouvé")
            {
                var selectedItem = (MedicamentItem)lstMedicaments.SelectedItem;
                txtRechercheMedicament.Text = selectedItem.Design;
            }
        }

        private void btnAjouterDetail_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier si un médicament est sélectionné et valide
                if (lstMedicaments.SelectedItem == null || lstMedicaments.SelectedItem.ToString() == "Aucun médicament trouvé")
                {
                    MessageBox.Show("Veuillez sélectionner un médicament valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Vérifier la quantité
                if (!int.TryParse(txtQuantite.Text, out int quantite) || quantite <= 0)
                {
                    MessageBox.Show("Veuillez entrer une quantité valide supérieure à 0.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Récupérer le médicament sélectionné
                var selectedItem = (MedicamentItem)lstMedicaments.SelectedItem;

                // Initialiser ou récupérer le DataTable
                DataTable dt = (dgvDetailsAchat.DataSource as DataTable) ?? new DataTable();
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("numMedoc", typeof(string));
                    dt.Columns.Add("Design", typeof(string));
                    dt.Columns.Add("quantite", typeof(int));
                }

                // Ajouter une nouvelle ligne au DataTable
                DataRow row = dt.NewRow();
                row["numMedoc"] = selectedItem.NumMedoc;
                row["Design"] = selectedItem.Design;
                row["quantite"] = quantite;
                dt.Rows.Add(row);

                // Mettre à jour le DataGridView
                dgvDetailsAchat.DataSource = dt;

                // Réinitialiser les champs
                txtRechercheMedicament.Clear();
                txtQuantite.Clear();
                lstMedicaments.SelectedIndex = -1;
                ChargerMedicaments(); // Recharger la liste complète
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du détail : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimerDetail_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier si une ligne est sélectionnée
                if (dgvDetailsAchat.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un détail à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmer la suppression
                if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce détail ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                // Récupérer le DataTable
                DataTable dt = (dgvDetailsAchat.DataSource as DataTable);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Aucun détail à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Supprimer la ligne sélectionnée
                int selectedIndex = dgvDetailsAchat.SelectedRows[0].Index;
                dt.Rows.RemoveAt(selectedIndex);

                // Mettre à jour le DataGridView
                dgvDetailsAchat.DataSource = dt;

                // Rafraîchir l'affichage
                if (dt.Rows.Count == 0)
                {
                    dgvDetailsAchat.DataSource = null;
                }
                else
                {
                    dgvDetailsAchat.Refresh();
                }

                MessageBox.Show("Détail supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression du détail : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenererNumAchat()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numAchat FROM ACHAT ORDER BY numAchat";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    List<int> numeros = new List<int>();
                    while (reader.Read())
                    {
                        string numAchat = reader["numAchat"].ToString();
                        string numeroPart = numAchat.Substring(3); // Extrait les chiffres après "ACH"
                        if (int.TryParse(numeroPart, out int numero))
                        {
                            numeros.Add(numero);
                        }
                    }
                    reader.Close();

                    // Trouver le premier numéro manquant
                    if (numeros.Count == 0)
                    {
                        return "ACH001"; // Si la table est vide, commencer par ACH001
                    }

                    numeros.Sort();
                    int expectedNumber = 1;
                    foreach (int num in numeros)
                    {
                        if (num != expectedNumber)
                        {
                            break;
                        }
                        expectedNumber++;
                    }

                    // Générer le numéro manquant ou le suivant
                    return $"ACH{expectedNumber:D3}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la génération du numéro d'achat : " + ex.Message);
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomClient.Text) || (dgvDetailsAchat.DataSource as DataTable)?.Rows.Count == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs et ajouter au moins un médicament.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Générer numAchat si c'est un nouvel achat
                        if (!estModification)
                        {
                            numAchat = GenererNumAchat();
                        }

                        // Insérer ou modifier l'achat
                        string queryAchat;
                        if (estModification)
                        {
                            queryAchat = "UPDATE ACHAT SET nomClient = @nomClient, dateAchat = @dateAchat WHERE numAchat = @numAchat";
                        }
                        else
                        {
                            queryAchat = "INSERT INTO ACHAT (numAchat, nomClient, dateAchat) VALUES (@numAchat, @nomClient, @dateAchat)";
                        }

                        MySqlCommand cmdAchat = new MySqlCommand(queryAchat, conn, transaction);
                        cmdAchat.Parameters.AddWithValue("@numAchat", numAchat);
                        cmdAchat.Parameters.AddWithValue("@nomClient", txtNomClient.Text);
                        cmdAchat.Parameters.AddWithValue("@dateAchat", dtpDateAchat.Value);
                        cmdAchat.ExecuteNonQuery();

                        // Supprimer les anciens détails si modification
                        if (estModification)
                        {
                            string queryDeleteDetails = "DELETE FROM ACHAT_DETAILS WHERE numAchat = @numAchat";
                            MySqlCommand cmdDelete = new MySqlCommand(queryDeleteDetails, conn, transaction);
                            cmdDelete.Parameters.AddWithValue("@numAchat", numAchat);
                            cmdDelete.ExecuteNonQuery();
                        }

                        // Restaurer le stock des médicaments supprimés (présents dans initialDetails mais pas dans le nouveau DataTable)
                        DataTable dtCurrent = (dgvDetailsAchat.DataSource as DataTable);
                        var initialMedocs = initialDetails.AsEnumerable().Select(r => r.Field<string>("numMedoc")).ToList();
                        var currentMedocs = dtCurrent.AsEnumerable().Select(r => r.Field<string>("numMedoc")).ToList();

                        foreach (DataRow initialRow in initialDetails.Rows)
                        {
                            string initialNumMedoc = initialRow["numMedoc"].ToString();
                            if (!currentMedocs.Contains(initialNumMedoc))
                            {
                                int quantiteInitiale = Convert.ToInt32(initialRow["quantite"]);
                                string queryRestoreStock = "UPDATE MEDICAMENT SET stock = stock + @quantite WHERE numMedoc = @numMedoc";
                                MySqlCommand cmdRestoreStock = new MySqlCommand(queryRestoreStock, conn, transaction);
                                cmdRestoreStock.Parameters.AddWithValue("@quantite", quantiteInitiale);
                                cmdRestoreStock.Parameters.AddWithValue("@numMedoc", initialNumMedoc);
                                cmdRestoreStock.ExecuteNonQuery();
                            }
                        }

                        // Insérer les nouveaux détails et mettre à jour le stock
                        DataTable dtDetails = (dgvDetailsAchat.DataSource as DataTable);
                        foreach (DataRow row in dtDetails.Rows)
                        {
                            string numMedoc = row["numMedoc"].ToString();
                            int quantite = Convert.ToInt32(row["quantite"]);

                            // Vérifier le stock actuel
                            string queryStock = "SELECT stock FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                            MySqlCommand cmdStock = new MySqlCommand(queryStock, conn, transaction);
                            cmdStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                            object stockObj = cmdStock.ExecuteScalar();
                            int stockActuel = stockObj != DBNull.Value ? Convert.ToInt32(stockObj) : 0;

                            if (stockActuel < quantite)
                            {
                                throw new Exception($"Stock insuffisant pour le médicament {numMedoc}. Stock actuel : {stockActuel}, Quantité demandée : {quantite}");
                            }

                            // Mettre à jour le stock
                            int nouveauStock = stockActuel - quantite;
                            string queryUpdateStock = "UPDATE MEDICAMENT SET stock = @nouveauStock WHERE numMedoc = @numMedoc";
                            MySqlCommand cmdUpdateStock = new MySqlCommand(queryUpdateStock, conn, transaction);
                            cmdUpdateStock.Parameters.AddWithValue("@nouveauStock", nouveauStock);
                            cmdUpdateStock.Parameters.AddWithValue("@numMedoc", numMedoc);
                            cmdUpdateStock.ExecuteNonQuery();

                            // Insérer le détail de l'achat
                            string queryDetail = "INSERT INTO ACHAT_DETAILS (numAchat, numMedoc, quantite) VALUES (@numAchat, @numMedoc, @quantite)";
                            MySqlCommand cmdDetail = new MySqlCommand(queryDetail, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@numAchat", numAchat);
                            cmdDetail.Parameters.AddWithValue("@numMedoc", numMedoc);
                            cmdDetail.Parameters.AddWithValue("@quantite", quantite);
                            cmdDetail.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show(estModification ? "Achat modifié avec succès !" : "Achat enregistré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Générer la facture PDF
                        GenererFacturePDF(numAchat, txtNomClient.Text, dtpDateAchat.Value, dtDetails);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erreur lors de l'enregistrement de l'achat : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                double[] columnWidths = new double[] { tableWidth * 0.25, tableWidth * 0.35, tableWidth * 0.20, tableWidth * 0.20 };

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

                // Numéro d'achat et Date d'achat
                gfx.DrawString($"Numéro d'achat : {numAchat}", fontNormal, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 20), leftFormat);
                yPosition += 20;
                gfx.DrawString($"Date d'achat : {dateAchat:dd/MM/yyyy}", fontNormal, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 20), leftFormat);
                yPosition += 40;

                // Tableau
                gfx.DrawRectangle(XPens.Black, margin, yPosition, tableWidth, 20);
                XStringFormat headerFormat = new XStringFormat { Alignment = XStringAlignment.Center };
                gfx.DrawString("Numéro", fontTableHeader, XBrushes.Black, new XRect(margin, yPosition + 5, columnWidths[0], 20), headerFormat);
                gfx.DrawString("Désignation", fontTableHeader, XBrushes.Black, new XRect(margin + columnWidths[0], yPosition + 5, columnWidths[1], 20), headerFormat);
                gfx.DrawString("Quantité", fontTableHeader, XBrushes.Black, new XRect(margin + columnWidths[0] + columnWidths[1], yPosition + 5, columnWidths[2], 20), headerFormat);
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
                        string numMedoc = row["numMedoc"] != DBNull.Value ? row["numMedoc"].ToString() : "N/A";
                        string design = row["Design"] != DBNull.Value ? row["Design"].ToString() : "Non défini";
                        int quantite = row["quantite"] != DBNull.Value ? Convert.ToInt32(row["quantite"]) : 0;

                        // Récupérer le prix unitaire depuis la table MEDICAMENT
                        string query = "SELECT prix_unitaire FROM MEDICAMENT WHERE numMedoc = @numMedoc";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@numMedoc", numMedoc);
                        object prixUnitaireObj = cmd.ExecuteScalar();
                        int prixUnitaire = prixUnitaireObj != DBNull.Value ? Convert.ToInt32(prixUnitaireObj) : 0;
                        double prixTotalLigne = quantite * prixUnitaire;
                        prixTotalGeneral += prixTotalLigne;

                        gfx.DrawRectangle(XPens.Black, margin, yPosition, tableWidth, 20);
                        gfx.DrawString(numMedoc, fontNormal, XBrushes.Black, new XRect(margin, yPosition + 5, columnWidths[0], 20), cellFormat);
                        gfx.DrawString(design, fontNormal, XBrushes.Black, new XRect(margin + columnWidths[0], yPosition + 5, columnWidths[1], 20), designFormat);
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

                // Pied de page
                yPosition += 40;
                XStringFormat footerFormat = new XStringFormat { Alignment = XStringAlignment.Center };
                gfx.DrawString("Merci de votre achat !", fontNormal, XBrushes.Black, new XRect(margin, yPosition, page.Width - 2 * margin, 20), footerFormat);

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

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}