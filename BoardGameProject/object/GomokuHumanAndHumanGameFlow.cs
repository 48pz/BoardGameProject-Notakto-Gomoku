

using System.Text.Json;

namespace BoardGameProject
{
    public class GomokuHumanAndHumanGameFlow : GameFlowBase
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

        public GomokuHumanAndHumanGameFlow(string aGameType, string aGameMode, UIBase aUi)
        {
            gameMode = aGameMode;
            gameType = aGameType;
            ui = aUi;
        }

        private GomokuPlayerBase player1;
        private GomokuPlayerBase player2;
        public GomokuBoard gomokuBoard;
        private GomokuChecker checker;
        private GomokuSaver saver;
        private GomokuActionManager am;
        private List<int[,]> boardHistory = new List<int[,]>();

        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        public override bool SelectPosition(ref int player, out bool isGameOver, ref int round)
        {
            isGameOver = false;
            bool isValid;
            (int, int) pos;
            gomokuBoard.CurrentPlayer = player;
            if (player == 1)
            {
                pos = player1.GetPosition();
            }
            else
            {
                pos = player2.GetPosition();
            }
            //save
            if (pos == (999, 999))
            {
                string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                gomokuBoard.Round = round;
                saver.SaveBoardInfo(gomokuBoard, baseDir);
               
                return true;
            }
            //load
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

                            if (board.ValidationStr.Equals(GlobalVar.GOMOKU) && board.GameMode.Equals(GlobalVar.HUMANVSHUMAN))
                            {
                                gomokuBoard = board;
                                Console.WriteLine("\nLoading Successfully!");
                                round = board.Round;
                                gomokuBoard.PrintBoard(round);
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("Error: Wrong file type. Please check the game type and game mode are correct.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            //undo
            else if (pos == (997, 997))
            {
                while (true)
                {
                    Console.WriteLine("Which round do you want to undo to?");
                    int inputRound;
                    if (int.TryParse(Console.ReadLine(), out inputRound))
                    {
                        if (inputRound > 0 && inputRound < round)
                        {
                            if (am.Undo(boardHistory, inputRound, gomokuBoard))
                            {
                                int temp = round;
                                round = inputRound;
                                //redo
                                Console.WriteLine("Confirm undo: enter undo to confirm; enter redo to cancel.");
                                (int, int) confirm;
                                confirm =  player ==1? confirm = player1.GetPosition(): confirm = player2.GetPosition();
                                
                                
                                if (confirm == (996, 996))
                                {
                                    gomokuBoard.Round = temp - 1;
                                    am.Redo(gomokuBoard);
                                    round = temp;
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter the position.");
                                    round++;
                                    pos = player == 1 ? pos = player1.GetPosition() : pos = player2.GetPosition();
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error: enter a number between 1 and {0}", (round - 1));
                            }
                        }
                        else
                        {
                            Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                        }

                    }


                }
                return false;

            }


            isValid = checker.IsValidPlace(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1);


            //check validity then place chess
            if (isValid)//input valid
            {
                if (gomokuBoard.PlaceChess(pos.Item1 - 1, pos.Item2 - 1, player))
                {
                    //am.RecordMove(new Move(pos.Item1 - 1, pos.Item2 - 1, player));
                    SaveBoardHistory();
                    Console.WriteLine($"Player{player} places at {pos.Item1}, {pos.Item2}");
                }
                else
                {
                    Console.WriteLine("Failed to place move");

                }
                gomokuBoard.PrintBoard(round);
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

        public override void SetUp()
        {
            var boards = BoardFactory.CreateBoard(GlobalVar.GOMOKU);
            gomokuBoard = boards[0] as GomokuBoard;

            if (gomokuBoard != null)
            {
                gomokuBoard.PrintBoard(1);
            }
            else
            {
                Console.WriteLine("Failed to convert IBoard to GomokuBoard.");
            }
            gomokuBoard.PrintBoard(1);
            checker = new GomokuChecker();
            player2 = GomokuPlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = GomokuPlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            Console.WriteLine("\nPlayer1: Human");
            Console.WriteLine("Player2: Human");
            gomokuBoard.GameMode = gameMode;
            saver = new GomokuSaver();
            am = new GomokuActionManager();
        }

        public void SaveBoardHistory()
        {
            int[,] currentBoard = new int[gomokuBoard.Size, gomokuBoard.Size];
            for (int i = 0; i < gomokuBoard.Size; i++)
            {
                for (int j = 0; j < gomokuBoard.Size; j++)
                {
                    currentBoard[i, j] = gomokuBoard.Cells[i][j];
                }
            }
            boardHistory.Add(currentBoard);
        }
    }
}
