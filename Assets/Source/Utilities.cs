using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Save;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl
{
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Quadrant : byte
    {
        FIRST,
        SECOND,
        THIRD,
        FORTH,
    }

    public static class Utilities
    {
        public static (int x, int y) ToVector(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return (0, 1);
                case Direction.Down:
                    return (0, -1);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Right:
                    return (1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public static Direction GetRandomDirection()
        {
            System.Random rand = new System.Random();
            Array enumValues = Enum.GetValues(typeof(Direction));
            return (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        }

        public static Direction GetDirectionFromCadran(Quadrant cadran)
        {
            System.Random rand = new System.Random();
            Enum[] enumValues = cadran switch
            {
                Quadrant.FIRST => new Enum[] { Direction.Down, Direction.Left },
                Quadrant.SECOND => new Enum[] { Direction.Down, Direction.Right },
                Quadrant.THIRD => new Enum[] { Direction.Up, Direction.Right },
                Quadrant.FORTH => new Enum[] { Direction.Up, Direction.Left },
                _ => (Enum[])Enum.GetValues(typeof(Direction)),
            };
            return (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        }

        public static List<Item> GetGearFromLoadedGame(List<ItemToSave> gear)
        {
            var newGear = new List<Item>();

            foreach (var item in gear)
            {
                var go = new GameObject();
                Item component = null;

                go.AddComponent<SpriteRenderer>();
                go.name = item.Name;

                switch (item.Name)
                {
                    case "Sword":
                        component = go.AddComponent<Sword>();
                        break;
                    case "Axe":
                        component = go.AddComponent<Axe>();
                        break;
                    case "Health":
                        component = go.AddComponent<HealthPotion>();
                        break;
                    case "Key":
                        component = go.AddComponent<Key>();
                        break;

                }

                component.Position = item.Position;
                component.SomethingAbove = item.SomethingAbove;

                newGear.Add(component);
            }

            return newGear;
        }

        public static List<Character> GetCharactersFromLoaded(List<CharacterToSave> enemies)
        {
            var newGear = new List<Character>();

            foreach (var item in enemies)
            {
                var go = new GameObject();
                Character component = null;

                go.AddComponent<SpriteRenderer>();
                go.name = item.Name;

                switch (item.Name)
                {
                    case "Orc":
                        component = go.AddComponent<Orc>();
                        break;
                    case "Skeleton":
                        component = go.AddComponent<Skeleton>();
                        break;
                    case "Ghost":
                        component = go.AddComponent<Ghost>();
                        break;

                }

                component.Position = item.Position;
                component.SetHealth(item.Health);

                newGear.Add(component);
            }

            return newGear;
        }
    }
}
