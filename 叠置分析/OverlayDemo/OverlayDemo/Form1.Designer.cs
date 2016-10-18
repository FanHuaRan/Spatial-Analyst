namespace OverlayDemo
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.多边形裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多边形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点线关系判断ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.判断ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示文本框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.多边形裁剪ToolStripMenuItem,
            this.点线关系判断ToolStripMenuItem,
            this.显示文本框ToolStripMenuItem,
            this.清除ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(698, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 多边形裁剪ToolStripMenuItem
            // 
            this.多边形裁剪ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画线ToolStripMenuItem,
            this.多边形ToolStripMenuItem,
            this.裁剪ToolStripMenuItem});
            this.多边形裁剪ToolStripMenuItem.Name = "多边形裁剪ToolStripMenuItem";
            this.多边形裁剪ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.多边形裁剪ToolStripMenuItem.Text = "多边形裁剪";
            // 
            // 画线ToolStripMenuItem
            // 
            this.画线ToolStripMenuItem.Name = "画线ToolStripMenuItem";
            this.画线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.画线ToolStripMenuItem.Text = "画线";
            this.画线ToolStripMenuItem.Click += new System.EventHandler(this.画线ToolStripMenuItem_Click);
            // 
            // 多边形ToolStripMenuItem
            // 
            this.多边形ToolStripMenuItem.Name = "多边形ToolStripMenuItem";
            this.多边形ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.多边形ToolStripMenuItem.Text = "画多边形";
            this.多边形ToolStripMenuItem.Click += new System.EventHandler(this.多边形ToolStripMenuItem_Click);
            // 
            // 裁剪ToolStripMenuItem
            // 
            this.裁剪ToolStripMenuItem.Name = "裁剪ToolStripMenuItem";
            this.裁剪ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.裁剪ToolStripMenuItem.Text = "裁剪";
            this.裁剪ToolStripMenuItem.Click += new System.EventHandler(this.裁剪ToolStripMenuItem_Click);
            // 
            // 点线关系判断ToolStripMenuItem
            // 
            this.点线关系判断ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画点ToolStripMenuItem,
            this.画线ToolStripMenuItem1,
            this.判断ToolStripMenuItem});
            this.点线关系判断ToolStripMenuItem.Name = "点线关系判断ToolStripMenuItem";
            this.点线关系判断ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.点线关系判断ToolStripMenuItem.Text = "点线关系判断";
            // 
            // 画点ToolStripMenuItem
            // 
            this.画点ToolStripMenuItem.Name = "画点ToolStripMenuItem";
            this.画点ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.画点ToolStripMenuItem.Text = "画点";
            this.画点ToolStripMenuItem.Click += new System.EventHandler(this.画点ToolStripMenuItem_Click);
            // 
            // 画线ToolStripMenuItem1
            // 
            this.画线ToolStripMenuItem1.Name = "画线ToolStripMenuItem1";
            this.画线ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.画线ToolStripMenuItem1.Text = "画线";
            this.画线ToolStripMenuItem1.Click += new System.EventHandler(this.画线ToolStripMenuItem1_Click);
            // 
            // 判断ToolStripMenuItem
            // 
            this.判断ToolStripMenuItem.Name = "判断ToolStripMenuItem";
            this.判断ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.判断ToolStripMenuItem.Text = "判断";
            this.判断ToolStripMenuItem.Click += new System.EventHandler(this.判断ToolStripMenuItem_Click);
            // 
            // 显示文本框ToolStripMenuItem
            // 
            this.显示文本框ToolStripMenuItem.Name = "显示文本框ToolStripMenuItem";
            this.显示文本框ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.显示文本框ToolStripMenuItem.Text = "显示文本框";
            this.显示文本框ToolStripMenuItem.Click += new System.EventHandler(this.显示文本框ToolStripMenuItem_Click);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.清除ToolStripMenuItem.Text = "清除";
            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(481, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 484);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(217, 484);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(698, 484);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(698, 484);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 509);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 多边形裁剪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多边形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 裁剪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点线关系判断ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画线ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 判断ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示文本框ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
    }
}

