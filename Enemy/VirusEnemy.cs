using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : Enemy, IDamageable
{

    [SerializeField]
    GameObject acidPrefab;
    public SpriteRenderer _acidSprite;

    float fireRate;
    float nextFire;
    public override void Init()
    {
        base.Init();
    }
    public override void Start()
    {
        _acidSprite = acidPrefab.GetComponentInChildren<SpriteRenderer>();
        base.Start();
        fireRate = 1f;
        nextFire = Time.time + fireRate;
        
    }
    public override void Update()
    {
        base.Update();
        if (inCombat)
        {
            CheckIfTimeToFire();
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            if (sprite.flipX)
            {
                _acidSprite.flipX = true;
            }
            else
            {
                _acidSprite.flipX = false;
            }
            Instantiate(acidPrefab, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }

    }

}
