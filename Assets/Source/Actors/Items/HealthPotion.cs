namespace DungeonCrawl.Actors.Items
{
    public class HealthPotion : Item
    {
        public override ItemType Type => ItemType.HEALTH;

        public override int Value => 15;

        public override int DefaultSpriteId => 569;

        public override string DefaultName => "Health";
    }
}
