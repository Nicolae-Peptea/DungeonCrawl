using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Items;

namespace DungeonCarawl.Save
{
    [Serializable]
    public class ItemToSerialize
    {

        public string Name { get; set; }

        public bool SomethingAbove { get; set; }

        public int Type { get; set; }

        public  int Value { get; set; }

        public (int x, int y) Position { get; set; }

        public ItemToSerialize(Item item)
        {
            this.Name = item.DefaultName;
            this.SomethingAbove = item.SomethingAbove;
            this.Type = (int)item.Type;
            this.Value = item.Value;
            this.Position = item.Position;

        }

        public ItemToSerialize()
        {

        }
    }
}
