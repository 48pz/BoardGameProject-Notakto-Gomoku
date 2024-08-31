

namespace BoardGameProject
{
    public interface ISaver<TBoard>
    {
        void SaveBoardInfo(TBoard board, string savePath);
        void SaveBoardInfo(List<TBoard> boardList, string savePath);
    }
}
