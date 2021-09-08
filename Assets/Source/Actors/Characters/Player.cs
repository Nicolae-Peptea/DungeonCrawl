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

        public override int Health { get; protected set; } = 100;

        public override int Attack { get; protected set; } = 5;

        

        private Inventory _inventory = new Inventory();

        private void Start()
        {
            CameraController.Singleton.Position = this.Position;
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Character character && character.IsAlive)
            {
                ApplyDamage(character.Attack);
            }
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
                if (_inventory.IsInventoryVisible)
                {
                    _inventory.DisplayInventory();
                    _inventory.DisplayInventory();
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventory.DisplayInventory();
            }
        }

        private void PickItems()
        {
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (actorAtTargetPosition != null)
            {
                EnhanceAbility(actorAtTargetPosition);
                _inventory.AddItem(actorAtTargetPosition.Clone());
                ActorManager.Singleton.DestroyActor(actorAtTargetPosition);
            }
        }

        private void EnhanceAbility(Item item)
        {
            if (item.Type == ItemType.ATTACK)
            {
                Attack += item.Value;
            }
        }
    }
}
