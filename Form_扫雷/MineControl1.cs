using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_扫雷
{
    public partial class MineControl1 : UserControl
    {
        public MineControl1()
        {
            InitializeComponent();
        }
        //扫雷成功
        public delegate void MineSweeppedSuccessfulEventHandeler(object sender, EventArgs e);
        public event MineSweeppedSuccessfulEventHandeler MineSweepped;

        //扫雷失败
        public delegate void MineSweeppedFaileEventHandelr(object sender, EventArgs e);
        public event MineSweeppedFaileEventHandelr MineSweeppedFaile;

        /// <summary>
        /// 初始化实例数量
        /// </summary>
        /// <param name="PaneLineCount">每行方格数量</param>
        /// <param name="MineCount">地雷数量</param>
        public void Init(int PaneLineCount, int MineCount)
        {
            for (int i = 0; i < PaneLineCount * PaneLineCount; i++)
            {
                Pane pane = new Pane();
                pane.MouseDown += new MouseEventHandler(pane_MouseDown);  //动态地设置事件
                this.Controls.Add(pane);
            }
            //布局所有方格位置
            LayoutPanes();
            //随机布雷
            RandomMineLaying(MineCount);
            //计算每个方格四周的地雷数
            foreach (Pane pane in this.Controls)
            {
                pane.AroundMineCounts = GetAroundMineCounts(pane);
            }

        }

        public void pane_MouseDown(object sender, EventArgs e)
        {
            
        }




        /// <summary>
        /// 当前雷区重新布局
        /// </summary>
        public void LayoutPanes()
        {
            int _paneLine = (int)Math.Sqrt(this.Controls.Count);     //行数
            int _paneWidth = this.Width / _paneLine;   //每个方格的宽度
            int _paneHeight = this.Height / _paneLine; //每个方格的高度

            int _paneIndex = 0;   //初始方格下标
            int _paneX = 0;
            int _paneY = 0;

            //方格布局
            for (int i = 0; i < _paneLine; i++)
            {
                _paneY = i * _paneHeight; //方格的纵坐标
                for (int j = 0; j < _paneLine; j++)
                {
                    _paneX = j * _paneWidth; //方格的横坐标
                    Pane newPane = this.Controls[_paneIndex] as Pane; //接收第_paneIndex个控件
                    //设置方格的大小
                    newPane.Size = new Size(_paneWidth, _paneHeight);
                    //方格的位置
                    newPane.Location = new Point(_paneX, _paneY);
                    _paneIndex++;
                }
            }


        }

        /// <summary>
        /// 查看所有方格状态
        /// </summary>
        public void ViewAll()
        {
            foreach (Pane item in this.Controls)
            {
                if (item.GetState != PaneState.Opend)
                {
                    item.Open();
                }
            }
        }

        /// <summary>
        /// 随机布雷
        /// </summary>
        /// <param name="MineCount">雷的数量</param>
        public void RandomMineLaying(int MineCount)
        {
            Random r = new Random();

            //随机给方框设置“有雷”状态
            for (int i = 0; i < MineCount; i++)
            {
                int index = r.Next(0, this.Controls.Count);
                Pane pane = (Pane)this.Controls[index];
                pane.IsMine = true;
            }
        }

        public int GetAroundMineCounts(Pane pane)
        {
            int mineCounts = 0;
            List<Pane> currentPane = GetAroundPaneCounts(pane);
            foreach (Pane searchPane in currentPane)
            {
                if (searchPane.IsMine == true)
                {
                    mineCounts += 1;
                }
            }
            return mineCounts;
            //int paneWidth = pane.Width;
            //int paneHight = pane.Height;
            //int aroundmineCounts = 0;
            //foreach (Pane searchPane in this.Controls)
            //{
            //    //   判断水平相邻的两个地块是否有雷  
            //    //如果循环到的方格的x坐标+方格的宽度等于传进来的方格x坐标，且Y坐标相等，则代表它们相邻
            //    if (searchPane.Left + paneWidth == pane.Left && searchPane.Top == pane.Top && searchPane.IsMine == true ||
            //        searchPane.Left - paneWidth == pane.Left && searchPane.Top == pane.Top && searchPane.IsMine == true)
            //    {
            //        aroundmineCounts += 1;
            //    }
            //    //   判断垂直相邻的两个地块是否有雷  
            //    if (searchPane.Top - paneHight == pane.Top && searchPane.Left == pane.Left && searchPane.IsMine == true ||
            //        searchPane.Top + paneHight == pane.Top && searchPane.Left == pane.Left && searchPane.IsMine == true)
            //    {
            //        aroundmineCounts += 1;
            //    }
            //    //判断对角是否有雷
            //    if (Math.Abs(searchPane.Top - pane.Top) == paneHight &&
            //        Math.Abs(pane.Left - searchPane.Left) == paneWidth && searchPane.IsMine == true)
            //    {
            //        aroundmineCounts += 1;
            //    }
            //}
            //return aroundmineCounts;

        }

        /// <summary>
        /// 获取当前方格周围的方格数量
        /// </summary>
        /// <param name="currentPane"></param>
        /// <returns></returns>
        public List<Pane> GetAroundPaneCounts(Pane currentPane)
        {
            List<Pane> aroundPaneCounts = new List<Pane>();
            int paneWidth = currentPane.Width;
            int paneHight = currentPane.Height;
            
            foreach (Pane searchPane in this.Controls)
            {
                //   判断水平相邻的两个地块是否有方格 
                //如果循环到的方格的x坐标+方格的宽度等于传进来的方格x坐标，且Y坐标相等，则代表它们相邻
                if (Math.Abs(searchPane.Top - currentPane.Top) == paneHight && searchPane.Left == currentPane.Left||
                    Math.Abs(searchPane.Left - currentPane.Left) == paneWidth && searchPane.Top == currentPane.Top)
                {
                    aroundPaneCounts.Add(searchPane);
                }
                //判断对角是否有雷
                if (Math.Abs(searchPane.Top - currentPane.Top) == paneHight &&
                    Math.Abs(currentPane.Left - searchPane.Left) == paneWidth && searchPane.IsMine == true)
                {
                    aroundPaneCounts.Add(searchPane);
                }
            }
            return aroundPaneCounts;
        }



    }
}
