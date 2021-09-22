using DungeonCrawl.Save;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonCrawl.Save
{
    public class Deserialize
    {
        public static PlayerToSave DeserializePlayer()
        {
            string path = Application.dataPath + @"/export-dates/2021-09-22T10-37-10.json";
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PlayerToSave>(json);
        }
    }
}
