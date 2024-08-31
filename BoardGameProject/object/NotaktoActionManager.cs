

namespace BoardGameProject
{
    /// <summary>
    /// notakto redo and undo class
    /// </summary>
    public class NotaktoActionManager : IActionManager<NotaktoBoard>
    {
        private Stack<List<int[,]>> redoStack = new Stack<List<int[,]>>();
        public void Clear()
        {
            redoStack.Clear();
        }

        /// <summary>
        /// redo
        /// </summary>
        /// <returns></returns>
        public List<NotaktoBoard> Redo()
        {
            if (redoStack.Count == 0)
            {
                Console.WriteLine("Error: No moves to redo.");
                return null;
            }

            List<int[,]> boardState = redoStack.Pop();
            List<NotaktoBoard> boards = new List<NotaktoBoard>();

            foreach (var state in boardState)
            {
                NotaktoBoard board = new NotaktoBoard(state.GetLength(0));

                for (int i = 0; i < state.GetLength(0); i++)
                {
                    for (int j = 0; j < state.GetLength(1); j++)
                    {
                        board.Cells[i][j] = state[i, j];
                    }
                }

                boards.Add(board);
            }

            return boards;
        }

        /// <summary>
        /// undo function
        /// </summary>
        /// <param name="boardsHistory"></param>
        /// <param name="targetRound"></param>
        /// <returns></returns>
        public List<NotaktoBoard> Undo(List<List<int[,]>> boardsHistory, int targetRound)
        {
            if (targetRound < 0 || targetRound >= boardsHistory.Count)
            {
                Console.WriteLine("Error: Invalid round number.");
                return null;
            }

            List<int[,]> targetBoardState = boardsHistory[targetRound];
            List<NotaktoBoard> boards = new List<NotaktoBoard>();

            List<int[,]> currentBoardState = new List<int[,]>();
            foreach (var board in boardsHistory[boardsHistory.Count - 1]) 
            {
                int size = board.GetLength(0);
                int[,] boardCopy = new int[size, size];
                Array.Copy(board, boardCopy, board.Length);
                currentBoardState.Add(boardCopy);
            }
            redoStack.Push(currentBoardState);

            foreach (var boardState in targetBoardState)
            {
                NotaktoBoard board = new NotaktoBoard(boardState.GetLength(0));

                for (int i = 0; i < boardState.GetLength(0); i++)
                {
                    for (int j = 0; j < boardState.GetLength(1); j++)
                    {
                        board.Cells[i][j] = boardState[i, j];
                    }
                }

                boards.Add(board);
            }

            return boards;
        }



        public bool Undo(List<int[,]> history, int targetRound, NotaktoBoard board)
        {
            throw new NotImplementedException();
        }
        public bool Redo(NotaktoBoard board)
        {
            throw new NotImplementedException();
        }
        public void SaveBoardState(NotaktoBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
