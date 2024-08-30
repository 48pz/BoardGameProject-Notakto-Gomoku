
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
        private GomokuActionManager am;
        private List<int[,]> boardHistory = new List<int[,]>();


        public override void SetUp()
        {
            gomokuBoard = new GomokuBoard(10);
            gomokuBoard.PrintBoard(1);
            checker = new GomokuChecker();
            player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");
            gomokuBoard.GameMode = gameMode;
            saver = new GomokuSaver();
            am = new GomokuActionManager();
        }

        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
        }

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

                // 识别特殊指令坐标并处理
                if (pos == (999, 999))  // Save
                {
                    string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saver.SaveBoardInfo(gomokuBoard, baseDir);
                    Console.WriteLine("Game saved successfully.");
                    return true;
                }
                else if (pos == (998, 998))  // Load
                {
                    PerformLoadOperation(ref round);
                    return false;
                }
                else if (pos == (997, 997))  // Undo
                {
                    PerformUndoOperation(ref round);
                    return true;
                }
            }

            // 常规坐标验证和处理
            bool isValid = checker.IsValidPlace(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1);

            if (isValid)
            {
                if (gomokuBoard.PlaceChess(pos.Item1 - 1, pos.Item2 - 1, player))
                {
                    SaveBoardHistory();  // 每次下棋后保存状态
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

        private void PerformUndoOperation(ref int round)
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
                                am.Redo(gomokuBoard);
                                round = temp;
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

                        if (loadedBoard.ValidationStr.Equals(GlobalVar.GOMOKU))
                        {
                            gomokuBoard = loadedBoard;
                            Console.WriteLine("\nLoading Successfully!");
                            gomokuBoard.PrintBoard(round);
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
