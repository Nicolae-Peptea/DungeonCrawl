using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Save
{
    [Serializable]
    public class PlayerToSerialize
    {
        
        public int Health { get; private set; }

        public int Attack { get; private set; }

        public int CurrentSpriteId { get; private set; }

        public string DefaultName { get; private set; }

        public PlayerToSerialize (Player player)
        {
            Health = player.Health;
            Attack = player.Attack;
            CurrentSpriteId = player.currentSpriteId;
            DefaultName = player.DefaultName;
        }
    }
}
