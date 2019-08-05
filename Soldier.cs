namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Soldier
    {
        private Location m_Cell;
        private bool m_IsKing;
        private EnumCharsInBoard.enumCharsInBoard m_sign;
        private EnumDirection.enumDirection m_Direction;

        public Soldier(Location io_cell, EnumCharsInBoard.enumCharsInBoard i_sign, EnumDirection.enumDirection i_Direction)
        {
            m_Cell = io_cell;
            Sign = i_sign;
            Direction = i_Direction;
        }

        public Soldier(Location io_cell, EnumCharsInBoard.enumCharsInBoard i_sign, EnumDirection.enumDirection i_Direction, bool i_isKing)
        {
            m_Cell = io_cell;
            Sign = i_sign;
            m_IsKing = i_isKing;
            Direction = i_Direction;
        }

        public Location Cell
        {
            get
            {
                return m_Cell;
            }

            set
            {
                m_Cell = value;
            }
        }

       public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }

        internal EnumCharsInBoard.enumCharsInBoard Sign
        {
            get
            {
                return m_sign;
            }

            set
            {
                m_sign = value;
            }
        }

        internal EnumDirection.enumDirection Direction
        {
            get
            {
                return m_Direction;
            }

            set
            {
                m_Direction = value;
            }
        }

        public List<Move> GetPossibleMoves(Board io_board)
        {
            List<Move> possibleMoves = getPossibleMovesForSoldier(io_board);
            taggedMoveAsKingMoveOrIfSoldierWillBeEating(possibleMoves, io_board);
            return possibleMoves;
        }

        internal void MakeToKing()
        {
            m_IsKing = true;
            Direction = EnumDirection.enumDirection.AllDirection;
            setKingSign();
        }

        private void taggedMoveAsKingMoveOrIfSoldierWillBeEating(List<Move> io_possibleMoves, Board io_board)
        {
            foreach (Move possibleMove in io_possibleMoves)
            {
                taggedIfTheMoveIsKingMove(possibleMove, io_board);
                taggedMoveThatAfterThemTheSoldierWillBeEating(possibleMove, io_board);
            }
        }

        private void taggedMoveThatAfterThemTheSoldierWillBeEating(Move io_possibleMoves, Board io_board)
        {
            if (checkForTopCell(io_possibleMoves, io_board) || checkForBottomCell(io_possibleMoves, io_board))
            { 
                io_possibleMoves.IsCanBeEatingByThisMove = true;
            }
        }

        private bool checkForBottomCell(Move io_possibleMoves, Board io_board)
        {
            bool willBeEaten = false;
            List<AddValueToCell> bottomCells = getAddValueToCellForBottomCell();
            Location bottomCellLocation;
            Location oppositeCellLocation;

            foreach (AddValueToCell bottomCellItem in bottomCells)
            {
                bottomCellLocation = new Location((int)io_possibleMoves.CellTo.Row + bottomCellItem.AddToRow, (int)io_possibleMoves.CellTo.Column + bottomCellItem.AddToColumn);
                if (io_board.IsCellIsValid(bottomCellLocation) && io_board.CanGoBottomToTop(bottomCellLocation) && !io_board.IsCellsContainsSameGroupSoldier(io_possibleMoves.CellFrom, bottomCellLocation))
                {
                    oppositeCellLocation = new Location((int)io_possibleMoves.CellTo.Row + (bottomCellItem.AddToRow * -1), (int)io_possibleMoves.CellTo.Column + (bottomCellItem.AddToColumn * -1));
                    if (io_board.IsCellIsValid(oppositeCellLocation) && (io_board.IsCellIsEmpty(oppositeCellLocation) || io_possibleMoves.CellFrom.IsLocationsEqual(oppositeCellLocation)))
                    {
                        willBeEaten = true;
                    }
                }
            }

            return willBeEaten;
        }

        private bool checkForTopCell(Move io_possibleMove, Board io_board)
        {
            bool willBeEaten = false;
            List<AddValueToCell> topCells = getAddValueToCellForTopCell();
            Location topCellLocation;            
            Location oppositeCellLocation;

            foreach (AddValueToCell topCellItem in topCells)
            {
                topCellLocation = new Location((int)io_possibleMove.CellTo.Row + topCellItem.AddToRow, (int)io_possibleMove.CellTo.Column + topCellItem.AddToColumn);
                if (io_board.IsCellIsValid(topCellLocation) && io_board.CanGoTopToBottom(topCellLocation) && !io_board.IsCellsContainsSameGroupSoldier(io_possibleMove.CellFrom, topCellLocation))
                {
                    oppositeCellLocation = new Location((int)io_possibleMove.CellTo.Row + (topCellItem.AddToRow * -1), (int)io_possibleMove.CellTo.Column + (topCellItem.AddToColumn * -1));
                    if (io_board.IsCellIsValid(oppositeCellLocation) && io_board.IsCellIsEmpty(oppositeCellLocation))
                    {
                        willBeEaten = true;
                    }
                }
            }

            return willBeEaten;
        }

        private List<AddValueToCell> getAddValueToCellForBottomCell()
        {
            List<AddValueToCell> bottomCells = new List<AddValueToCell>();
            int io_AddToRow = -1;
            int io_AddToColumn = 1;
            AddValueToCell addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);

            bottomCells.Add(addValueToCellItem);
            io_AddToRow = -1;
            io_AddToColumn = -1;
            addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
            bottomCells.Add(addValueToCellItem);
            return bottomCells;
        }

        private List<AddValueToCell> getAddValueToCellForTopCell()
        {
            List<AddValueToCell> topCells = new List<AddValueToCell>();
            int io_AddToRow = 1;
            int io_AddToColumn = 1;
            AddValueToCell addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);

            topCells.Add(addValueToCellItem);
            io_AddToRow = 1;
            io_AddToColumn = -1;
            addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
            topCells.Add(addValueToCellItem);
            return topCells;
        }

        private void taggedIfTheMoveIsKingMove(Move io_move, Board io_board)
        {
            bool isKingMove = true;
            bool isDownCell = true;

            if (Direction == EnumDirection.enumDirection.DownToForward)
            { 
                isDownCell = false;
            }

            if (io_board.GetIFLocationIsTopOfTheBoardByDirection(isDownCell, io_move.CellTo) && !this.m_IsKing)
            {
                
                io_move.IsKingMove = isKingMove;
            }
        }

        private List<Move> getPossibleMovesForSoldier(Board io_board)
        {
            List<Move> possibleMoves = new List<Move>();
            List<AddValueToCell> addToCell = getValueToAddForCellByDirection();
            Move possibleMove;

            foreach (AddValueToCell addToCellItem in addToCell)
            {
                possibleMove = getpossibleMoveForSoldierByAddedValue(io_board, addToCellItem);
                if (possibleMove != null)
                {
                    possibleMoves.Add(possibleMove);
                }
            }

            return possibleMoves;
        }

        private void setKingSign()
        {
            if (Sign == EnumCharsInBoard.enumCharsInBoard.Player1Soldier)
            {
                Sign = EnumCharsInBoard.enumCharsInBoard.Player1King;
            }
            else if(Sign == EnumCharsInBoard.enumCharsInBoard.Player2Soldier)
            {
                Sign = EnumCharsInBoard.enumCharsInBoard.Player2King;
            }
        }

        private Move getpossibleMoveForSoldierByAddedValue(Board io_board, AddValueToCell i_addValue)
        {
            bool eating = true;
            Move possibleMove = new Move(m_Cell, new Location((EnumRowCols.enumRows)((int)m_Cell.Row + i_addValue.AddToRow), (EnumRowCols.enumCols)((int)m_Cell.Column + i_addValue.AddToColumn)));

            if (io_board.IsCellIsValid(possibleMove.CellTo))
            {
                if (!io_board.IsCellIsEmpty(possibleMove.CellTo))
                {
                    if (io_board.IsCellsContainsSameGroupSoldier(Cell, possibleMove.CellTo))
                    {
                        possibleMove = null;
                    }
                    else
                    {
                        possibleMove = new Move(m_Cell, new Location(possibleMove.CellTo.Row + i_addValue.AddToRow, possibleMove.CellTo.Column + i_addValue.AddToColumn));
                        possibleMove.IsEating = eating;

                        if (!io_board.IsCellIsValid(possibleMove.CellTo) || !io_board.IsCellIsEmpty(possibleMove.CellTo))
                        {
                            possibleMove = null;
                        }
                    }
                }
            }
            else
            {
                possibleMove = null;
            }

            return possibleMove;
        }

        private List<AddValueToCell> getValueToAddForCellByDirection()
        {
            List<AddValueToCell> addedToCellList = new List<AddValueToCell>();
            AddValueToCell addValueToCellItem;
            int io_AddToRow;
            int io_AddToColumn;

            if (Direction == EnumDirection.enumDirection.ForwardToDown || Direction == EnumDirection.enumDirection.AllDirection)
            {
                io_AddToRow = 1;
                io_AddToColumn = 1;
                addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
                addedToCellList.Add(addValueToCellItem);
            }

            if (Direction == EnumDirection.enumDirection.ForwardToDown || Direction == EnumDirection.enumDirection.AllDirection)
            {
                io_AddToRow = 1;
                io_AddToColumn = -1;
                addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
                addedToCellList.Add(addValueToCellItem);
            }

            if (Direction == EnumDirection.enumDirection.DownToForward || Direction == EnumDirection.enumDirection.AllDirection)
            {
                io_AddToRow = -1;
                io_AddToColumn = 1;
                addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
                addedToCellList.Add(addValueToCellItem);
            }

            if (Direction == EnumDirection.enumDirection.DownToForward || Direction == EnumDirection.enumDirection.AllDirection)
            {
                io_AddToRow = -1;
                io_AddToColumn = -1;
                addValueToCellItem = new AddValueToCell(io_AddToRow, io_AddToColumn);
                addedToCellList.Add(addValueToCellItem);
            }

            return addedToCellList;
        }
    }
}
