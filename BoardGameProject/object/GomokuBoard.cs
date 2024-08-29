using System.Text.Json.Serialization;

namespace BoardGameProject
{

    public class GomokuBoard : IBoard
    {
        private List<List<int>> cells; 
        private int size;
        private string gameName = GlobalVar.GOMOKU;
        private string gameMode;
        //Used to verify loading file
        private readonly string validationStr = GlobalVar.GOMOKU;
        private int currentPlayer;
        private int round;


        public int Round
        {
            get { return round; }
            set { round = value;}
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

        public GomokuBoard(int size)
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
            Console.WriteLine("\nRound: {0}", round);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    char symbol = cells[i][j] == 0 ? '.' : (cells[i][j] == 1 ? 'X' : 'O');
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }


        public List<(int, int)> GetAvaliablePositions()
        {
            List<(int, int)> abaliablePos = new List<(int, int)>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (cells[i][j] == 0)
                    {
                        abaliablePos.Add((i, j));
                    }
                }
            }
            return abaliablePos;
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
            if (cells[row][col] == 0)
            { // Check if cell is empty
                cells[row][col] = player; // Player 1 is 'X', Player 2 is 'O'
                return true;
            }
            return false;
        }
    }
}
