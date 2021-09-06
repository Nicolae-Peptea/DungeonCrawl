using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
        public int Damage { get; private set; }

        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        /// <summary>
        ///     All items are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
