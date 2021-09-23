using DungeonCrawl.Core;
using DungeonCrawl.Save;
using UnityEngine;

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

        public virtual void SetFromLoaded(GameState gameState)
        {

        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -2;
    }
}
