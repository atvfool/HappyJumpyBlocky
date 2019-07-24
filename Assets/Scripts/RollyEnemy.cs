using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyEnemy : MonoBehaviour
{
    public float rollForce = 3.5f;
    public float forceInterval = 1f;
    public float startDelay = 1f;
    public bool rollLeft = true;

    Rigidbody2D rb;
    float nextForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    void AddForce()
    {
        if (Time.time > nextForce)
        {
            nextForce = Time.time + forceInterval;
            rb.AddForce((rollLeft ? Vector2.left : Vector2.right) * rollForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Ground") || collision.collider.tag.Contains("Finish"))
        {
            AddForce();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
