
namespace BoardGameProject
{
    public class GameManager
    {
        private const string NOTAKTO_TYPE = "1";
        private const string GOMOKU_TYPE = "2";
        private const string HUMANVSHUMAN = "1";
        private const string COMPUTERVSHUMAN = "2";
        private string _gameType;
        private string _gameMode;


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

                quitFlag = ChooseGameMode(quitFlag);
                if (quitFlag == true) return;

                Setup();
                ////choose gomoku
                //if (_gameType.Equals(GlobalVar.GOMOKU))
                //{
                    
                //}
                //else//choose notakto
                //{

                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

        }

        /// <summary>
        /// set up game config
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Setup()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// select game mode
        /// </summary>
        /// <param name="quitFlag"></param>
        /// <returns></returns>
        private bool ChooseGameMode(bool quitFlag)
        {
            bool gameTypeMode = true;

            while (gameTypeMode)
            {
                string inputGameMode = Console.ReadLine();
                if (inputGameMode.Equals(HUMANVSHUMAN))
                {
                    _gameMode = GlobalVar.HUMANVSHUMAN;
                    gameTypeMode = false;
                }
                else if (inputGameMode.Equals(GOMOKU_TYPE))
                {
                    _gameMode = GlobalVar.COMPUTERVSHUMAN;
                    gameTypeMode = false;

                }
                else if (inputGameMode.Equals(GlobalVar.QUIT))
                {
                    gameTypeMode = false;
                    quitFlag = true;
                }
                else
                {
                    Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                }
            }

            return quitFlag;
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
                    _gameType = GlobalVar.NOTAKTO;
                    gameTypeFlag = false;
                }
                else if (inputGameType.Equals(GOMOKU_TYPE))
                {
                    ui = new GomokuUI();
                    _gameType = GlobalVar.GOMOKU;
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
