using OverlayDemo.Core;
using OverlayDemo.Entity;
using OverlayDemo.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverlayDemo
{
    public partial class Form1 : Form
    {
        //1为点线关系判断   
        //2位多边形裁剪
        private int authorFlag=0;
        //1为点 2为单线 3为多边形 4为连续线
        private int drawFlag=0;
        IGraphCore graphCore = null;
        ICutCore cutCore = null;
        private List<MyPoint> points=new List<MyPoint>();
        private List<MyPolyline> polyLines=new List<MyPolyline>();
        private MyPolygon polygon = new MyPolygon()
        {
            Polylines = new List<MyPolyline>()
        };
        private MyPoint recordPoint;
        public Form1()
        {
            InitializeComponent();
            graphCore = new GraphCoreClass();
            cutCore = new CutCoreClass(graphCore);
            Bitmap bitMap = new Bitmap(this . pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = bitMap;
        }

        private void 显示文本框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = !this.panel1.Visible;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (authorFlag == 0)
            {
                return;
            }
            MouseEventArgs e1 = e as MouseEventArgs;
            Bitmap map = new Bitmap(this.pictureBox1.Image);
            Graphics graphics = Graphics.FromImage(map);
            MyPoint point = new MyPoint(e1.X, e1.Y);
            if (e1.Button == MouseButtons.Left)
            {
                if (authorFlag == 1)
                {
                    if (drawFlag == 1)
                    {
                        DrawHelper.DrawPoint(graphics, e1.X, e1.Y, this.points.Count + 1);
                        this.points.Add(point);
                    }
                    else if (drawFlag == 2)
                    {
                        DrawContinuLine(graphics, point, this.polyLines, true);
                    }
                }
                else if (authorFlag == 2)
                {
                    if (drawFlag == 2)
                    {
                        DrawContinuLine(graphics, point,this.polyLines,false);
                        this.points.Add(point);
                    }
                    else if (drawFlag == 3)
                    {
                        DrawContinuLine(graphics, point, this.polygon.Polylines, false);
                    }
                }
            }
            else if (e1.Button == MouseButtons.Right)
            {
                if (authorFlag == 2 && drawFlag == 3)
                {
                    if (this.polygon.Polylines.Count > 0)
                    {
                        MyPoint point2 = this.polygon.Polylines[this.polygon.Polylines.Count - 1].Point2;
                        MyPoint point1= this.polygon.Polylines[0].Point1;
                        DrawHelper.DrawLine(graphics, point2.X, point2.Y, point1.X, point1.Y);
                        this.polygon.Polylines.Add(new MyPolyline(point2, point1));
                    }
                }
            }
            this.pictureBox1.Image = map;
        }

        private void DrawContinuLine(Graphics graphics, MyPoint point,List<MyPolyline> polylineParam,bool isShowNum)
        {
            if (recordPoint != null)
            {
                if (isShowNum)
                {
                    DrawHelper.DrawLine(graphics, recordPoint.X, recordPoint.Y, point.X, point.Y, polylineParam.Count + 1);
                }
                else
                {
                    DrawHelper.DrawLine(graphics, recordPoint.X, recordPoint.Y, point.X, point.Y);
                }
                polylineParam.Add(new MyPolyline(recordPoint, point));
            }
            this.recordPoint = point;
        }
        private void 多边形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Initial(2, 3, new Pen(Color.Yellow, 2));
        }

        private void Initial(int author,int draw,Pen pen)
        {
            authorFlag = author;
            drawFlag = draw;
            recordPoint = null;
            DrawHelper.MyPen = pen;
        }

        private void 画线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Initial(2, 2, new Pen(Color.Red, 2));
        }

        private void 裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Initial(0, 4, new Pen(Color.LimeGreen, 5));
            Bitmap map = new Bitmap(this.pictureBox1.Image);
            Graphics graphic=Graphics.FromImage(map);
            List<List<MyPoint>> reaultArrays=cutCore.CutAlgorithm(this.polygon, this.points);
            for(int i=0;i<reaultArrays.Count;i++)
            {
                List<MyPoint> tempArray=reaultArrays[i];
                MyPoint point1 = tempArray.Count > 0 ? tempArray[0] : null;
                for(int j=1;j<tempArray.Count;j++)
                {
                    MyPoint point2=tempArray[j];
                    DrawHelper.DrawLine(graphic, point1.X, point1.Y, point2.X, point2.Y);
                    point1 = point2;
                }
            }
            this.pictureBox1.Image = map;
        }

        private void 画点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Initial(1, 1, new Pen(Color.SkyBlue, 2));
        }

        private void 画线ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Initial(1, 2, new Pen(Color.LightPink, 2));
        }

        private void 判断ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             for(int i=0;i<points.Count;i++)
             {
                 for(int j=0;j<polyLines.Count;j++)
                 {
                   int result= graphCore.JudgePointWithLine(points[i], polyLines[j]);
                   ShowPointLineInfo(result, i + 1, j + 1);
                 }
             }
             this.panel1.Visible = true;
             Clean();
        }
        private void ShowPointLineInfo(int result,int pointID,int lineID)
        {
            string value = this.richTextBox1.Text;
            string info = string.Format("{0}号点在{1}号线",pointID,lineID);
            if (result==1)
            {
                info += "上";
            }
            else if(result==2)
            {
                info += "延长线上";
            }
            else if(result==3)
            {
                info += "右方";
            }
            else if(result==4)
            {
                info += "左方";
            }
            this.richTextBox1.Text = value + info + "\r\n";
        }
        private void Clean()
        {
            this.drawFlag = 0;
            this.authorFlag = 0;
            this.polygon.Polylines.Clear();
            this.points.Clear();
            this.polyLines.Clear();
            this.recordPoint = null;
        }
        private void CleanWindow()
        {
            Bitmap map = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = map;
            this.richTextBox1.Text = "";
        }
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clean();
            CleanWindow();
        }

    }
}
