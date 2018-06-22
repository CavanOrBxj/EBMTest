using ControlAstro.Utils;
using EBMTable;
using EBMTest.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMContentDe : Form
    {
        public BindingCollection<EBMTest.EBMContent.EBContent> EBContent_List;
       
        private string EBM_id = "";
        private string SectionName = "";
        private string GUID = "";

        public EBMContentDe(EBMTest.EBMContent.EBContent_AllData ebcontent)
        {
            InitializeComponent();
            this.Load += EBMContentDe_Load;
            dgvEBContent.AutoGenerateColumns = false;
            ComboBoxHelper.InitCodeCharacter(ColumnB_code_character_set);

            EBContent_List = new BindingCollection<EBMContent.EBContent>();

            if (ebcontent.EBContentList != null)
            {
                EBContent_List = ebcontent.EBContentList;
            }
            EBM_id = ebcontent.EBM_ID;
            SectionName = ebcontent.SectionName;
            GUID = ebcontent.Guid;
            dgvEBContent.DataSource = EBContent_List;
           
        }

        void EBMContentDe_Load(object sender, System.EventArgs e)
        {
         
        }

        private void dgvEBContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                   
                    if (e.ColumnIndex == ColumnB_auxiliary_data.Index)
                    {
                        EBMContentDetail detail = new EBMContentDetail(EBContent_List[e.RowIndex].list_auxiliary_data);
                        DialogResult result = detail.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            EBContent_List[e.RowIndex].list_auxiliary_data = detail.GetData();
                        }
                        detail.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 增加内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                  AddIndex();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 查看内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemInfo_Click(object sender, EventArgs e)
        {
            try
            {
                InfoIndex();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 编辑内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateIndex();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDel_Click(object sender, EventArgs e)
        {
            try
            {
                DelContent();
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void DelContent()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                EBContent_List.RemoveAt(dgvEBContent.SelectedRows[0].Index);
                dgvEBContent.DataSource = null;
                dgvEBContent.DataSource = EBContent_List;
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void AddIndex()
        {
            EBMContentInfo form = new EBMContentInfo(Enums.OperateType.Add);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.Content != null)
            {
                form.Content.EBM_ID =EBM_id;
                form.Content.Guid =GUID;
                form.Content.SectionName = SectionName;
                EBContent_List.Add(form.Content);
            }
            form.Dispose();

            dgvEBContent.DataSource = null;
            dgvEBContent.DataSource = EBContent_List;
           
        }

      

        private void InfoIndex()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                new EBMContentInfo(Enums.OperateType.Info, EBContent_List[dgvEBContent.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void UpdateIndex()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                EBMContentInfo form = new EBMContentInfo(Enums.OperateType.Update, EBContent_List[dgvEBContent.SelectedRows[0].Index]);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK && form.Content != null)
                {
                    EBContent_List[dgvEBContent.SelectedRows[0].Index] = form.Content;
                }
                form.Dispose();
                dgvEBContent.DataSource = null;
                dgvEBContent.DataSource = EBContent_List;
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void MenuStripEBContent_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuItemInfo.Enabled = dgvEBContent.RowCount > 0;
            MenuItemDel.Enabled = dgvEBContent.RowCount > 0;
            MenuItemUpdate.Enabled = dgvEBContent.RowCount > 0;
            Point downPoint = dgvEBContent.PointToClient(MousePosition);
            var hit = dgvEBContent.HitTest(downPoint.X, downPoint.Y);
            if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                dgvEBContent.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //    switch (type)
                //{
                //    case OperateType.Add:
                //        if (!ValidatData()) return;
                //        Content = GetEBContent();
                //        break;
                //    case OperateType.Delet:
                //        break;
                //    case OperateType.Update:
                //        Content = GetEBContent();
                //        break;
                //    case OperateType.Info:
                //        break;
                //}
            DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
