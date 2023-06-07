using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public bool canDoubleJump;
    public float doubleJumpSpeed;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;
    [HideInInspector]//oculto para poder controlarlo desde GameManager
    public ControllerType controller;


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

        FlipCharacter();
        if (isGrounded)
        {
            anim.SetBool("Jump", false);

        }
        else 
        {
            anim.SetBool("Jump", true);

        }

        FlipCharacter();
        Attack();

    }

    private void FixedUpdate()
    {

        Movement();
        Jump();
    }


    public void Attack ()
    {
        if(Input.GetButton("Fire1"))
        {
            anim.SetBool("Attack", true);//si presiono fire atacak sino es false
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);//sonido de golpear

        }
        else
        {
            anim.SetBool("Attack", false);
            

        }
    }


    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)// si esta en el suelo puede saltar
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            AudioManager.instance.PlayAudio(AudioManager.instance.jump);//sonido de saltar
            canDoubleJump= true;
        }
        //else
        //{
        //    if (canDoubleJump)
        //    {
        //        anim.SetBool("DoubleJump", true);
        //        rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
        //        AudioManager.instance.PlayAudio(AudioManager.instance.jump);//sonido de saltar
        //        canDoubleJump = false;
        //    }
        //}
        else if (!isGrounded && canDoubleJump == false)
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

    //public void HorizontalMovement(int value)
    //{
    //    velX = value;
    //}
   
}
