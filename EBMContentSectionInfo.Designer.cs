namespace EBMTest
{
    partial class EBMContentSectionInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EBMContentSectionInfo));
            this.txtSectionName = new System.Windows.Forms.TextBox();
            this.lblS_language_code = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSectionName
            // 
            this.txtSectionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSectionName.Location = new System.Drawing.Point(110, 35);
            this.txtSectionName.Name = "txtSectionName";
            this.txtSectionName.Size = new System.Drawing.Size(385, 21);
            this.txtSectionName.TabIndex = 3;
            this.txtSectionName.Tag = "";
            // 
            // lblS_language_code
            // 
            this.lblS_language_code.AutoSize = true;
            this.lblS_language_code.Location = new System.Drawing.Point(33, 39);
            this.lblS_language_code.Name = "lblS_language_code";
            this.lblS_language_code.Size = new System.Drawing.Size(71, 12);
            this.lblS_language_code.TabIndex = 53;
            this.lblS_language_code.Text = "Section名称";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(304, 76);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 55;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // EBMContentSectionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 137);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSectionName);
            this.Controls.Add(this.lblS_language_code);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMContentSectionInfo";
            this.ShowIcon = false;
            this.Text = "Section信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSectionName;
        private System.Windows.Forms.Label lblS_language_code;
        private System.Windows.Forms.Button btnOK;
    }
}