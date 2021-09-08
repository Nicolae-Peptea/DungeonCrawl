using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public override int Health { get; protected set; } = 10;

        public override int Attack { get; protected set; } = 10;

        private Player player;

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                ApplyDamage(player.Attack);
            }
            return true;
        }

        protected override void OnUpdate(float detaTime)
        {
            CheckForPlayerAround();

            if (player != null)
            {
                (int x, int y) newPosition;
                newPosition.x = player.Position.x;
            }
               

        }

        protected override void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Well, I was already dead anyway...");
        }

        private bool CheckForPlayerAround()
        {
            int buffer = 1;
            int minPositionX = Position.x - buffer;
            int maxPositionX = Position.x + buffer;

            int minPositionY = Position.y - buffer;
            int maxPositionY = Position.y + buffer;

            for (int X = minPositionX; X <= maxPositionX; X++)
            {
                for (int Y = minPositionY; Y <= maxPositionY; Y++)
                {
                    (int x, int y) positionToCheck = (Position.x + X, Position.y + Y);
                    var currentActor = ActorManager.Singleton.GetActorAt<Actor>(positionToCheck);

                    if (currentActor is Player player)
                    {
                        this.player = player;
                        return true;
                    } 
                }
            }
            return false;
        }

        public override int DefaultSpriteId => 313;
        public override string DefaultName => "Ghost";
    }
}
