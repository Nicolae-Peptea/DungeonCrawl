using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Orc : Character
    {
        public override int Health { get; protected set; } = 30;

        public override int Attack { get; protected set; } = 4;

        private float seconds = 0;

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                ApplyDamage(player.Attack);
                player.OnCollision(this);
            }
            return false;
        }

        protected override void OnUpdate(float detaTime)
        {
            seconds += detaTime;
            if (seconds > 1)
            {
                Direction randomDirection = Utilities.GetRandomDirection();
                TryMove(randomDirection);
                seconds = 0;
            } 
        }

        protected override void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 167;
        public override string DefaultName => "Orc";
    }
}
