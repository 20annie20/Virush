using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private Player player;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                player.FinishLevel();
            }
        }
    }
}
