namespace BoardGameProject
{
    public class UIUtils
    {

        public static void DisplayGameMode()
        {
            int width = 55;
            int padding = width - 2;

            Console.WriteLine("┌" + new string('─', width - 2) + "┐");

            PrintLine("Please enter number to select the mode!", padding);
            PrintLine("", padding);
            PrintLine("Computer VS Human : 1", padding);
            PrintLine("Human VS Human  : 2", padding);
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

    }
}
