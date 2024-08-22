

namespace BoardGameProject
{
    internal class NotaktoStrategy : IGameStrategy
    {
        /// <summary>
        /// singleton
        /// </summary>
        private static NotaktoStrategy _instance;
        private NotaktoStrategy() { }
        public static NotaktoStrategy GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NotaktoStrategy();
            }
            return _instance;
        }

        public void InitialiseBoard()
        {
            //List<NotaktoBoard> notaktoBoards = new List<NotaktoBoard>();
            //for (int i = 0; i < 3; i++)
            //{
            //    notaktoBoards.Add(new NotaktoBoard(3));
            //}

        }
    }
}
