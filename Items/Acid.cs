using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    readonly float moveSpeed = 7f;

	Rigidbody2D rb;

	Player target;
	Vector2 moveDirection;
    private SpriteRenderer _EnemySprite;

    // Use this for initialization
    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>();
        _EnemySprite = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SpriteRenderer>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		Destroy(gameObject, 3f);
      
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Player"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                Destroy(this.gameObject);
                hit.GetDamage(1); 
            }
        }
        else if(other.CompareTag("Coin"))
        {
            //the acid flies through the coins
        }
        else if(!other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
	}

}
