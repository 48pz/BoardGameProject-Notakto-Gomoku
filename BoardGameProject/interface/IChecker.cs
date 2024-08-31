
namespace BoardGameProject
{
    /// <summary>
    /// interface for checker
    /// </summary>
    /// <typeparam name="TBoard"></typeparam>
    public interface IChecker<TBoard>
    {

        bool IsValidPlace(TBoard board, int row, int col);
        bool IsWin(TBoard board, int row, int col, int player);
        bool IsWin(List<TBoard> boardList);
        bool IsDraw(TBoard board);
        bool IsValidPlace(List<TBoard> boardList, int boardIndex, int row, int col);
    }
}
