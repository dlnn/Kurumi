using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SkinSharp;
using _SmartCardProtocol;
using System.Threading;


namespace Kurumi
{
    public partial class Frm_Main : Form
    {

        public SkinH_Net Skin;
        public Frm_Main()
        {
            LoadSkin();
            //MessageBox.Show("Main");
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Load");          
            Control.CheckForIllegalCrossThreadCalls = false;
            //new Thread(new ThreadStart(IniDevice)).Start();

        }
        private void IniDevice()
        {
            api.IsResetting = true;
            try
            {
                api.Device = api.scp.InitializeDevice();
                //Thread.Sleep(100);
            }
            catch(Exception e)
            {
            }
            if (api.Device == null)
            {

                MessageBox.Show("没有检测到设备！", "Kurumi");
                Lb_State.Text = "初始化失败";
                api.IsResetting = false;
                return;
            }
            Lb_Device.Text = api.Lb_Device_Text + api.Device[0];


            if (Lb_Device.Width > Lb_HC.Width)
            {
                this.Width = Lb_Device.Location.X * 2 + Lb_Device.Width + 12;
            }


            Lb_State.Text = "初始化成功等待放入IC卡";
            Thread.Sleep(100);

            string mess = null;


                Thread.Sleep(300);
                mess = null;
                try
                {
                    api.scp.InitializeCard(api.Device);
                }
                catch (Exception e)
                {
                    mess = e.Message;
                }
            if (mess != null)
            {
                MessageBox.Show("没有检测到IC卡\n请确认放入IC卡后点击复位", "Kurumi");
                api.scp.CloseDevice();
                api.IsResetting = false;
                return;

            }
          
            //Thread.Sleep(100);
            Lb_State.Text = "已连接上IC卡";
            Readdata();

            api.IsResetting = false;

        }
        private void Readdata()
        {

            byte[] UID = api.scp.GetUID();
          
            if (UID == null)
            {
                MessageBox.Show("初始化失败或未连接上卡片\n请将IC卡放在读卡器上再点击复位", "Kurumi");
                //Lb_State.Text = "初始化失败";
                api.IsResetting = false;
                return;
            }
            Lb_UID.Text = api.Lb_UID_Text + api.scp.ByteSequenceToString(UID, "");
            bool Sector15;
            Sector15 = api.scp.AuthBlock(api.Sector_15_KeyA, 0, 15 * 4, SmartCardProtocol.KeyType.Key_A);
            byte[] Block0 = new byte[16];
            byte[] Block1 = new byte[16];
            byte[] Block2 = new byte[16];
            byte[] Block3 = new byte[16];

            //不是校园卡 直处理15扇区
            if (Sector15) //15扇区 已被修改成热水区
            {
                Block0 = api.scp.ReadBlock(15 * 4 + 0);
                Block1 = api.scp.ReadBlock(15 * 4 + 1);
                Block2 = api.scp.ReadBlock(15 * 4 + 2);
                Block3 = api.scp.ReadBlock(15 * 4 + 3);                

                int HotMoney = System.BitConverter.ToInt32(Block0, 0);

                byte[] CurentMemorybuff = new byte[4];
                
                CurentMemorybuff[0] = Block2[15];
                CurentMemorybuff[1] = Block2[14];
                CurentMemorybuff[2] = Block2[13];
                CurentMemorybuff[3] = Block2[12];
               

                int CurrentMoney = System.BitConverter.ToInt32(CurentMemorybuff, 0);

                int CurrentNum = System.BitConverter.ToInt32(Block2, 5);

                api.money = HotMoney / 100;
                Lb_HM.Text = string.Format(api.Lb_HM_Text, HotMoney / 100.0);
                Lb_HC.Text = string.Format(api.Lb_HC_Text, CurrentMoney / 100.0);
                Lb_HC1.Text= string.Format(api.Lb_HC1_Text,CurrentNum);
                Lb_State.Text = "热水卡";

            }
            else
            {
                //15扇区不是热水区
                if (api.scp.AuthBlock(api.Sector_Default_KeyA, 1, 15 * 4+0, SmartCardProtocol.KeyType.Key_A))
                {
                    Lb_State.Text = "空白卡";
                    //15扇区为空白
                    
                }
                else
                {
                    Lb_State.Text = "未知卡";
                    //15扇区不为空白
                }
            }


        }
        private void ClearFrm()
        {
            Lb_Device.Text = api.Lb_Device_Text;
            Lb_UID.Text = api.Lb_UID_Text+ "00000000";
            Lb_HM.Text = string.Format(api.Lb_HM_Text, 0.00);
            Lb_HC.Text = string.Format(api.Lb_HC_Text, 0.00);

        }
       
        private void LoadSkin()
        {
            Skin = new SkinH_Net();
            Skin.AttachEx("skin\\skin.she", "");
            api.scp = new SmartCardProtocol();
        }



        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            api.scp.CloseDevice();
            Application.Exit();
        }

        private void Lb_Device_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show(Lb_Device.Width.ToString());
        }



        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (api.IsResetting)
            {
                MessageBox.Show("重置中", "Kurumi");
            }
            else
            {
                api.scp.CloseDevice();
                ClearFrm();
                IniDevice();
            }    

        }

        public void 更新数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (api.IsResetting)
            {
                MessageBox.Show("重置中", "Kurumi");
            }
            else
            {
                ClearFrm();
                Lb_Device.Text = api.Lb_Device_Text + api.Device[0];
                Readdata();

                MessageBox.Show( "刷新数据完毕","Kurumi");
               
            }
        }



        private void Lb_HM_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
        }

        private void Frm_Main_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                if (Lb_State.Text == "未知卡")
                {
                    MessageBox.Show("[未知卡]不能进行此操作", "Kurumi");
                    return;
                }
                if (Lb_State.Text == "热水卡")
                {
                    Change change = new Change(false);
                    change.ShowDialog();
                }
                if (Lb_State.Text == "空白卡")
                {
                    DialogResult dr = MessageBox.Show("是否将当前IC卡制作成热水卡", "Kurumi", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            Change change = new Change(true);
                            change.ShowDialog();
                            break;
                    }
                }


                }
            if (e.KeyCode == Keys.Subtract)
            {
                if (Lb_State.Text == "未知卡")
                {
                    MessageBox.Show("[未知卡]不能进行此操作", "Kurumi");
                    return;
                }
                DialogResult dr = MessageBox.Show("是否清空当前热水卡使用 量/次数", "Kurumi", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        ClearCurrent();
                        break;
                }


            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F5)
            {
                更新数据ToolStripMenuItem_Click(sender, null);
            }
        }
        public void ClearCurrent()
        {
            byte[] block = new byte[16];
            block = api.scp.ReadBlock(15 * 4 + 2);
            block[5] = 0x00;
            block[15] = 0x00;
            if (api.scp.WriteBlock(15 * 4 + 2, block))
            {
                MessageBox.Show("清空成功\n刷新数据显示","Kurumi");
            }
            else
            {
                MessageBox.Show("清空失败", "Kurumi");
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序仅为作者偷懒乱写\n不服自己写去\nEmail:kukuku@kuhakukun.cn", "Kurumi");
           
        }
        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[+] 修改金钱|制作热水卡\n[-] 清除当前使用 量/次数\n[Enrer][F5] 刷新", "Kurumi");
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            IniDevice();
        }

        private void Frm_Main_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
    class api
    {
        public static SmartCardProtocol scp;
        public static List<string> Device;
        public static bool IsResetting = false;
        public static int money = 0;
        public const string Lb_Device_Text = "Device:";
        public const string Lb_UID_Text = "UID:";
        public const string Lb_HM_Text = "当前热水余额:{0:F2}";
        public const string Lb_HC_Text = "当前热水使用量:{0:F2}/1.00";
        public const string Lb_HC1_Text = "当前热水使用量:{0}/40";
        public const string Sector_Default_KeyA = "FFFFFFFFFFFF";
        public const string Sector_15_KeyA = "010203040505";

        private static int GetSum(byte[] data)
        {
            int sum = 0;
            
            for (int i = 0;i<data.Length;i++)
            {
                               
                sum+=Convert.ToInt32(data[i]);

            }
            return sum;

        }
        private static byte GetCheckSum(byte[] block)
        {
            byte CheckSum = 0x00;
            int sum = GetSum(block);
            CheckSum = System.BitConverter.GetBytes(sum)[0];
            return CheckSum;
        }
        public static bool ChangeMoney(int Money,bool IsBlank)
        {
            byte[] Block0buff = new byte[16];

            bool bBlock0, bBlock1, bBlock2, bBlock3;

            System.BitConverter.GetBytes(Money).CopyTo(Block0buff, 0);
            Block0buff[4] = 0x10;
            Block0buff[5] = 0x27;
            Block0buff[6] = 0x00;
            Block0buff[7] = 0x01;
            Block0buff[8] = 0x00;
            Block0buff[9] = 0x00;
            Block0buff[10] = 0x01;
            Block0buff[11] = 0x01;
            Block0buff[12] = 0x01;
            Block0buff[13] = 0x01;
            Block0buff[14] = 0xF4;
            Block0buff[15] = GetCheckSum(Block0buff);
            bBlock0 = api.scp.WriteBlock(15 * 4 + 0, Block0buff);
            bBlock1 = api.scp.WriteBlock(15 * 4 + 1, Block0buff);
            if(IsBlank)
            {
                byte[] Block2buff = new byte[16];
                byte[] Block3buff = new byte[16];
                Block3buff = scp.ReadBlock(15 * 4 + 3);
                Block3buff[0] = 0x01;
                Block3buff[1] = 0x02;
                Block3buff[2] = 0x03;
                Block3buff[3] = 0x04;
                Block3buff[4] = 0x05;
                Block3buff[5] = 0x05;
                Block3buff[10] = Block3buff[0];
                Block3buff[11] = Block3buff[1];
                Block3buff[12] = Block3buff[2];
                Block3buff[13] = Block3buff[3];
                Block3buff[14] = Block3buff[4];
                Block3buff[15] = Block3buff[5];
                bBlock2 = api.scp.WriteBlock(15 * 4 + 2, Block2buff);
                bBlock3 = api.scp.WriteBlock(15 * 4 + 3, Block3buff);
                return bBlock0 && bBlock1 && bBlock2 && bBlock3;
            }
            return bBlock0 && bBlock1;

        }



    }
}
