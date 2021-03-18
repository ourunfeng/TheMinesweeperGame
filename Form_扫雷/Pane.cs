using Form_扫雷.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_扫雷
{
    public enum PaneState
    {
        Chosed,
        Opend,
        Marked

    }
    public class Pane : Button
    {
        public Pane()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private bool _isMine;
        private bool _isOpen;
        private PaneState _State;
        private int _aroundMineCounts;

        public bool IsMine { get => _isMine; set => _isMine = value; }
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }
        public PaneState GetState { get => _State; set => _State = value; }
        public int AroundMineCounts { get => _aroundMineCounts; set => _aroundMineCounts = value; }

        public void Open()
        {

            if (this.IsMine == true)
            {
                this.BackgroundImage = Resources.Mine;
                
                this.Enabled = false;
            }
            else
            {
                switch (this._aroundMineCounts)
                {
                    case 0:
                        this.BackgroundImage = null;
                        this.Enabled = false;
                        break;
                    case 1:
                        this.BackgroundImage = Resources.One;
                        this.Enabled = false;
                        break;
                    case 2:
                        this.BackgroundImage = Resources.Two;
                        this.Enabled = false;
                        break;
                    case 3:
                        this.BackgroundImage = Resources.Three;
                        this.Enabled = false;
                        break;
                    case 4:
                        this.BackgroundImage = Resources.Four;
                        this.Enabled = false;
                        break;
                    case 5:
                        this.BackgroundImage = Resources.Five;
                        this.Enabled = false;
                        break;
                    case 6:
                        this.BackgroundImage = Resources.Six;
                        this.Enabled = false;
                        break;
                    case 7:
                        this.BackgroundImage = Resources.Seven;
                        this.Enabled = false;
                        break;
                    case 8:
                        this.BackgroundImage = Resources.Eight;
                        this.Enabled = false;
                        break;

                }
            }
            this.GetState = PaneState.Opend;
        }

        public void Mark()
        {
            this.BackgroundImage = Resources.Mark;
            this._State = PaneState.Marked;
        }
        public void Reset()
        {
            this.BackgroundImage = null;
            this._State = PaneState.Chosed;
        }


    }
}
