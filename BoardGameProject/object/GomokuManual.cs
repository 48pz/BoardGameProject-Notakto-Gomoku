

namespace BoardGameProject
{
    /// <summary>
    /// gomoku manual class
    /// </summary>
    public class GomokuManual : IHelpSystem
    {
        public void DisplayUserManual()
        {
            Console.WriteLine("**Gomoku Manual**");
            Console.WriteLine("[1.Input Format]");
            Console.WriteLine("Input format: Enter TWO numbers between 1 and 10 (inclusive), separated by a space.");
            Console.WriteLine("The first number represents the row and the second number represents the column.");
            Console.WriteLine("\"ROW COL\": 5 6 means to place the chess piece in the 5th row and the 6th column.");
            Console.WriteLine("[2.Winning condition]");
            Console.WriteLine("Five chesses in a row (horizontally, vertically and diagonally)");
            Console.WriteLine("If there are no empty positions, the game is a draw.");
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
            Console.WriteLine("{4.5Enter \"help\"}: display this manual again.");
            Console.WriteLine("{4.6Enter \"quit\"}: Exit Game.\n");

        }
    }
}
