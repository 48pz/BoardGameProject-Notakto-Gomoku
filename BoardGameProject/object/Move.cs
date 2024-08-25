

namespace BoardGameProject
{
    public class Move
    {
        private int row;
        private int col;
        private int currentPlayer;
        public int Row
        {
            get { return row; }
        }
        public int Col
        {
            get { return col; }
        }
        public int CurrentPlayer
        {
            get { return currentPlayer; }
        }

        public Move(int aRow, int aCol, int aCurrentPlayer)
        {
            row = aRow;
            col = aCol;
            currentPlayer = aCurrentPlayer;
        }
    }
}
