using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
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
