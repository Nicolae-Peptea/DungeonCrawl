using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public new int Health => 25;

        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 313;
        public override string DefaultName => "Ghost";
    }
}
