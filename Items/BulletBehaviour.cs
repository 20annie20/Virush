using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private float speed;
    private Player player;
    private int damageValue;


    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        damageValue = player.DamageValue;
        Destroy(this.gameObject, 3);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y-0.3f, transform.localPosition.z);
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>();

        if (playerSprite.flipX == true)
        {
            speed = -6;
        }
        else
        {
            speed = 6;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                Destroy(this.gameObject);
                hit.GetDamage(damageValue); //the object colliding with bullet receives proper damage 
            }
        }
        else if(other.CompareTag("Coin"))
        {
            //the bullet flies through the coins
        }
        else if(!other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
