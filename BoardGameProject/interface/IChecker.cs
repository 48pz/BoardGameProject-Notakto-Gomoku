
namespace BoardGameProject
{
    public interface IChecker
    {


        bool IsValidPlace(IBoard board, int row, int col, int size);

        //bool IsWin(IBoard board, int row, int col, int player);   remeber modify this !

        bool IsDraw(IBoard board);

    }
}
