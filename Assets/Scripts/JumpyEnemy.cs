using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyEnemy : MonoBehaviour
{
    public float jumpHeight = 4f;
    public float jumpFrequency = 2f;
    public float jumpDelay = 0f;
    public enum Direction { Up, Down, Left, Right };
    public Direction direction = Direction.Up;

    Rigidbody2D rb;

    float time = 0;

    Vector2 jumpDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (jumpDelay > 0)
            time = Time.deltaTime + jumpDelay;
        
        switch (direction)
        {
            case Direction.Down:
                jumpDirection = Vector2.down;
                rb.gravityScale = -1;
                break;
            case Direction.Left:
                jumpDirection = Vector2.left;
                rb.gravityScale = 0;
                break;
            case Direction.Right:
                jumpDirection = Vector2.right;
                rb.gravityScale = 0;
                break;
            case Direction.Up:
            default:
                jumpDirection = Vector2.up;
                rb.gravityScale = 1;
                break;
        }

        InvokeRepeating("Jump", jumpDelay, jumpFrequency);
    }

    // Update is called once per frame
    void Jump()
    {
        rb.AddForce(jumpDirection  * jumpHeight, ForceMode2D.Impulse);
    }
}
