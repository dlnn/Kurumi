using System;
using System.Runtime.InteropServices;


namespace _SmartCardProtocol
{
    public class PCSC
    {

        public enum SCARD_SCPOE : int
        {
            USER = 0x00000000,
            TERMAINAL = 0x00000001,
            SYSTEM = 0x00000002
        }

        public class SCARD_IO_REQUEST
        {
            public int dwProtocol;
            public int cbPciLength;
        }
        /*===========================================================
		'   Error Codes
		'===========================================================*/
        public const int SCARD_F_INTERNAL_ERROR = -2146435071;
        public const int SCARD_E_CANCELLED = -2146435070;
        public const int SCARD_E_INVALID_HANDLE = -2146435069;
        public const int SCARD_E_INVALID_PARAMETER = -2146435068;
        public const int SCARD_E_INVALID_TARGET = -2146435067;
        public const int SCARD_E_NO_MEMORY = -2146435066;
        public const int SCARD_F_WAITED_TOO_LONG = -2146435065;
        public const int SCARD_E_INSUFFICIENT_BUFFER = -2146435064;
        public const int SCARD_E_UNKNOWN_READER = -2146435063;


        public const int SCARD_E_TIMEOUT = -2146435062;
        public const int SCARD_E_SHARING_VIOLATION = -2146435061;
        public const int SCARD_E_NO_SMARTCARD = -2146435060;
        public const int SCARD_E_UNKNOWN_CARD = -2146435059;
        public const int SCARD_E_CANT_DISPOSE = -2146435058;
        public const int SCARD_E_PROTO_MISMATCH = -2146435057;


        public const int SCARD_E_NOT_READY = -2146435056;
        public const int SCARD_E_INVALID_VALUE = -2146435055;
        public const int SCARD_E_SYSTEM_CANCELLED = -2146435054;
        public const int SCARD_F_COMM_ERROR = -2146435053;
        public const int SCARD_F_UNKNOWN_ERROR = -2146435052;
        public const int SCARD_E_INVALID_ATR = -2146435051;
        public const int SCARD_E_NOT_TRANSACTED = -2146435050;
        public const int SCARD_E_READER_UNAVAILABLE = -2146435049;
        public const int SCARD_P_SHUTDOWN = -2146435048;
        public const int SCARD_E_PCI_TOO_SMALL = -2146435047;

        public const int SCARD_E_READER_UNSUPPORTED = -2146435046;
        public const int SCARD_E_DUPLICATE_READER = -2146435045;
        public const int SCARD_E_CARD_UNSUPPORTED = -2146435044;
        public const int SCARD_E_NO_SERVICE = -2146435043;
        public const int SCARD_E_SERVICE_STOPPED = -2146435042;

        public const int SCARD_W_UNSUPPORTED_CARD = -2146435041;
        public const int SCARD_W_UNRESPONSIVE_CARD = -2146435040;
        public const int SCARD_W_UNPOWERED_CARD = -2146435039;
        public const int SCARD_W_RESET_CARD = -2146435038;
        public const int SCARD_W_REMOVED_CARD = -2146435037;

        public const int SCARD_S_SUCCESS = 0;
        /// <summary>
        /// 创建读卡器设备管理上下文
        /// </summary>
        /// <param name="dwScope">读卡器可使用范围，SCARD_SCPOE.USER为当前用户,
        /// TERMAINAL未知，SYSTEM为当前系统所有用户</param>
        /// <param name="reserverd1">保留，必须为0</param>
        /// <param name="reserverd2">保留，必须为0</param>
        /// <param name="hContext">传入传出参数，设备管理器句柄</param>
        /// <returns></returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardEstablishContext(UInt32 dwScope
                                                      , int reserverd1
                                                      , int reserverd2
                                                      , ref int hContext);

        /// <summary>
        /// 返回读卡设备列表
        /// </summary>
        /// <param name="hContext">设备管理器句柄</param>
        /// <param name="mszGroups">没什么用的参数，送null就好</param>
        /// <param name="mszReaders">设备名称集合</param>
        /// <param name="mszReadersSize">设备名称集合总长度</param>
        /// <returns>如果返回为0，则代表获取读卡设备成功</returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardListReaders(int hContext
                                                , byte[] mszGroups
                                                , byte[] mszReaders
                                                , ref int mszReadersSize);

        /// <summary>
        /// 卡片连接
        /// </summary>
        /// <param name="hContext">设备句柄</param>
        /// <param name="szReader">读卡器名称</param>
        /// <param name="dwShareMode">卡片公用模式</param>
        /// <param name="dwProtocols">协议使用模式</param>
        /// <param name="hCard">卡连接句柄</param>
        /// <param name="activeProtocol">卡运行协议</param>
        /// <returns></returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardConnect(int hContext
                                             , string szReader
                                             , int dwShareMode
                                             , int dwProtocols
                                             , ref int hCard
                                             , ref int activeProtocol);

        /// <summary>
        /// 设置卡片状态
        /// </summary>
        /// <param name="hCard">卡片句柄</param>
        /// <param name="szReaderName">设备名称</param>
        /// <param name="hContent">设备句柄</param>
        /// <param name="pdwState">卡片所处状态</param>
        /// <param name="pdwProtocol">卡片使用的协议</param>
        /// <param name="ATR">ATR字串</param>
        /// <param name="length">ATR字串长度</param>
        /// <returns></returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardStatus(int hCard
                                            , string szReaderName
                                            , ref int hContent
                                            , ref int pdwState
                                            , ref int pdwProtocol
                                            , byte[] ATR
                                            , ref int length);


        /// <summary>
        /// 向卡片发送数据
        /// </summary>
        /// <param name="hCard">卡片句柄</param>
        /// <param name="pioSendPci">指令的协议头结构的指针</param>
        /// <param name="pbSendBuffer">写卡数据</param>
        /// <param name="cbSendLength">写卡数据长度</param>
        /// <param name="pioRecvPci"></param>
        /// <param name="pbRecvBuffer">返回数据</param>
        /// <param name="pcbRecvLength"></param>
        /// <returns></returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardTransmit(
                                 int hCard,
                                 IntPtr pioSendPci,
                                 byte[] pbSendBuffer,
                                 int cbSendLength,
                                 SCARD_IO_REQUEST pioRecvPci,
                                 byte[] pbRecvBuffer,
                                 ref int pcbRecvLength
            );


        /// <summary>
        /// 释放连接
        /// </summary>
        /// <param name="hCard">调用SCardConnect获得的引用值</param>
        /// <param name="dwDisposition">
        /// SCARD_LEAVE_CARD = 0;对卡不做任何操作
        /// SCARD_RESET_CARD = 1;卡复位
        /// SCARD_UNPOWER_CARD = 2;对卡断电
        /// SCARD_EJECT_CARD = 3; 弹出卡
        /// </param>
        /// <returns>
        /// </returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardDisconnect(
                                 int hCard,
                                 int dwDisposition
            );

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="hContext">资源句柄</param>
        /// <returns></returns>
        [DllImport("WinSCard.dll")]
        public static extern int SCardReleaseContext(
                                 int hContext
            );

        [DllImport("WinSCard.dll")]

        public static extern int SCardBeginTransaction(int hContext);
        public static string GetScardErrMsg(int ReturnCode)
        {
            switch (ReturnCode)
            {
                case SCARD_E_CANCELLED:
                    return ("The action was canceled by an SCardCancel request.");
                case SCARD_E_CANT_DISPOSE:
                    return ("The system could not dispose of the media in the requested manner.");
                case SCARD_E_CARD_UNSUPPORTED:
                    return ("The smart card does not meet minimal requirements for support.");
                case SCARD_E_DUPLICATE_READER:
                    return ("The reader driver didn't produce a unique reader name.");
                case SCARD_E_INSUFFICIENT_BUFFER:
                    return ("The data buffer for returned data is too small for the returned data.");
                case SCARD_E_INVALID_ATR:
                    return ("An ATR string obtained from the registry is not a valid ATR string.");
                case SCARD_E_INVALID_HANDLE:
                    return ("The supplied handle was invalid.");
                case SCARD_E_INVALID_PARAMETER:
                    return ("One or more of the supplied parameters could not be properly interpreted.");
                case SCARD_E_INVALID_TARGET:
                    return ("Registry startup information is missing or invalid.");
                case SCARD_E_INVALID_VALUE:
                    return ("One or more of the supplied parameter values could not be properly interpreted.");
                case SCARD_E_NOT_READY:
                    return ("The reader or card is not ready to accept commands.");
                case SCARD_E_NOT_TRANSACTED:
                    return ("An attempt was made to end a non-existent transaction.");
                case SCARD_E_NO_MEMORY:
                    return ("Not enough memory available to complete this command.");
                case SCARD_E_NO_SERVICE:
                    return ("The smart card resource manager is not running.");
                case SCARD_E_NO_SMARTCARD:
                    return ("The operation requires a smart card, but no smart card is currently in the device.");
                case SCARD_E_PCI_TOO_SMALL:
                    return ("The PCI receive buffer was too small.");
                case SCARD_E_PROTO_MISMATCH:
                    return ("The requested protocols are incompatible with the protocol currently in use with the card.");
                case SCARD_E_READER_UNAVAILABLE:
                    return ("The specified reader is not currently available for use.");
                case SCARD_E_READER_UNSUPPORTED:
                    return ("The reader driver does not meet minimal requirements for support.");
                case SCARD_E_SERVICE_STOPPED:
                    return ("The smart card resource manager has shut down.");
                case SCARD_E_SHARING_VIOLATION:
                    return ("The smart card cannot be accessed because of other outstanding connections.");
                case SCARD_E_SYSTEM_CANCELLED:
                    return ("The action was canceled by the system, presumably to log off or shut down.");
                case SCARD_E_TIMEOUT:
                    return ("The user-specified timeout value has expired.");
                case SCARD_E_UNKNOWN_CARD:
                    return ("The specified smart card name is not recognized.");
                case SCARD_E_UNKNOWN_READER:
                    return ("The specified reader name is not recognized.");
                case SCARD_F_COMM_ERROR:
                    return ("An internal communications error has been detected.");
                case SCARD_F_INTERNAL_ERROR:
                    return ("An internal consistency check failed.");
                case SCARD_F_UNKNOWN_ERROR:
                    return ("An internal error has been detected, but the source is unknown.");
                case SCARD_F_WAITED_TOO_LONG:
                    return ("An internal consistency timer has expired.");
                case SCARD_S_SUCCESS:
                    return ("No error was encountered.");
                case SCARD_W_REMOVED_CARD:
                    return ("The smart card has been removed, so that further communication is not possible.");
                case SCARD_W_RESET_CARD:
                    return ("The smart card has been reset, so any shared state information is invalid.");
                case SCARD_W_UNPOWERED_CARD:
                    return ("Power has been removed from the smart card, so that further communication is not possible.");
                case SCARD_W_UNRESPONSIVE_CARD:
                    return ("The smart card is not responding to a reset.");
                case SCARD_W_UNSUPPORTED_CARD:
                    return ("The reader cannot communicate with the card, due to ATR string configuration conflicts.");
                default:
                    return ("?");
            }
        }
    }
}
