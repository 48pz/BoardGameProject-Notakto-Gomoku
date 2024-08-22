

namespace BoardGameProject
{
    public abstract class UIBase
    {

        /// <summary>
        /// display the game type to select for the user
        /// </summary>
        public static void DisplayGameType()
        {

            int width = 55;
            int padding = width - 2;

            Console.WriteLine("┌" + new string('─', width - 2) + "┐");

            PrintLine("Welcome! Please enter number to select the game!", padding);
            PrintLine("", padding);
            PrintLine("Notakto : 1", padding);
            PrintLine("Gomoku  : 2", padding);

            Console.WriteLine("└" + new string('─', width - 2) + "┘");
        }



        /// <summary>
        /// print each line 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padding"></param>
        public static void PrintLine(string str, int padding)
        {
            str = str.PadRight(padding);
            Console.WriteLine("│" + str + "│");
        }

        public abstract void DisplayGameMode();
        public abstract string PassInfoToGameManager();
        public abstract void DisplayInfo(string info);
    }



}





