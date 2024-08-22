namespace BoardGameProject
{

    /// <summary>
    /// game flow"Template Method Pattern
    /// </summary>
    public abstract class GameFlowBase
    {
        public void play()
        {
            bool isGameOver = false;
            int currentPlayer = 1;
            SetUp();//it should include reloading game.

            while (!isGameOver)
            {
                while (!SelectPosition(currentPlayer))
                {
                    continue;
                }
                
                currentPlayer =  currentPlayer == 1 ? 2 : 1;//switch player
            }
            End();
        }


       
        public abstract void SetUp();
        public abstract bool SelectPosition(int  player);
        public abstract void End();
    }
}
