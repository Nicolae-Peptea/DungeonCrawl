using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DungeonCrawl.Save
{
    public class Deserialize
    {
        public static GameState DeserializeGameState()
        {
            string foldePath = Application.dataPath + @"/exported_saves";
            var directory = new DirectoryInfo(foldePath);

            string latestSaveFile = directory.GetFiles()
             .Where(f => f.Extension == ".json")
             .OrderByDescending(f => f.LastWriteTime)
             .First().FullName;

            string json = File.ReadAllText(latestSaveFile);

            GameState gameState = JsonConvert.DeserializeObject<GameState>(json);
            return gameState;
        }
    }
}
