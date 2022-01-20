using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DungeonCrawl.Save
{
    public static class Deserialize
    {
        public static GameState DeserializeGameState()
        {
            string foldePath = Application.dataPath + @"/exported_saves";
            DirectoryInfo directory = new DirectoryInfo(foldePath);

            FileInfo[] files = TryToGetFiles(directory);
            string fileName = TryToGetLatestJsonFile(files);

            string json = File.ReadAllText(fileName);
            GameState gameState = JsonConvert.DeserializeObject<GameState>(json);

            return gameState;
        }

        private static FileInfo[] TryToGetFiles(DirectoryInfo directory)
        {
            FileInfo[] files = directory.GetFiles();

            if (files.Length == 0)
            {
                throw new NullReferenceException();
            }

            return files;
        }

        private static string TryToGetLatestJsonFile(FileInfo[] files)
        {
            IOrderedEnumerable<FileInfo> latestSaveFile = files
             .Where(f => f.Extension == ".json")
             .OrderByDescending(f => f.LastWriteTime);

            bool tesy = !latestSaveFile.Any();

            if (!latestSaveFile.Any())
            {
                throw new ArgumentNullException();
            }

            return latestSaveFile.First().FullName;
        }

    }
}
