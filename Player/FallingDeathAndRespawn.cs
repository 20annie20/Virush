using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDeathAndRespawn : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 checkpoint = new Vector3(-6.1f, 1f, 0f);
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)//natrafienie na spadającego gracza
            {
                gameManager.Respawn(true);
            }
        }
    }
}