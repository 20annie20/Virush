using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public GameObject coinPrefab;
    [SerializeField]
    protected int coins;
    [SerializeField]
    protected int health;
    protected bool isAlive;
    protected SpriteRenderer sprite;

    public int Health
    {
        get => health;
        set => health = value;
    }
    public int Coins
    {
        get => coins;
        set => coins = value;
    }

    protected void ChangeColorWhenDamaged()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    IEnumerator ChangeColorRoutine()
    {
        Color red = new Color(1, -1f, -1f, 1);
        sprite.color = red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = new Color(1, 1, 1, 1);
    }
}
