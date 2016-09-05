using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kurumi
{
    public partial class Change : Form
    {
        bool IsBlank = false;
        public Change(bool IsBlank)
        {
            this.IsBlank = IsBlank;

            InitializeComponent();
            
        }

        private void Tb_Momey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                
            }
        }

        private void Change_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            if (IsBlank)
            {
                this.Text = "Kurumi - 制作热水卡";
            }
            else
            {
                this.Text = "Kurumi - 修改热水余额";

            }
        }

        private void Bt_Continue_Click(object sender, EventArgs e)
        {
            int money = Convert.ToInt32(Tb_Momey.Text.Trim());
            if (money <= 10)
            {
                MessageBox.Show("有点志气行不行", "Kurumi");
                return;
            }
            if (money >=999)
            {
                MessageBox.Show("这么浪会死的", "Kurumi");
                return;
            }
            money = Convert.ToInt32(Tb_Momey.Text.Trim() + "66");
            //api.ChangeMoney(money,IsBlank);
            if (api.ChangeMoney(money, IsBlank))
            {
                MessageBox.Show("修改成功\n刷新后即可显示", "Kurumi");
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败", "Kurumi");
            }

        }
      
        private void Bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tb_Momey_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Bt_Continue_Click(sender, null);
            }
        }
    }
}
