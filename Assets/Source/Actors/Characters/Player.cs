using UnityEngine;
using System.Collections.Generic;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Assets.Source.Core;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public override int DefaultSpriteId => 24;

        public override string DefaultName => "Player";

        public override int Health { get { return Health; } protected set { Health = 100; } }

        private List<Item> _inventory = new List<Item>();

        public int Defence { get; set; } = 5;

        public int Attack { get; set; } = 15;

        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickItems();
            }
        }

        private void PickItems()
        {
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (actorAtTargetPosition != null)
            {
                _inventory.Add(actorAtTargetPosition.Clone());
                ActorManager.Singleton.DestroyActor(actorAtTargetPosition);
            }
        }
    }
}
