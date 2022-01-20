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

        public int CurrentSpriteId { get; set; }

        public string DefaultName { get; set; }

        public int CurrentLevel { get; set; }

        public (int x, int y) Position { get; set; }

        public List<ItemToSave> Inventory { get; set; } = new List<ItemToSave>();

        public List<ItemToSave> Equipment { get; set; } = new List<ItemToSave>();

        public PlayerToSave()
        {
        }

        public PlayerToSave(Player player)
        {
            Health = player.Health;
            Attack = player.Attack;
            CurrentSpriteId = player.CurrentSpriteId;
            DefaultName = player.DefaultName;
            this.Position = player.Position;
            this.CurrentLevel = player.CurrentMapLevel;

            SetUpInventory(player.GetInventory());
            SetUpEquipment(player.GetEquipment());
        }


        private void SetUpInventory(List<Item> playerGear)
        {
            foreach (var item in playerGear)
            {
                ItemToSave serializedItem = CreateItemToSerialize(item);
                Inventory.Add(serializedItem);
            }
        }

        private void SetUpEquipment(List<Item> playerGear)
        {
            foreach (var item in playerGear)
            {
                ItemToSave serializedItem = CreateItemToSerialize(item);
                Equipment.Add(serializedItem);
            }
        }

        private ItemToSave CreateItemToSerialize(Item playerItem)
        {
            return new ItemToSave(playerItem);
        }

    }
}
