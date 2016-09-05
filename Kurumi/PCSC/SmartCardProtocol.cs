//
//项目名称：SmartCaridPtotocol
//描述：智能卡通讯协议 （为了完成作业开发的(╯°Д°)╯︵ ┻━┻）
//创建人：Dlnn
//创建时间：2016年5月27日
//作者邮箱：admin@dlnn.net
//参考资料：ACS官方说明文档
//没什么版权可言 
using System;
using System.Collections.Generic;
using System.Text;

namespace _SmartCardProtocol
{
    class SmartCardProtocol
    {
        #region 私有变量

        private string errMsg = "";
        private int runResult = -1;
        private static IntPtr SCARD_PCI_T0;
        private static IntPtr SCARD_PCI_T1;

       

        private const int SCARD_SUCCESS = 0;



        /// <summary>
        /// 上下文句柄
        /// </summary>
        private int hContext;
        /// <summary>
        /// 卡片句柄
        /// </summary>
        private int hCard = 0;
        /// <summary>
        /// 卡片使用的传输协议
        /// </summary>
        private int ActiveProtocol = 0;
        /// <summary>
        /// 使用的读卡器名称
        /// </summary>
        private string activeReaderName = "";
        #endregion
        #region 常量
        public enum KeyType : byte
        {
            Key_A=0x60,
            Key_B=0x61,
        }
        #endregion


        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns>返回设备列表</returns>
        public List<string> InitializeDevice()
        {
            //获取设备上下文句柄
            runResult = PCSC.SCardEstablishContext(0, 0, 0, ref hContext);
            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取设备上下文失败，错误代码为：" + runResult.ToString("X");
                //Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            if (hContext == 0)
            {
                errMsg = "未能获取设备上下文句柄";
               // Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }
            int mszReaderSize = 0;
            //获取读卡器列表，第一次用于获取字符串长度
            runResult = PCSC.SCardListReaders(hContext, null, null, ref mszReaderSize);

            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取读卡器列表，错误代码为：" + runResult.ToString("X");
               // Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            List<string> readerList = new List<string>();

            byte[] reads = new byte[mszReaderSize];

            //获取读卡器列表
            runResult = PCSC.SCardListReaders(hContext, null, reads, ref mszReaderSize);

            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取读卡器列表，错误代码为：" + runResult.ToString("X");
               // Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            ASCIIEncoding encoding = new ASCIIEncoding();
            string strBuffer = encoding.GetString(reads);

            char nullChar = '\0';

            int len = mszReaderSize;
            int index = 0;

            while (strBuffer[0] != nullChar)
            {
                index = strBuffer.IndexOf(nullChar);
                string reader = strBuffer.Substring(0, index);
                len = len - (reader.Length + 1);
                strBuffer = strBuffer.Substring(index + 1, len);
                readerList.Add(reader);
            }
            return readerList;
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public void CloseDevice()
        {
            if (hCard != 0)
            {
                runResult = PCSC.SCardDisconnect(hCard, 2);
               
            }

            if (hContext != 0)
            {
                runResult = PCSC.SCardReleaseContext(hContext);
               
            }

        }

        /// <summary>
        /// 初始化卡
        /// </summary>
        /// <param name="readerList">设备列表</param>
        /// <returns></returns>
        public void InitializeCard(List<string> readerList)
        {
            if (hContext == 0)
            {
                errMsg = "获取设备上下文句柄失败。初始化卡片前需初始化设备！";
               // Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            bool isConnectCard = false;

            for (int i = 0; i < readerList.Count; i++)
            {
                string reader = readerList[i];
                runResult = PCSC.SCardConnect(hContext, reader, 1, 2, ref hCard, ref ActiveProtocol);

                if (runResult != SCARD_SUCCESS)
                {
                    continue;
                }
                else
                {
                    activeReaderName = reader;
                    isConnectCard = true;
                    break;
                }
            }

            if (!isConnectCard)
            {
                errMsg = "未能搜索到卡片！";
              //  Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            int readerLength = 0;

            int cardStatus = 0;

            int cardProtocal = 0;

            int atrLength = 0;

            runResult = PCSC.SCardStatus(hCard, activeReaderName, ref readerLength
                                            , ref cardStatus, ref cardProtocal, null, ref atrLength);

            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取读卡器列表，错误代码为：" + runResult.ToString("X");
                //Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            if (cardStatus != 6)
            {
                errMsg = "卡片状态不合法！连接失败！";
                //Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

            byte[] ATR = new byte[atrLength];

            runResult = PCSC.SCardStatus(hCard, activeReaderName, ref readerLength
                                            , ref cardStatus, ref cardProtocal, ATR, ref atrLength);

            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取读卡器列表，错误代码为：" + runResult.ToString("X");
               // Log.WriteLog(errMsg);
                throw new Exception(errMsg);
            }

        }
        /// <summary>
        /// 发送APDU指令
        /// </summary>
        /// <param name="SendMessageBuffer">发送的指令</param>
        /// <param name="SW">返回的SW状态码</param>
        /// <returns></returns>
        public byte[] SendAPDU(byte[] SendMessageBuffer, byte[] SW)
        {
            byte[] receiveMessageByte = new byte[256];
            int receiveLength = 256;
            runResult = PCSC.SCardTransmit(hCard
                                , SCARD_PCI_T1
                                , SendMessageBuffer
                                , SendMessageBuffer.Length
                                , null,receiveMessageByte, ref receiveLength);

            if (runResult != SCARD_SUCCESS)
            {
                errMsg = "获取读卡器列表，错误代码为：" + runResult.ToString("X");
                //Log.WriteLog(errMsg);
                throw new Exception(errMsg);

            }
            if (receiveLength > 2)
            {
                byte[] retMessage = new byte[receiveLength - 2];
                for (int i = 0; i < receiveLength - 2; i++)
                {
                    retMessage[i] = receiveMessageByte[i];
                }
                SW[0] = receiveMessageByte[receiveLength - 2];
                SW[1] = receiveMessageByte[receiveLength - 1];
                return retMessage;
            }
            if (receiveLength == 2)
            {
                SW[0] = receiveMessageByte[0];
                SW[1] = receiveMessageByte[1];
                return null;
            }

            return receiveMessageByte;

        }
        /// <summary>
        /// 获取卡片UID
        /// </summary>
        /// <returns>返回received （已去除sw）</returns>
        public byte[] GetUID()
        {
            byte[] UID= new byte[4];
            byte[] SW = new byte[2];
            byte[] MessageBuffer = { 0xFF,0xCA,0x00, 0x00, 0x00 };
    
            try
            {
                UID = SendAPDU(MessageBuffer, SW);
                if ((SW[0] | SW[1]) == 0x90)
                {
                   // Console.WriteLine("GetUID[{0:X2},{0:X2}]", SW[0], SW[1]);
                    return UID;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="index">块索引</param>
        /// <returns>返回UID字节数组</returns>
        public byte[] ReadBlock(int index)
        {
            byte[] block=new byte[16];
            byte[] MessageBuffer = { 0xFF, 0xB0, 0x00, (byte)index, 16};
            byte[] SW = new byte[2];
            try
            {
                block = SendAPDU(MessageBuffer, SW);
                if ((SW[0] | SW[1]) == 0x90)
                {
                    return block;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="index">块索引</param>
        /// <param name="block">要写入的数据</param>
        /// <returns>返回读取的数据</returns>
        public bool WriteBlock(int index,byte[] block)
        {
            if (block.Length > 16)
            {
                return false;
            }

           //byte[] receiveMessageByte;
            byte[] MessageBuffer = new byte[5 + block.Length];
            byte[] SW = new byte[2];
            MessageBuffer[0] = 0xFF;
            MessageBuffer[1] = 0xD6;
            MessageBuffer[2] = 0x00;
            MessageBuffer[3] = (byte)index;
            MessageBuffer[4] = (byte)block.Length;
            block.CopyTo(MessageBuffer, 5);

            try
            {
               SendAPDU(MessageBuffer, SW);
                if ((SW[0] | SW[1]) == 0x90)
                {
                 
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;

        }
        /// <summary>
        /// 验证块
        /// </summary>
        /// <param name="Key">Key</param>
        /// <param name="KeyStore">Key存储编号</param>
        /// <param name="index">块索引</param>
        /// <param name="KeyType">Key类型</param>
        /// <returns></returns>
        public bool AuthBlock(string Key, int KeyStore ,int index, KeyType KeyType)
        {
            return LoadKey(Key, KeyStore) && AuthKey(index, KeyStore, KeyType);
        }
        public bool LoadKey(string Key, int KeyStore)
        {
            byte[] KeyBuffer = StringToByteSequence(Key);
            bool ret = LoadKey(KeyBuffer,KeyStore);
            return ret;
        }
        /// <summary>
        /// 加载Key
        /// </summary>
        /// <param name="Key">Key</param>
        /// <param name="KeyStore">Key存储编号</param>
        /// <returns></returns>
        private bool LoadKey(byte[] Key,int KeyStore)
        {
          //  byte[] receiveMessageByte;
            byte[] MessageBuffer = new byte[5 + Key.Length];
            byte[] SW = new byte[2];
            MessageBuffer[0] = 0xFF;
            MessageBuffer[1] = 0x82;
            MessageBuffer[2] = 0x00;
            MessageBuffer[3] = (byte)KeyStore; //KeyStore
            MessageBuffer[4] = 0x06;


            Key.CopyTo(MessageBuffer,5);
            
            try
            {
                 SendAPDU(MessageBuffer, SW);

                if ((SW[0] | SW[1]) == 0x90)
                {
                   // Console.WriteLine("LoadKey Success");
                    return true;
                }
            }
            catch (Exception e)
            {
                
               // Console.WriteLine("Exception LoadKey Flase[{0:X2},{0:X2}]",SW[0],SW[1]);
                return false;
            }
           // Console.WriteLine("LoadKey Flase[{0:X2},{0:X2}]", SW[0], SW[1]);
            return false;
        }
        /// <summary>
        /// 验证Key
        /// </summary>
        /// <param name="index">块索引</param>
        /// <param name="KeyStore">Key存储编号</param>
        /// <param name="KeyType">Key类型</param>
        /// <returns></returns>
        public bool AuthKey(int index, int KeyStore, KeyType KeyType)
        {
            byte[] receiveMessageByte;
            byte[] MessageBuffer = { 0xFF, 0x86, 0x00, 0x00, 0x05, 0x01, 0x00,(byte)index, (byte)KeyType,(byte)KeyStore};
            byte[] SW = new byte[2];
            try
            {
                receiveMessageByte = SendAPDU(MessageBuffer, SW);
                if ((SW[0] | SW[1]) == 0x90)
                {
                   // Console.WriteLine("AuthKey Success");
                    return true;
                }
            }
            catch (Exception e)
            {
               // Console.WriteLine("AuthKey Flase");
                return false;
            }
           // Console.WriteLine("AuthKey Flase");
            return false;
        }
        /// <summary>
        /// 更改设备LED状态（然并卵）
        /// </summary>
        public void ControlLED(int P2, int T1, int T2, int count, int buzzer)
        {
            byte[] MessageBuffer = { 0xFF, 0x00, 0x40, (byte)P2, 0x04, (byte)T1, (byte)T2, (byte)count, (byte)buzzer};
            byte[] SW = new byte[2];
            try
            {
                 SendAPDU(MessageBuffer, SW);

            }
            catch (Exception e)
            {
                
            }
            
        }

        public byte[] StringToByteSequence(string sourceString)
        {
            int i = 0, n = 0;
            int j = (sourceString.Length) / 2;

            byte[] a = new byte[j];
            for (i = 0, n = 0; n < j; i += 2, n++)
            {
                a[n] = Convert.ToByte(sourceString.Substring(i, 2), 16);
            }
            return a;
        }
        public string ByteSequenceToString(byte[] sourceByteSequence, string interval)
        {
            string temp = null;
            if (sourceByteSequence != null)
            {
                for (int i = 0; i < sourceByteSequence.Length; i++)
                {
                    temp += string.Format("{0:X2}", sourceByteSequence[i]) + interval;
                }
            }
            return temp;
        }
    }

}
