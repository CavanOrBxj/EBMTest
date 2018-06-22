using EBMTable;
using System;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureRebackLayout : UserControl
    {
        private EBMConfigure.Reback Reback;

        public ConfigureRebackLayout()
        {
            InitializeComponent();
            InitType();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitConfigRebackType(cbBoxB_reback_type);
        }

        public void InitData(object config)
        {
            Reback = config as EBMConfigure.Reback;
            pnlAddressType.InitAddressType(Reback.B_Address_type);
            cbBoxB_reback_type.SelectedValue = Reback.B_reback_type;

            switch (Reback.B_reback_type)
            {
                case (byte)1:
                    textS_reback_address.Text = Reback.S_reback_address;
                    textS_reback_address2.Text = "123";
                    break;
                case (byte)2:
                case (byte)4:
                case (byte)6:
                case (byte)8:
                    textS_reback_address.Text = Reback.S_reback_address + ":" + Reback.I_reback_port.ToString();
                    textS_reback_address2.Text = Reback.S_reback_address_backup + ":" + Reback.I_reback_port_Backup.ToString();
                    break;
                case (byte)3:
                case (byte)5:
                case (byte)7:
                case (byte)9:
                    textS_reback_address.Text = Reback.S_reback_address + ":" + Reback.I_reback_port.ToString();
                    break;
            }

            pnlTerminalAddress.InitData(Reback.Configure.list_Terminal_Address);
        }

        public EBMConfigure.Reback GetData()
        {
            try
            {
                if (Reback == null)
                {
                    Reback = new EBMConfigure.Reback();
                }
                EBConfigureRebackGX config = new EBConfigureRebackGX();
                Reback.Configure = config;
                Reback.B_Address_type = pnlAddressType.GetAddressType();
                Reback.B_reback_type = (byte)cbBoxB_reback_type.SelectedValue;

                switch (Reback.B_reback_type)
                {
                    case (byte)1:
                        Reback.S_reback_address = textS_reback_address.Text.Trim();

                        break;
                    case (byte)2:
                    case (byte)4:
                    case (byte)6:
                    case (byte)8:
                        Reback.S_reback_address = textS_reback_address.Text.Trim().Split(':')[0];
                        Reback.I_reback_port = Convert.ToInt32(textS_reback_address.Text.Trim().Split(':')[1]);
                        Reback.S_reback_address_backup = textS_reback_address2.Text.Trim().Split(':')[0];
                        Reback.I_reback_port_Backup = Convert.ToInt32(textS_reback_address2.Text.Trim().Split(':')[1]);
                        break;
                    case (byte)3:
                    case (byte)5:
                    case (byte)7:
                    case (byte)9:
                        string[] str = textS_reback_address.Text.Trim().Split(':');
                        string port = str[str.Length - 1];
                        Reback.S_reback_address = textS_reback_address.Text.Trim().Replace(":" + port, "");
                        Reback.I_reback_port = Convert.ToInt32(port);
                        break;

                }

                Reback.Configure.list_Terminal_Address = pnlTerminalAddress.GetData();
                return Reback;
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

        private void cbBoxB_reback_type_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbBoxB_reback_type.SelectedValue!=null)
                {

                    switch (cbBoxB_reback_type.SelectedValue.ToString())
                    { 
                        case "2":
                        case "4":
                        case "6":
                        case "8":
                             lblS_reback_address.Text = "回传主地址";
                        textS_reback_address.Size = new System.Drawing.Size(198, 21);
                        lblS_reback_address2.Visible = true;
                        textS_reback_address2.Visible = true;
                            break;
                        case "1":
                        case "3":
                        case "5":
                        case "7":
                        case "9": lblS_reback_address.Text = "回传地址";
                            textS_reback_address.Size = new System.Drawing.Size(299, 21);
                            lblS_reback_address2.Visible = false;
                            textS_reback_address2.Visible = false;
                            break;
                    }
                }
              
            }
            catch (Exception)
            {

                throw;
            }
        }

       

    }
}
