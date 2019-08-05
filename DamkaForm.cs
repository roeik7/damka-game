namespace B18_Ex05_WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Logic = B18_Ex05_Logic;

    public class DamkaForm : Form
    {
        private readonly System.ComponentModel.ComponentResourceManager m_Resources = new System.ComponentModel.ComponentResourceManager(typeof(Properties.Resources));
        private int m_BoardSize;
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private bool m_IsTwoPlayer;
        private ButtonCell[,] m_Cells;
        private Logic.Game m_LogicGame;
        private ButtonCell m_ChosenButton = null;

        public DamkaForm(int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsTwoPlayer)
        {
            m_BoardSize = i_BoardSize;
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            m_IsTwoPlayer = i_IsTwoPlayer;
            m_Cells = new ButtonCell[m_BoardSize, m_BoardSize];
            m_LogicGame = new B18_Ex05_Logic.Game(m_BoardSize, m_FirstPlayerName, m_SecondPlayerName, !m_IsTwoPlayer);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            int buttonWidth = 60, buttonHeight = 60, panelHeight = 60, formMargins = 40;
            Label labelPlayerOne, labelPlayerTwo;

            ////Form Size
            setFormParameters(m_BoardSize, panelHeight, formMargins, buttonHeight, buttonWidth);
           
            ////Labels
            labelPlayerOne = new Label();
            labelPlayerTwo = new Label();
            labelPlayerOne.Left = this.Width / 4;
            labelPlayerTwo.Left = this.Width / 2;
            setLabelParamters(labelPlayerOne, m_FirstPlayerName, Logic.Player.FirstPlayerScore);
            setLabelParamters(labelPlayerTwo, m_SecondPlayerName, Logic.Player.SecondPlayerScore);

            ////Matrix Of button
            setButtonMatrix(buttonWidth, buttonHeight, formMargins, panelHeight);
        }

        private void setButtonMatrix(int i_buttonWidth, int i_buttonHeight, int i_formMargins, int i_panelHeight)
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Cells[i, j] = new ButtonCell(new Logic.Location(i, j));
                    m_Cells[i, j].Left = i_formMargins / 2;
                    m_Cells[i, j].Width = i_buttonWidth;
                    m_Cells[i, j].Height = i_buttonHeight;
                    if (j == 0)
                    {
                        setFirstRowButton(i, i_formMargins, i_panelHeight, i_buttonHeight);
                    }
                    else
                    {
                        setButtonsPosition(i, j, i_formMargins, i_panelHeight, i_buttonHeight);
                        m_Cells[i, j].LeftLocation = m_Cells[i, j - 1].LeftLocation + i_buttonWidth;
                        m_Cells[i, j].TopLocation = m_Cells[i, j - 1].TopLocation;
                    }

                    setCellImageFunctionAndEnable(m_Cells[i, j]);
                    this.Controls.Add(m_Cells[i, j]);
                    m_Cells[i, j].MoveButtonToLocation(m_BoardSize);
                }
            }
        }

        private void setLabelParamters(Label i_Label, string i_PlayerName, int i_PlayerScore)
        {
            i_Label.BackColor = Color.Transparent;
            i_Label.Top = 20;
            i_Label.Text = string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
            i_Label.Font = new Font("Arial", 10, FontStyle.Bold);
             
            this.Controls.Add(i_Label);
        }

        private void setFormParameters(int i_BoardSize, int i_panelHeight, int i_formMargins, int i_buttonHeight, int i_buttonWidth)
        {
            this.Height = (i_buttonHeight * (i_BoardSize + 1)) + i_panelHeight;
            this.Width = (i_buttonWidth * i_BoardSize) + ((i_BoardSize - 1) * 2) + i_formMargins;
            this.BackgroundImage = (System.Drawing.Image)m_Resources.GetObject("BackGroudImage");
            this.Text = "Damka";
            this.Icon = (System.Drawing.Icon)m_Resources.GetObject("Icon");
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void setCellImageFunctionAndEnable(ButtonCell i_buttonCell)
        {
            Logic.EnumCharsInBoard.enumCharsInBoard soldier =  m_LogicGame.Board.GetCellContent(i_buttonCell.CellLocation);

            i_buttonCell.TabStop = false;
            i_buttonCell.FlatStyle = FlatStyle.Flat;
            i_buttonCell.FlatAppearance.BorderSize = 2;

            if (((int)i_buttonCell.CellLocation.Row + (int)i_buttonCell.CellLocation.Column) % 2 == 0)
            {
                i_buttonCell.Enabled = false;
                i_buttonCell.BackColor = Color.Black;
            }
            else
            {
                i_buttonCell.BackColor = Color.SaddleBrown;
            }

            ////Image And Function
            if (soldier  != Logic.EnumCharsInBoard.enumCharsInBoard.EmptyCell)
            {
                getApropriateImage(i_buttonCell, soldier);
                i_buttonCell.BackgroundImageLayout = ImageLayout.Stretch;
                i_buttonCell.Click -= new System.EventHandler(this.ButtonCellMakeAMoveClick_Click);
                i_buttonCell.Click += new System.EventHandler(this.ButtonCellSoldierClick_Click);
            }
           else
            {
                i_buttonCell.BackgroundImage = null;
                i_buttonCell.Click -= new System.EventHandler(this.ButtonCellSoldierClick_Click);
                i_buttonCell.Click += new System.EventHandler(this.ButtonCellMakeAMoveClick_Click);
            }
        }

        private void ButtonCellMakeAMoveClick_Click(object sender, EventArgs e)
        {
            Logic.Move move;
            string endOfGameMessege = string.Empty;

            if (m_ChosenButton != null)
            {
                move = new B18_Ex05_Logic.Move(m_ChosenButton.CellLocation, (sender as ButtonCell).CellLocation);
                if (m_LogicGame.MakeAPlayerMove(ref move))
                {
                    updateBoard(sender as ButtonCell, move);
                    m_ChosenButton.IsClicked = false;
                    m_ChosenButton = null;
                    if (m_LogicGame.IsGameOver(ref endOfGameMessege))
                    {
                        gameIsOver(endOfGameMessege);
                    }
                    else
                    { 
                        playNextMove();
                    }
                }
                else
                {
                    MessageBox.Show("Move is not valid");
                }
            }            
        }

        private void updateBoard(ButtonCell i_ButtonTo, Logic.Move i_Move)
        {
            ////Update Board
            setCellImageFunctionAndEnable(m_ChosenButton);
            setCellImageFunctionAndEnable(i_ButtonTo);
            if (i_Move.IsEating)
            {
                setCellImageFunctionAndEnable(m_Cells[(int)i_Move.CellEating.Row, (int)i_Move.CellEating.Column]);
            }
        }

        private void playNextMove()
        {
            string endOfGameMessege = string.Empty;
            Logic.Move computerMove;
            ButtonCell buttonTo;

            if (m_LogicGame.Player[(int)m_LogicGame.PlayerTurn].IsComputer)
            { 
                while (m_LogicGame.PlayerTurn == Logic.EnumPlayerTurn.enumPlayerTurn.SecondPlayer)
                {
                    computerMove = m_LogicGame.MakeACpmputerMove();
                    m_ChosenButton = m_Cells[(int)computerMove.CellFrom.Row, (int)computerMove.CellFrom.Column];
                    buttonTo = m_Cells[(int)computerMove.CellTo.Row, (int)computerMove.CellTo.Column];
                    updateBoard(buttonTo, computerMove);
                }
            }

            if (m_LogicGame.IsGameOver(ref endOfGameMessege))
            {
                gameIsOver(endOfGameMessege);
            }
        }

        private void gameIsOver(string i_EndOfGame)
        {
            DialogResult userChoice;

            userChoice = MessageBox.Show(string.Format("{0}{1}Would you like another Game?", i_EndOfGame, System.Environment.NewLine), "Game is over!", MessageBoxButtons.YesNo);
            if (userChoice == DialogResult.Yes)
            {
                DamkaForm anotherGame = new DamkaForm(m_BoardSize, m_FirstPlayerName, m_SecondPlayerName, m_IsTwoPlayer);
                this.Hide();
                anotherGame.ShowDialog();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void getApropriateImage(ButtonCell i_button, Logic.EnumCharsInBoard.enumCharsInBoard i_Soldier)
        {
            if (i_Soldier == Logic.EnumCharsInBoard.enumCharsInBoard.Player1Soldier)
            {
                i_button.BackgroundImage = (System.Drawing.Image)m_Resources.GetObject("O");
            }
            else if (i_Soldier == Logic.EnumCharsInBoard.enumCharsInBoard.Player1King)
            {
                i_button.BackgroundImage = (System.Drawing.Image)m_Resources.GetObject("U");
            }
            else if (i_Soldier == Logic.EnumCharsInBoard.enumCharsInBoard.Player2King)
            {
                i_button.BackgroundImage = (System.Drawing.Image)m_Resources.GetObject("K");
            }
            else if (i_Soldier == Logic.EnumCharsInBoard.enumCharsInBoard.Player2Soldier)
            {
                i_button.BackgroundImage = (System.Drawing.Image)m_Resources.GetObject("X");
            }
        }

        private void ButtonCellSoldierClick_Click(object sender, EventArgs e)
        {
            ButtonCell senderButtonCell = sender as ButtonCell;

            if (senderButtonCell.IsClicked)
            {
                senderButtonCell.BackColor = Color.SaddleBrown;
                m_ChosenButton = null;
            }
            else
            {
                if (m_ChosenButton != null)
                {
                    m_ChosenButton.BackColor = Color.SaddleBrown;
                    m_ChosenButton.IsClicked = false;
                    m_ChosenButton = senderButtonCell;
                }
                else
                {
                    m_ChosenButton = senderButtonCell;
                }

                senderButtonCell.BackColor = Color.Blue;
            }

            senderButtonCell.IsClicked = !senderButtonCell.IsClicked;
        }

        private void setFirstRowButton(int i_Row, int i_formMargins, int i_panelHeight, int i_buttonHeight)
        {
            int j = 0;
            setButtonsPosition(i_Row, j, i_formMargins, i_panelHeight, i_buttonHeight);
            m_Cells[i_Row, j].LeftLocation = i_formMargins / 2; 
            if (i_Row == 0)
            {
                m_Cells[i_Row, j].TopLocation = i_panelHeight;
            }
            else
            {
                m_Cells[i_Row, j].Top = m_Cells[i_Row - 1, j].Top + i_buttonHeight;
                m_Cells[i_Row, j].TopLocation = m_Cells[i_Row - 1, j].TopLocation + i_buttonHeight;
            }
        }

        private void setButtonsPosition(int i_Row, int i_Column, int i_formMargins, int i_panelHeight, int i_buttonHeight)
        {
            m_Cells[i_Row, i_Column].Left = i_formMargins / 2;
            m_Cells[i_Row, i_Column].Top = (i_buttonHeight * i_Row) + i_panelHeight; 
        }
    }
}