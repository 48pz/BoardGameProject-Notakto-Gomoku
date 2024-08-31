

namespace BoardGameProject
{
    public class NotaktoBoard : IBoard
    {

        private List<List<int>> cells;
        private int size;
        private string gameName = GlobalVar.NOTAKTO;
        private string gameMode;
        private readonly string validationStr = GlobalVar.NOTAKTO;
        private int currentPlayer;
        private int round;
        private bool isDisable;

        public bool IsDisable
        {
            get { return isDisable; }
            set { isDisable = value; }
        }

        public int Round
        {
            get { return round; }
            set { round = value; }
        }


        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public string ValidationStr
        {
            get { return validationStr; }
        }

        public string GameName
        {
            get { return gameName; }
            set { gameName = value; }
        }

        public string GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }

        public int Size
        {
            get { return cells.Count; }
            set { size = value; }
        }

        public List<List<int>> Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        public NotaktoBoard(int size)
        {
            this.size = size;
            cells = new List<List<int>>(size);

            for (int i = 0; i < size; i++)
            {
                cells.Add(new List<int>(new int[size]));
            }
            InitialiseBoard();
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
                    cells[i][j] = 0;
                }
            }
        }

        /// <summary>
        /// print board 
        /// </summary>
        public void PrintBoard(int round)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char symbol = cells[i][j] == 0 ? '.' : 'X';
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

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
            // Check if cell is empty
            if (cells[row][col] == 0)
            {
                // player 1 = 1 ,player 2 = 2
                cells[row][col] = player;
                return true;
            }
            return false;
        }

        public List<(int, int)> GetAvaliablePositions()
        {
            List<(int, int)> emptyPositions = new List<(int, int)>();

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (cells[row][col] == 0)
                    {
                        emptyPositions.Add((row, col));
                    }
                }
            }

            return emptyPositions;
        }
    }
}

