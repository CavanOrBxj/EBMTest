using EBMTable;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMMain : Form
    {
        private bool isAdminAccount = false;
        private Object Gtoken = null; //用于锁住

        public static IConfig cf = ConfigFile.Instanse;


        public bool AdminAccount
        {
            get { return isAdminAccount; }
            private set
            {
                if (isAdminAccount != value)
                {
                    isAdminAccount = value;
                    OnAdminAccountChanged();
                }
            }
        }
        public delegate void AdminAccountEventHandler(object sender, AdminAccountEventArgs e);
        public event AdminAccountEventHandler AdminAccountChanged;


        public bool IsStartStream { get; set; }
        bool isInitStream = false;
        EBMStream EbmStream;

         

        EBMIndex formIndex;
        EBMContent formContent;
        DailyBroadcast formDailyBroadcast;
        EBMConfigure formConfigure;
        EBMCertAuth formCertAuth;
        EBMStreamSet formStreamSet;
        EBMCASet formCASet;

        EBIndexTable EB_Index_Table = new EBIndexTable();
        DailyBroadcastTable Daily_Broadcast_Table = new DailyBroadcastTable();
        //EBConfigureTable EB_Configure_Table;
        EBContentTable EB_Content_Table = new EBContentTable();
       // list_EB_Content_Table EB_Content_Table_New = new list_EB_Content_Table();

        List<EBContentTable> list_EB_Content_Table = new List<EBContentTable>();//新增于20180403增于20180403

        EBCertAuthTable EB_CertAuth_Table = new EBCertAuthTable();

       // public USBE usb = new USBE();
        public System.IntPtr phDeviceHandle = (IntPtr)0;
        Thread thread;

        public EBMIndex EBMIndex { get { return formIndex; } }

        public EBMStream EbMStream
        {
            get { return EbmStream; }
            set { EbmStream = value; }
        }



        public Calcle calcel;

        Socket socketSend;


        public EBMMain()
        {
            InitializeComponent();
            AdminAccount = true;
            UpdateFormTitle("");
            string path = AppDomain.CurrentDomain.BaseDirectory;
            ConfigFile.Instanse.fileName = @path + "EBMTest.cfg";

            SingletonInfo.GetInstance().InlayCA.SignCounter = Convert.ToInt32(cf["SignCounter"].ToString());
            SingletonInfo.GetInstance().IsProtocolGX = cf["IsProtocolGX"].ToString() == "1" ? true : false;// 1表示广西协议（true） 2表示国标协议(false)

            IsStartStream = false;
            EbmStream = new EBMStream();
            MenuItemTSSetting_Click(MenuItemTSSetting, EventArgs.Empty);
            formStreamSet.WindowState = FormWindowState.Minimized;
            
            InitTable();
            InitEBStream();
            //if (formIndex == null || formIndex.IsDisposed)
            //{
            //    formIndex = new EBMIndex();
            //}
            //formIndex.MdiParent = this;
            //formIndex.Visible = false;


            calcel = new Calcle();
            calcel.MyEvent += new Calcle.MyDelegate(NetErrorDeal);


            InitStreamTable();
            this.Load += EBMMain_Load;
        }

        void EBMMain_Load(object sender, EventArgs e)
        {
            Gtoken = new object();
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Com_DataReceived);
        }

     
          private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
          {
              byte[] ReDatas = new byte[serialPort1.BytesToRead];
              serialPort1.Read(ReDatas, 0, ReDatas.Length);//读取数据
             //this.AddData(ReDatas);//输出数据
         }

        private void OnAdminAccountChanged()
        {
            if (AdminAccountChanged != null)
            {
                AdminAccountChanged(this, new AdminAccountEventArgs(AdminAccount));
            }
            MenuItemEBContent.Visible = AdminAccount;
            MenuItemCertAuth.Visible = AdminAccount;
            MenuItemConfigure.Visible = AdminAccount;
            if (!AdminAccount)
            {
                if (formContent != null && formContent.Visible) formContent.Close();
                if (formConfigure != null && formConfigure.Visible) formConfigure.Close();
                if (formCertAuth != null && formCertAuth.Visible) formCertAuth.Close();
            }
        }

        public bool InitEBStream()
        {
            try
            {
                JObject jo = TableData.TableDataHelper.ReadConfig();
                if (jo != null)
                {
                    EbmStream.ElementaryPid = Convert.ToInt32(jo["ElementaryPid"].ToString());
                    EbmStream.Stream_id = Convert.ToInt32(jo["Stream_id"].ToString());
                    EbmStream.Program_id = Convert.ToInt32(jo["Program_id"].ToString());
                    EbmStream.PMT_Pid = Convert.ToInt32(jo["PMT_Pid"].ToString());
                    EbmStream.Section_length = Convert.ToInt32(jo["Section_length"].ToString());
                    EbmStream.sDestSockAddress = jo["sDestSockAddress"].ToString();
                    EbmStream.sLocalSockAddress = jo["sLocalSockAddress"].ToString();
                    EbmStream.Stream_BitRate = Convert.ToInt32(jo["Stream_BitRate"].ToString());
                }

                InitStreamTable();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InitTable()
        {
            EB_Index_Table.Table_id = 0xfd;
            EB_Index_Table.Table_id_extension = 0;
            EB_Content_Table.Table_id = 0xfe;
            EB_Content_Table.Table_id_extension = 0;
            Daily_Broadcast_Table.Table_id = 0xfa;
            Daily_Broadcast_Table.Table_id_extension = 0;
            EB_CertAuth_Table.Table_id = 0xfc;
            EB_CertAuth_Table.Table_id_extension = 0;
            EbmStream.EB_Index_Table = EB_Index_Table;
            EbmStream.EB_Index_Table.ProtocolGX = SingletonInfo.GetInstance().IsProtocolGX;
        
          

        }

        public void InitStreamTable()
        {
            //设置需要发送的表
            if (formDailyBroadcast != null)
            {
                EbmStream.Daily_Broadcast_Table = formDailyBroadcast.GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
            }
            if (formIndex != null)
            {
                formIndex.GetEBIndexTable(ref EB_Index_Table);
                EbmStream.EB_Index_Table = EB_Index_Table;
            }
            //if (formConfigure != null)
            //{
            //    EbmStream.EB_Configure_Table = formConfigure.GetConfigureTable(ref EB_Configure_Table) ? EB_Configure_Table : null;
            //}
            if (formContent != null)
            {
                EbmStream.list_EB_Content_Table = formContent.GetlistContentTable(ref list_EB_Content_Table) ? list_EB_Content_Table : null;
             
              
            }
            if (formCertAuth != null)
            {
                EbmStream.EB_CertAuth_Table = formCertAuth.GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
            }

            if (SingletonInfo.GetInstance().IsUseCAInfo)
            {
                EbmStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(calcel.SignatureFunc);//每次在 Initialization()之前调用
            }
            else
            {
                EbmStream.SignatureCallbackRef = null;
            }
            EbmStream.Initialization();
            isInitStream = true;
            if (thread != null)
            {
                thread.Abort();
            }
            thread = new Thread(UpdateDataText);
            thread.IsBackground = true;
            thread.Start();
           
        }

        public void NetErrorDeal()
        {
            this.Invoke(new Action(() => 
            {
                SingletonInfo.GetInstance().OpenScramblerReturn = 2;//表示打开密码器的预制状态
                MenuItemStopStream_Click(null, null);
                MessageBox.Show("网络异常,数据发送停止！");
            }));
        
        }


        public void UpdateDataText()
        {
            try
            {
                if (IsStartStream)
                {
                    if (EbmStream.EB_Index_Table != null && formIndex != null && !formIndex.IsDisposed)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        EbmStream.EB_Index_Table.BuildEbIndexSection(); 
                        byte[] body;
                        do
                        {
                            Thread.Sleep(10);
                            body = EbmStream.EB_Index_Table.GetEbIndexSection(ref num);
                        }
                         
                        while (EbmStream.EB_Index_Table.Completed==false);

                        if (body!=null)
                        {
                            for (int i = 0; i < body.Length; i++)
                            {
                                if (i != 0 && i % 16 == 0) sb.Append("\n");
                                sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                            }
                            sb.Append("\n\n");
                        }
                        
                        formIndex.BeginInvoke(new MethodInvoker(() =>
                        {
                            formIndex.AppendDataText(sb.ToString());
                        }));
                    }

                    if (EbmStream.list_EB_Content_Table != null && formContent != null && !formContent.IsDisposed)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        byte[] body;

                            foreach (var item in EbmStream.list_EB_Content_Table)
                            {
                                item.BuildEBContentSection();
                                do
                                {
                                    Thread.Sleep(10);
                                    
                                }

                                while (item.Completed == false);

                                body = item.GetEBContentSection(ref num);
                                for (int i = 0; i < body.Length; i++)
                                {
                                    if (i != 0 && i % 16 == 0) sb.Append("\n");
                                    sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                                }
                                sb.Append("\n\n");
                                formContent.BeginInvoke(new MethodInvoker(() =>
                                {
                                    formContent.AppendDataText(sb.ToString());
                                }));
                            }
                    }

                    if (EbmStream.EB_Configure_Table != null && formConfigure != null && !formConfigure.IsDisposed)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        EbmStream.EB_Configure_Table.BuildEBConfigureSection();
                      //  var body = EbmStream.EB_Configure_Table.GetEBConfigureSection(ref num);

                        byte[] body;
                        do
                        {
                            Thread.Sleep(10);
                            body = EbmStream.EB_Configure_Table.GetEBConfigureSection(ref num);
                        }

                        while (EbmStream.EB_Configure_Table.Completed == false);

                        for (int i = 0; i < body.Length; i++)
                        {
                            if (i != 0 && i % 16 == 0) sb.Append("\n");
                            sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                        }
                        sb.Append("\n\n");
                        formConfigure.BeginInvoke(new MethodInvoker(() =>
                        {
                            formConfigure.AppendDataText(sb.ToString());
                        }));
                    }

                    if (EbmStream.EB_CertAuth_Table != null && formCertAuth != null && !formCertAuth.IsDisposed)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        EbmStream.EB_CertAuth_Table.BuildEBCertAuthSection();
                      //  var body = EbmStream.EB_CertAuth_Table.GetEBCertAuthSection(ref num);

                        byte[] body;
                        do
                        {
                            Thread.Sleep(10);
                            body = EbmStream.EB_CertAuth_Table.GetEBCertAuthSection(ref num);
                        }

                        while (EbmStream.EB_CertAuth_Table.Completed == false);


                        for (int i = 0; i < body.Length; i++)
                        {
                            if (i != 0 && i % 16 == 0) sb.Append("\n");
                            sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                        }
                        sb.Append("\n\n");
                        formCertAuth.BeginInvoke(new MethodInvoker(() =>
                        {
                            formCertAuth.AppendDataText(sb.ToString());
                        }));
                    }

                    if (EbmStream.Daily_Broadcast_Table != null && formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        EbmStream.Daily_Broadcast_Table.BuildDailyBroadcastSection();
                      //  var body = EbmStream.Daily_Broadcast_Table.GetDailyBroadcastSection(ref num);

                        byte[] body;
                        do
                        {
                            Thread.Sleep(10);
                            body = EbmStream.Daily_Broadcast_Table.GetDailyBroadcastSection(ref num);
                        }

                        while (EbmStream.Daily_Broadcast_Table.Completed == false);

                        for (int i = 0; i < body.Length; i++)
                        {
                            if (i != 0 && i % 16 == 0) sb.Append("\n");
                            sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                        }
                        sb.Append("\n\n");
                        formDailyBroadcast.BeginInvoke(new MethodInvoker(() =>
                        {
                            formDailyBroadcast.AppendDataText(sb.ToString());
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        #region MenuItem

        private void MenuItemStartStream_Click(object sender, EventArgs e)
        {
            try
            {
                if(cf["IsCASet"].ToString()=="0")
                {
                    MessageBox.Show("请先设置CA信息！");
                    return;
                }
                InitEBStream();
                if (EbmStream != null && isInitStream && !IsStartStream)
                {
                    //发送数据
                    EbmStream.StartStreaming();
                    IsStartStream = true;
                    if (formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                    {
                        formDailyBroadcast.InitColumnStop(true);
                    }

                    Thread thread = new Thread(UpdateDataText);
                    thread.IsBackground = true;
                    thread.Start();
                }

                SingletonInfo.GetInstance().IsStartSend = true;
            }
            catch (Exception)
            {
                throw;
            }
        }


     
        private void MenuItemStopStream_Click(object sender, EventArgs e)
        {
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
                IsStartStream = false;
                if (formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                {
                    formDailyBroadcast.InitColumnStop(false);
                }
            }

            SingletonInfo.GetInstance().IsStartSend = false;
        }

        private void MenuItemEBIndex_Click(object sender, EventArgs e)
        {
            if (formIndex == null || formIndex.IsDisposed)
            {
                formIndex = new EBMIndex();
                formIndex.EbmIdChanged += FormIndex_EbmIdChanged;
            }
            formIndex.MdiParent = this;
            formIndex.Show();
            if (formIndex.WindowState == FormWindowState.Minimized)
                formIndex.WindowState = FormWindowState.Normal;
            formIndex.BringToFront();
        }

        private void MenuItemEBContent_Click(object sender, EventArgs e)
        {
            if(formContent == null || formContent.IsDisposed)
            {
                formContent = new EBMContent();
            }
            formContent.MdiParent = this;
            formContent.Show();
            if (formContent.WindowState == FormWindowState.Minimized)
                formContent.WindowState = FormWindowState.Normal;
            formContent.BringToFront();
        }

        private void MenuItemDailyBroadcast_Click(object sender, EventArgs e)
        {
            if (formDailyBroadcast == null || formDailyBroadcast.IsDisposed)
            {
                formDailyBroadcast = new DailyBroadcast();
            }
            formDailyBroadcast.MdiParent = this;
            formDailyBroadcast.Show();
            if (formDailyBroadcast.WindowState == FormWindowState.Minimized)
                formDailyBroadcast.WindowState = FormWindowState.Normal;
            formDailyBroadcast.BringToFront();
        }

        private void MenuItemConfigure_Click(object sender, EventArgs e)
        {
            if (formConfigure == null || formConfigure.IsDisposed)
            {
                formConfigure = new EBMConfigure();
            }
            formConfigure.MdiParent = this;
            formConfigure.Show();
            if (formConfigure.WindowState == FormWindowState.Minimized)
                formConfigure.WindowState = FormWindowState.Normal;
            formConfigure.BringToFront();
        }

        private void MenuItemCertAuth_Click(object sender, EventArgs e)
        {
            if (formCertAuth == null || formCertAuth.IsDisposed)
            {
                formCertAuth = new EBMCertAuth();
            }
            formCertAuth.MdiParent = this;
            formCertAuth.Show();
            if (formCertAuth.WindowState == FormWindowState.Minimized)
                formCertAuth.WindowState = FormWindowState.Normal;
            formCertAuth.BringToFront();
        }

        private void MenuItemTSSetting_Click(object sender, EventArgs e)
        {
            if (formStreamSet == null || formStreamSet.IsDisposed)
            {
                formStreamSet = new EBMStreamSet();
            }
            formStreamSet.MdiParent = this;
            formStreamSet.Show();
            if (formStreamSet.WindowState == FormWindowState.Minimized)
                formStreamSet.WindowState = FormWindowState.Normal;
            formStreamSet.BringToFront();
        }

        private void Operate_ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            MenuItemStartStream.Enabled = EbmStream != null && isInitStream && !IsStartStream;
            MenuItemStopStream.Enabled = EbmStream != null && IsStartStream;
        }

        private void MenuItemTileH_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void MenuItemTileV_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void MenuItemCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void ReStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AdminAccount)
            {
                if (new EBMLogIn().ShowDialog() == DialogResult.OK)
                {
                    AdminAccount = true;
                }
            }
            else
            {
                if (MessageBox.Show(this, "是否退出管理员账户？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AdminAccount = false;
                }
            }
        }

        #endregion

        private void EBMMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            cf["IsCASet"] = "0";
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
                CloseScrambler();
                
                IsStartStream = false;
            }
            cf["SignCounter"] = SingletonInfo.GetInstance().InlayCA.SignCounter.ToString();
        }


        private void OpenScrambler()
        {
            switch (SingletonInfo.GetInstance().scramblernum)
            {
                case 0:
                    //未开密码器 
                    break;
                case 1:
                    //开启江南天安的密码器
                    int nReturn =SingletonInfo.GetInstance().usb.USB_OpenDevice(ref phDeviceHandle);
                
                    if (nReturn != 0)
                    {
                        LogRecord.WriteLogFile("天安密码器打开失败！");
                        MessageBox.Show("密码器打开失败！");

                    }
                    else
                    {
                        SingletonInfo.GetInstance().DeviceHandle = (int)phDeviceHandle;
                        LogRecord.WriteLogFile("天安密码器打开成功！");
                    }
                    break;
                case 2:
                    //开启江南科友密码器

                  //  IPAddress ip = IPAddress.Parse("127.0.0.1");
               //  IPEndPoint point = new IPEndPoint(ip, 8100);
                    socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
                    IPEndPoint point = new IPEndPoint(IPAddress.Parse("192.168.4.51"), 1818);

                 socketSend.Connect(point);

                 //开启新的线程，不停的接收服务器发来的消息
                 Thread c_thread = new Thread(Received);
                 c_thread.IsBackground = true;
                 c_thread.Start();

                 string msg = "\0\n12345678NC";
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(msg);
                 socketSend.Send(buffer);
                    break;

                case 5://调用内部CA接口  不需要打开密码器
                    break;

            }
        }


        private void Received()
        {
            while (true)
            {
                try
                {
                    //实际接收到的有效字节数    socketSend.ReceiveBufferSize这个值有点偏大 8192固定
                    int len = 1024;
                    byte[] bufferreal = new byte[len];
                    byte[] bufferUserful = new byte[len - 2]; //返回的数据比实际多了 \0< 两个字符

                    socketSend.Receive(bufferreal);
                    if (len == 0)
                    {
                        break;
                    }
                    Array.Copy(bufferreal, 2, bufferUserful, 0, len - 2);
                    string str = Encoding.UTF8.GetString(bufferUserful, 0, len - 2);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CloseScrambler()
        { 
           switch(SingletonInfo.GetInstance().scramblernum)
           {
               case 0:
                   //未开密码器 可直接关闭
                   break;
               case 1:
                   int nDeviceHandle = (int)phDeviceHandle;
                   int nReturn =SingletonInfo.GetInstance().usb.USB_CloseDevice(ref nDeviceHandle);
                   SingletonInfo.GetInstance().OpenScramblerReturn = 2;
                   //关闭江南天安的密码器
                   break;
               case 2:
                   //关闭江南科友密码器
                   break;

           }
        }

        private void MenuItemPlayVideo_Click(object sender, EventArgs e)
        {
            Forms.FormMediaStreamer form = new Forms.FormMediaStreamer();
            form.MdiParent = this;
            form.Show();
            form.BringToFront();
        }

        private void lblLogIn_DoubleClick(object sender, EventArgs e)
        {
            if (!AdminAccount)
            {
                if (new EBMLogIn().ShowDialog() == DialogResult.OK)
                {
                    AdminAccount = true;
                }
            }
            else
            {
                if (MessageBox.Show(this, "是否退出管理员账户？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AdminAccount = false;
                }
            }
        }

        public void UpdateFormTitle(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Text = string.Join(" - ", System.Configuration.ConfigurationManager.AppSettings["EBMTitle"], Application.ProductVersion);
            }
            else
            {
                Text = string.Join(" - ", System.Configuration.ConfigurationManager.AppSettings["EBMTitle"], Application.ProductVersion, text);
            }
        }

        private void FormIndex_EbmIdChanged(object sender, EbmIdChangedEventArgs e)
        {
            if (formContent != null)
            {
                formContent.UpdateEbmIndex(e.ListEbmId);
            }
        }

        /// <summary>
        /// 弹出CA选择界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripCAName_Click(object sender, EventArgs e)
        {
            try
            {
                if (SingletonInfo.GetInstance().IsStartSend)  //在发送数据状态提示用户会停止数据的发送
                {
                    if (MessageBox.Show("修改CA将导致数据停止发送，是否确定继续操作？", @"提示",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MenuItemStopStream_Click(null, null);
                    }
                    else
                    {
                        return;
                    }
                
                }
                CloseScrambler();
               
                if (formCASet == null || formCASet.IsDisposed)
                {
                    formCASet = new EBMCASet();
                    formCASet.CASetEvent += new EBMCASet.CASetDelegate(OpenScrambler);//监听formCASet事件 

                   // formCASet.ff += new EBMCASet.CASetDelegate(OpenScrambler);

                }
                formCASet.MdiParent = this;
                formCASet.cmbCAname.SelectedValue =SingletonInfo.GetInstance().scramblernum;
                formCASet.Show();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }

}


