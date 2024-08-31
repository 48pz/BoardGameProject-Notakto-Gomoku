

namespace BoardGameProject
{
    /// <summary>
    /// interface for saver
    /// </summary>
    /// <typeparam name="TBoard"></typeparam>
    public interface ISaver<TBoard>
    {
        void SaveBoardInfo(TBoard board, string savePath);
        void SaveBoardInfo(List<TBoard> boardList, string savePath);
    }
}
