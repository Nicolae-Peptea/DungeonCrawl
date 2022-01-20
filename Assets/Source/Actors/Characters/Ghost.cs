using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Ghost : Character
    {
        public override int Health { get; protected set; } = 10;

        public override int Attack { get; protected set; } = 10;

        private Player _player;

        private bool _foundThePlayer;

        private const float SECONDS_TO_WAIT_UNTIL_MOVING = 0.2f;

        private float _wait = 0;

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player player)
            {
                ApplyDamage(player.Attack);
                player.OnCollision(this);
                return false;
            }
            return true;
        }

        protected override void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Well, I was already dead anyway...");
        }

        protected override void OnUpdate(float detaTime)
        {
            _wait += detaTime;

            if (_foundThePlayer)
            {
                if (_wait > SECONDS_TO_WAIT_UNTIL_MOVING)
                {
                    Direction direction = GetDirectionTowardsPlayer();
                    TryMove(direction);
                    _wait = 0;
                }
            }
            else
            {
                CheckForPlayerAround();
            }
        }

        private void CheckForPlayerAround()
        {
            int buffer = 3;
            int minPositionX = Position.x - buffer;
            int maxPositionX = Position.x + buffer;

            int minPositionY = Position.y - buffer;
            int maxPositionY = Position.y + buffer;

            for (int X = minPositionX; X <= maxPositionX; X++)
            {
                for (int Y = minPositionY; Y <= maxPositionY; Y++)
                {
                    (int x, int y) positionToCheck = (X, Y);
                    var currentActor = ActorManager.Singleton.GetActorAt<Actor>(positionToCheck);
                    if (currentActor is Player player)
                    {
                        this._player = player;
                        _foundThePlayer = true;
                    }
                }
            }
        }

        public Direction GetDirectionTowardsPlayer()
        {
            int ghostRow = Position.y;
            int ghostCol = Position.x;
            int playerRow = _player.Position.y;
            int playerCol = _player.Position.x;

            if (ghostRow > playerRow)
            {
                if (ghostCol < playerCol)
                {
                    return Utilities.GetDirectionFromCadran(Quadrant.SECOND);
                }
                else if (ghostCol > playerCol)
                {
                    return Utilities.GetDirectionFromCadran(Quadrant.FIRST);
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
                    return Utilities.GetDirectionFromCadran(Quadrant.THIRD);
                }
                else if (ghostCol > playerCol)
                {
                    return Utilities.GetDirectionFromCadran(Quadrant.FORTH);
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
