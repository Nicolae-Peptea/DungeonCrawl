using UnityEngine;
using System.Collections.Generic;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Assets.Source.Core;
using System;
using System.Linq;
using UnityEngine.UI;


namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public override int DefaultSpriteId => 24;

        public int currentSpriteId = 24;

        public override string DefaultName => "Player";

        public override int Health { get; protected set; } = 100;

        public override int Attack { get; protected set; } = 5;

        private Inventory _inventory = new Inventory();

        private List<Item> _equipment = new List<Item>();

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
            return _inventory.GetInventory().Any(item => item is Key);
        }

        public void SetFields(int spriteId, int health, int attack,
             List<Item> equipment, List<Item> itemsList)
        {
            SetSprite(spriteId);
            Health = health;
            Attack = attack;
            _equipment = equipment;
            _inventory.SetInventory(itemsList);
        }

        public void UseKey()
        {
            foreach (Item item in _inventory.GetInventory())
            {
                if (item is Key)
                {
                    _inventory.RemoveItem(item);
                }
            }
        }

        public (List<Item>, List<Item>) GetEquipmentAndInventory()
        {
            List<Item> clonedequipment = new List<Item>(_equipment);
            List<Item> clonedinventory = new List<Item>(_inventory.GetInventory());
            return (clonedequipment, clonedinventory);
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
            HideStatus();
            Utilities.DisplayEventScreen(true);
        }

        protected override void OnUpdate(float deltaTime)
        {
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
                _inventory.UpdateInventoryNumbers();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventory.DisplayInventory();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                TryToConsumeItem(ItemType.HEALTH);
                _inventory.UpdateInventoryNumbers();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                EquipItem(ItemType.ATTACK);
            }
        }

        private void PickItems()
        {
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (actorAtTargetPosition != null)
            {
                _inventory.AddItem(actorAtTargetPosition.Clone());
                ActorManager.Singleton.DestroyActor(actorAtTargetPosition);
            }
        }

        private void TryToConsumeItem(ItemType itemType)
        {
            try
            {
                Item item = _inventory.SelectItemByType(itemType);
                AlterAbility(item, false);
                _inventory.RemoveItem(item);
            }
            catch (NullReferenceException)
            {
                Debug.Log("Nu ai de astea");
            }
        }

        private void EquipItem(ItemType itemType)
        {
            try
            {
                Item itemFromInventory = _inventory.SelectItemByType(itemType);
                Item itemFromInventoryCopy = itemFromInventory.Clone();
                Item alreadyEquipped = _equipment.FirstOrDefault(item => item.Type == itemType);

                if (alreadyEquipped != null)
                {
                    AlterAbility(alreadyEquipped, true);
                    DropItem(alreadyEquipped);
                    _equipment.Remove(alreadyEquipped);
                }

                _inventory.RemoveItem(itemFromInventory);
                _equipment.Add(itemFromInventoryCopy);
                AlterAbility(itemFromInventoryCopy, false);
                UpdateStatus();

                ChangeSkin();
            }
            catch (NullReferenceException)
            {
                Debug.Log("Already equpped");
            }

        }

        private void AlterAbility(Item item, bool Decrease)
        {
            int valueToAdd = Decrease ? -1 * item.Value : item.Value;

            if (item.Type == ItemType.ATTACK)
            {
                Attack += valueToAdd;
            }

            if (item.Type == ItemType.HEALTH)
            {
                int enhancedHealth = Health + valueToAdd;
                Health = enhancedHealth >= 100 ? 100 : enhancedHealth;
            }
        }

        private void DropItem(Item item)
        {
            if (item is Sword)
            {
                ActorManager.Singleton.Spawn<Sword>(Position, item.name);
            }

            if (item is Axe)
            {
                ActorManager.Singleton.Spawn<Axe>(Position, item.name);
            }

            Item itemAtPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            itemAtPosition.Hide();

        }

        private void ChangeSkin()
        {
            if (_equipment.Any(item => item is Sword))
            {
                SetSprite(26);
                currentSpriteId = 26;
            }
            if (_equipment.Any(item => item is Axe))
            {
                SetSprite(78);
                currentSpriteId = 78;
            }
        }

        private void DisplayStatus()
        {
            UpdateStatus();

            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void UpdateStatus()
        {
            GameObject.Find("HPNumber").GetComponent<Text>().text = "" + Health;
            GameObject.Find("AttackNumber").GetComponent<Text>().text = "" + Attack;
            GameObject.Find("DefenseNumber").GetComponent<Text>().text = "" + Attack;
        }

        private void HideStatus()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
