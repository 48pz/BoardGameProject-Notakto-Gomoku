



using System.Drawing;

namespace BoardGameProject
{
    public class NotaktoBoard : IBoard
    {
        private List<List<List<int>>> boards;
        private int count =3; // for Notakto
        private int size =3;
        private int currentBoardIndex = 0; // for Notakto
        private string gameName = GlobalVar.NOTAKTO;
        private string gameMode;
        //Used to verify loading file
        private readonly string validationStr = GlobalVar.NOTAKTO;
        private int currentPlayer;
        private int round;

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
            get { return size; }
        }
        public int Count
        {
            get { return count; }
        }
        public int CurrentBoardIndex
        {
            get { return currentBoardIndex; }
        }
        public List<List<List<int>>> Boards
        {
            get { return boards; }
        }
        public List<List<int>> Cells
        {
            get { return boards[currentBoardIndex]; }
        }

        public NotaktoBoard()
        {
            boards = new List<List<List<int>>>(count);

            for (int i = 0; i < count; i++)
            {
                var board = new List<List<int>>(size);
                for (int j= 0; j < size; j++)
                {
                    board.Add(new List<int>(new int[size]));
                }
                boards.Add(board);
            }
            InitialiseBoards();
        }

        private void InitialiseBoards()
        {
            for (int b = 0; b < count; b++)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        boards[b][i][j] = 0;
                    }
                }
            }
        }

        public void PrintBoard(int round)
        {
            Console.WriteLine("\nRound: {0}", round);
            for (int b = 0; b < count; b++) // for display Notakto 3 boards
            {
                Console.WriteLine("Board {0}:", b + 1);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        char symbol = boards[currentBoardIndex][i][j] == 0 ? '.' : 'X';
                        Console.Write($"{symbol} ");
                    }
                    Console.WriteLine();
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
                    if (boards[currentBoardIndex][i][j] == 0)
                    {
                        abaliablePos.Add((i, j));
                    }
                }
            }
            return abaliablePos;
        }

        public bool PlaceChess(int row, int col, int player)
        {
            if (boards[currentBoardIndex][row][col] == 0)
            { // Check if cell is empty
                boards[currentBoardIndex][row][col] = player; // Player 1 is 'X', Player 2 is 'O'
                return true;
            }
            return false;
        }

        /// <summary>
        /// Switch to the specified chessboard
        /// </summary>
        /// <param name="index"></param>
        public void SwitchBoard(int index)
        {
            if (index >= 0 && index < count)
            {
                currentBoardIndex = index;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid board index.");
            }
        }
    }

}
