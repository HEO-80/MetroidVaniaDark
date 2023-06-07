using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    public Joystick joystick;

    public float runSpeedHorizontal = 2;
    public float speed = 2;
    public float jumpHeight = 2;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;
    [HideInInspector]//oculto para poder controlarlo desde GameManager
    public ControllerType controller;
    public bool isAttacking;

    //public static PlayerController instance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY);

        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Walk", true);

        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("Walk", true);

        }else if (horizontalMove == 0)
        {
            anim.SetBool("Walk", false);
            //AudioManager.instance.PlayAudio(AudioManager.instance.grass);
        }


        if (isGrounded)//si esta tocando el suelo puede saltar
        {
            anim.SetBool("Jump", false);

        }
        else
        {
            anim.SetBool("Jump", true);

        }
        
    }

    private void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * runSpeedHorizontal;
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime * speed;

    }


    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackCO());
         }
       
    }
    IEnumerator AttackCO()
    {
        isAttacking= true;
        anim.SetBool("Attack", true);//si presiono fire atacak si no es false
        AudioManager.instance.PlayAudio(AudioManager.instance.hit);//sonido de golpear
        yield return new WaitForSeconds(0.2f);
        isAttacking= false;
        anim.SetBool("Attack", false);
    }

    public void Jump()
    {
        
        if (isGrounded)
        {
            anim.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            AudioManager.instance.PlayAudio(AudioManager.instance.jump);//sonido de saltar
        }

        else if(!isGrounded)
        {
            anim.SetBool("Jump", false);
        }
    }

        public void Movement()
    {

        //Andar en horizontal
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;

        rb.velocity = new Vector2(velX * speed, velY);

        if (rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true);

        }
        else
        {
            anim.SetBool("Walk", false);
            //AudioManager.instance.PlayAudio(AudioManager.instance.grass);
        }


    }

    public void FlipCharacter()
    {
        //andar segun la direccion

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

 
}
