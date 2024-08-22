

namespace BoardGameProject
{
    public interface IBoard
    {
        public List<(int, int)> GetAvaliablePositions();
        public void PrintBoard();
        public bool PlaceChess(int row, int col, int player);
    }
}
