
namespace BoardGameProject
{
    /// <summary>
    /// notakto help system
    /// </summary>
    public class NotaktoManual : IHelpSystem
    {
        public void DisplayUserManual()
        {
            Console.WriteLine("**Notakto Manual**");

            Console.WriteLine("[1.Input Format]");
            Console.WriteLine("Input format: Enter THREE numbers between 1 and 3 (inclusive), separated by a space.");
            Console.WriteLine("The first number represents the board number,");
            Console.WriteLine("the second number represents the row, and");
            Console.WriteLine("the third number represents the column.");
            Console.WriteLine("\"BOARD ROW COL\": 1 2 3 means play the chess in the second row and third column of the first board.");

            Console.WriteLine("[2.Winning condition]");
            Console.WriteLine("When a board has three in a row (horizontally, vertically and diagonally),");
            Console.WriteLine("the board will no longer be able to play chess.");
            Console.WriteLine("When all three boards cannot play chess,");
            Console.WriteLine("the player who played chess last loses, and the other player wins.");

            Console.WriteLine("[3.Default rules]");
            Console.WriteLine("Player1 goes first.");
            Console.WriteLine("If it is computer vs. human mode, the default computer is Player1.");

            Console.WriteLine("[4.Command list]");
            Console.WriteLine("{4.1Enter \"save\"}: save the current board information,");
            Console.WriteLine("the file save path is C:\\Users\\{Username}\\Documents\\MyBoardGameSaves.");
            Console.WriteLine("{4.2Enter \"load\"}: restore the board information from the save file, enter the full path information,");
            Console.WriteLine("and the game type and game mode of the restored file should be consistent with the current game type and mode.");
            Console.WriteLine("{4.3Enter \"undo\"}: enter the number of rounds, and perform the operation of undoing to a certain round (previous).");
            Console.WriteLine("{4.4Enter \"redo\"}: the undo operation will be confirmed after the undo operation is completed,");
            Console.WriteLine("and entering \"redo\" will cancel the undo operation.");
            Console.WriteLine("{4.5Enter \"help\"}: display this manual again.\n");

        }
    }
}
