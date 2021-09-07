namespace DungeonCrawl.Actors.Items
{
    public class Sword : Item
    {
        public override int DefaultSpriteId => 415;
        public override string DefaultName => "Sword";

        public int AddedValue { get; private set; } = 10;

        public override Item Clone()
        {
            Item deepCopy = new Sword();
            return deepCopy;
        }
    }
}
