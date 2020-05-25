using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    protected float speed;
    protected SpriteRenderer characterSprite;
    protected void InitializeBullet(string tag, int givenSpeed)
    {
        Destroy(this.gameObject, 3);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.3f, transform.localPosition.z);
        characterSprite = GameObject.FindGameObjectWithTag(tag).GetComponentInChildren<SpriteRenderer>();

        if (characterSprite.flipX == true)
        {
            speed = -givenSpeed;
        }
        else
        {
            speed = givenSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
