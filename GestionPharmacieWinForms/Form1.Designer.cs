namespace GestionPharmacieWinForms
{
    partial class Form1
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
            dgvMedicaments = new DataGridView();
            pnlButtons = new Panel();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnRafraichir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMedicaments).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMedicaments
            // 
            dgvMedicaments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMedicaments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMedicaments.BackgroundColor = Color.White;
            dgvMedicaments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMedicaments.GridColor = Color.FromArgb(200, 200, 200);
            dgvMedicaments.Location = new Point(20, 20);
            dgvMedicaments.Margin = new Padding(12);
            dgvMedicaments.Name = "dgvMedicaments";
            dgvMedicaments.ReadOnly = true;
            dgvMedicaments.RowHeadersWidth = 51;
            dgvMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicaments.Size = new Size(760, 360);
            dgvMedicaments.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.Anchor = AnchorStyles.Bottom;
            pnlButtons.Controls.Add(btnAjouter);
            pnlButtons.Controls.Add(btnModifier);
            pnlButtons.Controls.Add(btnSupprimer);
            pnlButtons.Controls.Add(btnRafraichir);
            pnlButtons.Location = new Point(200, 390);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(526, 50);
            pnlButtons.TabIndex = 5;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(800, 450);
            Controls.Add(pnlButtons);
            Controls.Add(dgvMedicaments);
            MinimizeBox = false;
            Name = "Form1";
            Text = "Gestion Pharmacie";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvMedicaments).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private DataGridView dgvMedicaments;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnRafraichir;
        private Panel pnlButtons;
    }
}