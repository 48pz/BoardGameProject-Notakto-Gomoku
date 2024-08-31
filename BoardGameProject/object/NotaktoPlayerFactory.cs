

namespace BoardGameProject
{
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
