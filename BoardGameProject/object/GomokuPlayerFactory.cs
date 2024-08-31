
namespace BoardGameProject
{
    /// <summary>
    /// class of player factory: factory method pattern
    /// </summary>
    public class GomokuPlayerFactory
    {
        public static GomokuPlayerBase CreatePlayer(string playerType)
        {

            switch (playerType)
            {
                case GlobalVar.HUMAN:
                    return new GomokuHumanPlayer();
                case GlobalVar.COMPUTER:
                    return new GomokuComputerPlayer();
                default:
                    throw new ArgumentException("Invalid player type");
            }

        }
    }
}
