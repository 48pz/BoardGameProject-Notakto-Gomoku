namespace BoardGameProject
{
    public class GomokuBoard : IBoard
    {

        private int[,] cells;
        private int size;
        public int Size
        {
            get { return cells.GetLength(0); }  
        }
        public int[,] Cells
        {
            get { return cells;  }
        }



        public GomokuBoard(int size)
        {
            this.size = size;
            cells = new int[size, size];
            InitialiseBoard();
            PrintBoard();
        }

        /// <summary>
        /// initail board info
        /// </summary>
        private void InitialiseBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// print board 
        /// </summary>
        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char symbol = cells[i, j] == 0 ? '.' : (cells[i, j] == 1 ? 'X' : 'O');
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
        }


        public List<(int, int)> GetAvaliablePositions()
        {
            List<(int, int)> AbaliablePos = new List<(int, int)>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (cells[i, j] == 0)
                    {
                        AbaliablePos.Add((i, j));
                    }
                }
            }
            return AbaliablePos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool PlaceChess(int row, int col, int player)
        {
            if (cells[row, col] == 0)
            { // Check if cell is empty
                cells[row, col] = player; // Player 1 is 'X', Player 2 is 'O'
                return true;
            }
            return false;
        }
    }
}
