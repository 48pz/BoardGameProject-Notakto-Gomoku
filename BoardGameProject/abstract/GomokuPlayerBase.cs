
namespace BoardGameProject
{
    public abstract class GomokuPlayerBase
    {
        public abstract (int, int) GetPosition(IBoard board = null);

        public abstract void PassPosition();
    }
}
