namespace BoardGameProject
{
    /// <summary>
    /// notakto human player class
    /// </summary>
    public class NotaktoHumanPlayer : NotaktoPlayerBase
    {
        /// <summary>
        /// Handling input operations
        /// </summary>
        /// <param name="boards"></param>
        /// <returns></returns>
        public override List<int> GetPosition(List<NotaktoBoard> boards)
        {
            while (true)
            {
                string inputs = Console.ReadLine();
                //save
                if (inputs.Equals(GlobalVar.SAVE))
                {
                    return new List<int> { (int)Command.save };
                }
                //load
                else if (inputs.Equals(GlobalVar.LOAD))
                {
                    return new List<int> { (int)Command.load };
                }
                //undo
                else if (inputs.Equals(GlobalVar.UNDO))
                {
                    return new List<int> { (int)Command.undo };
                }
                //redo
                else if (inputs.Equals(GlobalVar.REDO))
                {
                    return new List<int> { (int)Command.redo };
                }

                string[] pos = inputs.Split(' ');
                if (pos.Length != 3) { Console.WriteLine(GlobalVar.USERINPUTSINVALIDMSG); continue; }
                if (int.TryParse(pos[0], out int x) && int.TryParse(pos[1], out int y) && int.TryParse(pos[2], out int z))
                {
                    if (x >= 1 && x <= 3 && y >= 1 && y <= 3 && z >= 1 && z <= 3)
                    {
                        return new List<int> { x, y, z };
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
