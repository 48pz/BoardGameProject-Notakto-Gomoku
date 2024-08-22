
namespace BoardGameProject
{
    /// <summary>
    /// class of board factory  factory method pattern
    /// </summary>
    public class BoardFactory
    {
        public static List<IBoard> CreateBoard(string gameType)
        {
            List<IBoard> boards = new List<IBoard>();
            if (gameType == GlobalVar.GOMOKU)
            {
                boards.Add(new GomokuBoard(10));
            }else if (gameType == GlobalVar.NOTAKTO)
            {
                for (int i = 0; i < 3; i++)
                {
                    boards.Add(new NotaktoBoard());
                }
            }

            return boards;
        }
    }
}
