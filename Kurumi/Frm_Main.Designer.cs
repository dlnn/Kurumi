namespace Kurumi
{
    partial class Frm_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.Lb_Device = new System.Windows.Forms.Label();
            this.Lb_UID = new System.Windows.Forms.Label();
            this.Lb_HM = new System.Windows.Forms.Label();
            this.Lb_HC = new System.Windows.Forms.Label();
            this.Lb_State = new System.Windows.Forms.Label();
            this.CMS_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.复位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lb_HC1 = new System.Windows.Forms.Label();
            this.CMS_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lb_Device
            // 
            this.Lb_Device.AutoSize = true;
            this.Lb_Device.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_Device.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_Device.Location = new System.Drawing.Point(12, 9);
            this.Lb_Device.Name = "Lb_Device";
            this.Lb_Device.Size = new System.Drawing.Size(64, 16);
            this.Lb_Device.TabIndex = 0;
            this.Lb_Device.Text = "Device:";
            // 
            // Lb_UID
            // 
            this.Lb_UID.AutoSize = true;
            this.Lb_UID.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_UID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_UID.Location = new System.Drawing.Point(12, 34);
            this.Lb_UID.Name = "Lb_UID";
            this.Lb_UID.Size = new System.Drawing.Size(104, 16);
            this.Lb_UID.TabIndex = 1;
            this.Lb_UID.Text = "UID:00000000";
            // 
            // Lb_HM
            // 
            this.Lb_HM.AutoSize = true;
            this.Lb_HM.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_HM.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_HM.Location = new System.Drawing.Point(12, 59);
            this.Lb_HM.Name = "Lb_HM";
            this.Lb_HM.Size = new System.Drawing.Size(112, 16);
            this.Lb_HM.TabIndex = 5;
            this.Lb_HM.Text = "热水余额:0.00";
            this.Lb_HM.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Lb_HM_PreviewKeyDown);
            // 
            // Lb_HC
            // 
            this.Lb_HC.AutoSize = true;
            this.Lb_HC.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_HC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_HC.Location = new System.Drawing.Point(12, 84);
            this.Lb_HC.Name = "Lb_HC";
            this.Lb_HC.Size = new System.Drawing.Size(200, 16);
            this.Lb_HC.TabIndex = 6;
            this.Lb_HC.Text = "当天热水使用量:0.00/1.00";
            // 
            // Lb_State
            // 
            this.Lb_State.AutoSize = true;
            this.Lb_State.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_State.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_State.Location = new System.Drawing.Point(12, 134);
            this.Lb_State.Name = "Lb_State";
            this.Lb_State.Size = new System.Drawing.Size(104, 16);
            this.Lb_State.TabIndex = 7;
            this.Lb_State.Text = "设备初始化中";
            // 
            // CMS_Main
            // 
            this.CMS_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新数据ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.复位ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.说明ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.关于ToolStripMenuItem});
            this.CMS_Main.Name = "contextMenuStrip1";
            this.CMS_Main.Size = new System.Drawing.Size(125, 110);
            // 
            // 刷新数据ToolStripMenuItem
            // 
            this.刷新数据ToolStripMenuItem.Name = "刷新数据ToolStripMenuItem";
            this.刷新数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.刷新数据ToolStripMenuItem.Text = "刷新数据";
            this.刷新数据ToolStripMenuItem.Click += new System.EventHandler(this.更新数据ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // 复位ToolStripMenuItem
            // 
            this.复位ToolStripMenuItem.Name = "复位ToolStripMenuItem";
            this.复位ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复位ToolStripMenuItem.Text = "复位";
            this.复位ToolStripMenuItem.Click += new System.EventHandler(this.重置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
            // 
            // 说明ToolStripMenuItem
            // 
            this.说明ToolStripMenuItem.Name = "说明ToolStripMenuItem";
            this.说明ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.说明ToolStripMenuItem.Text = "说明";
            this.说明ToolStripMenuItem.Click += new System.EventHandler(this.说明ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(121, 6);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // Lb_HC1
            // 
            this.Lb_HC1.AutoSize = true;
            this.Lb_HC1.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_HC1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_HC1.Location = new System.Drawing.Point(12, 109);
            this.Lb_HC1.Name = "Lb_HC1";
            this.Lb_HC1.Size = new System.Drawing.Size(176, 16);
            this.Lb_HC1.TabIndex = 8;
            this.Lb_HC1.Text = "当天热水使用次数:0/40";
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(249, 161);
            this.ContextMenuStrip = this.CMS_Main;
            this.Controls.Add(this.Lb_HC1);
            this.Controls.Add(this.Lb_State);
            this.Controls.Add(this.Lb_HC);
            this.Controls.Add(this.Lb_HM);
            this.Controls.Add(this.Lb_UID);
            this.Controls.Add(this.Lb_Device);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kurumi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.Shown += new System.EventHandler(this.Frm_Main_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Frm_Main_KeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Frm_Main_PreviewKeyDown);
            this.CMS_Main.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lb_Device;
        private System.Windows.Forms.Label Lb_UID;
        private System.Windows.Forms.Label Lb_HM;
        private System.Windows.Forms.Label Lb_HC;
        private System.Windows.Forms.Label Lb_State;
        private System.Windows.Forms.ContextMenuStrip CMS_Main;
        private System.Windows.Forms.ToolStripMenuItem 刷新数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 复位ToolStripMenuItem;
        private System.Windows.Forms.Label Lb_HC1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 说明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
    }
}

