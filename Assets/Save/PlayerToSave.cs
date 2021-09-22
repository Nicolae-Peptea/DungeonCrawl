using DungeonCarawl.Save;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;
using System;
using System.Collections.Generic;

namespace DungeonCrawl.Save
{
    [Serializable]
    public class PlayerToSave
    {
        public int Health { get; set; }

        public int Attack { get; set; }

        public int CurrentSpriteId { get;  set; }

        public string DefaultName { get;  set; }

        public (int x, int y) Position { get;  set; }

        public List<ItemToSerialize> Inventory { get; set; } = new List<ItemToSerialize>();

        public List<ItemToSerialize> Equipment { get; set; } = new List<ItemToSerialize>();

        public PlayerToSave()
        {

        }

        public PlayerToSave (Player player)
        {
            Health = player.Health;
            Attack = player.Attack;
            CurrentSpriteId = player.currentSpriteId;
            DefaultName = player.DefaultName;
            this.Position = player.Position;

            SetUpInventory(player.GetInventory());
            SetUpEquipment(player.GetEquipment());

        }

        private void SetUpInventory(List<Item> playerGear)
        {
            foreach (var item in playerGear)
            {
                ItemToSerialize serializedItem = CreateItemToSerialize(item);
                Inventory.Add(serializedItem);
            }
        }

        private void SetUpEquipment(List<Item> playerGear)
        {
            foreach (var item in playerGear)
            {
                ItemToSerialize serializedItem = CreateItemToSerialize(item);
                Equipment.Add(serializedItem);
            }
        }

        private ItemToSerialize CreateItemToSerialize(Item playerItem)
        {
            return new ItemToSerialize(playerItem);
        }


    }
}
