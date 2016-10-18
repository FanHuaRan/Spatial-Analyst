using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DijkstraDemo.Dijkstra;
using DijkstraDemo.Util;
using System.Diagnostics;
namespace DijkstraDemo
{
    public partial class Form1 : Form
    {
        private IDijkstra dijksta = new SimpleDijkstra();
        //1为画点 2为画线
        private int shapeType = 0;
        private Bitmap recordMap;
        private bool isDraw = false;
        private MyPoint startPoint = null;
        private List<MyPoint> points = new List<MyPoint>();
        private List<MyPolyline> polylines = new List<MyPolyline>();
        private bool isSingle=false;
        private List<List<int>> paths= null;
        private List<int> path = null;
        public Form1()
        {
            
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panel3.Visible = false;
            this.pictureBox1.Image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Sorting = SortOrder.None;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ClearResult();
        }
        #region 画图方法
        
        #endregion
        //清除计算和缓存结果
        private void ClearResult()
        {
            this.pictureBox1.Image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.listView1.Items.Clear();
            this.points.Clear();
            this.polylines.Clear();
            this.toolStripButton3.Enabled = true;
            if (this.paths != null)
            {
                this.paths = null;
            }
            if (this.path != null)
            {
                this.path = null;
            }
        }
        //计算最短路径
        private void AcculatePath()
        {
            if(String.IsNullOrEmpty(this.startTextBox.Text))
            {
                MessageBox.Show("请输入起点");
                return;
            }
            int startIndex = int.Parse(this.startTextBox.ToString())-1;
            List<List<float>> cost=UtilHelper.GetDistances(this.points,this.polylines);
            if(String.IsNullOrEmpty(this.endTextBox.Text))
            {
                AcculateAllPath(cost,startIndex);
            }
            else
            {
                int endIndex = int.Parse(this.endTextBox.ToString())-1;
                AcculateSinglePath(cost,startIndex,endIndex);
            }
            this.panel3.Visible = true;
          //  this.toolStripButton3.Enabled = false;
        }
        private void ListViewAddItem(int startIndex, int endIndex, List<int> path, float dist)
        {
            ListViewItem item = new ListViewItem((startIndex + 1).ToString());
            item.SubItems.Add((endIndex + 1).ToString());
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < path.Count; i++)
            {
                if (i != path.Count - 1)
                {
                    builder.Append((path[i] + 1).ToString() + ",");
                }
                else
                {
                    builder.Append((path[i] + 1).ToString());
                }
            }
            item.SubItems.Add(builder.ToString());
            item.SubItems.Add(dist.ToString());
            listView1.Items.Add(item);
        }
        private List<int> AcculateSinglePath(List<List<float>> cost, int startIndex, int endIndex)
        {
            float dist;
            path = dijksta.SHORTEST_PATH(cost, startIndex, endIndex, out dist);
            ListViewAddItem(startIndex, endIndex, path, dist);
            isSingle = true;
            return path;
        }
  
        private List<List<int>> AcculateAllPath(List<List<float>> cost, int startIndex)
        {
            List<float> dists;
            paths = dijksta.ALL_SHORTEST_PATH(cost, startIndex, out dists);
            for (int i = 0; i < paths.Count;i++)
            {
                if(i!=startIndex)
                {
                    ListViewAddItem(startIndex, i, paths[i], dists[i]);
                }
            }
            isSingle = false;
            return paths;
        }
    

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.paths != null)
            {
                this.paths = null;
            }
            if (this.path != null)
            {
                this.path = null;
            }
            this.listView1.Items.Clear();
            AcculatePath();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            shapeType = 1;
         //   this.recordMap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            shapeType = 2;
          //  this.recordMap = new Bitmap(pictureBox1.Image);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.recordMap = new Bitmap(this.pictureBox1.Image);
            if(shapeType==1)
            {
                MyPoint point = new MyPoint(e.X, e.Y);
                this.points.Add(point);
                Graphics graphic = Graphics.FromImage(recordMap);
                DrawHelper.DrawPoint(graphic, e.X, e.Y, points.Count);
                this.pictureBox1.Image=recordMap;
            }
            else if(shapeType==2)
            {
                 startPoint = MyPoint.LookForPoint(points, e.X, e.Y, 30);
                if(startPoint==null)
                {
                    return;
                }
                Graphics graphic = Graphics.FromImage(recordMap);
                isDraw = true;
            }
       
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(shapeType==2)
            {
                if(isDraw)
                {
                    isDraw = false;
                    Bitmap map = new Bitmap(recordMap);
                    Graphics graphic = Graphics.FromImage(map);
                    MyPoint endPoint = MyPoint.LookForPoint(points, e.X, e.Y, 30);
                    if (endPoint == null)
                    {
                        this.pictureBox1.Image = recordMap;
                        return;
                    }
                    DrawHelper.DrawLine(graphic, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                    this.pictureBox1.Image = map;
                    this.recordMap = map;
                    this.polylines.Add(new MyPolyline(startPoint, endPoint));
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (shapeType == 2)
            {
                if (isDraw)
                {
                    Bitmap map = new Bitmap(recordMap);
                    Graphics graphic = Graphics.FromImage(map);
                    DrawHelper.DrawLine(graphic, startPoint.X, startPoint.Y,e.X,e.Y);
                    this.pictureBox1.Image = map;
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem item=e.Item;
            int startIndex=int.Parse(item.SubItems[0].Text)-1;
            int endIndex=int.Parse(item.SubItems[1].Text)-1;
            Bitmap map = new Bitmap(recordMap);
            Graphics graphic = Graphics.FromImage(map);
            if(isSingle)
            {
              List<Point> drawPoints=new List<Point>();
              for (int i = 0; i < path.Count;i++ )
              {
                  drawPoints.Add(new Point(points[path[i]].X, points[path[i]].Y));
              }
              DrawHelper.DrawPath(graphic, drawPoints);
            }
            else
            {
                List<Point> drawPoints = new List<Point>();
                List<int> myPath = paths[endIndex];
                for (int i = 0; i < myPath.Count; i++)
                {
                    drawPoints.Add(new Point(points[myPath[i]].X, points[myPath[i]].Y));
                }
                DrawHelper.DrawPath(graphic, drawPoints);
            }
            this.pictureBox1.Image = map;
        }

    

  
    }
}
