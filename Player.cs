namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Player
    {
        // $G$ DSN-999 (-3) m_Soldiers should be readonly.
        private bool m_IsComputer;
        private string m_Name;
        private readonly List<Soldier> m_Soldiers = new List<Soldier>();
        private EnumCharsInBoard.enumCharsInBoard m_PlayerSign;
        private bool m_DoubleEatingMode = false;
        private List<Move> m_MoveForDoubleEatingMode;
        private static int m_FirstPlayerScore = 0;
        private static int m_SecondPlayerScore = 0;

        public Player(string io_name, bool io_isFirsPlayer, Board board, bool io_IsComputer, EnumCharsInBoard.enumCharsInBoard i_PlayerSign)
        {
            Name = io_name;
            setGameForPlayer(io_isFirsPlayer, board);
            IsComputer = io_IsComputer;
            PlayerSign = i_PlayerSign;
        }

        public int calculateScore()
        {
            int score = 0;

            foreach (Soldier soldier in m_Soldiers)
            {
                if (soldier.IsKing)
                { 
                    score += 4;
                }
                else
                {
                    score += 1;
                }
            }

            return score;
        }

        public static void UpdaterScore(int i_Toadd, int i_Player)
        {
            if (i_Player == 0)
            { 
                m_FirstPlayerScore += i_Toadd;
            }
            
            if(i_Player == 1)
            {
                m_SecondPlayerScore += i_Toadd;

            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public EnumCharsInBoard.enumCharsInBoard PlayerSign
        {
            get
            {
                return m_PlayerSign;
            }

            set
            {
                m_PlayerSign = value;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }

            set
            {
                m_IsComputer = value;
            }
        }

        public bool DoubleEatingMode
        {
            get
            {
               return m_DoubleEatingMode;
            }
        }

        public static int FirstPlayerScore
        {
            get
            {
                return m_FirstPlayerScore;
            }

        }

        public static int SecondPlayerScore
        {
            get
            {
                return m_SecondPlayerScore;
            }

        }

        public void DoubleEatingMoveDone()
        {
            m_DoubleEatingMode = false;
            m_MoveForDoubleEatingMode = null;
        }

        public Soldier GetSoldierByLocation(Location io_SoldierLocation)
        {
            Soldier wantedSoldier = null;
            bool isSoldierFound = false;

            for (int i = 0; i < m_Soldiers.Count && !isSoldierFound; i++)
            {
                if (m_Soldiers[i].Cell.IsLocationsEqual(io_SoldierLocation))
                {
                    wantedSoldier = m_Soldiers[i];
                    isSoldierFound = true;
                }
            }

            return wantedSoldier;
        }

        public void RemoveSoldierFromList(Soldier io_eatingSoldier)
        {
            m_Soldiers.Remove(io_eatingSoldier);
        }

        public void SetDoubleEatingMode(List<Move> possibleMovesForAfterMoveSoldier)
        {
            m_DoubleEatingMode = true;
            m_MoveForDoubleEatingMode = possibleMovesForAfterMoveSoldier;
        }

        public bool IsThereAnySoldir()
        {
            return m_Soldiers.Count > 0;
        }

        public bool IsThereAnyMoves(Board board)
        {
            bool thereIsMove = false;

            for (int i = 0; i < m_Soldiers.Count && !thereIsMove; i++)
            {
                if (m_Soldiers[i].GetPossibleMoves(board).Count > 0)
                {
                    thereIsMove = true;
                }
            }

            return thereIsMove;
        }

        public List<Move> GetAllPossibleMoves(Board io_board)
        {
            if (!m_DoubleEatingMode)
            {
                return getAllPossibleMovesInUsualCase(io_board);
            }
            else
            {
                return m_MoveForDoubleEatingMode;
            }
        }

        public int GetNumberOfSoldier()
        {
            return m_Soldiers.Count;
        }

        private void setLinesAndSignForPlayer(out int io_StartLine, out int io_FinishLine, out EnumCharsInBoard.enumCharsInBoard io_soldirSign, out EnumDirection.enumDirection io_Direction, bool io_isFirsPlayer, Board board)
        {
            if (io_isFirsPlayer)
            {
                io_StartLine = 0;
                io_FinishLine = (board.Size - 2) / 2;
                io_soldirSign = EnumCharsInBoard.enumCharsInBoard.Player1Soldier;
                io_Direction = EnumDirection.enumDirection.ForwardToDown;
            }
            else
            {
                io_StartLine = ((board.Size - 2) / 2) + 2;
                io_FinishLine = io_StartLine + ((board.Size - 2) / 2);
                io_soldirSign = EnumCharsInBoard.enumCharsInBoard.Player2Soldier;
                io_Direction = EnumDirection.enumDirection.DownToForward;
            }
        }

        private void setGameForPlayer(bool io_isFirsPlayer, Board board)
        {
            int startLine, finishLine;
            EnumCharsInBoard.enumCharsInBoard soldirSign;
            EnumDirection.enumDirection soldierDirection;

            setLinesAndSignForPlayer(out startLine, out finishLine, out soldirSign, out soldierDirection, io_isFirsPlayer, board);
            for (int i = startLine; i < finishLine; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        Location soldierLocation = new Location(i, j);
                        board.SetSpecificCell((char)soldirSign, i, j);
                        m_Soldiers.Add(new Soldier(soldierLocation, soldirSign, soldierDirection));
                    }
                    else
                    {
                        board.SetSpecificCell((char)EnumCharsInBoard.enumCharsInBoard.EmptyCell, i, j);
                    }
                }
            }
        }

        private List<Move> getAllPossibleMovesInUsualCase(Board io_board)
        {
            List<Move> allPossibleMoves = new List<Move>();

            foreach (Soldier curretnSokldier in m_Soldiers)
            {
                foreach (Move currentMove in curretnSokldier.GetPossibleMoves(io_board))
                {
                    allPossibleMoves.Add(currentMove);
                }
            }

            allPossibleMoves = ifThereIsEatingMoveRemoveOtherMoves(allPossibleMoves);
            return allPossibleMoves;
        }

        private List<Move> ifThereIsEatingMoveRemoveOtherMoves(List<Move> io_possibleMoves)
        {
            List<Move> alternativePossibleMove = new List<Move>();
            if (existAtLeastOneEatingMove(io_possibleMoves))
            {
               foreach (Move moveItem in io_possibleMoves)
                {
                        if (moveItem.IsEating)
                        {
                            alternativePossibleMove.Add(moveItem);
                        }
                }
            }
            else
            {
                alternativePossibleMove = io_possibleMoves;
            }

            return alternativePossibleMove;
        }

        private bool existAtLeastOneEatingMove(List<Move> io_possibleMoves)
        {
            bool thereIsEatingMove = false;

            for (int i = 0; i < io_possibleMoves.Count && !thereIsEatingMove; i++)
            {
                if (io_possibleMoves[i].IsEating)
                {
                    thereIsEatingMove = true;
                }
            }

            return thereIsEatingMove;
        }
    }
}
