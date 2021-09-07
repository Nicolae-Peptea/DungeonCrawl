namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
        public bool SomethingAbove { get; private set; } = false;

        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        public void Hide()
        {
            SetSprite(1);
            SomethingAbove = true;
        }

        public void MakeVisible()
        {
            SetSprite(DefaultSpriteId);
            SomethingAbove = false;
        }

        /// <summary>
        ///     All items are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
