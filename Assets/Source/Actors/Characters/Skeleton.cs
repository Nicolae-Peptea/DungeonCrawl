using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public override int Health { get; set; } = 25;

        public override int Attack { get; set; } = 2;

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                ApplyDamage(player.Attack);
                player.OnCollision(this);
            }
               
            return false;
        }

        protected override void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
