namespace B18_Ex05_WinForm
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using Logic = B18_Ex05_Logic;

    public class ButtonCell : Button
    {
        private Logic.Location m_CellLocation;
        private bool m_IsClicked;
        private int m_LeftLocation;
        private int m_TopLocation;
        private Timer m_Timer;

        public ButtonCell(Logic.Location i_CellLocation)
        {
            m_Timer = new Timer();
            m_CellLocation = i_CellLocation;
            m_IsClicked = false;
        }

        public Logic.Location CellLocation
        {
            get
            {
                return m_CellLocation;
            }
        }

        public bool IsClicked
        {
            get
            {
                return m_IsClicked;
            }

            set
            {
                m_IsClicked = value;
            }
        }

        public int LeftLocation
        {
            get
            {
                return m_LeftLocation;
            }

            set
            {
                m_LeftLocation = value;
            }
        }

        public int TopLocation
        {
            get
            {
                return m_TopLocation;
            }

            set
            {
                m_TopLocation = value;
            }
        }

        public void MoveButtonToLocation(int i_BoardSize)
        {
            m_Timer.Interval = 2;
            m_Timer.Tick += new System.EventHandler(this.moveButtonToPositionPerTime);
            this.m_Timer.Start();
        }

        private void moveButtonToPositionPerTime(object sender, EventArgs e)
        {
            if (this.Left < this.m_LeftLocation)
            {
                    this.Left += 1;
            }
            else
            {
                this.m_Timer.Stop();
            }
        }
    }
}
