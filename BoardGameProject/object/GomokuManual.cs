﻿

namespace BoardGameProject
{
    public class GomokuManual : IHelpSystem
    {
        public void DisplayUserManual()
        {
            Console.WriteLine("Gomoku Manual");
            Console.WriteLine("Gomoku is a classic strategy game played on a 15x15 grid.");
            Console.WriteLine("The goal is to be the first player to align five of your pieces in a row.");
            Console.WriteLine("You should choose the position by input two integer are seperated by ' 'space ");
        }
    }
}
