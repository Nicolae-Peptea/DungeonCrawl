using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Save
{
    public class CharacterToSave
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int Attack { get; set; }

        public bool IsAlive { get; set; } = true;

        public (int x, int y) Position { get; set; }

        public CharacterToSave(Character character)
        {
            Name = character.DefaultName;
            Health = character.Health;
            Attack = character.Attack;
            IsAlive = character.IsAlive;
            Position = character.Position;
        }

        public CharacterToSave()
        {
        }
    }
}
