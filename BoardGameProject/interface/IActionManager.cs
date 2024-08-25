

namespace BoardGameProject
{
    public interface IActionManager<TBoard>
    {
        void SaveBoardState(TBoard board);
        bool Undo(List<int[,]> history, int targetRound, TBoard board);
        bool Redo(TBoard board);
        void Clear();
    }
}
