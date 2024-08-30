namespace BoardGameProject
{

    /// <summary>
    /// game flow"Template Method Pattern
    /// </summary>
    public abstract class GomokuGameFlowBase
    {
        public void play()
        {
            try
            {
                bool isGameOver = false;
                int currentPlayer = 1;
                int round = 1;
                SetUp();//it should include reloading game.

                while (!isGameOver)
                {
                    while (!SelectPosition(ref currentPlayer, out isGameOver, ref round))
                    {
                        continue;
                    }
                    currentPlayer = currentPlayer == 1 ? 2 : 1;//switch player
                    round++;
                }
                End();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

        }



        public abstract void SetUp();
        public abstract bool SelectPosition(ref int player, out bool isGameOver, ref int round);
        public abstract void End();
    }
}
