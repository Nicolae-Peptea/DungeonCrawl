using UnityEngine;

namespace DungeonCrawl.Actors.Items
{
    public class Axe : Item
    {
        public override int DefaultSpriteId => 374;

        public override string DefaultName => "Axe";

        public override ItemType Type => ItemType.ATTACK;

        public override int Value => 20;
    }
}
