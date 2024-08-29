
namespace BoardGameProject
{
    public class NotaktoManual : IHelpSystem
    {
        public void DisplayUserManual()
        {
            Console.WriteLine("NotaktoManual------------------");
            Console.WriteLine("The game is played on multiple 3x3 boards");
            Console.WriteLine("The first player to create a three-in-a-row loses.");
            Console.WriteLine("You should input three integers in this game.");
            Console.WriteLine("The first integer represent the board number, comfirm the choice by press Enter.");
            Console.WriteLine("Then you should input two integer are seperated by ' 'space which indicate the position.");
        }
    }
}
