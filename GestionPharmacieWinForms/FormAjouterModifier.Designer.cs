namespace GestionPharmacieWinForms
{
    partial class FormAjouterModifier
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
            lblNumMedoc = new Label();
            txtNumMedoc = new TextBox();
            lblDesignation = new Label();
            txtDesignation = new TextBox();
            lblPrixUnitaire = new Label();
            txtPrixUnitaire = new TextBox();
            lblStock = new Label();
            txtStock = new TextBox();
            lblDatePeremption = new Label();
            txtDatePeremption = new TextBox();
            btnEnregistrer = new Button();
            btnAnnuler = new Button();
            pnlForm = new Panel();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // lblNumMedoc
            // 
            lblNumMedoc.AutoSize = true;
            lblNumMedoc.Location = new Point(0, 8);
            lblNumMedoc.Name = "lblNumMedoc";
            lblNumMedoc.Size = new Size(150, 20);
            lblNumMedoc.TabIndex = 0;
            lblNumMedoc.Text = "Numéro Médicament";
            // 
            // txtNumMedoc
            // 
            txtNumMedoc.BorderStyle = BorderStyle.FixedSingle;
            txtNumMedoc.ForeColor = System.Drawing.Color.Black;
            txtNumMedoc.Location = new Point(238, 3);
            txtNumMedoc.Name = "txtNumMedoc";
            txtNumMedoc.Size = new Size(200, 27);
            txtNumMedoc.TabIndex = 1;
            txtNumMedoc.TextChanged += txtNumMedoc_TextChanged;
            // 
            // lblDesignation
            // 
            lblDesignation.AutoSize = true;
            lblDesignation.Location = new Point(0, 38);
            lblDesignation.Name = "lblDesignation";
            lblDesignation.Size = new Size(89, 20);
            lblDesignation.TabIndex = 2;
            lblDesignation.Text = "Désignation";
            // 
            // txtDesignation
            // 
            txtDesignation.BorderStyle = BorderStyle.FixedSingle;
            txtDesignation.ForeColor = System.Drawing.Color.Black;
            txtDesignation.Location = new Point(238, 31);
            txtDesignation.Name = "txtDesignation";
            txtDesignation.Size = new Size(200, 27);
            txtDesignation.TabIndex = 3;
            // 
            // lblPrixUnitaire
            // 
            lblPrixUnitaire.AutoSize = true;
            lblPrixUnitaire.Location = new Point(0, 68);
            lblPrixUnitaire.Name = "lblPrixUnitaire";
            lblPrixUnitaire.Size = new Size(118, 20);
            lblPrixUnitaire.TabIndex = 4;
            lblPrixUnitaire.Text = "Prix Unitaire (Ar)";
            // 
            // txtPrixUnitaire
            // 
            txtPrixUnitaire.BorderStyle = BorderStyle.FixedSingle;
            txtPrixUnitaire.ForeColor = System.Drawing.Color.Black;
            txtPrixUnitaire.Location = new Point(238, 61);
            txtPrixUnitaire.Name = "txtPrixUnitaire";
            txtPrixUnitaire.Size = new Size(200, 27);
            txtPrixUnitaire.TabIndex = 5;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(0, 98);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(45, 20);
            lblStock.TabIndex = 6;
            lblStock.Text = "Stock";
            // 
            // txtStock
            // 
            txtStock.BorderStyle = BorderStyle.FixedSingle;
            txtStock.ForeColor = System.Drawing.Color.Black;
            txtStock.Location = new Point(238, 91);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(200, 27);
            txtStock.TabIndex = 7;
            // 
            // lblDatePeremption
            // 
            lblDatePeremption.AutoSize = true;
            lblDatePeremption.Location = new Point(0, 128);
            lblDatePeremption.Name = "lblDatePeremption";
            lblDatePeremption.Size = new Size(227, 20);
            lblDatePeremption.TabIndex = 8;
            lblDatePeremption.Text = "Date Péremption (YYYY-MM-DD)";
            // 
            // txtDatePeremption
            // 
            txtDatePeremption.BorderStyle = BorderStyle.FixedSingle;
            txtDatePeremption.ForeColor = System.Drawing.Color.Black;
            txtDatePeremption.Location = new Point(238, 121);
            txtDatePeremption.Name = "txtDatePeremption";
            txtDatePeremption.Size = new Size(200, 27);
            txtDatePeremption.TabIndex = 9;
            // 
            // btnEnregistrer
            // 
            btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnEnregistrer.FlatStyle = FlatStyle.Flat;
            btnEnregistrer.ForeColor = System.Drawing.Color.White;
            btnEnregistrer.Location = new Point(138, 158);
            btnEnregistrer.Name = "btnEnregistrer";
            btnEnregistrer.Size = new Size(120, 40);
            btnEnregistrer.TabIndex = 10;
            btnEnregistrer.Text = "✔️ Enregistrer";
            btnEnregistrer.UseVisualStyleBackColor = false;
            btnEnregistrer.Click += btnEnregistrer_Click;
            // 
            // btnAnnuler
            // 
            btnAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnAnnuler.FlatStyle = FlatStyle.Flat;
            btnAnnuler.ForeColor = System.Drawing.Color.White;
            btnAnnuler.Location = new Point(262, 158);
            btnAnnuler.Name = "btnAnnuler";
            btnAnnuler.Size = new Size(120, 40);
            btnAnnuler.TabIndex = 11;
            btnAnnuler.Text = "❌ Annuler";
            btnAnnuler.UseVisualStyleBackColor = false;
            btnAnnuler.Click += btnAnnuler_Click;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(btnAnnuler);
            pnlForm.Controls.Add(btnEnregistrer);
            pnlForm.Controls.Add(txtDatePeremption);
            pnlForm.Controls.Add(lblDatePeremption);
            pnlForm.Controls.Add(txtStock);
            pnlForm.Controls.Add(lblStock);
            pnlForm.Controls.Add(txtPrixUnitaire);
            pnlForm.Controls.Add(lblPrixUnitaire);
            pnlForm.Controls.Add(txtDesignation);
            pnlForm.Controls.Add(lblDesignation);
            pnlForm.Controls.Add(txtNumMedoc);
            pnlForm.Controls.Add(lblNumMedoc);
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(452, 226);
            pnlForm.TabIndex = 12;
            // 
            // FormAjouterModifier
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            ClientSize = new Size(474, 250);
            Controls.Add(pnlForm);
            MinimizeBox = false;
            Name = "FormAjouterModifier";
            Text = "Ajouter/Modifier un Médicament";
            WindowState = FormWindowState.Maximized;
            Resize += FormAjouterModifier_Resize;
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Label lblNumMedoc;
        private TextBox txtNumMedoc;
        private Label lblDesignation;
        private TextBox txtDesignation;
        private Label lblPrixUnitaire;
        private TextBox txtPrixUnitaire;
        private Label lblStock;
        private TextBox txtStock;
        private Label lblDatePeremption;
        private TextBox txtDatePeremption;
        private Button btnEnregistrer;
        private Button btnAnnuler;
        private Panel pnlForm;
    }
}