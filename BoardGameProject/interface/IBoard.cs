

namespace BoardGameProject
{
    /// <summary>
    /// interface for chess board
    /// </summary>
    public interface IBoard
    {
        int Size { get; }
        List<List<int>> Cells { get; }
        public List<(int, int)> GetAvaliablePositions();
        public void PrintBoard(int round);
        public bool PlaceChess(int row, int col, int player);
    }
}
