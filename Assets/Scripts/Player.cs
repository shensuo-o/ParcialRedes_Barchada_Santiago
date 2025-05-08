using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    //stats
    [SerializeField] public float HP;
    [SerializeField] public float MaxHP;
    [SerializeField] private float Speed;

    //variables para el movimiento
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float HorizontalInput;

    //variables para el salto
    [SerializeField] private float jumpForce;
    [SerializeField] private float JumpStartTime;
    [SerializeField] private float jumpTime;
    [SerializeField] public bool isJumping;
    [SerializeField] private bool isGrounded;

    //variables para el dash
    [SerializeField] private bool canDash;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    [SerializeField] private bool dashRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        Jump();
        if (HorizontalInput == 1)
        {
            dashRight = true;
        }
        else if (HorizontalInput == -1)
        {
            dashRight = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movement(HorizontalInput);
    }

    private void Movement(float dir)//Toma la variable de direccion y la usa para moverse con velocity del rigidbody.
    {
        var xVel = dir * Speed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVel, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    private void Jump()//Salto que se hace mas alto contra mas se sostiene apretado el boton.
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTime = JumpStartTime;

            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (dashRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else if (!dashRight)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        }

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }

        if (collision.gameObject.layer == 10)
        {
            HP = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = false;
        }
    }
}
