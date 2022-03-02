using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Enum to be used while comunicating with the mecanim for GameCharacter
public enum CharacterMecanimParameters
{
    Alive, MoveHorz, MoveVert, Grounded, Air, Meleed, Shooted, Slided, Hurt
}

// Enum with the physics layers used by GameCharacters
public enum CharacterLayer
{
    Player = 8, PlayerOnly = 9, Enemy = 10, EnemyOnly = 11, Corpse = 13, CharOnly = 14
}

public static class GameCharacterUtils
{
    ////////////// CharacterMecanimParameters Extensions
    // Optimization for mecanim, storing the hash of the parameters here, to use instead of string
    static int?[] parameterHashes = Extensions.Populate<int?>(null, Enum.GetValues(typeof(CharacterMecanimParameters)).Length);
    public static int Hash(this CharacterMecanimParameters param)
    {
        int index = (int)param;
        if(parameterHashes[index] == null)
        {
            parameterHashes[index] = Animator.StringToHash(param.ToString());
        }

        return parameterHashes[index] ?? -1;
    }

    ////////////// CharacterLayer Extensions
    public static CharacterLayer GetEnemyLayer(this CharacterLayer layer)
    {
        switch (layer)
        {
            case CharacterLayer.Player: return CharacterLayer.Enemy;
            case CharacterLayer.PlayerOnly: return CharacterLayer.EnemyOnly;
            case CharacterLayer.Enemy: return CharacterLayer.Player;
            case CharacterLayer.EnemyOnly: return CharacterLayer.PlayerOnly;            
        }

        // Never should get here
        return CharacterLayer.Player;
    }
    public static CharacterLayer GetDamageLayer(this CharacterLayer layer)
    {
        switch (layer)
        {
            case CharacterLayer.Player: return CharacterLayer.PlayerOnly;
            case CharacterLayer.PlayerOnly: return CharacterLayer.PlayerOnly;
            case CharacterLayer.Enemy: return CharacterLayer.EnemyOnly;
            case CharacterLayer.EnemyOnly: return CharacterLayer.EnemyOnly;
        }

        // Never should get here
        return CharacterLayer.Player;
    }
    public static CharacterLayer GetEnemyDamageLayer(this CharacterLayer layer)
    {
        return layer.GetEnemyLayer().GetDamageLayer();
    }

}
