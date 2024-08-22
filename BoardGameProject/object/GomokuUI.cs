

using System;

namespace BoardGameProject
{
    public class GomokuUI : UIBase
    {
        public override void DisplayGameMode()
        {
            UIUtils.DisplayGameMode();
        }

        public override string PassInfoToGameManager()
        {
            throw new NotImplementedException();
        }


        public override void DisplayInfo(string info)
        {
            Console.WriteLine(info);
        }


  

    }
}
