namespace BoardGameProject
{

    /// <summary>
    /// game flow"Template Method Pattern
    /// </summary>
    public abstract class GameFlowBase
    {
        public void play()
        {
            try
            {
                bool isGameOver = false;
                int currentPlayer = 1;
                SetUp();//it should include reloading game.

                while (!isGameOver)
                {
                    while (!SelectPosition(currentPlayer, out isGameOver))
                    {
                        continue;
                    }
                    currentPlayer = currentPlayer == 1 ? 2 : 1;//switch player
                }
                End();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

        }



        public abstract void SetUp();
        public abstract bool SelectPosition(int player, out bool isGameOver);
        public abstract void End();
    }
}
