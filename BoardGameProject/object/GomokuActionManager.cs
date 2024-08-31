

namespace BoardGameProject
{
    /// <summary>
    /// Gomoku undo and redo
    /// </summary>
    public class GomokuActionManager : IActionManager<GomokuBoard>
    {
        private Stack<int[,]> redoStack = new Stack<int[,]>();

        public void Clear()
        {
            redoStack.Clear();
        }

        /// <summary>
        /// redo 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool Redo(GomokuBoard board)
        {
            if (redoStack.Count > 0)
            {
                int[,] redoState = redoStack.Pop();
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        board.Cells[i][j] = redoState[i, j];
                    }
                }
                Console.WriteLine("Redo completed");
                board.PrintBoard(board.Round);
                return true;
            }
            else
            {
                Console.WriteLine("No moves to redo.");
                return false;
            }
        }

        /// <summary>
        /// undo to target round
        /// </summary>
        /// <param name="history"></param>
        /// <param name="targetRound"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool Undo(List<int[,]> history, int targetRound, GomokuBoard board)
        {
            if (targetRound > 0 && targetRound < history.Count)
            {
                //save current board info
                int[,] currentBoard = new int[board.Size, board.Size];

                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        currentBoard[i, j] = board.Cells[i][j];
                    }
                }
                redoStack.Push(currentBoard);

                int[,] targetBoard = history[targetRound - 1];
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        board.Cells[i][j] = targetBoard[i, j];
                    }
                }
                Console.WriteLine("Undo to round {0}", targetRound);
                board.PrintBoard(targetRound);
                return true;
            }
            else
            {
                Console.WriteLine("Error: Invalid round number.");
                return false;
            }

        }

        public List<NotaktoBoard> Redo()
        {
            throw new NotImplementedException();
        }
        public void SaveBoardState(GomokuBoard board)
        {
            throw new NotImplementedException();
        }

    }
}
