namespace GestionPharmacieWinForms
{
    partial class FormDashboard
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
            pnlSidebar = new Panel();
            lblUtilisateur = new Label();
            btnVoirRuptures = new Button();
            btnRechercherMedicaments = new Button();
            btnGestionAchats = new Button();
            btnGestionEntrees = new Button();
            btnGestionPharmacie = new Button();
            lblTitre = new Label();
            pnlMain = new Panel();
            pnlHistogramme = new Panel();
            picHistogramme = new PictureBox();
            cboPeriode = new ComboBox();
            lblHistogrammeTitle = new Label();
            pnlTopMedicaments = new Panel();
            dgvTopMedicaments = new DataGridView();
            lblTopMedicamentsTitle = new Label();
            pnlRecetteCard = new Panel();
            lblRecetteTotale = new Label();
            lblRecetteTitle = new Label();
            pnlAchatCard = new Panel();
            lblTotalAchats = new Label();
            lblAchatTitle = new Label();
            pnlEntreeCard = new Panel();
            lblTotalEntrees = new Label();
            lblEntreeTitle = new Label();
            pnlMedicamentCard = new Panel();
            lblTotalMedicaments = new Label();
            lblMedicamentTitle = new Label();
            btnLoadData = new Button();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlHistogramme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHistogramme).BeginInit();
            pnlTopMedicaments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopMedicaments).BeginInit();
            pnlRecetteCard.SuspendLayout();
            pnlAchatCard.SuspendLayout();
            pnlEntreeCard.SuspendLayout();
            pnlMedicamentCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(0, 122, 204);
            pnlSidebar.Controls.Add(lblUtilisateur);
            pnlSidebar.Controls.Add(btnVoirRuptures);
            pnlSidebar.Controls.Add(btnRechercherMedicaments);
            pnlSidebar.Controls.Add(btnGestionAchats);
            pnlSidebar.Controls.Add(btnGestionEntrees);
            pnlSidebar.Controls.Add(btnGestionPharmacie);
            pnlSidebar.Controls.Add(lblTitre);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(225, 600);
            pnlSidebar.TabIndex = 0;
            // 
            // lblUtilisateur
            // 
            lblUtilisateur.AutoSize = true;
            lblUtilisateur.ForeColor = Color.White;
            lblUtilisateur.Location = new Point(20, 400);
            lblUtilisateur.Name = "lblUtilisateur";
            lblUtilisateur.Size = new Size(151, 20);
            lblUtilisateur.TabIndex = 3;
            lblUtilisateur.Text = "Bienvenue, Utilisateur";
            // 
            // btnVoirRuptures
            // 
            btnVoirRuptures.BackColor = Color.FromArgb(30, 144, 255);
            btnVoirRuptures.FlatStyle = FlatStyle.Flat;
            btnVoirRuptures.ForeColor = Color.White;
            btnVoirRuptures.Location = new Point(20, 300);
            btnVoirRuptures.Name = "btnVoirRuptures";
            btnVoirRuptures.Size = new Size(180, 40);
            btnVoirRuptures.TabIndex = 6;
            btnVoirRuptures.Text = "⚠️ Ruptures de Stock";
            btnVoirRuptures.UseVisualStyleBackColor = false;
            btnVoirRuptures.Click += btnVoirRuptures_Click;
            btnVoirRuptures.MouseEnter += btnVoirRuptures_MouseEnter;
            btnVoirRuptures.MouseLeave += btnVoirRuptures_MouseLeave;
            // 
            // btnRechercherMedicaments
            // 
            btnRechercherMedicaments.BackColor = Color.FromArgb(30, 144, 255);
            btnRechercherMedicaments.FlatStyle = FlatStyle.Flat;
            btnRechercherMedicaments.ForeColor = Color.White;
            btnRechercherMedicaments.Location = new Point(20, 250);
            btnRechercherMedicaments.Name = "btnRechercherMedicaments";
            btnRechercherMedicaments.Size = new Size(180, 40);
            btnRechercherMedicaments.TabIndex = 5;
            btnRechercherMedicaments.Text = "🔍 Rechercher Médicaments";
            btnRechercherMedicaments.UseVisualStyleBackColor = false;
            btnRechercherMedicaments.Click += btnRechercherMedicaments_Click;
            btnRechercherMedicaments.MouseEnter += btnRechercherMedicaments_MouseEnter;
            btnRechercherMedicaments.MouseLeave += btnRechercherMedicaments_MouseLeave;
            // 
            // btnGestionAchats
            // 
            btnGestionAchats.BackColor = Color.FromArgb(30, 144, 255);
            btnGestionAchats.FlatStyle = FlatStyle.Flat;
            btnGestionAchats.ForeColor = Color.White;
            btnGestionAchats.Location = new Point(20, 200);
            btnGestionAchats.Name = "btnGestionAchats";
            btnGestionAchats.Size = new Size(180, 40);
            btnGestionAchats.TabIndex = 4;
            btnGestionAchats.Text = "\U0001f6d2 Gestion des Achats";
            btnGestionAchats.UseVisualStyleBackColor = false;
            btnGestionAchats.Click += btnGestionAchats_Click;
            btnGestionAchats.MouseEnter += btnGestionAchats_MouseEnter;
            btnGestionAchats.MouseLeave += btnGestionAchats_MouseLeave;
            // 
            // btnGestionEntrees
            // 
            btnGestionEntrees.BackColor = Color.FromArgb(30, 144, 255);
            btnGestionEntrees.FlatStyle = FlatStyle.Flat;
            btnGestionEntrees.ForeColor = Color.White;
            btnGestionEntrees.Location = new Point(20, 150);
            btnGestionEntrees.Name = "btnGestionEntrees";
            btnGestionEntrees.Size = new Size(180, 40);
            btnGestionEntrees.TabIndex = 2;
            btnGestionEntrees.Text = "📋 Gestion des Entrées";
            btnGestionEntrees.UseVisualStyleBackColor = false;
            btnGestionEntrees.Click += btnGestionEntrees_Click;
            btnGestionEntrees.MouseEnter += btnGestionEntrees_MouseEnter;
            btnGestionEntrees.MouseLeave += btnGestionEntrees_MouseLeave;
            // 
            // btnGestionPharmacie
            // 
            btnGestionPharmacie.BackColor = Color.FromArgb(30, 144, 255);
            btnGestionPharmacie.FlatStyle = FlatStyle.Flat;
            btnGestionPharmacie.ForeColor = Color.White;
            btnGestionPharmacie.Location = new Point(20, 100);
            btnGestionPharmacie.Name = "btnGestionPharmacie";
            btnGestionPharmacie.Size = new Size(180, 40);
            btnGestionPharmacie.TabIndex = 1;
            btnGestionPharmacie.Text = "💊 Gestion Pharmacie";
            btnGestionPharmacie.UseVisualStyleBackColor = false;
            btnGestionPharmacie.Click += btnGestionPharmacie_Click;
            btnGestionPharmacie.MouseEnter += btnGestionPharmacie_MouseEnter;
            btnGestionPharmacie.MouseLeave += btnGestionPharmacie_MouseLeave;
            // 
            // lblTitre
            // 
            lblTitre.AutoSize = true;
            lblTitre.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitre.ForeColor = Color.White;
            lblTitre.Location = new Point(20, 20);
            lblTitre.Name = "lblTitre";
            lblTitre.Size = new Size(111, 28);
            lblTitre.TabIndex = 0;
            lblTitre.Text = "Pharmacie";
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.Controls.Add(pnlHistogramme);
            pnlMain.Controls.Add(pnlTopMedicaments);
            pnlMain.Controls.Add(pnlRecetteCard);
            pnlMain.Controls.Add(pnlAchatCard);
            pnlMain.Controls.Add(pnlEntreeCard);
            pnlMain.Controls.Add(pnlMedicamentCard);
            pnlMain.Controls.Add(btnLoadData);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(225, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(575, 600);
            pnlMain.TabIndex = 2;
            // 
            // pnlHistogramme
            // 
            pnlHistogramme.Controls.Add(picHistogramme);
            pnlHistogramme.Controls.Add(cboPeriode);
            pnlHistogramme.Controls.Add(lblHistogrammeTitle);
            pnlHistogramme.Location = new Point(100, 500);
            pnlHistogramme.Name = "pnlHistogramme";
            pnlHistogramme.Size = new Size(600, 450);
            pnlHistogramme.TabIndex = 5;
            // 
            // picHistogramme
            // 
            picHistogramme.Location = new Point(10, 40);
            picHistogramme.Name = "picHistogramme";
            picHistogramme.Size = new Size(580, 400);
            picHistogramme.SizeMode = PictureBoxSizeMode.StretchImage;
            picHistogramme.TabIndex = 2;
            picHistogramme.TabStop = false;
            // 
            // cboPeriode
            // 
            cboPeriode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPeriode.Location = new Point(450, 10);
            cboPeriode.Name = "cboPeriode";
            cboPeriode.Size = new Size(140, 28);
            cboPeriode.TabIndex = 1;
            cboPeriode.SelectedIndexChanged += cboPeriode_SelectedIndexChanged;
            // 
            // lblHistogrammeTitle
            // 
            lblHistogrammeTitle.AutoSize = true;
            lblHistogrammeTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHistogrammeTitle.Location = new Point(10, 10);
            lblHistogrammeTitle.Name = "lblHistogrammeTitle";
            lblHistogrammeTitle.Size = new Size(77, 23);
            lblHistogrammeTitle.TabIndex = 0;
            lblHistogrammeTitle.Text = "Recettes";
            // 
            // pnlTopMedicaments
            // 
            pnlTopMedicaments.Controls.Add(dgvTopMedicaments);
            pnlTopMedicaments.Controls.Add(lblTopMedicamentsTitle);
            pnlTopMedicaments.Location = new Point(450, 20);
            pnlTopMedicaments.Name = "pnlTopMedicaments";
            pnlTopMedicaments.Size = new Size(300, 300);
            pnlTopMedicaments.TabIndex = 4;
            // 
            // dgvTopMedicaments
            // 
            dgvTopMedicaments.AllowUserToAddRows = false;
            dgvTopMedicaments.AllowUserToDeleteRows = false;
            dgvTopMedicaments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopMedicaments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTopMedicaments.Location = new Point(10, 50);
            dgvTopMedicaments.Name = "dgvTopMedicaments";
            dgvTopMedicaments.ReadOnly = true;
            dgvTopMedicaments.RowHeadersVisible = false;
            dgvTopMedicaments.RowHeadersWidth = 51;
            dgvTopMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopMedicaments.Size = new Size(280, 240);
            dgvTopMedicaments.TabIndex = 1;
            // 
            // lblTopMedicamentsTitle
            // 
            lblTopMedicamentsTitle.AutoSize = true;
            lblTopMedicamentsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTopMedicamentsTitle.Location = new Point(10, 10);
            lblTopMedicamentsTitle.Name = "lblTopMedicamentsTitle";
            lblTopMedicamentsTitle.Size = new Size(242, 23);
            lblTopMedicamentsTitle.TabIndex = 0;
            lblTopMedicamentsTitle.Text = "Médicaments les plus vendus";
            // 
            // pnlRecetteCard
            // 
            pnlRecetteCard.BackColor = Color.FromArgb(255, 228, 225);
            pnlRecetteCard.Controls.Add(lblRecetteTotale);
            pnlRecetteCard.Controls.Add(lblRecetteTitle);
            pnlRecetteCard.Location = new Point(200, 380);
            pnlRecetteCard.Name = "pnlRecetteCard";
            pnlRecetteCard.Size = new Size(200, 100);
            pnlRecetteCard.TabIndex = 3;
            // 
            // lblRecetteTotale
            // 
            lblRecetteTotale.AutoSize = true;
            lblRecetteTotale.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRecetteTotale.Location = new Point(50, 50);
            lblRecetteTotale.Name = "lblRecetteTotale";
            lblRecetteTotale.Size = new Size(70, 37);
            lblRecetteTotale.TabIndex = 1;
            lblRecetteTotale.Text = "0 Ar";
            // 
            // lblRecetteTitle
            // 
            lblRecetteTitle.AutoSize = true;
            lblRecetteTitle.Font = new Font("Segoe UI", 10F);
            lblRecetteTitle.Location = new Point(50, 20);
            lblRecetteTitle.Name = "lblRecetteTitle";
            lblRecetteTitle.Size = new Size(117, 23);
            lblRecetteTitle.TabIndex = 0;
            lblRecetteTitle.Text = "Recette Totale";
            // 
            // pnlAchatCard
            // 
            pnlAchatCard.BackColor = Color.FromArgb(255, 245, 238);
            pnlAchatCard.Controls.Add(lblTotalAchats);
            pnlAchatCard.Controls.Add(lblAchatTitle);
            pnlAchatCard.Location = new Point(200, 260);
            pnlAchatCard.Name = "pnlAchatCard";
            pnlAchatCard.Size = new Size(200, 100);
            pnlAchatCard.TabIndex = 2;
            // 
            // lblTotalAchats
            // 
            lblTotalAchats.AutoSize = true;
            lblTotalAchats.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotalAchats.Location = new Point(80, 50);
            lblTotalAchats.Name = "lblTotalAchats";
            lblTotalAchats.Size = new Size(33, 37);
            lblTotalAchats.TabIndex = 1;
            lblTotalAchats.Text = "0";
            // 
            // lblAchatTitle
            // 
            lblAchatTitle.AutoSize = true;
            lblAchatTitle.Font = new Font("Segoe UI", 10F);
            lblAchatTitle.Location = new Point(50, 20);
            lblAchatTitle.Name = "lblAchatTitle";
            lblAchatTitle.Size = new Size(102, 23);
            lblAchatTitle.TabIndex = 0;
            lblAchatTitle.Text = "Total Achats";
            // 
            // pnlEntreeCard
            // 
            pnlEntreeCard.BackColor = Color.FromArgb(245, 245, 220);
            pnlEntreeCard.Controls.Add(lblTotalEntrees);
            pnlEntreeCard.Controls.Add(lblEntreeTitle);
            pnlEntreeCard.Location = new Point(200, 140);
            pnlEntreeCard.Name = "pnlEntreeCard";
            pnlEntreeCard.Size = new Size(200, 100);
            pnlEntreeCard.TabIndex = 1;
            // 
            // lblTotalEntrees
            // 
            lblTotalEntrees.AutoSize = true;
            lblTotalEntrees.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotalEntrees.Location = new Point(80, 50);
            lblTotalEntrees.Name = "lblTotalEntrees";
            lblTotalEntrees.Size = new Size(33, 37);
            lblTotalEntrees.TabIndex = 1;
            lblTotalEntrees.Text = "0";
            // 
            // lblEntreeTitle
            // 
            lblEntreeTitle.AutoSize = true;
            lblEntreeTitle.Font = new Font("Segoe UI", 10F);
            lblEntreeTitle.Location = new Point(50, 20);
            lblEntreeTitle.Name = "lblEntreeTitle";
            lblEntreeTitle.Size = new Size(107, 23);
            lblEntreeTitle.TabIndex = 0;
            lblEntreeTitle.Text = "Total Entrées";
            // 
            // pnlMedicamentCard
            // 
            pnlMedicamentCard.BackColor = Color.FromArgb(240, 248, 255);
            pnlMedicamentCard.Controls.Add(lblTotalMedicaments);
            pnlMedicamentCard.Controls.Add(lblMedicamentTitle);
            pnlMedicamentCard.Location = new Point(200, 20);
            pnlMedicamentCard.Name = "pnlMedicamentCard";
            pnlMedicamentCard.Size = new Size(200, 100);
            pnlMedicamentCard.TabIndex = 0;
            // 
            // lblTotalMedicaments
            // 
            lblTotalMedicaments.AutoSize = true;
            lblTotalMedicaments.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotalMedicaments.Location = new Point(80, 50);
            lblTotalMedicaments.Name = "lblTotalMedicaments";
            lblTotalMedicaments.Size = new Size(33, 37);
            lblTotalMedicaments.TabIndex = 1;
            lblTotalMedicaments.Text = "0";
            // 
            // lblMedicamentTitle
            // 
            lblMedicamentTitle.AutoSize = true;
            lblMedicamentTitle.Font = new Font("Segoe UI", 10F);
            lblMedicamentTitle.Location = new Point(20, 20);
            lblMedicamentTitle.Name = "lblMedicamentTitle";
            lblMedicamentTitle.Size = new Size(153, 23);
            lblMedicamentTitle.TabIndex = 0;
            lblMedicamentTitle.Text = "Total Médicaments";
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(450, 330);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(150, 35);
            btnLoadData.TabIndex = 6;
            btnLoadData.Text = "Charger Données";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += btnLoadData_Click;
            // 
            // FormDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Name = "FormDashboard";
            Text = "Tableau de Bord";
            WindowState = FormWindowState.Maximized;
            Resize += FormDashboard_Resize;
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlHistogramme.ResumeLayout(false);
            pnlHistogramme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHistogramme).EndInit();
            pnlTopMedicaments.ResumeLayout(false);
            pnlTopMedicaments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopMedicaments).EndInit();
            pnlRecetteCard.ResumeLayout(false);
            pnlRecetteCard.PerformLayout();
            pnlAchatCard.ResumeLayout(false);
            pnlAchatCard.PerformLayout();
            pnlEntreeCard.ResumeLayout(false);
            pnlEntreeCard.PerformLayout();
            pnlMedicamentCard.ResumeLayout(false);
            pnlMedicamentCard.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Panel pnlSidebar;
        private Label lblUtilisateur;
        private Button btnGestionAchats;
        private Button btnGestionEntrees;
        private Button btnGestionPharmacie;
        private Label lblTitre;
        private Panel pnlMain;
        private Panel pnlHistogramme;
        private PictureBox picHistogramme;
        private ComboBox cboPeriode;
        private Label lblHistogrammeTitle;
        private Panel pnlTopMedicaments;
        private DataGridView dgvTopMedicaments;
        private Label lblTopMedicamentsTitle;
        private Panel pnlRecetteCard;
        private Label lblRecetteTotale;
        private Label lblRecetteTitle;
        private Panel pnlAchatCard;
        private Label lblTotalAchats;
        private Label lblAchatTitle;
        private Panel pnlEntreeCard;
        private Label lblTotalEntrees;
        private Label lblEntreeTitle;
        private Panel pnlMedicamentCard;
        private Label lblTotalMedicaments;
        private Label lblMedicamentTitle;
        private Button btnRechercherMedicaments;
        private Button btnVoirRuptures;
        private Button btnLoadData;
    }
}