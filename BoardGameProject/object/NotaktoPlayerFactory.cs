

namespace BoardGameProject
{
    /// <summary>
    /// notakto player factory: factory pattern
    /// </summary>
    public class NotaktoPlayerFactory
    {
        public static NotaktoPlayerBase CreatePlayer(string playerType)
        {

            switch (playerType)
            {
                case GlobalVar.HUMAN:
                    return new NotaktoHumanPlayer();
                case GlobalVar.COMPUTER:
                    return new NotaktoComputerPlayer();
                default:
                    throw new ArgumentException("Invalid player type");
            }
        }
    }
}
