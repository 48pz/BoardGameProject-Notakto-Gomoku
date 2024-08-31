

namespace BoardGameProject
{
    /// <summary>
    /// gomoku ui class
    /// </summary>
    public class GomokuUI : UIBase
    {
        public override void DisplayGameMode()
        {
            UIUtils.DisplayGameMode();
        }

        public override void DisplayInfo(string info)
        {
            Console.WriteLine(info);
        }


    }
}
