
namespace BoardGameProject
{
    public class GameManager
    {
        private const string NOTAKTO_TYPE = "1";
        private const string GOMOKU_TYPE = "2";
        private string gameType;

        /// <summary>
        /// Singleton Pattern: only 1 gamemanager instance throughout the application
        /// </summary>
        private static GameManager _instance;
        private GameManager() { }
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public void RunApp()
        {
            try
            {
                UIBase.DisplayGameType();
                UIBase ui = null;

                bool quitFlag = false;
                ChooseGameType(ref ui, ref quitFlag);
                if (quitFlag == true) return;
                ui.DisplayGameMode();
                //choose gomoku
                if (gameType.Equals(GlobalVar.GOMOKU))
                {
                  
                }
                else//choose notakto
                {
                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());    
                throw;
            }
            
        }

        /// <summary>
        /// select game type
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="quitFlag"></param>
        private void ChooseGameType(ref UIBase ui, ref bool quitFlag)
        {
            bool gameTypeFlag = true;
           
            while (gameTypeFlag)
            {
                string inputGameType = Console.ReadLine();
                if (inputGameType.Equals(NOTAKTO_TYPE))
                {
                    ui = new NotaktoUI();
                    gameType = GlobalVar.NOTAKTO;
                    gameTypeFlag = false;
                }
                else if (inputGameType.Equals(GOMOKU_TYPE))
                {
                    ui = new GomokuUI();
                    gameType = GlobalVar.GOMOKU;
                    gameTypeFlag = false;

                }
                else if (inputGameType.Equals(GlobalVar.QUIT))
                {
                    gameTypeFlag = false;
                    quitFlag = true;
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid number, try again.");
                }

            }
        }
    }
}
