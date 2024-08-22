

namespace BoardGameProject
{
    internal class HumanPlayer : IPlayer
    {
        private string _currentInputs;
        public string CurrentInputs {
            get { return _currentInputs; }
            set { _currentInputs = value; }
        }

        public void GetPosition()
        {
            throw new NotImplementedException();
        }

        public void PassPosition()
        {
            throw new NotImplementedException();
        }
    }
}
