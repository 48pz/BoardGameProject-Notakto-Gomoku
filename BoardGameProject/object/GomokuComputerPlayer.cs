
using System;

namespace BoardGameProject
{
    public class GomokuComputerPlayer : GomokuPlayerBase
    {
        private Random random = new Random();

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
            }

            return (0, 0);
        }

        public override void PassPosition()
        {
            throw new NotImplementedException();
        }

    }
}
