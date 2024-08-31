
namespace BoardGameProject
{
    /// <summary>
    /// class of board factory  factory method pattern
    /// </summary>
    public class BoardFactory
    {
        public static List<IBoard> CreateBoard(string gameType)
        {
           
            if (gameType == GlobalVar.GOMOKU)
            {
                List<IBoard> boards = new List<IBoard>();
                boards.Add(new GomokuBoard(10));
                return boards;
            }
            else if (gameType == GlobalVar.NOTAKTO)
            {
                List<IBoard> boards = new List<IBoard>();
                for (int i = 0; i < 3; i++)
                {
                    boards.Add(new NotaktoBoard(3));
                }
                return boards;
            }

            else
            {
                throw new ArgumentException("Invalid game type");
            }
        }
    }
}
