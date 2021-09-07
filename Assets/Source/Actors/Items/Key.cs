namespace DungeonCrawl.Actors.Items
{
    public class Key : Item
    {
        public override int DefaultSpriteId => 559;
        public override string DefaultName => "Key";

        public override Item Clone()
        {
            Item deepCopy = new Key();
            return deepCopy;
        }
    }
}
