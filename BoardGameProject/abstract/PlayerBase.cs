
namespace BoardGameProject
{
    public abstract class PlayerBase
    {
        public abstract void GetPosition(IBoard board = null);
        public abstract void PassPosition();
    }
}
