namespace B18_Ex05_WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameSettingsForm());
        }
    }
}
