

namespace BoardGameProject
{
    /// <summary>
    /// gomoku human player class
    /// </summary>
    public class GomokuHumanPlayer : GomokuPlayerBase
    {
        private string _currentInputs;
        public string CurrentInputs
        {
            get { return _currentInputs; }
            set { _currentInputs = value; }
        }

        /// <summary>
        /// Handling input operations
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public override (int, int) GetPosition(IBoard board = null)
        {
            while (true)
            {
                string inputs = Console.ReadLine();


                if (inputs.Equals(GlobalVar.SAVE))
                {
                    return (999, 999);
                }
                else if (inputs.Equals(GlobalVar.LOAD))
                {
                    return (998, 998);
                }
                else if (inputs.Equals(GlobalVar.UNDO))
                {
                    return (997, 997);
                }
                else if (inputs.Equals(GlobalVar.REDO))
                {
                    return (996, 996);

                }
                else if (inputs.Equals(GlobalVar.HELP))
                {
                    return (995, 995);
                }
                else if (inputs.Equals(GlobalVar.QUIT))
                {
                    return (994, 994);
                }

                string[] pos = inputs.Split(' ');
                if (pos.Length != 2) { Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG); continue; }
                if (int.TryParse(pos[0], out int x) && int.TryParse(pos[1], out int y))
                {
                    if (x >= 1 && x <= 10 && y >= 1 && y <= 10)
                    {
                        return (x, y);
                    }
                    else
                    {
                        Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG);
                    continue;
                }

            }

        }
    }
}
