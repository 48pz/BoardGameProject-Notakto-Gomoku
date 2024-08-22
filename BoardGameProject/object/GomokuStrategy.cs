
namespace BoardGameProject
{
    internal class GomokuStrategy : IGameStrategy
    {
        /// <summary>
        /// singleton
        /// </summary>
        private static GomokuStrategy _instance;
        private GomokuStrategy() { }
        public static GomokuStrategy GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GomokuStrategy();
            }
            return _instance;
        }


        public void InitialiseBoard()
        {
            IBoard gomokuBoard = new GomokuBoard(10);
        }
    }
}
