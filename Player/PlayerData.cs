using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int maxHealth;
    public int coins;
    public float speedRate;
    public int damageValue;
    public PlayerData(Player player) 
    {
        level = player.Level;
        maxHealth = player.MaxHealth;
        coins = player.Coins;
        speedRate = player.SpeedRate;
        damageValue = player.DamageValue;
    }
}
