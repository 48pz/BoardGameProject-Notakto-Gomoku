

namespace BoardGameProject
{
    public interface IBoard
    {
        int Size { get; }
        int[,] Cells { get; }
        public List<(int, int)> GetAvaliablePositions();
        public void PrintBoard();
        public bool PlaceChess(int row, int col, int player);
    }
}
