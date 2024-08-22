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
            SetUp();//it should include reloading game.

            //while (!isGameOver)
            //{
                SelectPosition();
                //CheckPositionValid();

            //}
            //End();
        }

        public abstract void SetUp();
        public abstract void SelectPosition();
        public abstract void CheckPositionValid();
        public abstract void End();
    }
}
