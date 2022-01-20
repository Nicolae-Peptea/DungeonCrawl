using Assets.Source.Core;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using DungeonCrawl.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        public override int DefaultSpriteId => 24;

        public int CurrentSpriteId { get; private set; } = 24;

        public override string DefaultName => "Player";

        public override int Health { get; protected set; } = 100;

        public int CurrentMapLevel { get; private set; } = 1;

        public override int Attack { get; protected set; } = 5;

        private Inventory _inventory = new Inventory();

        private List<Item> _equipment = new List<Item>();

        private float _timeCounter = 0;

        private const float SECONDS_TO_WAIT_FOR_TEXT_DISAPPEARING = 5;
        public bool AreControlsVisible { get; set; } = false;

        private void Start()
        {
            CameraController.Singleton.Position = this.Position;
        }

        public List<Item> GetInventory()
        {
            return _inventory.GetContent();
        }

        public List<Item> GetEquipment()
        {
            return _equipment;
        }

        public void SetFromLoaded(GameState gameState)
        {
            PlayerToSave loadedPlayer = gameState.player;

            (int x, int y) positionOfLoadedPlayer = loadedPlayer.Position;
            int mapLevelOfLoadedPlayer = loadedPlayer.CurrentLevel;
            List<Item> loadedInventory = Utilities.GetGearFromLoadedGame(loadedPlayer.Inventory);
            List<Item> loadedEquipment = Utilities.GetGearFromLoadedGame(loadedPlayer.Equipment);
            int healthOfLoadedPlayer = loadedPlayer.Health;
            int attackOfLoadedPlayer = loadedPlayer.Attack;
            int spriteId = loadedPlayer.CurrentSpriteId;

            this.Position = (positionOfLoadedPlayer.x, positionOfLoadedPlayer.y);
            this.Health = healthOfLoadedPlayer;
            this.Attack = attackOfLoadedPlayer;
            this._inventory.SetInventory(loadedInventory);
            this._equipment = loadedEquipment;
            UpdateEquipment(loadedEquipment.FirstOrDefault());
            this.CurrentMapLevel = mapLevelOfLoadedPlayer;
            this.CurrentSpriteId = spriteId;
            this.SetSprite(spriteId);
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
            return _inventory.GetContent().Any(item => item is Key);
        }

        public Player Copy()
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();
            var component = go.AddComponent<Player>();
            component.SetSprite(CurrentSpriteId);
            component.Health = Health;
            component.Attack = Attack;
            component.CurrentMapLevel = CurrentMapLevel;
            component._equipment = _equipment;
            component._inventory.SetInventory(_inventory.GetContent());
            return component;
        }

        public void UseKey()
        {
            var itemToRemove = _inventory.GetContent().Single(item => item is Key);
            _inventory.RemoveItem(itemToRemove);
            _inventory.UpdateInventoryNumbers();
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
            HideStatus();
            SceneManager.LoadScene("End");
        }


        protected override void OnUpdate(float deltaTime)
        {
            _timeCounter += deltaTime;

            DisplayStatus();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

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

            if (Input.GetKeyDown(KeyCode.C))
            {
                DisplayControls();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                EquipItem(ItemType.ATTACK);
                _inventory.UpdateInventoryNumbers();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                TryToConsumeItem(ItemType.HEALTH);
                _inventory.UpdateInventoryNumbers();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventory.DisplayInventory();
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                Serialize.GameState(this);
                UserInterface.Singleton.SetText("Game State Saved Successfully!",
                        UserInterface.TextPosition.BottomCenter);
                _timeCounter = 0;
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                MapLoader.LoadGameState();
                UserInterface.Singleton.SetText("Saved Game Loaded Successfully!",
                        UserInterface.TextPosition.BottomCenter);
                _timeCounter = 0;
            }

            if (UserInterface.Singleton.HideText(UserInterface.TextPosition.BottomCenter,
                    _timeCounter, SECONDS_TO_WAIT_FOR_TEXT_DISAPPEARING))
            {
                _timeCounter = 0;
            };
        }

        public void AttemptLevelTransition()
        {
            int lastLevel = 3;

            if (AmIAtPortal())
            {
                if (CurrentMapLevel == lastLevel)
                {
                    SceneManager.LoadScene("Win");
                }
                else
                {
                    GoNextLevel();
                }
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
                UpdateEquipment(itemFromInventoryCopy);
                ChangeSkin();
            }
            catch (NullReferenceException)
            {
                Debug.Log("You don't have anything to equip of this kind");
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
            itemAtPosition.Show();

        }

        private void ChangeSkin()
        {
            int skinWithSword = 26;
            int skinWithAxe = 78;

            if (_equipment.Any(item => item is Sword))
            {
                SetSprite(skinWithSword);
                CurrentSpriteId = skinWithSword;
            }
            if (_equipment.Any(item => item is Axe))
            {
                SetSprite(skinWithAxe);
                CurrentSpriteId = skinWithAxe;
            }
        }

        private void DisplayStatus()
        {
            int visible = 1;
            UpdateStatus();

            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(visible, visible, visible);
            }
        }

        public void DisplayControls()
        {

            foreach (var gameObject in GameObject.FindGameObjectsWithTag("controls"))
            {
                gameObject.transform.localScale = AreControlsVisible
                    ? new Vector3(0, 0, 0)
                    : new Vector3(1, 1, 1);
            }

            AreControlsVisible = !AreControlsVisible;
        }

        private void UpdateStatus()
        {
            GameObject.Find("HPNumber").GetComponent<Text>().text = "" + Health;
            GameObject.Find("AttackNumber").GetComponent<Text>().text = "" + Attack;
        }

        private void UpdateEquipment(Item item)
        {
            GameObject.Find("SwordName").transform.localScale = new Vector3(0, 0, 0);

            if (item is Sword)
            {
                GameObject.Find("EquipmentSwordIcon").transform.localScale = new Vector3(1, 1, 1);
                GameObject.Find("EquipmentAxeIcon").transform.localScale = new Vector3(0, 0, 0);
            }
            else if (item is Axe)
            {
                GameObject.Find("EquipmentSwordIcon").transform.localScale = new Vector3(0, 0, 0);
                GameObject.Find("EquipmentAxeIcon").transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void HideStatus()
        {
            int invisible = 0;
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("status"))
            {
                gameObject.transform.localScale = new Vector3(invisible, invisible, invisible);
            }
        }

        private bool AmIAtPortal()
        {
            Portal item = ActorManager.Singleton.GetActorAt<Portal>(Position);
            return item != null;
        }

        private void GoNextLevel()
        {
            this.UseKey();
            CurrentMapLevel += 1;
            ActorManager.Singleton.DestroyAllActors();
            MapLoader.LoadMap(CurrentMapLevel, ((Player)this).Copy());
        }
    }
}
