
namespace BoardGameProject
{
    public interface IChecker<TBoard>
    {


        bool IsValidPlace(TBoard board, int row, int col);
        bool IsWin(TBoard board, int row, int col, int player); 
        bool IsDraw(TBoard board);

    }
}
