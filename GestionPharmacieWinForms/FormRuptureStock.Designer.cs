namespace GestionPharmacieWinForms
{
    partial class FormRuptureStock
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
            components = new System.ComponentModel.Container();
            pnlGrid = new Panel();
            dgvRuptureStock = new DataGridView();
            pnlButtons = new Panel();
            btnRafraichir = new Button();
            btnFermer = new Button();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRuptureStock).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvRuptureStock);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new System.Drawing.Point(0, 0);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(12);
            pnlGrid.Size = new System.Drawing.Size(584, 316);
            pnlGrid.TabIndex = 0;
            // 
            // dgvRuptureStock
            // 
            dgvRuptureStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRuptureStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRuptureStock.Dock = DockStyle.Fill;
            dgvRuptureStock.Location = new System.Drawing.Point(12, 12);
            dgvRuptureStock.Name = "dgvRuptureStock";
            dgvRuptureStock.ReadOnly = true;
            dgvRuptureStock.RowHeadersWidth = 51;
            dgvRuptureStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRuptureStock.Size = new System.Drawing.Size(560, 292);
            dgvRuptureStock.TabIndex = 0;
            dgvRuptureStock.BorderStyle = BorderStyle.FixedSingle;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnRafraichir);
            pnlButtons.Controls.Add(btnFermer);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new System.Drawing.Point(0, 316);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new System.Drawing.Size(584, 45);
            pnlButtons.TabIndex = 1;
            // 
            // btnRafraichir
            // 
            btnRafraichir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRafraichir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnRafraichir.FlatStyle = FlatStyle.Flat;
            btnRafraichir.ForeColor = System.Drawing.Color.White;
            btnRafraichir.Location = new System.Drawing.Point(378, 8);
            btnRafraichir.Name = "btnRafraichir";
            btnRafraichir.Size = new System.Drawing.Size(120, 40);
            btnRafraichir.TabIndex = 1;
            btnRafraichir.Text = "🔄 Rafraîchir";
            btnRafraichir.UseVisualStyleBackColor = false;
            btnRafraichir.Click += new System.EventHandler(this.btnRafraichir_Click);
            // 
            // btnFermer
            // 
            btnFermer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnFermer.FlatStyle = FlatStyle.Flat;
            btnFermer.ForeColor = System.Drawing.Color.White;
            btnFermer.Location = new System.Drawing.Point(478, 8);
            btnFermer.Name = "btnFermer";
            btnFermer.Size = new System.Drawing.Size(120, 40);
            btnFermer.TabIndex = 2;
            btnFermer.Text = "❌ Fermer";
            btnFermer.UseVisualStyleBackColor = false;
            btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // FormRuptureStock
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            ClientSize = new System.Drawing.Size(584, 361);
            Controls.Add(pnlGrid);
            Controls.Add(pnlButtons);
            FormBorderStyle = FormBorderStyle.Sizable; // Permet le redimensionnement
            Name = "FormRuptureStock";
            Text = "Médicaments en Rupture de Stock";
            WindowState = FormWindowState.Maximized; // Maximisé au démarrage
            ((System.ComponentModel.ISupportInitialize)dgvRuptureStock).EndInit();
            pnlGrid.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Panel pnlGrid;
        private DataGridView dgvRuptureStock;
        private Panel pnlButtons;
        private Button btnRafraichir;
        private Button btnFermer;
    }
}