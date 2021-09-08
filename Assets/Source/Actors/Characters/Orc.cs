using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Orc : Character
    {
        public override int Health { get; protected set; } = 100;

        public override int Attack { get; protected set; } = 5;

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 167;
        public override string DefaultName => "Orc";
    }
}
