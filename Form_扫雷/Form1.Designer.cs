
namespace Form_扫雷
{
    partial class Form1
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
            this.mineControl13 = new Form_扫雷.MineControl1();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mineControl13
            // 
            this.mineControl13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mineControl13.Location = new System.Drawing.Point(4, 60);
            this.mineControl13.Name = "mineControl13";
            this.mineControl13.Size = new System.Drawing.Size(977, 894);
            this.mineControl13.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 953);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mineControl13);
            this.Name = "Form1";
            this.Text = "扫雷";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion


        private MineControl1 mineControl13;
        private System.Windows.Forms.Button button1;
    }
}

