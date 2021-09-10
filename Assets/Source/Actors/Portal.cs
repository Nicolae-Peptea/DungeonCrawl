using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

public class Portal : Actor
{
    public override int DefaultSpriteId => 536;
    public override string DefaultName => "Portal";

    public bool Unlocked { get; private set; } = false;

    public override bool OnCollision(Actor anotherActor)
    {
        if (IsPlayerUnlocking(anotherActor))
        {
            if (((Player)anotherActor).HasKey() && Unlocked == false)
            {
                Unlock();
            }
            else if (Unlocked)
            {
                return true;
            }
        }

        return false;
    }

    public void Unlock()
    {
        int unlockedPortalId = 539;
        Unlocked = true;
        SetSprite(unlockedPortalId);
    }

    public bool IsPlayerUnlocking(Actor actor)
    {
        return actor is Player;
    }
}
