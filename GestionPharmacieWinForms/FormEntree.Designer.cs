namespace GestionPharmacieWinForms
{
    partial class FormEntree
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
            dgvEntrees = new DataGridView();
            pnlButtons = new Panel();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnRafraichir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEntrees).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // dgvEntrees
            // 
            dgvEntrees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEntrees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntrees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntrees.Location = new Point(20, 20);
            dgvEntrees.Margin = new Padding(12);
            dgvEntrees.Name = "dgvEntrees";
            dgvEntrees.ReadOnly = true;
            dgvEntrees.RowHeadersWidth = 51;
            dgvEntrees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntrees.Size = new Size(240, 150);
            dgvEntrees.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.Anchor = AnchorStyles.Bottom;
            pnlButtons.Controls.Add(btnAjouter);
            pnlButtons.Controls.Add(btnModifier);
            pnlButtons.Controls.Add(btnSupprimer);
            pnlButtons.Controls.Add(btnRafraichir);
            pnlButtons.Location = new Point(150, 400);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(532, 50);
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
            btnAjouter.TabIndex = 1;
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
            btnModifier.TabIndex = 2;
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
            btnSupprimer.TabIndex = 3;
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.UseVisualStyleBackColor = false;
            btnSupprimer.Click += btnSupprimer_Click;
            // 
            // btnRafraichir
            // 
            btnRafraichir.BackColor = Color.FromArgb(149, 165, 166);
            btnRafraichir.FlatStyle = FlatStyle.Flat;
            btnRafraichir.ForeColor = Color.White;
            btnRafraichir.Location = new Point(400, 10);
            btnRafraichir.Name = "btnRafraichir";
            btnRafraichir.Size = new Size(120, 40);
            btnRafraichir.TabIndex = 4;
            btnRafraichir.Text = "🔄 Rafraîchir";
            btnRafraichir.UseVisualStyleBackColor = false;
            btnRafraichir.Click += btnRafraichir_Click;
            // 
            // FormEntree
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(800, 450);
            Controls.Add(pnlButtons);
            Controls.Add(dgvEntrees);
            MinimizeBox = false;
            Name = "FormEntree";
            Text = "Gestion des Entrées";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvEntrees).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private DataGridView dgvEntrees;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnRafraichir;
        private Panel pnlButtons;
    }
}