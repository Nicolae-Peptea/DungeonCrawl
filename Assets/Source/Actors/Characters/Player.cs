using UnityEngine;
using System.Collections.Generic;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Assets.Source.Core;
using System;
using UnityEngine.UI;

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

        public bool HasKey()
        {
            foreach (Item item in _inventory.GetInventory())
            {
                if (item is Key)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasAtLeastOneSword()
        {
            foreach (Item item in _inventory.GetInventory())
            {
                if (item is Sword)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
            HideStatus();
            Utilities.DisplayDeadScreen();
        }

        protected override void OnUpdate(float deltaTime)
        {
            Debug.Log("x: " + Position.x + ", y: " + Position.y);
            DisplayStatus();

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

        public void DisplayStatus()
        {
            GameObject.Find("HPNumber").GetComponent<Text>().text = "" + Health;
            GameObject.Find("AttackNumber").GetComponent<Text>().text = "" + Attack;
            GameObject.Find("DefenseNumber").GetComponent<Text>().text = "" + Attack;

            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public void HideStatus()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
        }

        public void DisplayDeadScreen()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("deadScreen"))
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
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
            if (HasAtLeastOneSword())
            {
                SetSprite(26);
            }
        }

        private void EnhanceAbility(Item item)
        {
            if (item.Type == ItemType.ATTACK)
            {
                Attack += item.Value;
            }

            if (item.Type == ItemType.HEALTH)
            {
                Health += item.Value;
            }
        }
    }
}
