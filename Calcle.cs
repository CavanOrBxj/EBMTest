using EBSignature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace EBMTest
{
   public  class Calcle
    {
         public delegate void MyDelegate();
         public event MyDelegate MyEvent;
         public void SignatureFunc(byte[] pdatabuf, int datalen, ref int random, ref byte[] signature)
         {
             byte[] strSignture = new byte[100];
             byte[] pucSignature = pdatabuf;

             if (!SingletonInfo.GetInstance().manuAddCert_sn)
             {
                 switch (SingletonInfo.GetInstance().scramblernum)
                 {
                     case 1:

                         int nDeviceHandle = SingletonInfo.GetInstance().DeviceHandle;


                         string eturn = SingletonInfo.GetInstance().usb.Platform_CalculateSingature_String(nDeviceHandle, 1, pucSignature, pucSignature.Length, ref strSignture);
                         strSignture.CopyTo(signature, 0);
                         if (eturn == "NetError")
                         {
                             MyEvent();//引发事件  提示主界面 
                         }
                         break;

                     case 5:

                         //switch (SingletonInfo.GetInstance().InlayCAType)
                         //{ 
                         //    case 0:
                         //        SingletonInfo.GetInstance().InlayCA.EbMsgCASign(pdatabuf, datalen, ref random, ref signature);

                         //        break;

                         //    case 1:

                         //        SingletonInfo.GetInstance().InlayCA.EbMsgPLSign(pdatabuf, datalen, ref random, ref signature);
                         //        break;

                         //    case 2:
                         //        SingletonInfo.GetInstance().InlayCA.EbMsgSign(pdatabuf, datalen,ref random, ref signature,2);
                         //        break;
                         //}

                         SingletonInfo.GetInstance().InlayCA.EbMsgSign(pdatabuf, datalen, ref random, ref signature, SingletonInfo.GetInstance().InlayCAType);
                         break;


                 }

             }
             else
             {

                 int certindex = SingletonInfo.GetInstance().DicCert[SingletonInfo.GetInstance().CurrentCert_SN];


                 SingletonInfo.GetInstance().InlayCA.EbMsgSign(pdatabuf, datalen, ref random, ref signature, certindex);
             }
            
             
             string strData = null;
             for (int i = 0; i < pucSignature.Length; i++)
             {
                 strData += " " + pucSignature[i].ToString("X2");
             }
             LogRecord.WriteLogFile("原文：" + strData);
             string strData2 = null;
             for (int i = 0; i < signature.Length; i++)
             {
                 strData2 += " " + signature[i].ToString("X2");
             }
             LogRecord.WriteLogFile("签名数据：" + strData2);

             if (SingletonInfo.GetInstance().ischecksignature)
             {
                 DateTime dt = DateTime.UtcNow;
                 string tt = UtcHelper.ConvertDateTimeInt(dt).ToString();

                 byte[] byteArray = System.Text.Encoding.Default.GetBytes(tt);
                 if (byteArray.Length > 0 && byteArray.Length < 64)
                 {
                     for (int i = 0; i < byteArray.Length; i++)
                     {
                         signature[10 + i] = byteArray[i];
                     }
                 }

             }

         }
    }
}
