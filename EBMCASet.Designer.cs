namespace EBMTest
{
    partial class EBMCASet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EBMCASet));
            this.lblCAName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbCAname = new System.Windows.Forms.ComboBox();
            this.chbCheckSignature = new System.Windows.Forms.CheckBox();
            this.panelInlayCA = new System.Windows.Forms.Panel();
            this.chbplatformsignatureNew = new System.Windows.Forms.RadioButton();
            this.chbCAsignature = new System.Windows.Forms.RadioButton();
            this.chbplatformsignature = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textPublicKey = new System.Windows.Forms.TextBox();
            this.chbUseSignature = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtCert_sn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelInlayCA.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCAName
            // 
            this.lblCAName.AutoSize = true;
            this.lblCAName.Location = new System.Drawing.Point(36, 46);
            this.lblCAName.Name = "lblCAName";
            this.lblCAName.Size = new System.Drawing.Size(41, 12);
            this.lblCAName.TabIndex = 72;
            this.lblCAName.Text = "CA名称";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(390, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 81;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbCAname
            // 
            this.cmbCAname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCAname.FormattingEnabled = true;
            this.cmbCAname.Location = new System.Drawing.Point(96, 43);
            this.cmbCAname.Name = "cmbCAname";
            this.cmbCAname.Size = new System.Drawing.Size(224, 20);
            this.cmbCAname.TabIndex = 82;
            this.cmbCAname.SelectedValueChanged += new System.EventHandler(this.cmbCAname_SelectedValueChanged);
            // 
            // chbCheckSignature
            // 
            this.chbCheckSignature.AutoSize = true;
            this.chbCheckSignature.Location = new System.Drawing.Point(96, 194);
            this.chbCheckSignature.Name = "chbCheckSignature";
            this.chbCheckSignature.Size = new System.Drawing.Size(96, 16);
            this.chbCheckSignature.TabIndex = 83;
            this.chbCheckSignature.Text = "测试错误签名";
            this.chbCheckSignature.UseVisualStyleBackColor = true;
            // 
            // panelInlayCA
            // 
            this.panelInlayCA.Controls.Add(this.chbplatformsignatureNew);
            this.panelInlayCA.Controls.Add(this.chbCAsignature);
            this.panelInlayCA.Controls.Add(this.chbplatformsignature);
            this.panelInlayCA.Location = new System.Drawing.Point(331, 37);
            this.panelInlayCA.Name = "panelInlayCA";
            this.panelInlayCA.Size = new System.Drawing.Size(249, 37);
            this.panelInlayCA.TabIndex = 86;
            this.panelInlayCA.Visible = false;
            // 
            // chbplatformsignatureNew
            // 
            this.chbplatformsignatureNew.AutoSize = true;
            this.chbplatformsignatureNew.Location = new System.Drawing.Point(180, 10);
            this.chbplatformsignatureNew.Name = "chbplatformsignatureNew";
            this.chbplatformsignatureNew.Size = new System.Drawing.Size(59, 16);
            this.chbplatformsignatureNew.TabIndex = 92;
            this.chbplatformsignatureNew.TabStop = true;
            this.chbplatformsignatureNew.Text = "新平台";
            this.chbplatformsignatureNew.UseVisualStyleBackColor = true;
            // 
            // chbCAsignature
            // 
            this.chbCAsignature.AutoSize = true;
            this.chbCAsignature.Location = new System.Drawing.Point(111, 10);
            this.chbCAsignature.Name = "chbCAsignature";
            this.chbCAsignature.Size = new System.Drawing.Size(59, 16);
            this.chbCAsignature.TabIndex = 91;
            this.chbCAsignature.TabStop = true;
            this.chbCAsignature.Text = "CA签名";
            this.chbCAsignature.UseVisualStyleBackColor = true;
            this.chbCAsignature.CheckedChanged += new System.EventHandler(this.chbCAsignature_CheckedChanged);
            // 
            // chbplatformsignature
            // 
            this.chbplatformsignature.AutoSize = true;
            this.chbplatformsignature.Location = new System.Drawing.Point(10, 10);
            this.chbplatformsignature.Name = "chbplatformsignature";
            this.chbplatformsignature.Size = new System.Drawing.Size(95, 16);
            this.chbplatformsignature.TabIndex = 90;
            this.chbplatformsignature.TabStop = true;
            this.chbplatformsignature.Text = "平台证书签名";
            this.chbplatformsignature.UseVisualStyleBackColor = true;
            this.chbplatformsignature.CheckedChanged += new System.EventHandler(this.chbplatformsignature_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 87;
            this.label1.Text = "公钥证书";
            // 
            // textPublicKey
            // 
            this.textPublicKey.Location = new System.Drawing.Point(96, 91);
            this.textPublicKey.Multiline = true;
            this.textPublicKey.Name = "textPublicKey";
            this.textPublicKey.ReadOnly = true;
            this.textPublicKey.Size = new System.Drawing.Size(484, 86);
            this.textPublicKey.TabIndex = 88;
            this.textPublicKey.Text = resources.GetString("textPublicKey.Text");
            // 
            // chbUseSignature
            // 
            this.chbUseSignature.AutoSize = true;
            this.chbUseSignature.Location = new System.Drawing.Point(241, 194);
            this.chbUseSignature.Name = "chbUseSignature";
            this.chbUseSignature.Size = new System.Drawing.Size(72, 16);
            this.chbUseSignature.TabIndex = 89;
            this.chbUseSignature.Text = "启用签名";
            this.chbUseSignature.UseVisualStyleBackColor = true;
            this.chbUseSignature.CheckedChanged += new System.EventHandler(this.chbUseSignature_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(96, 223);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 90;
            this.checkBox1.Text = "添加证书";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtCert_sn
            // 
            this.txtCert_sn.Location = new System.Drawing.Point(142, 250);
            this.txtCert_sn.Name = "txtCert_sn";
            this.txtCert_sn.Size = new System.Drawing.Size(151, 21);
            this.txtCert_sn.TabIndex = 91;
            this.txtCert_sn.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 92;
            this.label2.Text = "证书号";
            this.label2.Visible = false;
            // 
            // EBMCASet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 308);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCert_sn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chbUseSignature);
            this.Controls.Add(this.textPublicKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelInlayCA);
            this.Controls.Add(this.chbCheckSignature);
            this.Controls.Add(this.cmbCAname);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCAName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EBMCASet";
            this.ShowIcon = false;
            this.Text = "CA设置";
            this.panelInlayCA.ResumeLayout(false);
            this.panelInlayCA.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCAName;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.ComboBox cmbCAname;
        private System.Windows.Forms.CheckBox chbCheckSignature;
        private System.Windows.Forms.Panel panelInlayCA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPublicKey;
        private System.Windows.Forms.CheckBox chbUseSignature;
        private System.Windows.Forms.RadioButton chbCAsignature;
        private System.Windows.Forms.RadioButton chbplatformsignature;
        private System.Windows.Forms.RadioButton chbplatformsignatureNew;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtCert_sn;
        private System.Windows.Forms.Label label2;
    }
}