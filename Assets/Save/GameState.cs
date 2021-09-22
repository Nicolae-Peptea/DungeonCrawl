using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Save
{
    public class GameState
    {
        public PlayerToSave player;
        public List<ItemToSave> items = new List<ItemToSave>();
        public List<CharacterToSave> enemies = new List<CharacterToSave>();

       public GameState(PlayerToSave player, List<ItemToSave> items, List<CharacterToSave> enemies) 
        {
            this.player = player;
            this.items = items;
            this.enemies = enemies;
        }

        public GameState()
        {
        }
    }
}
