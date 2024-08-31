

namespace BoardGameProject
{
    /// <summary>
    /// notakto checker class
    /// </summary>
    public class NotaktoChecker : IChecker<NotaktoBoard>
    {
        /// <summary>
        /// check validity
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="boardIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsValidPlace(List<NotaktoBoard> boardList, int boardIndex, int row, int col)
        {
            //boardIndex, row, col are valid
            if (boardIndex < 0 || boardIndex > 2 || row < 0 || row > 2 || col < 0 || col > 2)
            {
                return false;
            }
            var selectedBoard = boardList[boardIndex];
            //check the board is disabled
            if (selectedBoard.IsDisable) return false;
            //Check position occupancy
            if (selectedBoard.Cells[row][col] != 0) return false;

            return true;

        }

        /// <summary>
        /// check board availability
        /// </summary>
        /// <param name="boardList"></param>
        public void CheckAndDisableBoards(List<NotaktoBoard> boardList)
        {
            foreach (var board in boardList)
            {
                if (IsThreeInRow(board) || IsThreeInColumn(board) || IsThreeInDiagonal(board))
                {
                    board.IsDisable = true;
                }
            }
        }

        private bool IsThreeInRow(NotaktoBoard board)
        {
            for (int row = 0; row < board.Size; row++)
            {
                if (board.Cells[row][0] != 0 &&
                    board.Cells[row][1] != 0 &&
                    board.Cells[row][2] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsThreeInColumn(NotaktoBoard board)
        {
            for (int col = 0; col < board.Size; col++)
            {
                if (board.Cells[0][col] != 0 &&
                    board.Cells[1][col] != 0 &&
                    board.Cells[2][col] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsThreeInDiagonal(NotaktoBoard board)
        {
            if (board.Cells[0][0] != 0 &&
                board.Cells[1][1] != 0 &&
                board.Cells[2][2] != 0)
            {
                return true;
            }

            if (board.Cells[0][2] != 0 &&
                board.Cells[1][1] != 0 &&
                board.Cells[2][0] != 0)
            {
                return true;
            }

            return false;
        }
        

        /// <summary>
        /// All boards have isdisable = true
        /// </summary>
        /// <param name="boardList"></param>
        /// <returns></returns>
        public bool IsWin(List<NotaktoBoard> boardList)
        {
            bool allDisabled = boardList.All(board => board.IsDisable);

            if (allDisabled)
            {
                Console.WriteLine("All boards are disabled. Current player loses the game.");
                return true;
            }
            return false;
        }



        public bool IsDraw(NotaktoBoard board)
        {
            throw new NotImplementedException();
        }
        public bool IsValidPlace(NotaktoBoard board, int row, int col)
        {
            throw new NotImplementedException();
        }
        public bool IsWin(NotaktoBoard board, int row, int col, int player)
        {
            throw new NotImplementedException();
        }
    }
}
