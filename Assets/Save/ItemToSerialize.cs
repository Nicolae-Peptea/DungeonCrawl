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

        public string Name { get; private set; }

        public bool SomethingAbove { get; private set; }

        public int Type { get; private set; }

        public  int Value { get; private set; }

        public (int x, int y) Position { get; private set; }

        public ItemToSerialize(Item item)
        {
            this.Name = item.GetType().Name;
            this.SomethingAbove = item.SomethingAbove;
            this.Type = (int)item.Type;
            this.Value = item.Value;
            this.Position = item.Position;

        }
    }
}
