using ControlAstro.Utils;
using EBMTable;
using EBMTest.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMContent : Form
    {
        private BindingCollection<EBContent_AllData> EBContent_List_AllData;
        private BindingCollection<EBContent_AllData> SelectEBContent_List;

        public EBMContent()
        {
            InitializeComponent();
            EBContent_List_AllData = new BindingCollection<EBContent_AllData>();
            SelectEBContent_List = new BindingCollection<EBContent_AllData>();
            Load += EBMContent_Load;
            dgvEBContent.AutoGenerateColumns = false;
         
        }

        private void EBMContent_Load(object sender, EventArgs e)
        {
            InitEbmId();
            InitContent();
            showdata();
        }

        private void InitContent()
        {
            try
            {
                var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Content);
                if (jo != null)
                {
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    BindingCollection<EBContentTMP> EBContent_ListTMP = Serializer.Deserialize<BindingCollection<EBContentTMP>>(jo["0"].ToString());
                    BindingCollection<EBContent> EBContent_ListTMP2 = JsonConvert.DeserializeObject<BindingCollection<EBContent>>(jo["1"].ToString());
                    if (EBContent_ListTMP != null)
                    {
                        foreach (EBContentTMP item in EBContent_ListTMP)
                        {
                            EBContent_AllData tmp = new EBContent_AllData();
                            tmp.Guid = item.Guid;
                            tmp.SectionName = item.SectionName;
                            tmp.SendState = item.SendState;
                            tmp.EBM_ID = item.EBM_ID;
                            tmp.EBContentList = GetRelateEBContentlist(tmp.Guid, tmp.EBM_ID, EBContent_ListTMP2);
                            EBContent_List_AllData.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private BindingCollection<EBContent> GetRelateEBContentlist(string guid, string ebmid, BindingCollection<EBContent> list)
        {
            BindingCollection<EBContent> tmp = new BindingCollection<EBContent>();
            foreach (EBContent item in list)
            {
                if (item.Guid == guid && item.EBM_ID == ebmid)
                {
                    tmp.Add(item);
                }
            }
            return tmp;
        }
        private void InitEbmId()
        {
            List<string> ebmId = new List<string>();

            var ebmIndex = (MdiParent as EBMMain).EBMIndex;
            if (ebmIndex != null)
            {
                ebmId = ebmIndex.GetEbmId();
            }
            else
            {
                BindingCollection<EBMIndex.EBIndexEx> index = new BindingCollection<EBMIndex.EBIndexEx>();
                ebmId = new List<string>();
                var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);
                if (jo != null)
                {
                    index = JsonConvert.DeserializeObject<BindingCollection<EBMIndex.EBIndexEx>>(jo["0"].ToString());
                  
                }
                foreach (var dex in index)
                {
                    ebmId.Add(dex.S_EBM_id);
                }
            }
            cbBoxEBMId.DataSource = ebmId;
        }




        public bool GetlistContentTable(ref  List<EBContentTable> oldTableList)
        {
            try
            {
                oldTableList.Clear();
                foreach (EBContent_AllData item in EBContent_List_AllData)
                {
                    EBContentTable pp = new EBContentTable();
                    pp.ProtocolGX = SingletonInfo.GetInstance().IsProtocolGX;
                    List<MultilangualContent> listMulti = GetSendMultilangualContentNew(item.EBContentList);
                    if (listMulti == null || listMulti.Count == 0)
                    {
                       // return false;
                        continue;
                    }
                    pp.list_multilangual_content = listMulti;
                    pp.S_EBM_id = item.EBM_ID;
                    pp.Repeat_times = pnlRepeatTimes.GetRepeatTimes();
                    pp.Table_id = 0xfe;
                    pp.Table_id_extension = 0;
                   
                    oldTableList.Add(pp);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return false;
            }
        }

        private List<MultilangualContent> GetSendMultilangualContentNew(BindingCollection<EBContent> EBContent_List)
        {
            if (EBContent_List.Count == 0)
            {
                return null;
            }
            List<MultilangualContent> listMulti = new List<MultilangualContent>();
            foreach (var content in EBContent_List)
            {
                var contentex = DeepCopy(content.MultilangualContent);
                contentex.list_auxiliary_data = content.GetAuxiliaryData();
                listMulti.Add(contentex);
            }
            return listMulti;
        }

        private T DeepCopy<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj == null || obj is string || obj.GetType().IsValueType || obj.GetType().IsArray) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopy(field.GetValue(obj)));
                }
                catch { }
            }
            return (T)retval;
        }

        #region  右键事件
        private void MenuItemAdd_Click(object sender, System.EventArgs e)
        {
            AddSection();
        }

        private void MenuItemInfo_Click(object sender, System.EventArgs e)
        {
            InfoSection();
        }

        private void MenuItemUpdate_Click(object sender, System.EventArgs e)
        {
            UpdateSection();
        }

        private void MenuItemDel_Click(object sender, System.EventArgs e)
        {
            DelSection();
        }
        #endregion

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

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            AddSection();
        }
        private void DelSection()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                EBContent_AllData TobeDel = SelectEBContent_List[dgvEBContent.SelectedRows[0].Index];
                SelectEBContent_List.RemoveAt(dgvEBContent.SelectedRows[0].Index);
               string guid=  TobeDel.Guid;


               EBContent_AllData TobeDelNew = new EBContent_AllData();
               foreach (var item in EBContent_List_AllData)
               {
                   if (item.Guid == guid)
                   {
                       TobeDelNew = item;
                   }
               }
               if (TobeDelNew!=null)
                {
                    EBContent_List_AllData.Remove(TobeDelNew);
                }
               
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        
        }
        /// <summary>
        /// 增加Section 
        /// </summary>
        private void AddSection()
        {
            try
            {
                if (cbBoxEBMId.SelectedValue != null)
                {
                    if (dgvEBContent.Rows.Count == 0)
                    {
                        string EBM_ID = cbBoxEBMId.SelectedValue.ToString();
                        EBMContentSectionInfo form = new EBMContentSectionInfo(Enums.OperateType.Add, EBM_ID);
                        DialogResult result = form.ShowDialog();
                        if (result == DialogResult.OK && form.ContentAllData != null)
                        {
                            SelectEBContent_List.Add(form.ContentAllData);
                            EBContent_List_AllData.Add(form.ContentAllData);//同步到全局数据
                        }
                        form.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("已添加过一条数据，不能再添加！");
                    
                    }

                    
                }
                else
                {
                    MessageBox.Show("未发现可用EBM_ID");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

      
        /// <summary>
        /// 查看Section信息
        /// </summary>
        private void InfoSection()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                string EBM_ID = cbBoxEBMId.SelectedValue.ToString();
                new EBMContentSectionInfo(Enums.OperateType.Info, EBM_ID, SelectEBContent_List[dgvEBContent.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

    

        /// <summary>
        /// 更新Section
        /// </summary>
        private void UpdateSection()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                string EBM_ID = cbBoxEBMId.SelectedValue.ToString();
                EBMContentSectionInfo form = new EBMContentSectionInfo(Enums.OperateType.Update, EBM_ID, SelectEBContent_List[dgvEBContent.SelectedRows[0].Index]);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK && form.ContentAllData != null)
                {
                    SelectEBContent_List[dgvEBContent.SelectedRows[0].Index] = form.ContentAllData;

                    string guid = SelectEBContent_List[dgvEBContent.SelectedRows[0].Index].Guid;
                    EBContent_AllData TobeDel = new EBContent_AllData();
                    foreach (var item in EBContent_List_AllData)
                    {
                        if (item.Guid == guid)
                        {
                            TobeDel = item;
                        
                        }
                    }
                    if (TobeDel!=null)
                    {
                        EBContent_List_AllData.Remove(TobeDel);
                        EBContent_List_AllData.Add(SelectEBContent_List[dgvEBContent.SelectedRows[0].Index]);
                    }

                }
                form.Dispose();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void dgvEBContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == ColumnSendState.Index)
                    {
                        SelectEBContent_List[e.RowIndex].SendState = !SelectEBContent_List[e.RowIndex].SendState;
                        (MdiParent as EBMMain).InitStreamTable();
                    }
                    if (e.ColumnIndex == Detail_data.Index)
                    {
                        EBMContentDe detail = new EBMContentDe(SelectEBContent_List[e.RowIndex]);
                        DialogResult result = detail.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            SelectEBContent_List[e.RowIndex].EBContentList = detail.EBContent_List;

                            #region 数据更新模块
                            string guid = SelectEBContent_List[dgvEBContent.SelectedRows[0].Index].Guid;
                            EBContent_AllData TobeDel = new EBContent_AllData();
                            foreach (var item in EBContent_List_AllData)
                            {
                                if (item.Guid == guid)
                                {
                                    TobeDel = item;

                                }
                            }
                            if (TobeDel != null)
                            {
                                EBContent_List_AllData.Remove(TobeDel);
                                EBContent_List_AllData.Add(SelectEBContent_List[dgvEBContent.SelectedRows[0].Index]);
                            }
                            #endregion
                        }
                        detail.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
            }
            
        }

       

        /// <summary>
        /// 具有相同EBM_ID的
        /// </summary>
        public class EBContent_AllData
        {
            public bool SendState { get; set; }

            public string EBM_ID { get; set; }
            public string SectionName { get; set; }
            public string Guid { get; set; }
            public BindingCollection<EBContent> EBContentList { get; set; }
        }

        public class EBContentTMP
        {
            public bool SendState { get; set; }

            public string EBM_ID { get; set; }
            public string SectionName { get; set; }
            public string Guid { get; set; }
        }

        public class EBContent
        {
            public string Guid { get; set; }
            public string EBM_ID { get; set; }
            public string SectionName { get; set; }

            public MultilangualContent MultilangualContent { get; set; }
            public byte B_code_character_set
            {
                get { return MultilangualContent.B_code_character_set; }
                set { MultilangualContent.B_code_character_set = value; }
            }
            public byte[] B_message_text
            {
                get { return MultilangualContent.B_message_text; }
                set { MultilangualContent.B_message_text = value; }
            }
            public string S_language_code
            {
                get { return MultilangualContent.S_language_code; }
                set { MultilangualContent.S_language_code = value; }
            }
            public string MessageText { get; set; }

            public List<Auxiliary> list_auxiliary_data { get; set; }

            public List<AuxiliaryData> GetAuxiliaryData()
            {
                List<AuxiliaryData> data = new List<AuxiliaryData>();
                if (list_auxiliary_data == null) return data;
                for (int i = 0; i < list_auxiliary_data.Count; i++)
                {
                    data.Add(list_auxiliary_data[i].GetAuxData());
                }
                return data;
            }
        }

        public class Auxiliary
        {
            public AuxiliaryData GetAuxData()
            {
                AuxiliaryData data = new AuxiliaryData();
                data.B_auxiliary_data_type = Type;
                data.B_auxiliary_data = TableData.TableDataHelper.GetFileData(DisplayData);
                return data;
            }
            public byte Type { get; set; }
            public string DisplayData { get; set; }
            private string typeString;
            public string TypeString
            {
                get { return ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ContentAuxiliaryData, Type); }
                set { typeString = value; }
            }
        }

        private void EBMContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.Content, EBContent_List_AllData);
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void dgvEBContent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }

        public void UpdateEbmIndex(List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                cbBoxEBMId.DataSource = list;
            }
        }

        /// <summary>
        /// EBM_ID值发生变化时，对应的文本和辅助数据变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBoxEBMId_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                showdata();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void showdata()
        {
            if (cbBoxEBMId.SelectedValue != null)
            {
                SelectEBContent_List.Clear();
                string ebmid = cbBoxEBMId.SelectedValue.ToString();
                foreach (EBContent_AllData item in EBContent_List_AllData)
                {
                    if (item.EBM_ID == ebmid)
                    {
                        SelectEBContent_List.Add(item);
                    }
                }

                dgvEBContent.DataSource = SelectEBContent_List;
            }
            else
            {
                MessageBox.Show("未发现可用EBM_ID");

            }
        }
    }
}
