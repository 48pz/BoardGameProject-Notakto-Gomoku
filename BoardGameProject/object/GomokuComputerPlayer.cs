

namespace BoardGameProject
{
    /// <summary>
    /// gomoku ai player class
    /// </summary>
    public class GomokuComputerPlayer : GomokuPlayerBase
    {
        private Random random = new Random();

        /// <summary>
        /// get random position
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public override (int, int) GetPosition(IBoard board)
        {
            var avaliablePos = board.GetAvaliablePositions();
            if (avaliablePos.Count > 0)
            {
                var (row, col) = avaliablePos[random.Next(avaliablePos.Count)];
                return (row+1, col+1);

            }
            else
            {
                //Console.WriteLine("No empty positions available."); end game
                return (0, 0);
            }
            
        }
    }
}
