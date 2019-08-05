namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Move
    {
        private Location m_cellFrom;
        private Location m_cellTo;
        private Location m_cellEating;
        private bool m_IsEating = false;
        private bool m_IsKingMove = false;
        private bool m_IsCanBeEatingByThisMove = false;

        public Move(Location io_cellFrom, Location io_cellTo)
        {
            CellTo = io_cellTo;
            CellFrom = io_cellFrom;
        }

        public Location CellTo
        {
            get
            {
                return m_cellTo;
            }

            set
            {
                m_cellTo = value;
            }
        }

        public bool IsEating
        {
            get
            {
                return m_IsEating;
            }

            set
            {
                m_IsEating = value;
            }
        }

       

        public Location CellFrom
        {
            get
            {
                return m_cellFrom;
            }

            set
            {
                m_cellFrom = value;
            }
        }

        public bool IsKingMove
        {
            get
            {
                return m_IsKingMove;
            }

            set
            {
                m_IsKingMove = value;
            }
        }

        public bool IsCanBeEatingByThisMove
        {
            get
            {
                return m_IsCanBeEatingByThisMove;
            }

            set
            {
                m_IsCanBeEatingByThisMove = value;
            }
        }

        public Location CellEating
        {
            get
            {
                return m_cellEating;
            }

            set
            {
                m_cellEating = value;
            }
        }

        internal bool IsSameMove(Move i_move)
        {
            return m_cellTo.IsLocationsEqual(i_move.CellTo) && m_cellFrom.IsLocationsEqual(i_move.m_cellFrom);
        }
    }
}
