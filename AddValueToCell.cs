namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class AddValueToCell
    {
        private int m_addToRow;
        private int m_addToColumn;


        public AddValueToCell(int io_addToRow, int io_addToColumn)
        {
            m_addToRow = io_addToRow;
            m_addToColumn = io_addToColumn;
        }


        public int AddToRow
        {
            get
            {
                return m_addToRow;
            }

            set
            {
                m_addToRow = value;
            }
        }

        public int AddToColumn
        {
            get
            {
                return m_addToColumn;
            }

            set
            {
                m_addToColumn = value;
            }
        }
    }
}
