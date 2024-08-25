
using System.Text.Json;

namespace BoardGameProject
{
    public class GomokuAIAndHumanGameFlow : GameFlowBase
    {
        private string gameType;
        private string gameMode;
        private UIBase ui;

        public string GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        public string GameMode
        {

            get { return gameMode; }
            set { gameMode = value; }
        }
        public UIBase UI
        {
            get { return ui; }
            set { ui = value; }
        }

        public GomokuAIAndHumanGameFlow(string aGameType, string aGameMode, UIBase aUi)
        {
            gameMode = aGameMode;
            gameType = aGameType;
            ui = aUi;
        }


        private PlayerBase player1;
        private PlayerBase player2;
        public GomokuBoard gomokuBoard;
        private GomokuChecker checker;
        private GomokuSaver saver;


        

        public override void SetUp()
        {
            gomokuBoard = new GomokuBoard(10);
            gomokuBoard.PrintBoard();
            checker = new GomokuChecker();
            player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");
            gomokuBoard.GameMode = gameMode;
            saver = new GomokuSaver();

        }

        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
        }

        public override bool SelectPosition(ref int player, out bool isGameOver)
        {
            isGameOver = false;
            bool isValid;
            (int, int) pos;
            gomokuBoard.CurrentPlayer = player; 
            if (player == 1)
            {
                pos = player1.GetPosition(gomokuBoard);
            }
            else
            {
                pos = player2.GetPosition();
                if (pos == (999, 999))
                {
                    string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    //save to desktop
                    saver.SaveBoardInfo(gomokuBoard, baseDir);
                    //save then game over;
                    isGameOver = true;
                    return true;
                }
                else if (pos == (998, 998))
                {
                    Console.WriteLine("Please enter the FULL PATH to load the game:");

                    try
                    {
                        while (true)
                        {
                            string savePath = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(savePath) || !File.Exists(savePath))
                            {
                                Console.WriteLine("Error: file does not exist!");
                                continue;
                            }

                            string jsonStr = File.ReadAllText(savePath);
                            if (gameType.Equals(GlobalVar.GOMOKU))
                            {
                                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                                GomokuBoard board = JsonSerializer.Deserialize<GomokuBoard>(jsonStr, options);

                                if (board.ValidationStr.Equals(GlobalVar.GOMOKU))
                                {
                                    gomokuBoard = board;
                                    Console.WriteLine("\nLoading Successfully!");
                                    gomokuBoard.PrintBoard();
                                    return false;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            isValid = checker.IsValidPlace(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1);


            //check validity then place chess
            if (isValid)//input valid
            {
                if (gomokuBoard.PlaceChess(pos.Item1 - 1, pos.Item2 - 1, player))
                {
                    Console.WriteLine($"Player{player} places at {pos.Item1}, {pos.Item2}");
                }
                else
                {
                    Console.WriteLine("Failed to place move");

                }
                gomokuBoard.PrintBoard();
                if (checker.IsDraw(gomokuBoard))
                {

                    ui.DisplayInfo("Game End: Draw!");
                    isGameOver = true;

                }
                else if (checker.IsWin(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1, player))
                {

                    Console.WriteLine("Game End: Player{0} Won!", player);
                    isGameOver = true;

                }
                return true;

            }
            else //invalid
            {
                Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);

                return false;
            }


        }


    }
}
