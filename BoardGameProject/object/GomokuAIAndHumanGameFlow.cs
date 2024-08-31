
using System.Text.Json;

namespace BoardGameProject
{
    /// <summary>
    /// Gomoku, Computer vs. human game flow
    /// </summary>
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


        private GomokuPlayerBase player1;
        private GomokuPlayerBase player2;
        public GomokuBoard gomokuBoard;
        private GomokuChecker checker;
        private GomokuSaver saver;
        private GomokuActionManager am;
        private List<int[,]> boardHistory = new List<int[,]>();

        /// <summary>
        /// initialisation
        /// </summary>
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
            checker = new GomokuChecker();
            player2 = GomokuPlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = GomokuPlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");
            gomokuBoard.GameMode = gameMode;
            saver = new GomokuSaver();
            am = new GomokuActionManager();
        }

        /// <summary>
        /// after game over
        /// </summary>
        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }


        /// <summary>
        /// main operation
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isGameOver"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public override bool SelectPosition(ref int player, out bool isGameOver, ref int round)
        {
            isGameOver = false;
            (int, int) pos;
            gomokuBoard.CurrentPlayer = player;

            if (player == 1)
            {
                pos = player1.GetPosition(gomokuBoard);
            }
            else
            {
                pos = player2.GetPosition();

                
                if (pos == (999, 999))  // Save
                {
                    string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saver.SaveBoardInfo(gomokuBoard, baseDir);
                    Console.WriteLine("Game saved successfully.");
                    //save then game over;
                    isGameOver = true;
                    return true;
                }
                else if (pos == (998, 998))  // Load
                {
                    PerformLoadOperation(ref round);
                    return false;
                }
                else if (pos == (997, 997))  // Undo
                {
                    while (true)
                    {
                        Console.WriteLine("Which round do you want to undo to?");
                        if (int.TryParse(Console.ReadLine(), out int inputRound))
                        {
                            if (inputRound > 0 && inputRound < round)
                            {
                                if (am.Undo(boardHistory, inputRound, gomokuBoard))
                                {
                                    Console.WriteLine($"Undo to round {inputRound} completed.");

                                    int temp = round;
                                    round = inputRound;
                                    //redo
                                    Console.WriteLine("Confirm undo: enter undo to confirm; enter redo to cancel.");
                                    (int, int) confirm = player2.GetPosition();
                                    if (confirm == (996, 996))
                                    {
                                        gomokuBoard.Round = temp - 1;
                                        am.Redo(gomokuBoard);
                                        return false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter the position.");
                                        round++;
                                        pos = player2.GetPosition();
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error: enter a number between 1 and {round - 1}.");
                            }
                        }
                    }
                }
            }

            
            bool isValid = checker.IsValidPlace(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1);

            if (isValid)
            {
                if (gomokuBoard.PlaceChess(pos.Item1 - 1, pos.Item2 - 1, player))
                {
                    SaveBoardHistory();  
                    Console.WriteLine($"Player {player} places at {pos.Item1}, {pos.Item2}");
                }
                else
                {
                    Console.WriteLine("Failed to place move.");
                }

                gomokuBoard.PrintBoard(round);

                if (checker.IsDraw(gomokuBoard))
                {
                    ui.DisplayInfo("Game End: Draw!");
                    isGameOver = true;
                }
                else if (checker.IsWin(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1, player))
                {
                    Console.WriteLine($"Game End: Player {player} Won!");
                    isGameOver = true;
                }

                return true;
            }
            else
            {
                Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                return false;
            }
        }


        /// <summary>
        /// load
        /// </summary>
        /// <param name="round"></param>
        private void PerformLoadOperation(ref int round)
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
                        GomokuBoard loadedBoard = JsonSerializer.Deserialize<GomokuBoard>(jsonStr, options);

                        if (loadedBoard.ValidationStr.Equals(GlobalVar.GOMOKU) && loadedBoard.GameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                        {
                            gomokuBoard = loadedBoard;
                            Console.WriteLine("\nLoading Successfully!");
                            gomokuBoard.PrintBoard(round);
                            return;
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

        /// <summary>
        /// save history
        /// </summary>
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
