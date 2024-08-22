
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

                //Setup(gameType, gameMode);

                if (gameType == GlobalVar.GOMOKU)   
                {
                    //computer vs human logic
                    if (gameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                    {
                         gf = new GomokuAIAndHumanGameFlow(gameType, gameMode, ui);
                                            }
                    else//human vs human logic
                    {
                         gf = new GomokuHumanAndHumanGameFlow();
                    }

                }
                else
                {
                    //computer vs human logic
                    if (gameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                    {
                         gf = new NotaktoAIAndHumanGameFlow();
                    }
                    else//human vs human logic
                    {
                         gf = new NotaktoHumanAndHumanGameFlow();
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
        /// set up game config
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="gameMode"></param>
        //private void Setup(string gameType, string gameMode)
        //{
        //    if (gameType.Equals(GlobalVar.GOMOKU))
        //    {
        //        strategy = GomokuStrategy.GetInstance();

        //    }
        //    else
        //    {
        //        strategy = NotaktoStrategy.GetInstance();

        //    }
        //    if (gameMode == GlobalVar.COMPUTERVSHUMAN)
        //    {
        //        player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
        //        player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
        //        Console.WriteLine("\nPlayer1: Computer");
        //        Console.WriteLine("Player2: Human");
        //    }
        //    else
        //    {
        //        player1 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
        //        player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
        //        Console.WriteLine("\nPlayer1: Human");
        //        Console.WriteLine("Player2: Human");
        //    }
        //    Console.WriteLine("Player1 plays X, Player2 plays O");
        //    strategy.InitialiseBoard();
        //}


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
                    Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                }

            }
        }
    }
}
