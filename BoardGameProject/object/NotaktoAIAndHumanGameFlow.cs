namespace BoardGameProject
{
    /// <summary>
    /// notakto computer vs. human game flow class
    /// </summary>
    public class NotaktoAIAndHumanGameFlow : GameFlowBase
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

        public NotaktoAIAndHumanGameFlow(string aGameType, string aGameMode, UIBase aUi)
        {
            gameMode = aGameMode;
            gameType = aGameType;
            ui = aUi;
        }

        private NotaktoPlayerBase player1;
        private NotaktoPlayerBase player2;
        public NotaktoBoard notaktoBoard1;
        public NotaktoBoard notaktoBoard2;
        public NotaktoBoard notaktoBoard3;
        public List<NotaktoBoard> boardList;
        private NotaktoChecker checker;
        private NotaktoSaver saver;
        private NotaktoActionManager am;
        private List<List<int[,]>> boardsHistory = new List<List<int[,]>>();
        private NotaktoManual manual;

        /// <summary>
        /// after gameover
        /// </summary>
        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(); 
        }

        /// <summary>
        /// main operations
        /// </summary>
        public override void SetUp()
        {
            var boards = BoardFactory.CreateBoard(GlobalVar.NOTAKTO);
            Console.WriteLine("\nRound: {0}", 1);
            boardList = new List<NotaktoBoard>(3);
            for (int i = 1; i <= boards.Count; i++)
            {
                switch (i)
                {
                    case 1:
                        notaktoBoard1 = boards[i - 1] as NotaktoBoard;
                        notaktoBoard1.GameMode = gameMode;
                        boardList.Add(notaktoBoard1);
                        break;
                    case 2:
                        notaktoBoard2 = boards[i - 1] as NotaktoBoard;
                        notaktoBoard2.GameMode = gameMode;
                        boardList.Add(notaktoBoard2);
                        break;

                    case 3:
                        notaktoBoard3 = boards[i - 1] as NotaktoBoard;
                        notaktoBoard3.GameMode = gameMode;
                        boardList.Add(notaktoBoard3);
                        break;
                }
            }
            foreach (var board in boardList)
            {
                board.PrintBoard(1);
            }

            checker = new NotaktoChecker();
            player1 = NotaktoPlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            player2 = NotaktoPlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");
            saver = new NotaktoSaver();
            am = new NotaktoActionManager();
            manual = new NotaktoManual();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isGameOver"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public override bool SelectPosition(ref int player, out bool isGameOver, ref int round)
        {
            isGameOver = false;
            foreach (var board in boardList)
            {
                board.CurrentPlayer = player;
            }
            List<int> pos;
            if (player == 1)
            {
                pos = player1.GetPosition(boardList);
            }
            else
            {

                pos = player2.GetPosition();

                Command cmd = (Command)pos[0];
                //save
                if (cmd.Equals(Command.save))
                {
                    string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saver.SaveBoardInfo(boardList, baseDir);
                    Console.WriteLine("Game saved successfully.");
                    return true;
                }
                else if (cmd.Equals(Command.help))
                {
                    manual.DisplayUserManual();
                    return false;
                }
                else if (cmd.Equals(Command.quit))
                {
                    isGameOver = true;
                    return true;
                }
                else if (cmd.Equals(Command.load))  // Load
                {
                    boardList = saver.LoadGame();
                    PrintBoardList(boardList[0].Round);

                    return false;
                }
                else if (cmd.Equals(Command.undo))
                {
                    while (true)
                    {
                        Console.WriteLine("Which round do you want to undo to?");
                        if (int.TryParse(Console.ReadLine(), out int inputRound))
                        {
                            if (inputRound > 0 && inputRound < round)
                            {
                                boardList = am.Undo(boardsHistory, inputRound-1);
                                if (boardList == null) continue;
                                Console.WriteLine($"Undo to round {inputRound} completed.");
                                PrintBoardList(inputRound);
                                int temp = round;
                                round = inputRound;
                                //redo
                                Console.WriteLine("Confirm undo: enter undo to confirm; enter redo to cancel.");

                                pos = player2.GetPosition();
                                Command comfirm = (Command)pos[0];
                                if (comfirm.Equals(Command.redo))
                                {
                                    notaktoBoard1.Round = temp - 1;
                                    boardList = am.Redo();
                                    PrintBoardList(notaktoBoard1.Round);
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


            bool isValid = checker.IsValidPlace(boardList, pos[0] - 1, pos[1] - 1, pos[2] - 1);
            if (isValid)
            {
                if (PlaceChess(boardList, pos, player))
                {
                    //update round
                    boardList[0].Round = round;
                    SaveBoardHistory();
                    Console.WriteLine($"Player {player} places at Board number:{pos[0]}, Row:{pos[1]}, Column:{pos[2]}");
                }
                else
                {
                    Console.WriteLine("Failed to place move.");
                }
                //print board
                for (int i = 1; i <= boardList.Count; i++)
                {
                    if (i == 1)
                    {
                        Console.WriteLine("\nRound: {0}", round);
                    }
                    boardList[i - 1].PrintBoard(round);
                }

                checker.CheckAndDisableBoards(boardList);
                if (checker.IsWin(boardList))
                {
                    int winner = player == 1 ? 2 : 1;
                    Console.WriteLine($"Game End: Player {winner} Won!");
                    isGameOver = true;
                }

            }
            else
            {
                Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                return false;
            }
            return true;


        }


        /// <summary>
        /// print all of boards
        /// </summary>
        /// <param name="round"></param>
        private void PrintBoardList(int round)
        {
            for (int i = 1; i <= boardList.Count; i++)
            {
                if (i == 1)
                {
                    Console.WriteLine("\nRound: {0}", round);
                }
                boardList[i - 1].PrintBoard(round);
            }
        }


        /// <summary>
        /// save board history
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="boardsHistory"></param>
        private void SaveBoardHistory()
        {
            List<int[,]> currentBoardsState = new List<int[,]>();

            foreach (var board in boardList)
            {
                int size = board.Size;
                int[,] boardState = new int[size, size];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        boardState[i, j] = board.Cells[i][j];
                    }
                }

                currentBoardsState.Add(boardState);
            }

            boardsHistory.Add(currentBoardsState);
        }



        /// <summary>
        /// Assign which board to play chess
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="pos"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool PlaceChess(List<NotaktoBoard> boardList, List<int> pos, int player)
        {
            int boardIndex = pos[0] - 1;
            int row = pos[1] - 1;
            int col = pos[2] - 1;

            if (boardIndex < 0 || boardIndex >= boardList.Count)
            {
                Console.WriteLine("Invalid board index.");
                return false;
            }

            var selectedBoard = boardList[boardIndex];

            return selectedBoard.PlaceChess(row, col, player);
        }

    }
}
