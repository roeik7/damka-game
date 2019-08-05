namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Ex02.ConsoleUtils;

    public class UIConsole
    {
        public const char m_a = 'a';
        public const char m_A = 'A';
        public const char m_equal = '=';
        public const char sperator = '|';
        private Game m_game;
        private string m_latstmove = null;

        // $G$ DSN-999 (-10) Bad practice to preform these calls from ctor 
        public UIConsole()
        {
            intialeGame();
        }

        public Game Game
        {
            get
            {
                return m_game;
            }
        }

        public void printBoard()
        {
            string speartor = new string(m_equal, (Game.Board.Size * 4) + 2);
            string spaces = "  ";

            Console.Write(spaces);
            Console.Write("  ");
            for (int i = 0; i < Game.Board.Size; i++)
            {
                Console.Write(" {0}  ", (char)(m_A + i));
            }

            Console.Write(System.Environment.NewLine);
            Console.WriteLine(spaces + speartor);
            for (int i = 0; i < Game.Board.Size; i++)
            {
                Console.Write(" {0} |", (char)(m_a + i));
                for (int j = 0; j < Game.Board.Size; j++)
                {
                    Console.Write(" {0} |", Game.Board.GetSpecificCell(i, j));
                }

                Console.Write(System.Environment.NewLine);
                Console.WriteLine(spaces + speartor);
            }
        }

        private void intialeGame()
        {
            int boardSize, numberOfPlayer;
            string playerOneUserName, playerTwoUserName = "Computer";
            bool isComputerPlayer = true;

            Console.WriteLine("Please Insert Board Size:");

            boardSize = int.Parse(Console.ReadLine());
            checkAndHandleIfBoardSizeIsValid(ref boardSize);
            Console.WriteLine("Please Insert Your Name:");
            playerOneUserName = Console.ReadLine();
            checkAndHandleIfUserNameIsValid(ref playerOneUserName);
            Console.WriteLine("Please Insert Amount Of Player:");
            numberOfPlayer = int.Parse(Console.ReadLine());
            checkAndHandleIfNnumberOfPlayerIsValid(ref numberOfPlayer);
            if (numberOfPlayer == 2)
            {
                Console.WriteLine("Please Insert Name Of Other User:");
                playerTwoUserName = Console.ReadLine();
                checkAndHandleIfUserNameIsValid(ref playerTwoUserName);
                m_game = new Game(boardSize, playerOneUserName, playerTwoUserName, !isComputerPlayer);
            }
            else
            {
                m_game = new Game(boardSize, playerOneUserName, playerTwoUserName, isComputerPlayer);
            }

            Ex02.ConsoleUtils.Screen.Clear();
            play();
        }

        private void printLastMoveAndTurn()
        {
            EnumPlayerTurn.enumPlayerTurn lastPlayer = m_game.SwitchPlayerTurn();

            if (m_latstmove != null)
            {
                Console.WriteLine(m_latstmove);
            }

            Console.Write("{0}'s Turn ({1}):", m_game.Player[(int)m_game.PlayerTurn].Name, (char)m_game.Player[(int)m_game.PlayerTurn].PlayerSign);
        }

        private string getLastMoveString(string i_playerName, char i_playerSign, string i_move)
        {
            return string.Format("{0}'s move was ({1}):{2}", i_playerName, i_playerSign, i_move);
        }

        private bool getAndHandleMove()
        {
            bool Computer = true;
            bool moveIsValid = true;

            if (m_game.Player[(int)m_game.PlayerTurn].IsComputer == Computer)
            {
                handleComputerMove();
            }
            else
            {
                moveIsValid = handleUserMove();
            }

            return moveIsValid;
        }

        private bool handleUserMove()
        {
            bool moveIsValid = true;

            m_latstmove = Console.ReadLine();
            checkAndHandleIfCharactersAreValid(ref m_latstmove);
            if (m_latstmove != "Q")
            {
                Location cellFrom = new Location(m_latstmove[1] - m_a, m_latstmove[0] - m_A);
                Location cellTo = new Location(m_latstmove[4] - m_a, m_latstmove[3] - m_A);
                Move move = new Move(cellFrom, cellTo);
                m_latstmove = getLastMoveString(m_game.Player[(int)m_game.PlayerTurn].Name, (char)m_game.Player[(int)m_game.PlayerTurn].PlayerSign, m_latstmove);
                moveIsValid = m_game.MakeAPlayerMove(ref move);
            }
            else
            {
                if (m_game.TryToQuit())
                {
                    m_latstmove = null;
                    gameIsOver("User Quit");
                }
                else
                {
                    moveIsValid = false;
                }
            }

            if (!moveIsValid)
            {
                Console.WriteLine("Move Is not Valid Please Try Again:");
            }

            return moveIsValid;
        }

        private void handleComputerMove()
        {
            Move choosenMove;

            m_latstmove = string.Format("{0}'s move was ({1}):", m_game.Player[(int)m_game.PlayerTurn].Name, (char)m_game.Player[(int)m_game.PlayerTurn].PlayerSign);
            choosenMove = m_game.MakeACpmputerMove();
            m_latstmove += string.Format("{0}{1}>{2}{3}", makeEnumColumnToChar(choosenMove.CellFrom.Column), makeEnumRowsToChar(choosenMove.CellFrom.Row), makeEnumColumnToChar(choosenMove.CellTo.Column), makeEnumRowsToChar(choosenMove.CellTo.Row));
        }

        private char makeEnumColumnToChar(EnumRowCols.enumCols io_enumValue)
        {
            return (char)((int)io_enumValue + m_A);
        }

        private char makeEnumRowsToChar(EnumRowCols.enumRows io_enumValue)
        {
            return (char)((int)io_enumValue + m_a);
        }

        private void play()
        {
            bool keepPlaying = true, moveIsValid = false;
            string isGameOverMessage = string.Empty;

            while (keepPlaying)
            {
                printBoard();
                printLastMoveAndTurn();
                while (!moveIsValid)
                {
                    moveIsValid = getAndHandleMove();
                }

                keepPlaying = !m_game.IsGameOver(ref isGameOverMessage);
                moveIsValid = false;
                Ex02.ConsoleUtils.Screen.Clear();
            }

            gameIsOver(isGameOverMessage);
        }

        private void gameIsOver(string i_GameOverMessage)
        {
            char userInput;

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_GameOverMessage);
            Console.WriteLine("Would you like to play another game? Y");
            userInput = char.Parse(Console.ReadLine());

            if (userInput == 'Y')
            {
                intialeGame();
            }
        }

        private void checkAndHandleIfBoardSizeIsValid(ref int io_boardSize)
        {
            while ((io_boardSize != 6) && (io_boardSize != 8) && (io_boardSize != 10))
            {
                Console.WriteLine("The size of the board you entered is invalid (you must enter 6,8 or 10)");
                io_boardSize = int.Parse(Console.ReadLine());
            }
        }

        private void checkAndHandleIfUserNameIsValid(ref string io_playerOneUserName)
        {
            while (io_playerOneUserName.Contains(" ") || io_playerOneUserName.Length > 20)
            {
                Console.WriteLine("Please enter a username with no more than 20 characters and no spaces");
                io_playerOneUserName = Console.ReadLine();
            }
        }

        private void checkAndHandleIfNnumberOfPlayerIsValid(ref int io_numberOfPlayer)
        {
            while ((io_numberOfPlayer != 1) && (io_numberOfPlayer != 2))
            {
                Console.WriteLine("The number of players allowed is 1 or 2");
                io_numberOfPlayer = int.Parse(Console.ReadLine());
            }
        }

        // $G$ CSS-013 (0) Bad variable name (should be in the form of i_PascalCase).
        private void checkAndHandleIfCharactersAreValid(ref string m_latstmove)
        {
            bool NotQuiteGame = true;
            int five = 5;

            NotQuiteGame = !checkIfQuit(m_latstmove);
        
            while (NotQuiteGame && (m_latstmove.Length != five && (!isCapitalLetterAndInTheField(m_latstmove[0], m_latstmove[3], m_game.Board.Size) || m_latstmove[2] != '>' || !isLowerLetterAndInTheField(m_latstmove[1], m_latstmove[4], m_game.Board.Size))))
            {
                Console.WriteLine("Please enter valid letters for moves");
                m_latstmove = Console.ReadLine();
                NotQuiteGame = !checkIfQuit(m_latstmove);
            }
        }

        private bool checkIfQuit(string m_latstmove)
        {
            return m_latstmove == "Q";
        }

        private bool isLowerLetterAndInTheField(char i_firstLetterToCheck, char i_secondLetterToCheck, int boardSize)
        {
            bool isIntheField = (i_firstLetterToCheck >= 'a') && (i_firstLetterToCheck < 'a' + boardSize) && (i_secondLetterToCheck >= 'a') && (i_secondLetterToCheck < 'a' + boardSize);

            return isIntheField;
        }

        private bool isCapitalLetterAndInTheField(char i_firstLetterToCheck, char i_secondLetterToCheck, int boardSize)
        {
            bool isIntheField = (i_firstLetterToCheck >= 'A') && (i_firstLetterToCheck < 'A' + boardSize) && (i_secondLetterToCheck >= 'A') && (i_secondLetterToCheck < 'A' + boardSize);

            return isIntheField;
        }
    }
}
