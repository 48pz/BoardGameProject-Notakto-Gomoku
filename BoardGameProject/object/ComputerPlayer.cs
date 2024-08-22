
using System;

namespace BoardGameProject
{
    public class ComputerPlayer : PlayerBase
    {
        private Random random = new Random();

        public override (int,int) GetPosition(IBoard board)
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
                    { 
                        Console.WriteLine($"Computer places at {row + 1}, {col + 1}");
                        return (row, col);
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
            return (0, 0);
        }

        public override void PassPosition()
        {
            throw new NotImplementedException();
        }
    }
}
