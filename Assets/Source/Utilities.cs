using System;

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
            Random rand = new Random();
            Array enumValues = Enum.GetValues(typeof(Direction));
            return (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        }

        public static Direction GetRandomDirectionFromCadran(int cadran)
        {
            Random rand = new Random();
            Enum[] enumValues;

            switch (cadran)
            {
                case 1:
                    enumValues = new Enum[] { Direction.Down, Direction.Right };
                    break;
                case 2:
                    enumValues = new Enum[] { Direction.Down, Direction.Left };
                    break;
                case 3:
                    enumValues = new Enum[] { Direction.Up, Direction.Right };
                    break;
                case 4:
                    enumValues = new Enum[] { Direction.Up, Direction.Left };
                    break;
                default:
                    enumValues = (Enum[])Enum.GetValues(typeof(Direction));
                    break;
            }
            return (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        }
    }
}
