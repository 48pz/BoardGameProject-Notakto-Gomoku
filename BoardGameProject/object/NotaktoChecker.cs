
namespace BoardGameProject
{
    public class NotaktoChecker : IChecker<NotaktoBoard>
    {
        public bool IsDraw(NotaktoBoard board)
        {
            // Check if all boards have a triple 'X'
            for (int b = 0; b < board.Boards.Count; b++)
            {
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        // As long as there is an empty space on one board, it is not a draw
                        if (board.Boards[b][i][j] == 0) return false;
                    }
                }
                return true;
            }
            // All boards are full
            return false;
        }

        public bool IsValidPlace(NotaktoBoard board, int row, int col)
        {
            {
                if (row >= 0 && row < board.Size && col >= 0 && col < board.Size)
                {
                    return board.Boards[board.CurrentBoardIndex][row][col] == 0;
                }
                return false;
             }
        }

        public bool IsWin(NotaktoBoard board, int row, int col, int player)
        {
            // Check if the current player has formed a triple on the current board
            return CheckDirection(board, row, col, player, 1, 0) ||
                   CheckDirection(board, row, col, player, 0, 1) ||
                   CheckDirection(board, row, col, player, 1, 1) ||
                   CheckDirection(board, row, col, player, 1, -1);
        }

        private bool CheckDirection(NotaktoBoard board, int row, int col, int player, int v1, int v2)
        {
            int count = 1;
            count += CountDirection(board, row, col, player, v1, v2);
            count += CountDirection(board, row, col, player, -v1, -v2);
            return count >= 3;
        }
        private int CountDirection(NotaktoBoard board, int row, int col, int player, int v1, int v2)
        {
            int count = 0;
            row += v1;
            col += v2;
            while (row >= 0 && row < board.Size && col >= 0 && col < board.Size 
                && board.Boards[board.CurrentBoardIndex][row][col] == player)
            {
                count++;
                row += v1;
                col += v2;
            }
            return count;
        }
    }
}
