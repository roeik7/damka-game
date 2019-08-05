namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class Location
    {
        private EnumRowCols.enumCols m_column;
        private EnumRowCols.enumRows m_row;
        
        public Location(EnumRowCols.enumRows i_row, EnumRowCols.enumCols i_column)
        {
            Row = i_row;
            Column = i_column;
        }

        public Location(int i_row, int i_column)
        {
            Row = (EnumRowCols.enumRows)i_row;
            Column = (EnumRowCols.enumCols)i_column;
        }

        public EnumRowCols.enumCols Column
        {
            get
            {
                return m_column;
            }

            set
            {
                m_column = value;
            }
        }

        public EnumRowCols.enumRows Row
        {
            get
            {
                return m_row;
            }

            set
            {
                m_row = value;
            }
        }

        public bool IsLocationsEqual(Location other)
        {
            return m_column == other.Column && m_row == other.Row;
        }
    }
}
