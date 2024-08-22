
using System;

namespace BoardGameProject
{
    public class ComputerPlayer : PlayerBase
    {
        private Random random = new Random();

        public override void GetPosition(IBoard board)
        {
            //human
            if (board == null)
            {

            }
            else//computer
            {
                var avaliablePos = board.GetAvaliablePositions();
                if (avaliablePos.Count > 0)
                {
                    var (row, col) = avaliablePos[random.Next(avaliablePos.Count)];
                    if (board.PlaceChess(row, col, 1))
                    { // Assume 1 represents the computer player
                        Console.WriteLine($"Computer places at {row + 1}, {col + 1}");
                        board.PrintBoard();
                    }
                    else
                    {
                        Console.WriteLine("Failed to place move");
                    }
                }
                else
                {
                    //Console.WriteLine("No empty positions available."); end game
                }
            }
        }

        public override void PassPosition()
        {
            throw new NotImplementedException();
        }
    }
}
