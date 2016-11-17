using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  MrFan.Tool.Delaunry;
namespace MyVoronoiDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _delaynay = new Delaynay();
            Bitmap myMap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = myMap;
        }
        //1为画点 2为包围壳
        int ShapeFlag = 0;
        //用于实现橡皮筋
        Bitmap oldMap;
        private Point m_StartPoint;
        private Delaynay _delaynay;
        //是否已经构建
        private bool isBuild = false;
        private bool _IsDown=false;
        bool isShowTri = false;
        bool isShowDelaunry = true;
        bool isShowPoint = true;
        bool isShowCirclePoint = true;
        bool isShowNum = false;
       const bool isShowBBox = true;
        #region 相关创建和显示
        //显示tin
        private void ShowTriangle(Graphics g)
        {
            if (_delaynay.DS.VerticesNum > 2)
                _delaynay.CreateTIN();
            for (int i = 0; i < _delaynay.DS.TriangleNum; i++)
            {
                Point point1 = new Point(Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V1Index].x), Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V1Index].y));
                Point point2 = new Point(Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V2Index].x), Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V2Index].y));
                Point point3 = new Point(Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V3Index].x), Convert.ToInt32(_delaynay.DS.Vertex[_delaynay.DS.Triangle[i].V3Index].y));

                Pen p = new Pen(Color.Red, 1);
                g.DrawLine(p, point1, point2);
                g.DrawLine(p, point2, point3);
                g.DrawLine(p, point1, point3);
                //显示数字标识
                if (isShowNum)
                    g.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSerif, 9), Brushes.Red,
                        (float)(point1.X + point2.X + point3.X) / 3, (float)(point1.Y + point2.Y + point3.Y) / 3);
            }
        }
        //创建泰森多边形
        private void ShowDelanury(Graphics g)
        {
            if (_delaynay.DS.TriangleNum == 0)
            {
                //生成Tin
                _delaynay.CreateTIN();
                //计算圆心
                _delaynay.CalculateBC();
            }
            //正式创建图
            _delaynay.CreateVoronoi(g);
        }
        //显示顶点
        private void ShowPoint(Graphics g)
        {
            Pen p = new Pen(Color.Red, 1);
            for (int i = 0; i <_delaynay.DS.VerticesNum; i++)
            {
                g.DrawEllipse(p, _delaynay.DS.Vertex[i].x, _delaynay.DS.Vertex[i].y, 2, 2);
                if (isShowNum)      //显示数字标识
                    g.DrawString((_delaynay.DS.Vertex[i].ID + 1).ToString(), new Font(FontFamily.GenericMonospace, 12), Brushes.Red,
                        (float)(_delaynay.DS.Vertex[i].x), (float)(_delaynay.DS.Vertex[i].y));
            }
        }
        //显示三角形外心
        private void ShowCirclePoint(Graphics g)
        {
            _delaynay.CalculateBC();    //求出每个三角形的圆心
            Pen p2 = new Pen(Color.SkyBlue, 1);
            for (int i = 0; i < _delaynay.DS.TriangleNum; i++) //显示
            {
                g.DrawEllipse(p2, Convert.ToSingle(_delaynay.DS.Barycenters[i].X), Convert.ToSingle(_delaynay.DS.Barycenters[i].Y), 3, 3);
                //显示数字标识
                if (isShowNum)     
                    g.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSerif, 12), Brushes.Black,
                        (float)(_delaynay.DS.Barycenters[i].X), (float)(_delaynay.DS.Barycenters[i].Y));
            }
        }
        private void ShowBBOX(Graphics g)
        {
            Pen myPen = new Pen(Color.Black, 1);
            int width=(int)(_delaynay.DS.BBOX.XRight-_delaynay.DS.BBOX.XLeft);
            int height=(int)(_delaynay.DS.BBOX.YBottom-_delaynay.DS.BBOX.YTop);
            g.DrawRectangle(myPen, _delaynay.DS.BBOX.XLeft, _delaynay.DS.BBOX.YTop, width, height);
        }
        //总绘图
        private void CommonDraw()
        {
            Bitmap myMap=new Bitmap(this.groupBox1.Width,this.groupBox1.Height);
            Graphics g=Graphics.FromImage(myMap);
            if(isShowBBox)
            {
                ShowBBOX(g);
            }
            if(isShowPoint)
            {
                ShowPoint(g);
            }
            if(isShowTri)
            {
                ShowTriangle(g);
            }
            if(isShowDelaunry)
            {
                ShowDelanury(g);
            }
            if(isShowCirclePoint)
            {
                ShowCirclePoint(g);
            }
            this.pictureBox1.Image=myMap;
        }
       #endregion
        #region 菜单按钮事件
        private void 包围壳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeFlag = 2;
        }

        private void 画点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeFlag = 1;
        }
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myMap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = myMap;
            _delaynay = new Delaynay();
            this.isBuild = false;
            ClearUiData();
        }
        private void 显示号码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isShowNum = !isShowNum;
            Bitmap myMap = (Bitmap)this.pictureBox1.Image;
            ShowPoint(Graphics.FromImage(myMap));
            this.pictureBox1.Image = myMap;
        }

        private void tinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isBuild)
            {
                if (DialogResult.OK == MessageBox.Show("还未构建，是否现在构建？", "警告", MessageBoxButtons.OKCancel))
                {
                    构建ToolStripMenuItem_Click(null, null);
                }
                else return;
            }
            isShowTri = !isShowTri;
            CommonDraw();
        }

        private void delaunaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (!isBuild)
            {
                if (DialogResult.OK == MessageBox.Show("还未构建，是否现在构建？", "警告", MessageBoxButtons.OKCancel)) 
                {
                    构建ToolStripMenuItem_Click(null, null);
                }
                else 
                 return;
             }
            isShowDelaunry = !isShowDelaunry;
            CommonDraw();
        }
       //圆心显示
        private void 圆心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isShowCirclePoint = !isShowCirclePoint;
            CommonDraw();
        }
        //构建按钮事件
        private void 构建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.isShowDelaunry = true;
            this.isBuild = true;
            CommonDraw();
            ShowDetail();
        }
        #endregion
        #region 交互绘图方法:包围壳、顶点
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDown)
            {
                Point point = new Point(e.X, e.Y);
                //最重要的一步  从之前的图片中创造绘图资源 可以保存之前的图画 再其基础上进行绘画
                Bitmap map = new Bitmap(oldMap);
                Graphics myGraphic = Graphics.FromImage(map);
                DrawBunDary(myGraphic, m_StartPoint, point);
                myGraphic.Dispose();
                this.pictureBox1.Image = map;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.oldMap = (Bitmap)this.pictureBox1.Image;
            if (isBuild)
            {
                if (DialogResult.OK == MessageBox.Show("已经构建成功，是否重新编辑？", "警告", MessageBoxButtons.OKCancel)) ;
                {
                    清除ToolStripMenuItem_Click(null, null);
                }
                return;
            }
            switch (ShapeFlag)
            {
                case 1:
                    //检查是否有重复点 若该点已有则不再加入
                    for (int i = 0; i < _delaynay.DS.VerticesNum; i++)
                    {
                        if ((long)e.X == _delaynay.DS.Vertex[i].x && (long)e.Y == _delaynay.DS.Vertex[i].y)
                            return;
                    }
                    //加点            
                    _delaynay.DS.Vertex[_delaynay.DS.VerticesNum].x = e.X;
                    _delaynay.DS.Vertex[_delaynay.DS.VerticesNum].y = e.Y;
                    _delaynay.DS.Vertex[_delaynay.DS.VerticesNum].ID = _delaynay.DS.VerticesNum;
                    _delaynay.DS.VerticesNum++;
                    //画点
                    this.Cursor = Cursors.Hand;
                    Bitmap myMap = (Bitmap)this.pictureBox1.Image;
                    ShowPoint(Graphics.FromImage(myMap));
                    this.pictureBox1.Image = myMap;
                    ShowDetail();
                    break;
                case 2:
                    _IsDown = true;
                    m_StartPoint = new Point(e.X, e.Y);
                    //先保存原始图片资源
                    oldMap = (Bitmap)this.pictureBox1.Image;
                    this.Cursor = Cursors.Cross;
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_IsDown)
            {
                Point point = new Point(e.X, e.Y);
                //最重要的一步  从之前的图片中创造绘图资源 可以保存之前的图画 再其基础上进行绘画
                _IsDown = false;
                //存入包围壳
                if (m_StartPoint.X < point.X)
                {
                    _delaynay.DS.BBOX.XLeft = m_StartPoint.X;
                    _delaynay.DS.BBOX.XRight = point.X;
                }
                else
                {
                    _delaynay.DS.BBOX.XLeft = point.X;
                    _delaynay.DS.BBOX.XRight = m_StartPoint.X;
                }
                if (m_StartPoint.Y < point.Y)
                {
                    _delaynay.DS.BBOX.YTop = m_StartPoint.Y;
                    _delaynay.DS.BBOX.YBottom = point.Y;
                }
                else
                {
                    _delaynay.DS.BBOX.YTop = point.Y;
                    _delaynay.DS.BBOX.YBottom = m_StartPoint.Y;
                }
            }
            this.Cursor = Cursors.Default;
        }
        void DrawBunDary(Graphics g, Point p0, Point p1)
        {
            Pen myPen = new Pen(Color.Black, 3.0f);
            int widthX = Math.Abs(p0.X - p1.X);
            int widthY = Math.Abs(p0.Y - p1.Y);
            g.DrawRectangle(myPen, p0.X, p0.Y, widthX, widthY);
        }
        #endregion
        #region 表格显示相关和清除
        private void Form1_Load(object sender, EventArgs e)
        {
            //设置两个listview样式
            pointView.Columns.Add("Point ID", 100, HorizontalAlignment.Center);
            pointView.Columns.Add("X", 100, HorizontalAlignment.Center);
            pointView.Columns.Add("Y", 100, HorizontalAlignment.Center);
            triView.Columns.Add("Triangle ID", 90, HorizontalAlignment.Center);
            triView.Columns.Add("p1", 60, HorizontalAlignment.Center);
            triView.Columns.Add("p2", 60, HorizontalAlignment.Center);
            triView.Columns.Add("p3", 60, HorizontalAlignment.Center);
            pointView.View = View.Details;
            pointView.LabelEdit = true;
            pointView.AllowColumnReorder = true;
            pointView.FullRowSelect = true;
            pointView.GridLines = true;
            pointView.Sorting = SortOrder.None;
            triView.View = View.Details;
            triView.LabelEdit = true;
            triView.AllowColumnReorder = true;
            triView.FullRowSelect = true;
            triView.GridLines = true;
            triView.Sorting = SortOrder.None;
        }
        //表格输出详细信息
        private void ShowDetail()
        {
            this.pointLab.Text = this._delaynay.DS.VerticesNum.ToString();
            this.TriLab.Text = this._delaynay.DS.TriangleNum.ToString();
            this.pointView.Items.Clear();
            this.triView.Items.Clear();
            //顶点显示
            for(int i=0;i<_delaynay.DS.VerticesNum;i++)
            {
                ListViewItem item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(_delaynay.DS.Vertex[i].x.ToString());
                item.SubItems.Add(_delaynay.DS.Vertex[i].y.ToString());
                pointView.Items.Add(item);
            }
            //三角形显示
            if(this.isBuild)
            {
                for (int i = 0; i < _delaynay.DS.TriangleNum; i++)
                {
                    ListViewItem item = new ListViewItem((i + 1).ToString());
                    item.SubItems.Add(_delaynay.DS.Triangle[i].V1Index.ToString());
                    item.SubItems.Add(_delaynay.DS.Triangle[i].V2Index.ToString());
                    item.SubItems.Add(_delaynay.DS.Triangle[i].V3Index.ToString());
                    triView.Items.Add(item);
                }
            }
        }
        //清除页面的数据
        void ClearUiData()
        {
            this.pointLab.Text = "0";
            this.TriLab.Text = "0";
            this.pointView.Items.Clear();
            this.triView.Items.Clear();
        }
        private void moreBtt_Click(object sender, EventArgs e)
        {
            this.detal_panel.Visible = !this.detal_panel.Visible;
        }
        #endregion
    }
}
