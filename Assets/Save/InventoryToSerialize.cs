using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Items;

namespace Assets.Save
{
    [Serializable]
    public class InventoryToSerialize
    {
        public bool SomethingAbove { get; private set; }

        public int Type { get; private set; }

        public  int Value { get; private set; }

        public string[] Items;

        public InventoryToSerialize(Inventory inventory)
        {
            List<Item> items = inventory.GetInventory();

            Items = new string[items.Count];
            int index = 0;

            foreach (var item in items)
            {
                Items[index] = item.DefaultName;
                index++;
            }
        }
    }
}
