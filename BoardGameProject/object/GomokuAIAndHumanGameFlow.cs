namespace BoardGameProject
{
    public class GomokuAIAndHumanGameFlow : GameFlowBase
    {
        private string gameType;
        private string gameMode;
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

        public GomokuAIAndHumanGameFlow(string aGameType, string aGameMode)
        {
            gameMode = aGameMode;
            gameType = aGameType;   
        }

        private PlayerBase player1;
        private PlayerBase player2;
        public IBoard gomokuBoard;

        public override void SetUp()
        {
            gomokuBoard = new GomokuBoard(10);
            player2 = PlayerFactory.CreatePlayer(GlobalVar.HUMAN);
            player1 = PlayerFactory.CreatePlayer(GlobalVar.COMPUTER);
            Console.WriteLine("\nPlayer1: Computer");
            Console.WriteLine("Player2: Human");

        }

        public override void CheckPositionValid()
        {

        }

        public override void End()
        {
            throw new NotImplementedException();
        }

        public override void SelectPosition()
        {

            player1.GetPosition(gomokuBoard);
           
        }

       
    }
}
