using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private Rigidbody2D rd;
    private SpriteRenderer sr = null;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rd = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        #region Playerが視界に入ったら行動開始
        if (sr.isVisible) 
        {
            rd.velocity = new Vector2(4, 0);
        }
        #endregion
        else
        {
            return;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       /* if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }*/
    }
}
