using EBMTable;
using EBSignature;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EBMTest
{
    public partial class EBMCASet : Form
    {
        public static IConfig cf = ConfigFile.Instanse;
       public delegate void CASetDelegate();

        public event CASetDelegate CASetEvent;

       // EbmSignature InlayCA ;

        string InlayCAPBK = "200000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";
        string InLayPLPBK = "300000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";

        string InLayPLPBKNew = "300000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F0597900";



        string TIANANPBK = "100000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";
       // public delegate void CASetDelegate();

      //  public CASetDelegate ff;
        public EBMCASet()
        {
            InitializeComponent();

            string path = AppDomain.CurrentDomain.BaseDirectory;
            ConfigFile.Instanse.fileName = @path + "EBMTest.cfg";

           // InlayCA = new EbmSignature();
            this.Load += EBMCASet_Load;

      
        }

        void EBMCASet_Load(object sender, EventArgs e)
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("江南天安", 1);
            dict.Add("内置CA",5);
            ComboBind.Binding(cmbCAname, dict);


            EBCert tmp =SingletonInfo.GetInstance().InlayCA.GetEBCert(0);//0表示CA_CERT
            InlayCAPBK = tmp.Cert;

            EBCert tmp1 = SingletonInfo.GetInstance().InlayCA.GetEBCert(1);//1表示PL_CERT

            EBCert tmp2 = SingletonInfo.GetInstance().InlayCA.GetEBCert(2);//2表示PL_CERTNew
            InLayPLPBK = tmp1.Cert;

            InLayPLPBKNew = tmp2.Cert;


            chbUseSignature.Checked = cf["IsUseCA"].ToString() == "1" ? true : false;



            cmbCAname.SelectedValue=Convert.ToInt32(cf["CAtype"].ToString());


            if (Convert.ToInt32(cmbCAname.SelectedValue) == 5) //内置CA
            {
                panelInlayCA.Visible = true;

                switch (cf["InlayCA"].ToString())
                {
                    case "0":
                        chbCAsignature.Checked = true;
                        textPublicKey.Text = InlayCAPBK;
                        break;
                    case "1":
                        chbplatformsignature.Checked = true;
                        textPublicKey.Text = InLayPLPBK;
                        break;
                    case "2":
                        chbplatformsignatureNew.Checked = true;
                        textPublicKey.Text = InLayPLPBKNew;
                        break;
                
                }
            }
            else
            {
                panelInlayCA.Visible = false;
            }

            chbCheckSignature.Checked = cf["CheckSignature"].ToString() == "1" ? true : false;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbCAname.SelectedValue) == 5)
                {

                    if (chbplatformsignature.Checked == false && chbCAsignature.Checked == false && chbplatformsignatureNew.Checked == false)
                    {
                        MessageBox.Show("请勾选平台证书签名/CA签名！");
                        return;
                    }

                    if (chbplatformsignatureNew.Checked)
                    {
                        SingletonInfo.GetInstance().InlayCAType = 2;
                    }
                    if (chbplatformsignature.Checked)
                    {
                        SingletonInfo.GetInstance().InlayCAType = 1;
                    }
                    if (chbCAsignature.Checked)
                    {
                        SingletonInfo.GetInstance().InlayCAType = 0;
                    }
                }

                if (chbCheckSignature.Checked)
                {
                    SingletonInfo.GetInstance().ischecksignature = true;
                }
                else
                {
                    SingletonInfo.GetInstance().ischecksignature = false;
                }

                if (SingletonInfo.GetInstance().OpenScramblerReturn != 0)
                {
                    SingletonInfo.GetInstance().scramblernum = Convert.ToInt32(cmbCAname.SelectedValue);

                    if (CASetEvent != null)
                        CASetEvent();//引发事件
                }

                SingletonInfo.GetInstance().IsUseCAInfo = chbUseSignature.Checked; //是否启用签名

                cf["IsCAInfoSet"] = "1";
                cf["IsUseCA"] = chbUseSignature.Checked ? "1" : "0";
                if (!chbUseSignature.Checked)
                {
                    //初始化  
                }

                cf["CAtype"] = Convert.ToInt32(cmbCAname.SelectedValue).ToString();
                if (Convert.ToInt32(cmbCAname.SelectedValue) == 5)
                {
                    if (chbplatformsignatureNew.Checked)
                    {
                        cf["InlayCA"] = "2";
                    }
                    if (chbplatformsignature.Checked)//平台签名
                    {
                        cf["InlayCA"] = "1";
                    }
                    if (chbCAsignature.Checked)
                    {
                        cf["InlayCA"] = "0";
                    }
                }

                cf["CheckSignature"] = chbCheckSignature.Checked ? "1" : "0";
                cf["IsCASet"] = "1";




                if (checkBox1.Checked)
                {
                   SingletonInfo.GetInstance().manuAddCert_sn = true;
                   SingletonInfo.GetInstance().CurrentCert_SN = txtCert_sn.Text;

                   EBCert tmp=new EBCert();
                    tmp.Cert_sn= SingletonInfo.GetInstance().CurrentCert_SN;
                  switch( tmp.Cert_sn)

                  {
                      case "000000000007":
                          tmp.PriKey="57C6140969FE97A75C3C7AFBFDCDFA3951147BF6EE7FC391AABAA8721FEA9AB0";
                          tmp.PubKey="B086AA7AFBACBD9C81A7717BD4C493291EA0EDACD1ADEF1A4AB05BD14BBF1F4A78A195559C02D0195C1675BC6CF817CCB32A675BCAE12C52C672840C377C50C2";
                          break;
                      case "000000000008":
                            tmp.PriKey="E884E9313BACCC26F393B21FCF992E73A6FE359D86149EB573EF47A7FF82B0E3";
                          tmp.PubKey="4DE92C85B9104476298D61D8A8BE570D8A00D60F30BD2254D5DFEEC7A873A5DB63FD5936E4EB35F6AA8EECBB85346ADEA08F406BFD3D2C11548431D57A6FDF6F";
                          break;
                      case "000000000009":
                            tmp.PriKey="1208B7BBACB1265EF100DBB2C0F9E55820EA4FAEEECB54B00F3EBE665398CE95";
                          tmp.PubKey="4466BFA3C0CF3342F4970FCBEA10A95A439B8D9DB4AC90BB123FBC2E500418DA7BB039EB63FA27C8A91C3C51CF4FCE109C501DDE9456615A13E7C159FBFF8B73";
                          break;
                  }


                  int index = SingletonInfo.GetInstance().InlayCA.AddEBCert(tmp);
                  SingletonInfo.GetInstance().DicCert.Add(tmp.Cert_sn, index);

                }

                Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void EBMStreamSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void cmbCAname_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbCAname.SelectedValue) == 1) //江南天安
                {
                    textPublicKey.Text = TIANANPBK;
                    panelInlayCA.Visible = false;
                }
                else   //内置CA
                {
                    panelInlayCA.Visible = true;
                    textPublicKey.Text = "";
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void chbplatformsignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbplatformsignature.Checked == true)
                {

                    textPublicKey.Text =InLayPLPBK ;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chbCAsignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbCAsignature.Checked == true)
                {

                    textPublicKey.Text = InlayCAPBK;
                }

                if (chbplatformsignature.Checked==true)
                {
                    textPublicKey.Text = InLayPLPBK;
                
                }

                if (chbplatformsignatureNew.Checked==true)
                {
                    textPublicKey.Text = InLayPLPBKNew;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chbUseSignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //(MdiParent as EBMMain).EbMStream.EB_CertAuth_Table = GetCertAuthTable(ref (MdiParent as EBMMain).EbMStream.EB_CertAuth_Table) ? (MdiParent as EBMMain).EbMStream.EB_CertAuth_Table : null;

                (MdiParent as EBMMain).EbMStream.Initialization();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    label2.Visible = true;
                    txtCert_sn.Visible = true;
                }
                else
                {
                    label2.Visible = false;
                    txtCert_sn.Visible = false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
