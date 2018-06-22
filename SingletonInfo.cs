using System.Threading;
using System.Collections.Generic;
using System.Data;
using EBSignature;

namespace EBMTest
{
    public class SingletonInfo
    {
        private static SingletonInfo _singleton;
        public int DeviceHandle;

        public USBE usb;

        public int scramblernum;//记录当前所使用的是哪种CA 江南天安-1 江南科友-2     内置CA-5

        public bool ischecksignature;//记录当前是否需要签名检测

        public int OpenScramblerReturn; //天安密码器打开情况   0表示成功 

        public int InlayCAType;//内置CA的类型  0表示EbMSGCASignature  1表示EbMSGPLSignature 2表示EbMSGPLSignature

        public bool IsUseCAInfo;//表明是否启用CA  true表示启用  false表示不启用

        public EbmSignature InlayCA;

        public bool IsStartSend;//是否已经启动发送

        public  List<EBContentTableTMP> list_EB_Content_TableTMP;//新增于20180403 

        public bool IsProtocolGX;//是否广西协议  20180522


        public bool manuAddCert_sn;//是否人工增加证书

        public Dictionary<string, int> DicCert;//证书号与证书索引字典
        public string CurrentCert_SN;//当前系统中所用的证书号

        private SingletonInfo()                                                                 
        {
            scramblernum = 0;
            DeviceHandle = 0;
            ischecksignature = false;
            OpenScramblerReturn = 2;
            InlayCAType = 0;
            IsUseCAInfo = true; //默认启用CA
            InlayCA=new EbmSignature();
            usb = new USBE();
            IsStartSend = false;
            list_EB_Content_TableTMP = new List<EBContentTableTMP>();

            IsProtocolGX = true;

            manuAddCert_sn = false;
            CurrentCert_SN = "";
            DicCert = new Dictionary<string, int>();
        }

        public static SingletonInfo GetInstance()
        {
            if (_singleton == null)
            {
                Interlocked.CompareExchange(ref _singleton, new SingletonInfo(), null);
            }
            return _singleton;
        }




    }
}