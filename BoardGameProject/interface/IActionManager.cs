

namespace BoardGameProject
{
    /// <summary>
    /// interface to undo and redo
    /// </summary>
    /// <typeparam name="TBoard"></typeparam>
    public interface IActionManager<TBoard>
    {
        void SaveBoardState(TBoard board);
        bool Undo(List<int[,]> history, int targetRound, TBoard board);
        bool Redo(TBoard board);
        List<NotaktoBoard> Redo();
        void Clear();
    }
}
