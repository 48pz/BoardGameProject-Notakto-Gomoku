namespace BoardGameProject
{
    public class NotaktoComputerPlayer : NotaktoPlayerBase
    {
        public override List<int> GetPosition(List<NotaktoBoard> boards)
        {
            List<(int boardIndex, int row, int col)> availablePos = new List<(int boardIndex, int row, int col)>();
            Random random = new Random();

            for (int i = 0; i < boards.Count; i++)
            {
                if (!boards[i].IsDisable)
                {
                    var emptyPositions = boards[i].GetAvaliablePositions();
                    foreach (var pos in emptyPositions)
                    {
                        availablePos.Add((i, pos.Item1, pos.Item2));
                    }
                }                  
            }

            if (availablePos.Count > 0)
            {
                var pos = availablePos[random.Next(availablePos.Count)];
                return new List<int> { pos.boardIndex +1 , pos.row+1, pos.col + 1 };
            }

            return null;
        }
    }
}
