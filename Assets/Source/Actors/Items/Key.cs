namespace DungeonCrawl.Actors.Items
{
    public class Key : Item
    {
        public override int DefaultSpriteId => 559;

        public override string DefaultName => "Key";

        public override ItemType Type => ItemType.GO_NEXT_LEVEL;

        public override int Value => 0;
    }
}
