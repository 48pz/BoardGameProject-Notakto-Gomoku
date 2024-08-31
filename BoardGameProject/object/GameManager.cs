
namespace BoardGameProject
{
    public class GameManager
    {
        private const string NOTAKTO_TYPE = "1";
        private const string GOMOKU_TYPE = "2";
        private const string HUMANVSHUMAN = "2";
        private const string COMPUTERVSHUMAN = "1";
        private string gameType;
        private string gameMode;
        private GameFlowBase gf;


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


        /// <summary>
        /// Main thread
        /// </summary>
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


                //Assign different game flows according to game types and modes
                if (gameType == GlobalVar.GOMOKU)
                {
                    //computer vs human logic
                    if (gameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                    {
                        gf = new GomokuAIAndHumanGameFlow(gameType, gameMode, ui);
                    }
                    else//human vs human logic
                    {
                        gf = new GomokuHumanAndHumanGameFlow(gameType, gameMode, ui);
                    }
                }
                else
                {
                    //computer vs human logic
                    if (gameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                    {
                        gf = new NotaktoAIAndHumanGameFlow(gameType, gameMode, ui);
                    }
                    else//human vs human logic
                    {
                        gf = new NotaktoHumanAndHumanGameFlow(gameType, gameMode, ui);
                    }

                }
                gf.play();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

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
                    gameMode = GlobalVar.HUMANVSHUMAN;
                    gameTypeMode = false;
                }
                else if (inputGameMode.Equals(COMPUTERVSHUMAN))
                {
                    gameMode = GlobalVar.COMPUTERVSHUMAN;
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
                    gameType = GlobalVar.NOTAKTO;
                    DisplayNotaktoManual();
                    gameTypeFlag = false;
                }
                else if (inputGameType.Equals(GOMOKU_TYPE))
                {
                    ui = new GomokuUI();
                    gameType = GlobalVar.GOMOKU;
                    DisplayGomokuManual();
                    gameTypeFlag = false;

                }
                else if (inputGameType.Equals(GlobalVar.QUIT))
                {
                    gameTypeFlag = false;
                    quitFlag = true;
                }
                else
                {
                    Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                }
            }
        }

        /// <summary>
        /// display notakto manual
        /// </summary>
        private void DisplayNotaktoManual()
        {
            NotaktoManual manual = new NotaktoManual();
            manual.DisplayUserManual();
        }

        /// <summary>
        /// display Gomoku manual
        /// </summary>
        private void DisplayGomokuManual()
        {
            GomokuManual manual = new GomokuManual();
            manual.DisplayUserManual();
        }    

    }
}

