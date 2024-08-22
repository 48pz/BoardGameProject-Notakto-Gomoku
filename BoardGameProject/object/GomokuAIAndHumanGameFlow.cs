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
            player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");

        }

        public override void End()
        {
            throw new NotImplementedException();
        }

        public override void SelectPosition(int player)
        {
            var pos = player1.GetPosition(gomokuBoard);
            bool isValid = checker.IsValidPlace(gomokuBoard, pos.Item1, pos.Item2);
            if (isValid)//input valid
            {
                if (checker.IsDraw(gomokuBoard))
                {
                    gomokuBoard.PrintBoard();
                    ui.DisplayInfo("Game End: Draw!");
                }else if (checker.IsWin(gomokuBoard, pos.Item1,pos.Item2, player)){
                    gomokuBoard.PrintBoard();
                    Console.WriteLine("Game End: Player{0} Win!", player);
                }

            }
            else //invalid
            {
                Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
            }

        }

        public override void CheckPositionValid(int player)
        {
            throw new NotImplementedException();
        }
    }
}
