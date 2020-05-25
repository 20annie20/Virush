using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        Debug.Log("System: saving");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player)
        {
            coins = player.Coins,
            level = player.Level,
            maxHealth = player.MaxHealth,
            speedRate = player.SpeedRate,
            damageValue = player.DamageValue
        };

        Debug.Log("Coins in data: "+ data.coins);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("what is in the saved data: " + "maxHealth " + data.maxHealth
            + ", level " + data.level + ", coins " + data.coins +
            ", damageValue" + data.damageValue + ", speedRate" + data.speedRate
            ); 
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("what is in the loaded data: " + "maxHealth " + data.maxHealth
            + ", level " + data.level + ", coins " + data.coins +
            ", damageValue" + data.damageValue + ", speedRate" + data.speedRate);
            return data;

        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
