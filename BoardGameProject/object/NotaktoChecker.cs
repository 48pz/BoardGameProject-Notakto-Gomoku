

namespace BoardGameProject
{
    public class NotaktoChecker : IChecker<NotaktoBoard>
    {
        //public bool IsDraw(NotaktoBoard board)
        //{
        //    // Not applicable for Notakto.
        //    return false;
        //}

        //public bool IsValidPlace(NotaktoBoard board, int row, int col)
        //{
        //    if (board.IsBoardLocked(board.CurrentBoardIndex))
        //    {
        //        return false; 
        //    }

        //    return row >= 0 && row < board.Size && col >= 0 && col < board.Size && board.Boards[board.CurrentBoardIndex][row][col] == 0;
        //}

        //public bool IsWin(NotaktoBoard board, int row, int col, int player)
        //{
        //    bool isWin = CheckDirection(board, row, col, player, 1, 0) ||  
        //                 CheckDirection(board, row, col, player, 0, 1) ||  
        //                 CheckDirection(board, row, col, player, 1, 1) ||  
        //                 CheckDirection(board, row, col, player, 1, -1);   

        //    if (isWin)
        //    {
        //        board.LockBoard(board.CurrentBoardIndex); 
        //    }

        //    return isWin;
        //}

        //private bool CheckDirection(NotaktoBoard board, int row, int col, int player, int v1, int v2)
        //{
        //    int count = 1;
        //    count += CountDirection(board, row, col, player, v1, v2);
        //    count += CountDirection(board, row, col, player, -v1, -v2);
        //    return count >= 3;
        //}
        //private int CountDirection(NotaktoBoard board, int row, int col, int player, int v1, int v2)
        //{
        //    int count = 0;
        //    row += v1;
        //    col += v2;
        //    while (row >= 0 && row < board.Size && col >= 0 && col < board.Size 
        //        && board.Boards[board.CurrentBoardIndex][row][col] == player)
        //    {
        //        count++;
        //        row += v1;
        //        col += v2;
        //    }
        //    return count;
        //}
        public bool IsDraw(NotaktoBoard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidPlace(NotaktoBoard board, int row, int col)
        {
            throw new NotImplementedException();
        }

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

        public bool IsWin(NotaktoBoard board, int row, int col, int player)
        {
            throw new NotImplementedException();
        }


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
        //internal void CheckAndDisableBoards(List<NotaktoBoard> boardList)
        //{
        //    foreach (var board in boardList)
        //    {
        //        if (IsThreeInRow(board) || IsThreeInColumn(board) || IsThreeInDiagonal(board))
        //        {
        //            board.IsDisable = true;
        //        }
        //    }
        //}

        //private bool IsThreeInRow(NotaktoBoard board)
        //{
        //    for (int row = 0; row < board.Size; row++)
        //    {
        //        if (board.Cells[row][0] != 0 &&
        //            board.Cells[row][0] == board.Cells[row][1] &&
        //            board.Cells[row][1] == board.Cells[row][2])
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsThreeInColumn(NotaktoBoard board)
        //{
        //    for (int col = 0; col < board.Size; col++)
        //    {
        //        if (board.Cells[0][col] != 0 &&
        //            board.Cells[0][col] == board.Cells[1][col] &&
        //            board.Cells[1][col] == board.Cells[2][col])
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsThreeInDiagonal(NotaktoBoard board)
        //{
        //    if (board.Cells[0][0] != 0 &&
        //        board.Cells[0][0] == board.Cells[1][1] &&
        //        board.Cells[1][1] == board.Cells[2][2])
        //    {
        //        return true;
        //    }

        //    if (board.Cells[0][2] != 0 &&
        //        board.Cells[0][2] == board.Cells[1][1] &&
        //        board.Cells[1][1] == board.Cells[2][0])
        //    {
        //        return true;
        //    }

        //    return false;
        //}

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
    }
}
