namespace MyVoronoiDemo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.detal_panel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pointView = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.triView = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.画点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.包围壳ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.构建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delaunaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆心ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示号码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pointLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TriLab = new System.Windows.Forms.Label();
            this.moreBtt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.detal_panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.detal_panel);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("仿宋", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(901, 598);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "绘图区";
            // 
            // detal_panel
            // 
            this.detal_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detal_panel.Controls.Add(this.tabControl1);
            this.detal_panel.Location = new System.Drawing.Point(573, 3);
            this.detal_panel.Name = "detal_panel";
            this.detal_panel.Size = new System.Drawing.Size(322, 582);
            this.detal_panel.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(322, 582);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pointView);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(314, 554);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "顶点";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pointView
            // 
            this.pointView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointView.Location = new System.Drawing.Point(3, 3);
            this.pointView.Name = "pointView";
            this.pointView.Size = new System.Drawing.Size(308, 548);
            this.pointView.TabIndex = 0;
            this.pointView.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.triView);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(314, 554);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "三角形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // triView
            // 
            this.triView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triView.Location = new System.Drawing.Point(3, 3);
            this.triView.Name = "triView";
            this.triView.Size = new System.Drawing.Size(308, 548);
            this.triView.TabIndex = 0;
            this.triView.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(895, 576);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip1.Font = new System.Drawing.Font("仿宋", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画点ToolStripMenuItem,
            this.包围壳ToolStripMenuItem,
            this.构建ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.清除ToolStripMenuItem,
            this.显示号码ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(901, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 画点ToolStripMenuItem
            // 
            this.画点ToolStripMenuItem.Name = "画点ToolStripMenuItem";
            this.画点ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.画点ToolStripMenuItem.Text = "画点";
            this.画点ToolStripMenuItem.Click += new System.EventHandler(this.画点ToolStripMenuItem_Click);
            // 
            // 包围壳ToolStripMenuItem
            // 
            this.包围壳ToolStripMenuItem.Name = "包围壳ToolStripMenuItem";
            this.包围壳ToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.包围壳ToolStripMenuItem.Text = "包围壳";
            this.包围壳ToolStripMenuItem.Click += new System.EventHandler(this.包围壳ToolStripMenuItem_Click);
            // 
            // 构建ToolStripMenuItem
            // 
            this.构建ToolStripMenuItem.Name = "构建ToolStripMenuItem";
            this.构建ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.构建ToolStripMenuItem.Text = "构建";
            this.构建ToolStripMenuItem.Click += new System.EventHandler(this.构建ToolStripMenuItem_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tinToolStripMenuItem,
            this.delaunaryToolStripMenuItem,
            this.圆心ToolStripMenuItem});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // tinToolStripMenuItem
            // 
            this.tinToolStripMenuItem.Name = "tinToolStripMenuItem";
            this.tinToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.tinToolStripMenuItem.Text = "Tin";
            this.tinToolStripMenuItem.Click += new System.EventHandler(this.tinToolStripMenuItem_Click);
            // 
            // delaunaryToolStripMenuItem
            // 
            this.delaunaryToolStripMenuItem.Name = "delaunaryToolStripMenuItem";
            this.delaunaryToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.delaunaryToolStripMenuItem.Text = "Voronoi";
            this.delaunaryToolStripMenuItem.Click += new System.EventHandler(this.delaunaryToolStripMenuItem_Click);
            // 
            // 圆心ToolStripMenuItem
            // 
            this.圆心ToolStripMenuItem.Name = "圆心ToolStripMenuItem";
            this.圆心ToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.圆心ToolStripMenuItem.Text = "外心";
            this.圆心ToolStripMenuItem.Click += new System.EventHandler(this.圆心ToolStripMenuItem_Click);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.清除ToolStripMenuItem.Text = "清除";
            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // 显示号码ToolStripMenuItem
            // 
            this.显示号码ToolStripMenuItem.Name = "显示号码ToolStripMenuItem";
            this.显示号码ToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.显示号码ToolStripMenuItem.Text = "显示号码";
            this.显示号码ToolStripMenuItem.Click += new System.EventHandler(this.显示号码ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(708, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "顶点：";
            // 
            // pointLab
            // 
            this.pointLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointLab.AutoSize = true;
            this.pointLab.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pointLab.Location = new System.Drawing.Point(755, 7);
            this.pointLab.Name = "pointLab";
            this.pointLab.Size = new System.Drawing.Size(11, 12);
            this.pointLab.TabIndex = 4;
            this.pointLab.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(772, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "三角形：";
            // 
            // TriLab
            // 
            this.TriLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TriLab.AutoSize = true;
            this.TriLab.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TriLab.Location = new System.Drawing.Point(831, 7);
            this.TriLab.Name = "TriLab";
            this.TriLab.Size = new System.Drawing.Size(11, 12);
            this.TriLab.TabIndex = 6;
            this.TriLab.Text = "0";
            // 
            // moreBtt
            // 
            this.moreBtt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreBtt.Font = new System.Drawing.Font("仿宋", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moreBtt.Location = new System.Drawing.Point(857, 2);
            this.moreBtt.Name = "moreBtt";
            this.moreBtt.Size = new System.Drawing.Size(38, 23);
            this.moreBtt.TabIndex = 7;
            this.moreBtt.Text = "More";
            this.moreBtt.UseVisualStyleBackColor = true;
            this.moreBtt.Click += new System.EventHandler(this.moreBtt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 622);
            this.Controls.Add(this.moreBtt);
            this.Controls.Add(this.TriLab);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pointLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Voronoi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.detal_panel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 画点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 包围壳ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 构建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delaunaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆心ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 显示号码ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label pointLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TriLab;
        private System.Windows.Forms.Button moreBtt;
        private System.Windows.Forms.Panel detal_panel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView pointView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView triView;


    }
}

