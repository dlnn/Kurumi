namespace Kurumi
{
    partial class Change
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Tb_Momey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Bt_Continue = new System.Windows.Forms.Button();
            this.Bt_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "余额:";
            // 
            // Tb_Momey
            // 
            this.Tb_Momey.BackColor = System.Drawing.SystemColors.Window;
            this.Tb_Momey.Location = new System.Drawing.Point(60, 12);
            this.Tb_Momey.MaxLength = 3;
            this.Tb_Momey.Name = "Tb_Momey";
            this.Tb_Momey.Size = new System.Drawing.Size(100, 21);
            this.Tb_Momey.TabIndex = 1;
            this.Tb_Momey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Momey_KeyPress);
            this.Tb_Momey.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Tb_Momey_PreviewKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(160, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = ".66";
            // 
            // Bt_Continue
            // 
            this.Bt_Continue.Location = new System.Drawing.Point(34, 45);
            this.Bt_Continue.Name = "Bt_Continue";
            this.Bt_Continue.Size = new System.Drawing.Size(55, 21);
            this.Bt_Continue.TabIndex = 3;
            this.Bt_Continue.Text = "确定";
            this.Bt_Continue.UseVisualStyleBackColor = true;
            this.Bt_Continue.Click += new System.EventHandler(this.Bt_Continue_Click);
            // 
            // Bt_Cancel
            // 
            this.Bt_Cancel.Location = new System.Drawing.Point(119, 45);
            this.Bt_Cancel.Name = "Bt_Cancel";
            this.Bt_Cancel.Size = new System.Drawing.Size(55, 21);
            this.Bt_Cancel.TabIndex = 4;
            this.Bt_Cancel.Text = "取消";
            this.Bt_Cancel.UseVisualStyleBackColor = true;
            this.Bt_Cancel.Click += new System.EventHandler(this.Bt_Cancel_Click);
            // 
            // Change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 78);
            this.Controls.Add(this.Bt_Cancel);
            this.Controls.Add(this.Bt_Continue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Tb_Momey);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Change";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kurumi";
            this.Load += new System.EventHandler(this.Change_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tb_Momey;
        private System.Windows.Forms.Button Bt_Continue;
        private System.Windows.Forms.Button Bt_Cancel;
    }
}