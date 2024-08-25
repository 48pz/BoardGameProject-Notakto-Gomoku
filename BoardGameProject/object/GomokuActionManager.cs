

namespace BoardGameProject
{
    public class GomokuActionManager : IActionManager<GomokuBoard>
    {
        private List<int[,]> history = new List<int[,]>();
        private Stack<int[,]> redoStack = new Stack<int[,]>();


        public void Clear()
        {
            history.Clear();
            redoStack.Clear();
        }

        public void SaveBoardState(GomokuBoard board)
        {
            int[,] currentState = new int[board.Size, board.Size];
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    currentState[i, j] = board.Cells[i][j];
                }
            }
            history.Add(currentState);
        }

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
                board.PrintBoard(history.Count);
                return true;
            }
            else
            {
                Console.WriteLine("No moves to redo.");
                return false;
            }
        }

        public bool Undo(List<int[,]> history, int targetRound, GomokuBoard board)
        {
            if (targetRound > 0 && targetRound < history.Count)
            {
                int[,] targetBoard = history[targetRound];
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

        //public bool Redo(GomokuBoard board)
        //{
        //    if (redoStack.Count > 0 && lastMoveByHumanPlayer)
        //    {
        //        Move move = redoStack.Pop();
        //        board.Cells[move.Row][move.Col] = move.CurrentPlayer;
        //        undoStack.Push(move);
        //        Console.WriteLine($"Redo：Player{move.CurrentPlayer} at ({move.Row + 1}, {move.Col + 1})");
        //        board.PrintBoard();
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error: Redo failed!");
        //        return false;
        //    }
        //}

        //public bool Undo(GomokuBoard board)
        //{
        //    if (undoStack.Count > 0 && lastMoveByHumanPlayer)
        //    {
        //        Move lastMove = undoStack.Pop();
        //        redoStack.Push(lastMove);
        //        board.Cells[lastMove.Row][lastMove.Col] = 0;
        //        Console.WriteLine($"Undo：Player{lastMove.CurrentPlayer} at ({lastMove.Row + 1}, {lastMove.Col + 1})");
        //        board.PrintBoard();
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error: Undo failed!");
        //        return false;
        //    }
        //}
    }
}
