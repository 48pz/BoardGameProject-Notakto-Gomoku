
namespace BoardGameProject
{
    public abstract class PlayerBase
    {
        public abstract (int, int) GetPosition(IBoard board = null);
        public abstract void PassPosition();
    }
}
