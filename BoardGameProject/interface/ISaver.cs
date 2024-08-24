

namespace BoardGameProject
{
    public interface ISaver<TBoard>
    {
        void SaveBoardInfo(TBoard board, string savePath);
    }
}
