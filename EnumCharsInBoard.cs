namespace B18_Ex05_Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EnumCharsInBoard
    {
        public enum enumCharsInBoard
        {
            Player1Soldier = 'O', Player1King = 'U', Player2Soldier = 'X', Player2King = 'K', EmptyCell = ' '
        }

        public static enumCharsInBoard Parse(char i_char)
        {
            enumCharsInBoard result;

            if (i_char == 'O')
            {
                result = enumCharsInBoard.Player1Soldier;
            }
            else if(i_char == 'U')
            {
                result = enumCharsInBoard.Player1King;
            }
            else if(i_char == 'X')
            {
                result = enumCharsInBoard.Player2Soldier;
            }
            else if(i_char == 'K')
            {
                result = enumCharsInBoard.Player2King;
            }
            else
            {
                result = enumCharsInBoard.EmptyCell;
            }

            return result;
        }
    }
}
