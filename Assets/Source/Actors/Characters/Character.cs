using DungeonCrawl.Core;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public abstract int Health { get; protected set; }

        public abstract int Attack { get; protected set; }

        public bool IsAlive { get; set; } = true;

        public void ApplyDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }

        public void SetHealth(int health)
        {
            this.Health = health;
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -2;

        internal void ShowItem()
        {
            throw new NotImplementedException();
        }
    }
}
