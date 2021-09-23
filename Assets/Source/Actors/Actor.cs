using Assets.Source.Core;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using UnityEngine;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        public (int x, int y) Position
        {
            get => _position;
            set
            {
                _position = value;
                transform.position = new Vector3(value.x, value.y, Z);
            }
        }

        private (int x, int y) _position;

        private SpriteRenderer _spriteRenderer;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SetSprite(DefaultSpriteId);
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public void SetSprite(int id)
        {
            _spriteRenderer.sprite = ActorManager.Singleton.GetSprite(id);
        }

        public void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            Actor actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);
            
            UserInterface.Singleton.SetText("",
                        UserInterface.TextPosition.BottomCenter);

            if (actorAtTargetPosition == null)
            {
                MoveOnEmptyPosition(targetPosition);
            }
            else
            {
                MoveOnOccupiedPosition(actorAtTargetPosition, targetPosition);
            }

            if (this is Player)
            {
                CameraController.Singleton.Position = this.Position;
            }
        }

        private void MoveOnEmptyPosition((int x, int y) targetPosition)
        {
            Item item = ActorManager.Singleton.GetActorAt<Item>(Position);

            if (item != null)
            {
                item.MakeVisible();
                item.Show();
            }

            Position = targetPosition;
        }

        private void MoveOnOccupiedPosition(Actor actor, (int x, int y) targetPosition)
        {
            if (actor.OnCollision(this))
            {
                // Allowed to move
                if (actor.GetType().IsSubclassOf(typeof(Item)))
                {
                    ((Item)actor).Hide();
                    ((Item)actor).Show();
                }

                Position = targetPosition;

                if (actor.GetType() == typeof(Portal) && this is Player player)
                {
                    player.AttemptLevelTransition();
                }
            }
            else
            {
                CanGoMessage();
            }
        }

        private void CanGoMessage()
        {
            if (this is Player)
            {
                UserInterface.Singleton.SetText("Well, it seems I can't go there!",
                UserInterface.TextPosition.BottomCenter);
            }
        }

        /// <summary>
        ///     Invoked whenever another actor attempts to walk on the same position
        ///     this actor is placed.
        /// </summary>
        /// <param name="anotherActor"></param>
        /// <returns>true if actor can walk on this position, false if not</returns>
        public virtual bool OnCollision(Actor anotherActor)
        {
            // All actors are passable by default
            return true;
        }

        /// <summary>
        ///     Invoked every animation frame, can be used for movement, character logic, etc
        /// </summary>
        /// <param name="deltaTime">Time (in seconds) since the last animation frame</param>
        protected virtual void OnUpdate(float deltaTime)
        {
        }

        /// <summary>
        ///     Can this actor be detected with ActorManager.GetActorAt()? Should be false for purely cosmetic actors
        /// </summary>
        public virtual bool Detectable => true;

        /// <summary>
        ///     Z position of this Actor (0 by default)
        /// </summary>
        public virtual int Z => 0;

        /// <summary>
        ///     Id of the default sprite of this actor type
        /// </summary>
        public abstract int DefaultSpriteId { get; }

        /// <summary>
        ///     Default name assigned to this actor type
        /// </summary>
        public abstract string DefaultName { get; }
    }
}