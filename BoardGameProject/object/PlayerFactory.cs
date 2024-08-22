
namespace BoardGameProject
{
    /// <summary>
    /// class of player factory: factory method pattern
    /// </summary>
    public class PlayerFactory
    {
        public static PlayerBase CreatePlayer(string playerType)
        {
            switch (playerType)
            {
                case GlobalVar.HUMAN:
                    return new HumanPlayer();
                case GlobalVar.COMPUTER:
                    return new ComputerPlayer();
                default:
                    throw new ArgumentException("Invalid player type");
            }

        }
    }
}
