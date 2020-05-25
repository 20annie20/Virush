using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//klasa dla obiektów enemy, są tu podstawowe statystyki, wprowadzenie bazowej metody Attack oraz funkcji update, 
//żeby nie zgubić jej w którymś podrzędnym obiekcie

public abstract class Enemy : Creature
{
    private const string Tag = "Player";
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Transform pointA, pointB;
    protected Vector3 currentTarget;
    protected Animator anim;
    protected Transform playerTransform;
    [SerializeField]
    protected float distance; //not private because the child is overriding the method using this value
    protected bool inCombat = false;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag(tag: Tag).GetComponent<Transform>();
    }

    public virtual void Start()
    {
        Init();
        currentTarget = pointB.localPosition;
        isAlive = true;

    }
    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            return;
        }
        if (isAlive)
        {
            Movement();
        }
    }
    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.localPosition;
            sprite.flipX = false;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.localPosition;
            sprite.flipX = true;
            anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        Vector3 direction = playerTransform.localPosition - transform.localPosition;

        if (anim.GetBool("InCombat"))
        {
            currentTarget = playerTransform.localPosition;
            if (direction.x > 0)
            {
            sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }
        }
        
        distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance < 6 && (transform.position != pointA.localPosition || transform.position != pointB.localPosition))
        {
            inCombat = true;
            anim.SetBool("InCombat", true);
        }
        else 
        {
            inCombat = false;
            anim.SetBool("InCombat", false);
            currentTarget = pointB.localPosition;

        }
    }

    public void GetDamage(int power)
    {
        {
            if (isAlive == false)
                return;

            Debug.Log("Damage");
            health -= power;

            if (health < 1)
            {
                isAlive = false;
                inCombat = false;
                Explode();

            }
            ChangeColorWhenDamaged();
            anim.SetBool("InCombat", true);
        }
    }
    public void Explode()
    {
        Debug.Log("BOOM");
        anim.SetTrigger("Death");
        StartCoroutine(BoomRoutine());
        ParticleSystem exp = GetComponentInChildren<ParticleSystem>();
        exp.Play();
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity) as GameObject;
        coin.GetComponent<Coin>().coins = coins;
        StartCoroutine(WaitForDeathRoutine());
        Debug.Log("pls die");
    }

    IEnumerator WaitForDeathRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    IEnumerator BoomRoutine()
    {  
        yield return new WaitForSeconds(1.5f);
        sprite.enabled = false;
    }
}
