﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerControls : MonoBehaviour
{
    public CButton jumpButton;
    public CButton leftButton;
    public CButton rightButton;
    public float speed = 30;
    public float jumpSpeed = 5;
    public bool facingRight = true;

    bool isJumping = false;

    bool isJumpingPressed = false;

    private float rayCastLength = 0.2f;

    private float width;
    private float height;

    private float jumpButtonPressTime;
    private float maxJumpTime = 0.2f;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private bool LevelComplete = false;

    private void Awake()
    {


        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;

    }

    void FixedUpdate()
    {
        if(!sr.isVisible)
        {
            OnBecameInvisible();
        }

        float horzMove = getHorizontalInput();
        Vector2 vect = rb.velocity;

        rb.velocity = new Vector2(horzMove * speed, vect.y);

        if (horzMove > 0 && !facingRight)
        {
            FlipSprite();
        }else if(horzMove < 0 && facingRight)
        {
            FlipSprite();
        }
        float vertMove = jumpButton.IsBtnPressed() ? 1 : Input.GetAxis("Jump");

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
        }

        if(IsOnGround() && !isJumping)
        {
            anim.SetBool("isJumping", false);
            if (vertMove > 0)
            {
                isJumping = true;
            }
        }
        else
        {
            anim.SetBool("isJumping", true);
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

        isJumpingPressed = false;
        
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
        RaycastHit2D groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-(height*0.75f)), -Vector2.up, rayCastLength); // Below Character
        RaycastHit2D groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x - (width), transform.position.y - (height * 0.75f)), Vector2.right, rayCastLength); // To bottom left Of Character
        RaycastHit2D groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x + (width), transform.position.y - (height * 0.75f)), -Vector2.right, rayCastLength); // To bottom Right of Character

        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y-(height  *0.75f)), -Vector2.up * rayCastLength, Color.green);
        //Debug.DrawRay(new Vector2(transform.position.x - (width), transform.position.y - (height * 0.75f)), Vector2.right * rayCastLength, Color.green);
        //Debug.DrawRay(new Vector2(transform.position.x + (width), transform.position.y - (height * 0.75f)), -Vector2.right * rayCastLength, Color.green);

        if ((groundCheck1.collider != null && groundCheck1.collider.tag.Contains("Ground")) 
            || (groundCheck2.collider != null && groundCheck2.collider.tag.Contains("Ground")) 
            || (groundCheck3.collider != null && groundCheck3.collider.tag.Contains("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int getHorizontalInput()
    {
        int direction = 0;

#if UNITY_EDITOR
        direction = (int)Input.GetAxisRaw("Horizontal");
#endif

        if (direction == 0)
        {
            if (leftButton.IsBtnPressed())
                direction = -1;
            else if (rightButton.IsBtnPressed())
                direction = 1;
        }
        

        return direction;
    }

    public void jumpButton_OnClick()
    {
        isJumpingPressed = true;
    }
    
    void OnBecameInvisible()
    {
        // Check that game isn't paused
        if(!PauseMenu.GameIsPaused && !LevelComplete)
        {
            HurtPlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Contains("Finish"))
        {
            LevelComplete = true;
            SaveManager.SaveProgress(PlayerPrefs.GetString("NextLevel"));
            (new LevelSelect()).SelectLevel(PlayerPrefs.GetString("NextLevel"));
        }else if(collision.collider.tag.Contains("Enemy"))
        {
            HurtPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Finish"))
        {
            Debug.Log("Level Complete:" + PlayerPrefs.GetString("CurrentLevel"));
            Debug.Log("Next Level:" + PlayerPrefs.GetString("NextLevel"));
            LevelComplete = true;
            SaveManager.SaveProgress(PlayerPrefs.GetString("NextLevel"));
            (new LevelSelect()).SelectLevel(PlayerPrefs.GetString("NextLevel"));
        }
    }

    void HurtPlayer()
    {
        //Debug.Log("Character Destroyed");
        //PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        Destroy(gameObject);
        //SceneManager.LoadScene("GameOver");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
