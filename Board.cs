namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Board
    {
        private const int m_spaceLine = 2;
        private char[,] m_board;
        private int m_size;

        public Board(int i_size)
        {
            int firstLineOfEmtyRows = (i_size / 2) - 1, firstLineOfPlayerTwo = (i_size / 2) + 1;
            m_board = new char[i_size, i_size];

            Size = i_size;
            for (int i = firstLineOfEmtyRows; i < firstLineOfEmtyRows + m_spaceLine; i++)
            {
                for (int j = 0; j < i_size; j++)
                {
                    m_board[i, j] = (char)EnumCharsInBoard.enumCharsInBoard.EmptyCell;
                }
            }
        }

        public int Size
        {
            get
            {
                return m_size;
            }

            set
            {
                m_size = value;
            }
        }
        
        public char GetSpecificCell(int i_row, int i_col)
        {
            return m_board[i_row, i_col];
        }
        
        public void SetSpecificCell(char i_charToUpdate, int i_row, int i_column)
        {
            m_board[i_row, i_column] = i_charToUpdate;
        }

        public void SetSpecificCell(char i_charToUpdate, Location i_cellToUpdate)
        {
            m_board[(int)i_cellToUpdate.Row, (int)i_cellToUpdate.Column] = i_charToUpdate;
        }

        public bool IsCellIsValid(Location io_cellLocation)
        {
            return (int)io_cellLocation.Column >= 0 && (int)io_cellLocation.Column < Size && (int)io_cellLocation.Row >= 0 && (int)io_cellLocation.Row < Size;
        }

        public bool IsCellIsEmpty(Location io_cellLocation)
        {
            return IsCellIsValid(io_cellLocation) && m_board[(int)io_cellLocation.Row, (int)io_cellLocation.Column] == (char)EnumCharsInBoard.enumCharsInBoard.EmptyCell;
        }

        public bool IsCellsContainsSameGroupSoldier(Location io_FromCell, Location io_ToCell)
        {
            bool isSameGroupSoldier;

            if (m_board[(int)io_FromCell.Row, (int)io_FromCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1King || m_board[(int)io_FromCell.Row, (int)io_FromCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1Soldier)
            {
                isSameGroupSoldier = m_board[(int)io_ToCell.Row, (int)io_ToCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1King || m_board[(int)io_ToCell.Row, (int)io_ToCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1Soldier;
            }
            else
            {
                isSameGroupSoldier = m_board[(int)io_ToCell.Row, (int)io_ToCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player2King || m_board[(int)io_ToCell.Row, (int)io_ToCell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player2Soldier;
            }

            return isSameGroupSoldier;
        }

        public bool CanGoBottomToTop(Location io_Cell)
        {
            bool BottomToTop = false;

            if (m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1King || m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1Soldier || m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player2King)
            {
                BottomToTop = true;
            }

            return BottomToTop;
    }

        public EnumCharsInBoard.enumCharsInBoard GetCellContent(Location i_CellLocation)
        {
            return EnumCharsInBoard.Parse(m_board[(int)i_CellLocation.Row, (int)i_CellLocation.Column]);
        }

        public bool CanGoTopToBottom(Location io_Cell)
        {
            bool TopToBottom = false;
            if (m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player1King || m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player2Soldier || m_board[(int)io_Cell.Row, (int)io_Cell.Column] == (char)EnumCharsInBoard.enumCharsInBoard.Player2King)
            { 
                TopToBottom = true;
            }

            return TopToBottom;
        }

        public char GetSpecificCell(Location cell)
        {
            return GetSpecificCell((int)cell.Row, (int)cell.Column);
        }

        internal bool GetIFLocationIsTopOfTheBoardByDirection(bool i_isDownCells, Location i_cell)
        {
            bool isLocationIsInTheTopOfTheBoard;
            int zero = 0;

            if (i_isDownCells)
            {
                isLocationIsInTheTopOfTheBoard = (int)i_cell.Row == m_size - 1;
            }
            else
            {
                isLocationIsInTheTopOfTheBoard = (int)i_cell.Row == zero;
            }

            return isLocationIsInTheTopOfTheBoard;
        }
    }
}