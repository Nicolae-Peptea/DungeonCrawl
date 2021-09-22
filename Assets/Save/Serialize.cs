using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Save
{
    public static class Serialize
    {
       public static void Player(Player player)
        {
            PlayerToSave playerToSerialize = new PlayerToSave(player);
            string json = JsonConvert.SerializeObject(playerToSerialize);
            string path = Application.dataPath + @"/exported_saves/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";
            File.WriteAllText(path, json);
        }

        public static void Inventory(Inventory inventory)
        {

        }
    }
}
