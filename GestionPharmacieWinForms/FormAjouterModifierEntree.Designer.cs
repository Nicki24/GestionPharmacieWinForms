namespace GestionPharmacieWinForms
{
    partial class FormAjouterModifierEntree
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
            cmbNumMedoc = new ComboBox();
            lblNumEntree = new Label();
            txtNumEntree = new TextBox();
            lblStockEntree = new Label();
            txtStockEntree = new TextBox();
            lblDateEntree = new Label();
            txtDateEntree = new TextBox();
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
            lblNumMedoc.Size = new Size(92, 20);
            lblNumMedoc.TabIndex = 0;
            lblNumMedoc.Text = "Médicament";
            // 
            // cmbNumMedoc
            // 
            cmbNumMedoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNumMedoc.FormattingEnabled = true;
            cmbNumMedoc.Location = new Point(259, 5);
            cmbNumMedoc.Name = "cmbNumMedoc";
            cmbNumMedoc.Size = new Size(200, 28);
            cmbNumMedoc.TabIndex = 1;
            cmbNumMedoc.FlatStyle = FlatStyle.Flat; // Style cohérent
            // 
            // lblNumEntree
            // 
            lblNumEntree.AutoSize = true;
            lblNumEntree.Location = new Point(0, 38);
            lblNumEntree.Name = "lblNumEntree";
            lblNumEntree.Size = new Size(109, 20);
            lblNumEntree.TabIndex = 2;
            lblNumEntree.Text = "Numéro Entrée";
            // 
            // txtNumEntree
            // 
            txtNumEntree.Location = new Point(259, 35);
            txtNumEntree.Name = "txtNumEntree";
            txtNumEntree.Size = new Size(200, 27);
            txtNumEntree.TabIndex = 3;
            txtNumEntree.BorderStyle = BorderStyle.FixedSingle;
            txtNumEntree.ForeColor = System.Drawing.Color.Black;
            // 
            // lblStockEntree
            // 
            lblStockEntree.AutoSize = true;
            lblStockEntree.Location = new Point(0, 68);
            lblStockEntree.Name = "lblStockEntree";
            lblStockEntree.Size = new Size(112, 20);
            lblStockEntree.TabIndex = 4;
            lblStockEntree.Text = "Quantité Entrée";
            // 
            // txtStockEntree
            // 
            txtStockEntree.Location = new Point(259, 65);
            txtStockEntree.Name = "txtStockEntree";
            txtStockEntree.Size = new Size(200, 27);
            txtStockEntree.TabIndex = 5;
            txtStockEntree.BorderStyle = BorderStyle.FixedSingle;
            txtStockEntree.ForeColor = System.Drawing.Color.Black;
            // 
            // lblDateEntree
            // 
            lblDateEntree.AutoSize = true;
            lblDateEntree.Location = new Point(0, 98);
            lblDateEntree.Name = "lblDateEntree";
            lblDateEntree.Size = new Size(193, 20);
            lblDateEntree.TabIndex = 6;
            lblDateEntree.Text = "Date Entrée (YYYY-MM-DD)";
            // 
            // txtDateEntree
            // 
            txtDateEntree.Location = new Point(259, 95);
            txtDateEntree.Name = "txtDateEntree";
            txtDateEntree.Size = new Size(200, 27);
            txtDateEntree.TabIndex = 7;
            txtDateEntree.BorderStyle = BorderStyle.FixedSingle;
            txtDateEntree.ForeColor = System.Drawing.Color.Black;
            // 
            // btnEnregistrer
            // 
            btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnEnregistrer.FlatStyle = FlatStyle.Flat;
            btnEnregistrer.ForeColor = System.Drawing.Color.White;
            btnEnregistrer.Location = new Point(138, 128);
            btnEnregistrer.Name = "btnEnregistrer";
            btnEnregistrer.Size = new Size(120, 40);
            btnEnregistrer.TabIndex = 8;
            btnEnregistrer.Text = "✔️ Enregistrer";
            btnEnregistrer.UseVisualStyleBackColor = false;
            btnEnregistrer.Click += btnEnregistrer_Click;
            // 
            // btnAnnuler
            // 
            btnAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnAnnuler.FlatStyle = FlatStyle.Flat;
            btnAnnuler.ForeColor = System.Drawing.Color.White;
            btnAnnuler.Location = new Point(264, 128);
            btnAnnuler.Name = "btnAnnuler";
            btnAnnuler.Size = new Size(120, 40);
            btnAnnuler.TabIndex = 9;
            btnAnnuler.Text = "❌ Annuler";
            btnAnnuler.UseVisualStyleBackColor = false;
            btnAnnuler.Click += btnAnnuler_Click;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(btnAnnuler);
            pnlForm.Controls.Add(btnEnregistrer);
            pnlForm.Controls.Add(txtDateEntree);
            pnlForm.Controls.Add(lblDateEntree);
            pnlForm.Controls.Add(txtStockEntree);
            pnlForm.Controls.Add(lblStockEntree);
            pnlForm.Controls.Add(txtNumEntree);
            pnlForm.Controls.Add(lblNumEntree);
            pnlForm.Controls.Add(cmbNumMedoc);
            pnlForm.Controls.Add(lblNumMedoc);
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(531, 186);
            pnlForm.TabIndex = 10;
            // 
            // FormAjouterModifierEntree
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            ClientSize = new Size(555, 250);
            Controls.Add(pnlForm);
            MinimizeBox = false;
            Name = "FormAjouterModifierEntree";
            Text = "Ajouter/Modifier une Entrée";
            Resize += FormAjouterModifierEntree_Resize;
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private Label lblNumMedoc;
        private ComboBox cmbNumMedoc;
        private Label lblNumEntree;
        private TextBox txtNumEntree;
        private Label lblStockEntree;
        private TextBox txtStockEntree;
        private Label lblDateEntree;
        private TextBox txtDateEntree;
        private Button btnEnregistrer;
        private Button btnAnnuler;
        private Panel pnlForm;
    }
}