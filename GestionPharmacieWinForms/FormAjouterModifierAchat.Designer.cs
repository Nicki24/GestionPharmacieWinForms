namespace GestionPharmacieWinForms
{
    partial class FormAjouterModifierAchat
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            lblNomClient = new Label();
            txtNomClient = new TextBox();
            lblDateAchat = new Label();
            dtpDateAchat = new DateTimePicker();
            lblMedicament = new Label();
            txtRechercheMedicament = new TextBox();
            lstMedicaments = new ListBox();
            lblQuantite = new Label();
            txtQuantite = new TextBox();
            btnAjouterDetail = new Button();
            btnSupprimerDetail = new Button();
            dgvDetailsAchat = new DataGridView();
            btnEnregistrer = new Button();
            btnAnnuler = new Button();
            pnlForm = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvDetailsAchat).BeginInit();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // lblNomClient
            // 
            lblNomClient.AutoSize = true;
            lblNomClient.Location = new Point(0, 10);
            lblNomClient.Name = "lblNomClient";
            lblNomClient.Size = new Size(84, 20);
            lblNomClient.TabIndex = 0;
            lblNomClient.Text = "Nom Client";
            // 
            // txtNomClient
            // 
            txtNomClient.BorderStyle = BorderStyle.FixedSingle;
            txtNomClient.ForeColor = Color.Black;
            txtNomClient.Location = new Point(100, 7);
            txtNomClient.Name = "txtNomClient";
            txtNomClient.Size = new Size(200, 27);
            txtNomClient.TabIndex = 1;
            // 
            // lblDateAchat
            // 
            lblDateAchat.AutoSize = true;
            lblDateAchat.Location = new Point(0, 40);
            lblDateAchat.Name = "lblDateAchat";
            lblDateAchat.Size = new Size(83, 20);
            lblDateAchat.TabIndex = 2;
            lblDateAchat.Text = "Date Achat";
            // 
            // dtpDateAchat
            // 
            dtpDateAchat.Location = new Point(100, 37);
            dtpDateAchat.Name = "dtpDateAchat";
            dtpDateAchat.Size = new Size(200, 27);
            dtpDateAchat.TabIndex = 3;
            // 
            // lblMedicament
            // 
            lblMedicament.AutoSize = true;
            lblMedicament.Location = new Point(0, 70);
            lblMedicament.Name = "lblMedicament";
            lblMedicament.Size = new Size(92, 20);
            lblMedicament.TabIndex = 4;
            lblMedicament.Text = "Médicament";
            // 
            // txtRechercheMedicament
            // 
            txtRechercheMedicament.BorderStyle = BorderStyle.FixedSingle;
            txtRechercheMedicament.ForeColor = Color.Black;
            txtRechercheMedicament.Location = new Point(100, 67);
            txtRechercheMedicament.Name = "txtRechercheMedicament";
            txtRechercheMedicament.Size = new Size(200, 27);
            txtRechercheMedicament.TabIndex = 5;
            txtRechercheMedicament.TextChanged += txtRechercheMedicament_TextChanged;
            // 
            // lstMedicaments
            // 
            lstMedicaments.BorderStyle = BorderStyle.FixedSingle;
            lstMedicaments.FormattingEnabled = true;
            lstMedicaments.Location = new Point(100, 94);
            lstMedicaments.Name = "lstMedicaments";
            lstMedicaments.Size = new Size(200, 42);
            lstMedicaments.TabIndex = 6;
            lstMedicaments.SelectedIndexChanged += lstMedicaments_SelectedIndexChanged;
            // 
            // lblQuantite
            // 
            lblQuantite.AutoSize = true;
            lblQuantite.Location = new Point(0, 148);
            lblQuantite.Name = "lblQuantite";
            lblQuantite.Size = new Size(66, 20);
            lblQuantite.TabIndex = 7;
            lblQuantite.Text = "Quantité";
            // 
            // txtQuantite
            // 
            txtQuantite.BorderStyle = BorderStyle.FixedSingle;
            txtQuantite.ForeColor = Color.Black;
            txtQuantite.Location = new Point(100, 145);
            txtQuantite.Name = "txtQuantite";
            txtQuantite.Size = new Size(100, 27);
            txtQuantite.TabIndex = 8;
            // 
            // btnAjouterDetail
            // 
            btnAjouterDetail.BackColor = Color.FromArgb(46, 204, 113);
            btnAjouterDetail.FlatStyle = FlatStyle.Flat;
            btnAjouterDetail.ForeColor = Color.White;
            btnAjouterDetail.Location = new Point(206, 143);
            btnAjouterDetail.Name = "btnAjouterDetail";
            btnAjouterDetail.Size = new Size(120, 40);
            btnAjouterDetail.TabIndex = 9;
            btnAjouterDetail.Text = "➕ Ajouter";
            btnAjouterDetail.UseVisualStyleBackColor = false;
            btnAjouterDetail.Click += btnAjouterDetail_Click;
            // 
            // btnSupprimerDetail
            // 
            btnSupprimerDetail.BackColor = Color.FromArgb(231, 76, 60);
            btnSupprimerDetail.FlatStyle = FlatStyle.Flat;
            btnSupprimerDetail.ForeColor = Color.White;
            btnSupprimerDetail.Location = new Point(330, 143);
            btnSupprimerDetail.Name = "btnSupprimerDetail";
            btnSupprimerDetail.Size = new Size(120, 40);
            btnSupprimerDetail.TabIndex = 10;
            btnSupprimerDetail.Text = "🗑️ Supprimer";
            btnSupprimerDetail.UseVisualStyleBackColor = false;
            btnSupprimerDetail.Click += btnSupprimerDetail_Click;
            // 
            // dgvDetailsAchat
            // 
            dgvDetailsAchat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetailsAchat.BackgroundColor = Color.White;
            dgvDetailsAchat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetailsAchat.Location = new Point(0, 178);
            dgvDetailsAchat.Name = "dgvDetailsAchat";
            dgvDetailsAchat.ReadOnly = true;
            dgvDetailsAchat.RowHeadersWidth = 51;
            dgvDetailsAchat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetailsAchat.Size = new Size(438, 150);
            dgvDetailsAchat.TabIndex = 11;
            // 
            // btnEnregistrer
            // 
            btnEnregistrer.BackColor = Color.FromArgb(46, 204, 113);
            btnEnregistrer.FlatStyle = FlatStyle.Flat;
            btnEnregistrer.ForeColor = Color.White;
            btnEnregistrer.Location = new Point(244, 334);
            btnEnregistrer.Name = "btnEnregistrer";
            btnEnregistrer.Size = new Size(120, 40);
            btnEnregistrer.TabIndex = 12;
            btnEnregistrer.Text = "✔️ Enregistrer";
            btnEnregistrer.UseVisualStyleBackColor = false;
            btnEnregistrer.Click += btnEnregistrer_Click;
            // 
            // btnAnnuler
            // 
            btnAnnuler.BackColor = Color.FromArgb(231, 76, 60);
            btnAnnuler.FlatStyle = FlatStyle.Flat;
            btnAnnuler.ForeColor = Color.White;
            btnAnnuler.Location = new Point(368, 334);
            btnAnnuler.Name = "btnAnnuler";
            btnAnnuler.Size = new Size(120, 40);
            btnAnnuler.TabIndex = 13;
            btnAnnuler.Text = "❌ Annuler";
            btnAnnuler.UseVisualStyleBackColor = false;
            btnAnnuler.Click += btnAnnuler_Click;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(btnAnnuler);
            pnlForm.Controls.Add(btnEnregistrer);
            pnlForm.Controls.Add(dgvDetailsAchat);
            pnlForm.Controls.Add(btnSupprimerDetail);
            pnlForm.Controls.Add(btnAjouterDetail);
            pnlForm.Controls.Add(txtQuantite);
            pnlForm.Controls.Add(lblQuantite);
            pnlForm.Controls.Add(lstMedicaments);
            pnlForm.Controls.Add(txtRechercheMedicament);
            pnlForm.Controls.Add(lblMedicament);
            pnlForm.Controls.Add(dtpDateAchat);
            pnlForm.Controls.Add(lblDateAchat);
            pnlForm.Controls.Add(txtNomClient);
            pnlForm.Controls.Add(lblNomClient);
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(493, 383);
            pnlForm.TabIndex = 14;
            // 
            // FormAjouterModifierAchat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(515, 398);
            Controls.Add(pnlForm);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAjouterModifierAchat";
            Text = "Ajouter/Modifier Achat";
            ((System.ComponentModel.ISupportInitialize)dgvDetailsAchat).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Label lblNomClient;
        private TextBox txtNomClient;
        private Label lblDateAchat;
        private DateTimePicker dtpDateAchat;
        private Label lblMedicament;
        private TextBox txtRechercheMedicament;
        private ListBox lstMedicaments;
        private Label lblQuantite;
        private TextBox txtQuantite;
        private Button btnAjouterDetail;
        private Button btnSupprimerDetail;
        private DataGridView dgvDetailsAchat;
        private Button btnEnregistrer;
        private Button btnAnnuler;
        private Panel pnlForm;
    }
}