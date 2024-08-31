

using System.Security.AccessControl;
using System.Text.Json;

namespace BoardGameProject
{
    public class NotaktoSaver : ISaver<NotaktoBoard>
    {
        /// <summary>
        /// save boards info to json file
        /// </summary>
        /// <param name="boardList"></param>
        /// <param name="savePath"></param>
        public void SaveBoardInfo(List<NotaktoBoard> boardList, string savePath)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = $"{boardList[0].GameName}_{boardList[0].GameMode}_{timestamp}.json";
                string saveFolderPath = Path.Combine(savePath, "MyBoardGameSaves");
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath);
                }
                string fullName = Path.Combine(saveFolderPath, fileName);
                string json = JsonSerializer.Serialize(boardList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fullName, json);
                Console.WriteLine("Game saved to {0}", fullName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: save failed!" + e.ToString());
                throw;
            }

        }

        /// <summary>
        /// load boards info
        /// </summary>
        /// <returns></returns>
        public List<NotaktoBoard> LoadGame()
        {
            Console.WriteLine("Please enter the FULL PATH to load the game:");
            try
            {
                while (true)
                {
                    string savePath = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(savePath) || !File.Exists(savePath))
                    {
                        Console.WriteLine("Error: file does not exist!");
                        continue;
                    }

                    string jsonStr = File.ReadAllText(savePath);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    List<NotaktoBoard> loadedBoards = JsonSerializer.Deserialize<List<NotaktoBoard>>(jsonStr, options);

                    if (loadedBoards != null && loadedBoards.Count > 0 && loadedBoards[0].ValidationStr.Equals(GlobalVar.NOTAKTO) &&
                        loadedBoards[0].GameMode.Equals(GlobalVar.COMPUTERVSHUMAN))
                    {
                        Console.WriteLine("\nLoading Successfully!");
                        return loadedBoards;
                    }
                    else
                    {
                        Console.WriteLine("Error: Wrong file type. Please check the game type and game mode are correct.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;

        }

        public void SaveBoardInfo(NotaktoBoard board, string savePath)
        {
            throw new NotImplementedException();
        }

    }
}
