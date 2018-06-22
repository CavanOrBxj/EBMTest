using EBMTable;
using EBMTest.Enums;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace EBMTest
{
    public partial class EBMContentSectionInfo : Form
    {
        private OperateType type;
        public EBMTest.EBMContent.EBContent_AllData ContentAllData { get; private set; }
        private string EBM_ID = "";


        public EBMContentSectionInfo(OperateType type,string EBM_id, EBMTest.EBMContent.EBContent_AllData content=null)
        {
            InitializeComponent();
            EBM_ID = EBM_id;
            if(type!= OperateType.Add)
            {
                if (content!=null)
                {
                    InitData(content);
                }
               
            }
            switch (type)
            {
                case OperateType.Add:
                    Text = "添加应急广播内容";
                    break;
                case OperateType.Info:
                    Text = "查看应急广播内容";
                    break;
                case OperateType.Update:
                    Text = "更新应急广播内容";
                    break;
            }
        }
        private void InitData(EBMTest.EBMContent.EBContent_AllData content)
        {
            txtSectionName.Text = content.SectionName;
            ContentAllData = content;
        }
   

        private void btnOK_Click(object sender, EventArgs e)
        {
                switch (type)
                {
                    case OperateType.Add:
                        if (!ValidatData()) return;
                        ContentAllData = GetEBContentAllData();
                        break;
                    case OperateType.Delet:
                        break;
                    case OperateType.Update:
                        ContentAllData = GetEBContentAllData();
                        break;
                    case OperateType.Info:
                        break;
                }
            DialogResult = DialogResult.OK;
        }



        private EBMTest.EBMContent.EBContent_AllData GetEBContentAllData()
        {
            try
            {
                if (ContentAllData == null)
                {
                    ContentAllData = new EBMContent.EBContent_AllData();
                }

               // ContentAllData.EBContentList = new System.Collections.Generic.List<EBMContent.EBContent>();
                ContentAllData.EBM_ID = EBM_ID;
                ContentAllData.Guid = Guid.NewGuid().ToString();
                ContentAllData.SectionName = txtSectionName.Text;
                ContentAllData.SendState = false;
                return ContentAllData;
            }
            catch
            {
                return null;
            }
        }
        public bool ValidatData()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        MessageBox.Show("\"" + c.Tag + "\"不允许为空，请检查并填写");
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
