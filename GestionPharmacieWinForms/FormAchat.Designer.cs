namespace GestionPharmacieWinForms
{
    partial class FormAchat
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
            lblAchats = new Label();
            dgvAchats = new DataGridView();
            pnlButtons = new Panel();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnGenererFacture = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAchats).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblAchats
            // 
            lblAchats.AutoSize = true;
            lblAchats.Location = new Point(20, 20);
            lblAchats.Name = "lblAchats";
            lblAchats.Size = new Size(53, 20);
            lblAchats.TabIndex = 0;
            lblAchats.Text = "Achats";
            // 
            // dgvAchats
            // 
            dgvAchats.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAchats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAchats.BackgroundColor = Color.White;
            dgvAchats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAchats.GridColor = Color.FromArgb(200, 200, 200);
            dgvAchats.Location = new Point(20, 45);
            dgvAchats.Margin = new Padding(12);
            dgvAchats.Name = "dgvAchats";
            dgvAchats.ReadOnly = true;
            dgvAchats.RowHeadersWidth = 51;
            dgvAchats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAchats.Size = new Size(240, 150);
            dgvAchats.TabIndex = 1;
            // 
            // pnlButtons
            // 
            pnlButtons.Anchor = AnchorStyles.Bottom;
            pnlButtons.Controls.Add(btnAjouter);
            pnlButtons.Controls.Add(btnModifier);
            pnlButtons.Controls.Add(btnSupprimer);
            pnlButtons.Controls.Add(btnGenererFacture);
            pnlButtons.Location = new Point(200, 400);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(529, 50);
            pnlButtons.TabIndex = 6;
            // 
            // btnAjouter
            // 
            btnAjouter.BackColor = Color.FromArgb(46, 204, 113);
            btnAjouter.FlatStyle = FlatStyle.Flat;
            btnAjouter.ForeColor = Color.White;
            btnAjouter.Location = new Point(10, 10);
            btnAjouter.Name = "btnAjouter";
            btnAjouter.Size = new Size(120, 40);
            btnAjouter.TabIndex = 2;
            btnAjouter.Text = "➕ Ajouter";
            btnAjouter.UseVisualStyleBackColor = false;
            btnAjouter.Click += btnAjouter_Click;
            // 
            // btnModifier
            // 
            btnModifier.BackColor = Color.FromArgb(52, 152, 219);
            btnModifier.FlatStyle = FlatStyle.Flat;
            btnModifier.ForeColor = Color.White;
            btnModifier.Location = new Point(140, 10);
            btnModifier.Name = "btnModifier";
            btnModifier.Size = new Size(120, 40);
            btnModifier.TabIndex = 3;
            btnModifier.Text = "✏️ Modifier";
            btnModifier.UseVisualStyleBackColor = false;
            btnModifier.Click += btnModifier_Click;
            // 
            // btnSupprimer
            // 
            btnSupprimer.BackColor = Color.FromArgb(231, 76, 60);
            btnSupprimer.FlatStyle = FlatStyle.Flat;
            btnSupprimer.ForeColor = Color.White;
            btnSupprimer.Location = new Point(270, 10);
            btnSupprimer.Name = "btnSupprimer";
            btnSupprimer.Size = new Size(120, 40);
            btnSupprimer.TabIndex = 4;
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.UseVisualStyleBackColor = false;
            btnSupprimer.Click += btnSupprimer_Click;
            // 
            // btnGenererFacture
            // 
            btnGenererFacture.BackColor = Color.FromArgb(149, 165, 166);
            btnGenererFacture.FlatStyle = FlatStyle.Flat;
            btnGenererFacture.ForeColor = Color.White;
            btnGenererFacture.Location = new Point(400, 10);
            btnGenererFacture.Name = "btnGenererFacture";
            btnGenererFacture.Size = new Size(120, 40);
            btnGenererFacture.TabIndex = 5;
            btnGenererFacture.Text = "📄 Générer Facture";
            btnGenererFacture.UseVisualStyleBackColor = false;
            btnGenererFacture.Click += btnGenererFacture_Click;
            // 
            // FormAchat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(900, 500);
            Controls.Add(dgvAchats);
            Controls.Add(lblAchats);
            Controls.Add(pnlButtons);
            MinimizeBox = false;
            Name = "FormAchat";
            Text = "Gestion des Achats";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvAchats).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label lblAchats;
        private DataGridView dgvAchats;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnGenererFacture;
        private Panel pnlButtons;
    }
}