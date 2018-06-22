namespace EBMTest
{
    partial class EBMContentDe
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EBMContentDe));
            this.dgvEBContent = new System.Windows.Forms.DataGridView();
            this.ColumnB_EBM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB_code_character_set = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnB_message_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnS_language_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB_auxiliary_data = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MenuStripEBContent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBContent)).BeginInit();
            this.MenuStripEBContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEBContent
            // 
            this.dgvEBContent.AllowUserToAddRows = false;
            this.dgvEBContent.AllowUserToDeleteRows = false;
            this.dgvEBContent.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEBContent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEBContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEBContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEBContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnB_EBM_ID,
            this.ColumnB_code_character_set,
            this.ColumnB_message_text,
            this.ColumnS_language_code,
            this.ColumnB_auxiliary_data});
            this.dgvEBContent.ContextMenuStrip = this.MenuStripEBContent;
            this.dgvEBContent.Location = new System.Drawing.Point(0, 0);
            this.dgvEBContent.MultiSelect = false;
            this.dgvEBContent.Name = "dgvEBContent";
            this.dgvEBContent.RowHeadersVisible = false;
            this.dgvEBContent.RowTemplate.Height = 23;
            this.dgvEBContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEBContent.Size = new System.Drawing.Size(623, 303);
            this.dgvEBContent.TabIndex = 5;
            this.dgvEBContent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEBContent_CellContentClick);
            // 
            // ColumnB_EBM_ID
            // 
            this.ColumnB_EBM_ID.DataPropertyName = "EBM_ID";
            this.ColumnB_EBM_ID.HeaderText = "EBM_ID";
            this.ColumnB_EBM_ID.Name = "ColumnB_EBM_ID";
            this.ColumnB_EBM_ID.Width = 200;
            // 
            // ColumnB_code_character_set
            // 
            this.ColumnB_code_character_set.DataPropertyName = "B_code_character_set";
            this.ColumnB_code_character_set.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColumnB_code_character_set.HeaderText = "编码字符集";
            this.ColumnB_code_character_set.Name = "ColumnB_code_character_set";
            // 
            // ColumnB_message_text
            // 
            this.ColumnB_message_text.DataPropertyName = "MessageText";
            this.ColumnB_message_text.HeaderText = "文本内容";
            this.ColumnB_message_text.Name = "ColumnB_message_text";
            // 
            // ColumnS_language_code
            // 
            this.ColumnS_language_code.DataPropertyName = "S_language_code";
            this.ColumnS_language_code.HeaderText = "语种代码";
            this.ColumnS_language_code.Name = "ColumnS_language_code";
            // 
            // ColumnB_auxiliary_data
            // 
            this.ColumnB_auxiliary_data.HeaderText = "辅助数据类型";
            this.ColumnB_auxiliary_data.Name = "ColumnB_auxiliary_data";
            this.ColumnB_auxiliary_data.Text = "编辑";
            this.ColumnB_auxiliary_data.UseColumnTextForButtonValue = true;
            // 
            // MenuStripEBContent
            // 
            this.MenuStripEBContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAdd,
            this.MenuItemInfo,
            this.MenuItemUpdate,
            this.MenuItemDel});
            this.MenuStripEBContent.Name = "MenuStripEBIndex";
            this.MenuStripEBContent.Size = new System.Drawing.Size(125, 92);
            this.MenuStripEBContent.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStripEBContent_Opening);
            // 
            // MenuItemAdd
            // 
            this.MenuItemAdd.Name = "MenuItemAdd";
            this.MenuItemAdd.Size = new System.Drawing.Size(124, 22);
            this.MenuItemAdd.Text = "添加内容";
            this.MenuItemAdd.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(124, 22);
            this.MenuItemInfo.Text = "查看内容";
            this.MenuItemInfo.Visible = false;
            this.MenuItemInfo.Click += new System.EventHandler(this.MenuItemInfo_Click);
            // 
            // MenuItemUpdate
            // 
            this.MenuItemUpdate.Name = "MenuItemUpdate";
            this.MenuItemUpdate.Size = new System.Drawing.Size(124, 22);
            this.MenuItemUpdate.Text = "编辑内容";
            this.MenuItemUpdate.Click += new System.EventHandler(this.MenuItemUpdate_Click);
            // 
            // MenuItemDel
            // 
            this.MenuItemDel.Name = "MenuItemDel";
            this.MenuItemDel.Size = new System.Drawing.Size(124, 22);
            this.MenuItemDel.Text = "删除内容";
            this.MenuItemDel.Click += new System.EventHandler(this.MenuItemDel_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "EBM_ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "EBM_ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "MessageText";
            this.dataGridViewTextBoxColumn2.HeaderText = "文本内容";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "S_language_code";
            this.dataGridViewTextBoxColumn3.HeaderText = "语种代码";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(410, 324);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // EBMContentDe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 370);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvEBContent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMContentDe";
            this.Text = "详情信息";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBContent)).EndInit();
            this.MenuStripEBContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEBContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB_EBM_ID;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnB_code_character_set;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB_message_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnS_language_code;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnB_auxiliary_data;
        private System.Windows.Forms.ContextMenuStrip MenuStripEBContent;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDel;
        private System.Windows.Forms.Button btnOK;

    }
}