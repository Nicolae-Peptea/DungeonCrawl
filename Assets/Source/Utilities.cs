using System;
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

        public static Direction GetRandomDirectionFromCadran(int cadran)
        {
            System.Random rand = new System.Random();
            Enum[] enumValues = cadran switch
            {
                1 => new Enum[] { Direction.Down, Direction.Right },
                2 => new Enum[] { Direction.Down, Direction.Left },
                3 => new Enum[] { Direction.Up, Direction.Right },
                4 => new Enum[] { Direction.Up, Direction.Left },
                _ => (Enum[])Enum.GetValues(typeof(Direction)),
            };
            return (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        }

        public static void DisplayEventScreen(bool dead)
        {
            string screen = dead ? "deadScreen" : "winScreen";

            foreach (var gameObject in GameObject.FindGameObjectsWithTag(screen))
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
