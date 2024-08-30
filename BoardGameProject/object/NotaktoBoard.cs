



using System.Drawing;
using System.Text.Json;

namespace BoardGameProject
{
    public class NotaktoBoard : IBoard
    {
        private List<List<List<int>>> boards;
        private int count = 3; // for Notakto
        private int size = 3;
        private int currentBoardIndex = 0; // for Notakto
        private string gameName = GlobalVar.NOTAKTO;
        private string gameMode;
        //Used to verify loading file
        private readonly string validationStr = GlobalVar.NOTAKTO;
        private int currentPlayer;
        private int round;

        private bool[] isBoardAvailable;

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
            isBoardAvailable = new bool[count];

            for (int i = 0; i < count; i++)
            {
                isBoardAvailable[i] = true;
                var board = new List<List<int>>(size);
                for (int j = 0; j < size; j++)
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
                        char symbol = boards[b][i][j] == 0 ? '.' : 'X';
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
            // 检查位置是否在有效范围内
            if (row < 0 || row >= size || col < 0 || col >= size)
            {
                Console.WriteLine("Error: Move is out of bounds.");
                return false;
            }

            // 检查位置是否可用并且棋盘没有被锁定
            if (isBoardAvailable[currentBoardIndex] && boards[currentBoardIndex][row][col] == 0)
            {
                boards[currentBoardIndex][row][col] = player;

                if (CheckWinOnBoard(currentBoardIndex))
                {
                    isBoardAvailable[currentBoardIndex] = false; // 标记棋盘为不可用
                    Console.WriteLine($"Board {currentBoardIndex + 1} is now locked.");
                    return true;
                }
                return true;
            }
            Console.WriteLine("Error: Invalid move. The position is already occupied or the board is locked.");
            return false;
        }
        private bool CheckWinOnBoard(int boardIndex)
        {
            var board = boards[boardIndex];
            // 检查横、竖、斜线的胜利条件
            for (int i = 0; i < size; i++)
            {
                if (board[i][0] != 0 && board[i][0] == board[i][1] && board[i][1] == board[i][2])
                    return true;
                if (board[0][i] != 0 && board[0][i] == board[1][i] && board[1][i] == board[2][i])
                    return true;
            }
            if (board[0][0] != 0 && board[0][0] == board[1][1] && board[1][1] == board[2][2])
                return true;
            if (board[0][2] != 0 && board[0][2] == board[1][1] && board[1][1] == board[2][0])
                return true;

            return false;
        }

        /// <summary>
        /// Switch to the specified chessboard
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

        public void LockBoard(int boardIndex)
        {
            isBoardAvailable[boardIndex] = false;
        }
        public bool IsBoardLocked(int boardIndex)
        {
            return !isBoardAvailable[boardIndex];
        }
        public bool AllBoardsLocked()
        {
            return !isBoardAvailable.Any(board => board); // 检查所有棋盘是否都被锁定
        }

    }
}

