using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance 
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    public Player player;
    public PlayerData data;

    private void Awake()
    {
        _instance = this;
        player = FindObjectOfType<Player>();
    }

    public void Respawn(bool giveDamage)
    {
        switch (player.Level) 
        {
            case 1:
                player.transform.position = new Vector3(-5.73f, 1f, 0);
                break;
            case 2:
                player.transform.position = new Vector3(-317f, -6.95f, 0);
                break;
            case 3:
                player.transform.position = new Vector3();
                break;
        }
        if (giveDamage)
        {
            player.GetDamage(1);
        }
        
    }
}
