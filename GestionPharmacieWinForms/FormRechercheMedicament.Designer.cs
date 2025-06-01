namespace GestionPharmacieWinForms
{
    partial class FormRechercheMedicament
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
            lblRechercheMedicament = new Label();
            txtRechercheMedicament = new TextBox();
            btnRechercher = new Button();
            dgvMedicaments = new DataGridView();
            btnAnnuler = new Button();
            pnlForm = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvMedicaments).BeginInit();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // lblRechercheMedicament
            // 
            lblRechercheMedicament.AutoSize = true;
            lblRechercheMedicament.Location = new Point(0, 10);
            lblRechercheMedicament.Name = "lblRechercheMedicament";
            lblRechercheMedicament.Size = new Size(169, 20);
            lblRechercheMedicament.TabIndex = 0;
            lblRechercheMedicament.Text = "Rechercher Médicament";
            // 
            // txtRechercheMedicament
            // 
            txtRechercheMedicament.Location = new Point(175, 3);
            txtRechercheMedicament.Name = "txtRechercheMedicament";
            txtRechercheMedicament.Size = new Size(200, 27);
            txtRechercheMedicament.TabIndex = 1;
            txtRechercheMedicament.BorderStyle = BorderStyle.FixedSingle;
            txtRechercheMedicament.ForeColor = System.Drawing.Color.Black;
            // 
            // btnRechercher
            // 
            btnRechercher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnRechercher.FlatStyle = FlatStyle.Flat;
            btnRechercher.ForeColor = System.Drawing.Color.White;
            btnRechercher.Location = new Point(381, 0);
            btnRechercher.Name = "btnRechercher";
            btnRechercher.Size = new Size(120, 40);
            btnRechercher.TabIndex = 2;
            btnRechercher.Text = "🔍 Rechercher";
            btnRechercher.UseVisualStyleBackColor = false;
            btnRechercher.Click += btnRechercher_Click;
            // 
            // dgvMedicaments
            // 
            dgvMedicaments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedicaments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMedicaments.Location = new Point(0, 40);
            dgvMedicaments.Name = "dgvMedicaments";
            dgvMedicaments.ReadOnly = true;
            dgvMedicaments.RowHeadersWidth = 51;
            dgvMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicaments.Size = new Size(438, 200);
            dgvMedicaments.TabIndex = 3;
            dgvMedicaments.BorderStyle = BorderStyle.FixedSingle;
            // 
            // btnAnnuler
            // 
            btnAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnAnnuler.FlatStyle = FlatStyle.Flat;
            btnAnnuler.ForeColor = System.Drawing.Color.White;
            btnAnnuler.Location = new Point(381, 246);
            btnAnnuler.Name = "btnAnnuler";
            btnAnnuler.Size = new Size(120, 40);
            btnAnnuler.TabIndex = 4;
            btnAnnuler.Text = "❌ Annuler";
            btnAnnuler.UseVisualStyleBackColor = false;
            btnAnnuler.Click += btnAnnuler_Click;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(btnAnnuler);
            pnlForm.Controls.Add(dgvMedicaments);
            pnlForm.Controls.Add(btnRechercher);
            pnlForm.Controls.Add(txtRechercheMedicament);
            pnlForm.Controls.Add(lblRechercheMedicament);
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(521, 286);
            pnlForm.TabIndex = 5;
            // 
            // FormRechercheMedicament
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            ClientSize = new Size(545, 310);
            Controls.Add(pnlForm);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            Name = "FormRechercheMedicament";
            Text = "Rechercher un Médicament";
            WindowState = FormWindowState.Maximized; // Ouvrir en plein écran
            Resize += FormRechercheMedicament_Resize;
            ((System.ComponentModel.ISupportInitialize)dgvMedicaments).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Label lblRechercheMedicament;
        private TextBox txtRechercheMedicament;
        private Button btnRechercher;
        private DataGridView dgvMedicaments;
        private Button btnAnnuler;
        private Panel pnlForm;
    }
}