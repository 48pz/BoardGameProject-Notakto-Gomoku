

namespace BoardGameProject
{
    /// <summary>
    /// notakto ui class
    /// </summary>
    public class NotaktoUI : UIBase
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
