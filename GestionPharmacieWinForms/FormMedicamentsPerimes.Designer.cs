namespace GestionPharmacieWinForms
{
    partial class FormMedicamentsPerimes
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

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.dgvMedicamentsPerimes = new System.Windows.Forms.DataGridView();
            this.btnAnnuler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentsPerimes)).BeginInit();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.AutoSize = false;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitre.Location = new System.Drawing.Point(20, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(560, 30);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Liste des Médicaments Périmés";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // dgvMedicamentsPerimes
            this.dgvMedicamentsPerimes.AllowUserToAddRows = false;
            this.dgvMedicamentsPerimes.AllowUserToDeleteRows = false;
            this.dgvMedicamentsPerimes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None; // Géré manuellement
            this.dgvMedicamentsPerimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvMedicamentsPerimes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicamentsPerimes.Location = new System.Drawing.Point(20, 60);
            this.dgvMedicamentsPerimes.Name = "dgvMedicamentsPerimes";
            this.dgvMedicamentsPerimes.ReadOnly = true;
            this.dgvMedicamentsPerimes.RowHeadersVisible = false;
            this.dgvMedicamentsPerimes.Size = new System.Drawing.Size(560, 250);
            this.dgvMedicamentsPerimes.TabIndex = 1;
            this.dgvMedicamentsPerimes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // btnAnnuler
            this.btnAnnuler.BackColor = System.Drawing.Color.FromArgb(255, 99, 71); // Rouge clair (Tomato)
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.btnAnnuler.Location = new System.Drawing.Point(480, 320);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(100, 40);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            this.btnAnnuler.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // FormMedicamentsPerimes
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.dgvMedicamentsPerimes);
            this.Controls.Add(this.lblTitre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMedicamentsPerimes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Médicaments Périmés - PharmShop";
            this.Load += new System.EventHandler(this.FormMedicamentsPerimes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentsPerimes)).EndInit();
            this.ResumeLayout(false);
        }

        private void FormMedicamentsPerimes_Load(object sender, EventArgs e)
        {
            AdjustControls();
        }

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.DataGridView dgvMedicamentsPerimes;
        private System.Windows.Forms.Button btnAnnuler;
    }
}