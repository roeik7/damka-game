namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Game
    {
        private Board m_board;
        private Player[] m_player = new Player[2];
        private EnumPlayerTurn.enumPlayerTurn m_PlayerTurn = EnumPlayerTurn.enumPlayerTurn.FirstPlayer;
        
        public Game(int io_boardSize, string i_FirstNamePlayer, string i_SecondNamePlayer, bool i_isOnePlayer)
        {
            bool firstPlayer = true;
            bool isComputer = true;

            m_board = new Board(io_boardSize);
            Player[0] = new Player(i_FirstNamePlayer, firstPlayer, m_board, !isComputer, EnumCharsInBoard.enumCharsInBoard.Player1Soldier);
            Player[1] = new Player(i_SecondNamePlayer, !firstPlayer, m_board, i_isOnePlayer, EnumCharsInBoard.enumCharsInBoard.Player2Soldier);
        }

        public Board Board
        {
            get
            {
                return m_board;
            }
        }

        public Player[] Player
        {
            get
            {
                return m_player;
            }

            set
            {
                m_player = value;
            }
        }

        public EnumPlayerTurn.enumPlayerTurn PlayerTurn
        {
            get
            {
                return m_PlayerTurn;
            }

            set
            {
                m_PlayerTurn = value;
            }
        }

        public bool MakeAPlayerMove(ref Move i_move)
        {
            bool isMoveValid = false;
            List<Move> possibleMoves = m_player[(int)m_PlayerTurn].GetAllPossibleMoves(m_board);
            
            for (int i = 0; i < possibleMoves.Count && !isMoveValid; i++)
            { 
                if (possibleMoves[i].IsSameMove(i_move))
                {
                    i_move = possibleMoves[i];
                    moveSoldier(possibleMoves[i], m_player[(int)m_PlayerTurn].GetSoldierByLocation(i_move.CellFrom));
                    isMoveValid = true;
                    if (m_player[(int)m_PlayerTurn].DoubleEatingMode)
                    {
                        m_player[(int)m_PlayerTurn].DoubleEatingMoveDone();
                    }
                }
            }
            
            return isMoveValid;
        }

        public bool TryToQuit()
        {
            return m_player[(int)m_PlayerTurn].GetNumberOfSoldier() < m_player[(int)SwitchPlayerTurn()].GetNumberOfSoldier();
        }

        public Move MakeACpmputerMove()
        {
            List<Move> possibleMoves = m_player[(int)m_PlayerTurn].GetAllPossibleMoves(m_board);
            Move choosenMove = chooseTheBestMove(possibleMoves);

            if (m_player[(int)m_PlayerTurn].DoubleEatingMode)
            {
                m_player[(int)m_PlayerTurn].DoubleEatingMoveDone();
            }

            if (choosenMove == null)
            {
                choosenMove = chooseMoveRanodm(possibleMoves);
            }

            MakeAPlayerMove(ref choosenMove);
            return choosenMove;
        }

        // $G$ DSN-002 (-3) Logical classes should not be UI interactive.
        public bool IsGameOver(ref string io_EndOfGameMessage)
        {
            bool isGameOver = false;

            if (isTie())
            {
                isGameOver = true;
                io_EndOfGameMessage = "It's a Tie!";
            }

            if (isPlayerWin())
            {
                isGameOver = true;
                io_EndOfGameMessage = string.Format("{0} Win!", m_player[(int)SwitchPlayerTurn()].Name);
            }

            return isGameOver;
        }

        public EnumPlayerTurn.enumPlayerTurn SwitchPlayerTurn()
        {
            return (EnumPlayerTurn.enumPlayerTurn)(((int)PlayerTurn + 1) % 2); 
        }

        private void moveSoldier(Move i_move, Soldier io_currentSoldier)
        {
            char soldierSign = m_board.GetSpecificCell(io_currentSoldier.Cell);

            ////update board
            m_board.SetSpecificCell((char)EnumCharsInBoard.enumCharsInBoard.EmptyCell, io_currentSoldier.Cell);
            m_board.SetSpecificCell(soldierSign, i_move.CellTo);

            if (i_move.IsKingMove)
            {
                io_currentSoldier.MakeToKing();
                m_board.SetSpecificCell((char)io_currentSoldier.Sign, i_move.CellTo);
            }

            if (i_move.IsEating)
            {
                handleEatingMoveInBoardAndDeleteSoldierFromPlayer(io_currentSoldier, i_move);
                if (thereIsAnotherOptionToEatHandldIt(io_currentSoldier, i_move))
                {
                    ////Give the player Another turn
                    m_PlayerTurn = SwitchPlayerTurn();
                }
            }

            m_PlayerTurn = SwitchPlayerTurn();

            ////update soldier
            io_currentSoldier.Cell = i_move.CellTo;
        }

        private bool thereIsAnotherOptionToEatHandldIt(Soldier io_currentSoldier, Move i_move)
        {
            bool thereIsMoveOfEating = false;
            Soldier afterMoveSoldier = new Soldier(i_move.CellTo, io_currentSoldier.Sign, io_currentSoldier.Direction, io_currentSoldier.IsKing);
            List<Move> possibleMovesForAfterMoveSoldier = afterMoveSoldier.GetPossibleMoves(m_board);

            for (int i = 0; i < possibleMovesForAfterMoveSoldier.Count && !thereIsMoveOfEating; i++)
            {
                if (possibleMovesForAfterMoveSoldier[i].IsEating)
                {
                    thereIsMoveOfEating = true;
                    Player[(int)PlayerTurn].SetDoubleEatingMode(possibleMovesForAfterMoveSoldier);
                }
            }

            return thereIsMoveOfEating;
        }

        private bool isPlayerWin()
        {
            bool playerWin;

            playerWin = !m_player[(int)m_PlayerTurn].IsThereAnySoldir() || !m_player[(int)m_PlayerTurn].IsThereAnyMoves(m_board);
            if(playerWin)
            {
                updatePlayerScore();
            }

            return playerWin;

        }

        private void updatePlayerScore()
        {
            int Winnerscore = m_player[(int)SwitchPlayerTurn()].calculateScore(), LoserScore = m_player[(int)m_PlayerTurn].calculateScore(); 

            B18_Ex05_Logic.Player.UpdaterScore(Winnerscore - LoserScore, (int)SwitchPlayerTurn());
        }

     
        private bool isTie()
        {
            return !Player[0].IsThereAnyMoves(m_board) && !Player[1].IsThereAnyMoves(m_board);
        }

        private void handleEatingMoveInBoardAndDeleteSoldierFromPlayer(Soldier io_currentSoldier, Move i_move)
        {
            int eatingRow, eatingColumn;
            Soldier eatingSoldier;

            eatingRow = (int)io_currentSoldier.Cell.Row + ((int)i_move.CellTo.Row - (int)io_currentSoldier.Cell.Row) / 2;
            eatingColumn = (int)io_currentSoldier.Cell.Column + ((int)i_move.CellTo.Column - (int)io_currentSoldier.Cell.Column) / 2;
            i_move.CellEating = new Location(eatingRow, eatingColumn);
            m_board.SetSpecificCell((char)EnumCharsInBoard.enumCharsInBoard.EmptyCell, i_move.CellEating);
            eatingSoldier = m_player[(int)SwitchPlayerTurn()].GetSoldierByLocation(i_move.CellEating);
            m_player[((int)SwitchPlayerTurn())].RemoveSoldierFromList(eatingSoldier);
        }

        private Move chooseTheBestMove(List<Move> possibleMoves)
        {
            int bestMoveScore = int.MinValue;
            Move bestMove = null;
            int movescore;

            foreach (Move moveItem in possibleMoves)
            {
                movescore = getMoveScore(moveItem);

                if (movescore > bestMoveScore)
                {
                    bestMove = moveItem;
                    bestMoveScore = movescore;
                }
            }

            if (bestMoveScore == 0)
            {
                bestMove = null;
            }

            return bestMove;
        }

        private int getMoveScore(Move moveItem)
        {
            int movescore = 0;

            if (moveItem.IsCanBeEatingByThisMove)
            {
                movescore += -5;
            }

            if (moveItem.IsEating)
            {
                movescore += +3;
            }

            if (moveItem.IsKingMove)
            {
                movescore += +2;
            }

            return movescore;
        }

        private Move chooseMoveRanodm(List<Move> possibleMoves)
        {
            Random randomNumber = new Random();
            List<Move> newMoves = ifItPossibleRemoveMoveThatWillBeEatingByThem(possibleMoves);

            return possibleMoves[randomNumber.Next(possibleMoves.Count)];
        }

        private List<Move> ifItPossibleRemoveMoveThatWillBeEatingByThem(List<Move> possibleMoves)
        {
            List<Move> movesWithOutWillBeEating = new List<Move>();

            foreach (Move moveItem in possibleMoves)
            {
                if (!moveItem.IsCanBeEatingByThisMove)
                {
                    movesWithOutWillBeEating.Add(moveItem);
                }
            }

            if (movesWithOutWillBeEating.Count == 0)
            {
                movesWithOutWillBeEating = possibleMoves;
            }

            return movesWithOutWillBeEating;
        }
    }
}
