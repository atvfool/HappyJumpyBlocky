﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float speed = 30;
    public float jumpSpeed = 5;
    public bool facingRight = true;

    bool isJumping = false;

    private float rayCastLength = 0.005f;

    private float width;
    private float height;

    private float jumpButtonPressTime;
    private float maxJumpTime = 0.2f;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;

    }

    void FixedUpdate()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");
        Vector2 vect = rb.velocity;

        rb.velocity = new Vector2(horzMove * speed, vect.y);

        if (horzMove > 0 && !facingRight)
        {
            FlipSprite();
        }else if(horzMove < 0 && facingRight)
        {
            FlipSprite();
        }
        float vertMove = Input.GetAxis("Jump");

        if(IsOnGround() && !isJumping)
        {
            if(vertMove > 0)
            {
                isJumping = true;
            }
        }

        if(jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0;
        }

        if(isJumping && jumpButtonPressTime < maxJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if(vertMove >= 1)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0;
        }
    }
    
    void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsOnGround()
    {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLength); // Below Character
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x - (width), transform.position.y-height), -Vector2.right, rayCastLength); // To bottom left Of Character
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x + (width), transform.position.y-height), Vector2.right, rayCastLength); // To bottom Right of Character

        bool leftWallCheck1 = Physics2D.Raycast(new Vector2(transform.position.x - (width), transform.position.y), -Vector2.right, rayCastLength); // To middle left Of Character
        bool leftWallCheck2 = Physics2D.Raycast(new Vector2(transform.position.x - (width), transform.position.y+height), -Vector2.right, rayCastLength); // To top left Of Character

        bool rightWallCheck1 = Physics2D.Raycast(new Vector2(transform.position.x + (width), transform.position.y), Vector2.right, rayCastLength); // To Right of Character
        bool rightWallCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width), transform.position.y+height), Vector2.right, rayCastLength); // To top Right of Character

        if (groundCheck1 || groundCheck2 || groundCheck3 || 
            leftWallCheck1 || leftWallCheck2 || 
            rightWallCheck1 || rightWallCheck2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Character Destroyed");
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}
