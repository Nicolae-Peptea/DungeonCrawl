using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Actor
{
    public override int DefaultSpriteId => 536;
    public override string DefaultName => "Portal";

    public bool Unlocked { get; private set; } = false;

    public override bool OnCollision(Actor anotherActor)
    {
        if (IsPlayerUnlocking(anotherActor))
        {
            if (((Player)anotherActor).HasKey())
            {
                Unlock();
                return true;
            }
        }
        return false;
    }

    public void Unlock()
    {
        Unlocked = true;
        SetSprite(539);
    }

    public bool IsPlayerUnlocking(Actor actor)
    {
        return actor is Player;
    }
}
