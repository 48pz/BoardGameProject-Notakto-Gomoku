
using System.Text.Json;

namespace BoardGameProject
{
    public class GomokuSaver : ISaver<GomokuBoard>
    {
        /// <summary>
        /// save info to file
        /// </summary>
        /// <param name="board"></param>
        /// <param name="savePath"></param>
        public void SaveBoardInfo(GomokuBoard board, string savePath)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = $"{board.GameName}_{board.GameMode}_{timestamp}.json";
                string saveFolderPath = Path.Combine(savePath, "MyBoardGameSaves");
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath);
                }
                string fullName = Path.Combine(saveFolderPath, fileName);

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(board, options);
                File.WriteAllText(fullName, jsonString);
                Console.WriteLine("Game saved to {0}", fullName);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: save failed!" + e.ToString());
            }
        }
    }
}
