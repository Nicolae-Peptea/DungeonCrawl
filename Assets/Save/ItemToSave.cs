using DungeonCrawl.Actors.Items;
using System;

namespace DungeonCrawl.Save
{
    [Serializable]
    public class ItemToSave
    {

        public string Name { get; set; }

        public bool SomethingAbove { get; set; }

        public int Type { get; set; }

        public int Value { get; set; }

        public (int x, int y) Position { get; set; }

        public ItemToSave(Item item)
        {
            this.Name = item.DefaultName;
            this.SomethingAbove = item.SomethingAbove;
            this.Type = (int)item.Type;
            this.Value = item.Value;
            this.Position = item.Position;

        }

        public ItemToSave()
        {
        }
    }
}
