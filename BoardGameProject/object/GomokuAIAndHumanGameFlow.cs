using System.Runtime.CompilerServices;

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


        public override void SetUp()
        {
            gomokuBoard = new GomokuBoard(10);

            checker = new GomokuChecker();
            player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");

        }

        public override void End()
        {
            Console.WriteLine("Game Over... See you next time...");
        }

        public override bool SelectPosition(int player, out bool isGameOver)
        {
            bool isValid;
            (int, int) pos;
            if (player == 1)
            {
                pos = player1.GetPosition(gomokuBoard);
            }
            else
            {
                pos = player2.GetPosition();

            }
            isValid = checker.IsValidPlace(gomokuBoard, pos.Item1 - 1, pos.Item2 - 1);
            isGameOver = false;

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

                    Console.WriteLine("Game End: Player{0} Win!", player);
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
