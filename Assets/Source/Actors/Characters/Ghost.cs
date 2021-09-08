using UnityEngine;
using DungeonCrawl.Core;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public override int Health { get; protected set; } = 10;

        public override int Attack { get; protected set; } = 10;

        private float seconds = 0;

        private const int DISTANCE = 5;

        private const float ACCEPTABLE_MOVING_INTERVAL = 0.2f;

        private Player player = (Player)ActorManager.Singleton.GetPlayer();

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
            seconds += detaTime;
            if (seconds > ACCEPTABLE_MOVING_INTERVAL)
            {
                Direction direction = GetDirectionTowardsPlayer();
                if (Math.Abs(player.Position.x - Position.x) <= DISTANCE
                    && Math.Abs(player.Position.y - Position.y) <= DISTANCE)
                {
                    TryMove(direction);
                }
                seconds = 0;
            }
        }

        protected override void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Well, I was already dead anyway...");
        }

        public Direction GetDirectionTowardsPlayer()
        {
            int ghostRow = Position.y;
            int ghostCol = Position.x;
            int playerRow = player.Position.y;
            int playerCol = player.Position.x;

            if (ghostRow > playerRow)
            {
                if (ghostCol < playerCol)
                {
                    return Utilities.GetRandomDirectionFromCadran(1);
                }
                else if (ghostCol > playerCol)
                {
                    return Utilities.GetRandomDirectionFromCadran(2);
                }
                else
                {
                    return Direction.Down;
                }
            }
            else if (ghostRow < playerRow)
            {
                if (ghostCol < playerCol)
                {
                    return Utilities.GetRandomDirectionFromCadran(3);
                }
                else if (ghostCol > playerCol)
                {
                    return Utilities.GetRandomDirectionFromCadran(4);
                }
                else
                {
                    return Direction.Up;
                }
            }
            else
            {
                if (ghostCol < playerCol)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
        }

        public override int DefaultSpriteId => 313;
        public override string DefaultName => "Ghost";
    }
}
