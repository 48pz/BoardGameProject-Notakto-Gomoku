

namespace BoardGameProject
{
    public class NotaktoActionManager : IActionManager<NotaktoBoard>
    {
        private List<List<int[,]>> history = new List<List<int[,]>>();
        private Stack<List<int[,]>> redoStack = new Stack<List<int[,]>>();

        public void Clear()
        {
            history.Clear();
            redoStack.Clear();
        }

        public void SaveBoardState(NotaktoBoard board)
        {
            List<int[,]> currentBoardsState = new List<int[,]>();
            for (int b = 0; b < board.Size; b++)
            {
                int[,] currentState = new int[board.Size, board.Size];
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        currentState[i, j] = board.Cells[i][j];
                    }
                }
                currentBoardsState.Add(currentState);
            }
            history.Add(currentBoardsState);
        }

        public bool Redo(NotaktoBoard board)
        {
            if (redoStack.Count > 0)
            {
                List<int[,]> redoState = redoStack.Pop();
                for (int b = 0; b < redoState.Count; b++)
                {
                    for (int i = 0; i < board.Size; i++)
                    {
                        for (int j = 0; j < board.Size; j++)
                        {
                            board.Boards[b][i][j] = redoState[b][i, j];
                        }
                    }
                }
                Console.WriteLine("Redo completed");
                board.PrintBoard(history.Count);
                return true;
            }
            else
            {
                Console.WriteLine("No moves to redo.");
                return false;
            }
        }

        public bool Undo(List<int[,]> history, int targetRound, NotaktoBoard board)
        {
            if (targetRound > 0 && targetRound < history.Count)
            {
                //save current board info
                List<int[,]> currentBoards = new List<int[,]>();
                for (int b = 0; b < board.Boards.Count; b++)
                {
                    int[,] currentBoard = new int[board.Size, board.Size];
                    for (int i = 0; i < board.Size; i++)
                    {
                        for (int j = 0; j < board.Size; j++)
                        {
                            currentBoard[i, j] = board.Boards[b][i][j];
                        }
                    }
                    currentBoards.Add(currentBoard);
                }
                redoStack.Push(currentBoards);

                // Restore to the target round state
                int[,] targetBoard = history[targetRound - 1];
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        board.Boards[board.CurrentBoardIndex][i][j] = targetBoard[i, j];
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

    }
}
