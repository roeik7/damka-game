namespace B18_Ex05_WinForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2.Text = string.Empty;
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.Enabled = false;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            string playerOneName = textBoxplayer1.Text;
            string playerTwoName = "Computer";
            int boardSize;
            bool isTwoPlayers = checkBoxPlayer2.Checked;
            
            if (isTwoPlayers)
            {
                playerTwoName = textBoxPlayer2.Text;
            }
            if (radioButtonSix.Checked == true)
            {
                boardSize = 6;
            }
            else if (radioButtonEight.Checked == true)
            {
                boardSize = 8;
            }
            else
            {
                boardSize = 10;
            }

            DamkaForm damka = new DamkaForm(boardSize, playerOneName, playerTwoName, isTwoPlayers);
            this.Hide();
            damka.ShowDialog();
            this.Close();
        }

        private void GameSettings_Load(object sender, EventArgs e)
        {
        }
    }
}