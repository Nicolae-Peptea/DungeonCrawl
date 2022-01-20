using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DungeonCrawl.Save
{
    public static class SaveGame
    {
        public static void GameState(Player player)
        {
            PlayerToSave playerToSerialize = new PlayerToSave(player);
            List<ItemToSave> items = PrepareItemsToSerialize();
            List<CharacterToSave> enemies = PrepareCharactersToSerialize();

            GameState gameState = new GameState(playerToSerialize, items, enemies);
            string gameStateJson = JsonConvert.SerializeObject(gameState);

            string path = Application.dataPath + @"/exported_saves/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";
            File.WriteAllText(path, gameStateJson);
        }

        private static List<ItemToSave> PrepareItemsToSerialize()
        {
            HashSet<Actor> actors = ActorManager.Singleton.GetAllActors();

            IEnumerable<Item> selectItems = actors.OfType<Item>();
            List<ItemToSave> objectsToSerialize = new List<ItemToSave>();

            foreach (var item in selectItems)
            {
                ItemToSave newCharacter = new ItemToSave(item);
                objectsToSerialize.Add(newCharacter);
            }

            return objectsToSerialize;
        }

        private static List<CharacterToSave> PrepareCharactersToSerialize()
        {
            HashSet<Actor> actors = ActorManager.Singleton.GetAllActors();

            IEnumerable<Character> selectItems = actors.OfType<Character>();
            List<CharacterToSave> objectsToSerialize = new List<CharacterToSave>();

            foreach (var item in selectItems)
            {
                if (item is Player)
                {
                    continue;
                }
                CharacterToSave newCharacter = new CharacterToSave(item);
                objectsToSerialize.Add(newCharacter);
            }

            return objectsToSerialize;
        }
    }
}
