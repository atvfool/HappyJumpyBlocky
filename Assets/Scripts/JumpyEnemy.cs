using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyEnemy : MonoBehaviour
{
    public float jumpHeight = 4f;
    public float jumpFrequency = 2f;
    public float jumpDelay = 0f;

    Rigidbody2D rb;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (jumpDelay > 0)
            time = Time.deltaTime + jumpDelay;

        InvokeRepeating("Jump", jumpDelay, jumpFrequency);
    }

    // Update is called once per frame
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
     
    }
}
