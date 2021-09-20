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
            Random rand = new System.Random();
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
    }
}
