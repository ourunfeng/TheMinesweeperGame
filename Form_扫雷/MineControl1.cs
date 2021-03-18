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
            
            Pane pane = (Pane)sender;
            this.CleaningPanes(pane);
            if (pane.IsMine)
            {
                MessageBox.Show("你触雷了！");
                pane.Open();
            }
            
        }
        public void CleaningPanes(Pane Selectpane)
        {
            //如果被选择的方格属于开启状态或者雷，则返回
            if (Selectpane.GetState == PaneState.Opend || Selectpane.IsMine == true)
            {
                return;
            }
            Selectpane.Open();
            List<Pane> aroundPaneCounts = new List<Pane>();
            aroundPaneCounts = GetAroundPaneCounts(Selectpane);
            //遍历被选中方格的周围方格
            foreach (Pane pane in aroundPaneCounts)
            {
                //如果周围的地雷数等于0，开启选中的方格
                if (GetAroundMineCounts(pane) == 0)
                {
                    CleaningPanes(pane);

                }
                //否则如果被选中的方格不是开启状态并且没有雷，那么开启
                else
                {
                    if (Selectpane.GetState != PaneState.Opend && Selectpane.IsMine != true)
                    {
                        Selectpane.Open();
                    }
                }
            }

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
