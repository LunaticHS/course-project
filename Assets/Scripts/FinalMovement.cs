﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class FinalMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private Transform pos;

    public float speed, jumpForce;
    public float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;
    public float startx, starty;
    public float velocityy;
    public int back = 0;
    public List<Block> blocks = new List<Block>();

    public bool isGround, isJump, isDashing;

    bool jumpPressed;
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        CheckH();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.4f, ground) /*&& (rb.velocity.y < 2.5 && rb.velocity.y > -2.5)*/;
        //isGround = (rb.velocity.y < 0.1);

        GroundMovement();

        Jump();


        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }

    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 1;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()//动画切换
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

    void CheckH()
    {
        if (transform.position.y<-10f)
        {
            Debug.Log(pos.position.y);
            rebirth();
        }
    }

    void rebirth()
    {
        transform.position = new Vector3(startx, starty, 0f);
        foreach(Block element in blocks)
        {
            element.reset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("???");
    }
}
