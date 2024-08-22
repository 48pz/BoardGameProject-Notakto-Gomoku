

namespace BoardGameProject
{
    public class HumanPlayer : PlayerBase
    {
        private string _currentInputs;
        public string CurrentInputs {
            get { return _currentInputs; }
            set { _currentInputs = value; }
        }



        public override void GetPosition(IBoard board = null)
        {
            throw new NotImplementedException();
        }

        public override void PassPosition()
        {
            throw new NotImplementedException();
        }
    }
}
