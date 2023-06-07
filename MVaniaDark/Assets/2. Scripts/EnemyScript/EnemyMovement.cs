using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isPatrol;
    public bool walksRight;
    public bool shouldWait;
    public float timeToWait;
    public bool isWaiting;




    public Transform wallCheck, pitCheck, groundCheck;
    bool wallDetected, pitDetected, isGrounded;
    public float detectRadius;
    public LayerMask whatIsGround;
    
    public Transform pointA, pointB;
    public bool goToA, goToB;





    // Start is called before the first frame update
    void Start()
    {
        goToA = true;
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        pitDetected= !Physics2D.OverlapCircle(pitCheck.position,detectRadius,whatIsGround);//detecta si hay suelo
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectRadius, whatIsGround);//si detecta pared
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, detectRadius, whatIsGround);//detecta si esta en el suelo

        if ((pitDetected || wallDetected) && isGrounded) // si detecta vacio o pared y esta en el suelo => hace flip
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;//enemigo no puede ser empujado por player

        } 
        if (isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;//enemigo congela la rotacion
            anim.SetBool("idle", false);

            if (!walksRight)
            {
                rb.velocity = new Vector2( -speed * Time.deltaTime, rb.velocity.y); //enemigo camina a la izda
            }
            else
            {
                rb.velocity = new Vector2( speed * Time.deltaTime, rb.velocity.y);//enemigo camina a la drcha
            }
        }
        if (isPatrol)
        {
            
            if (goToA)
            {
                if(!isWaiting) 
                {
                    anim.SetBool("idle", false);
                    rb.velocity = new Vector2(-speed * Time.deltaTime, -rb.velocity.y);
                }
               

                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    if(shouldWait)
                    {
                        StartCoroutine(waiting());
                    }

                    Flip();
                    goToA= false;
                    goToB= true;
                }
            
            }

            if(goToB)
            {
                if (!isWaiting)
                {
                    anim.SetBool("idle", false);
                    rb.velocity = new Vector2(speed * Time.deltaTime, -rb.velocity.y);
                }
                
                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    Flip();
                    goToA = true;
                    goToB = false;
                }
            }
        }

    }
    IEnumerator waiting() //funcion esperar unos segundos
    {
        anim.SetBool("idle", true);
        isWaiting= true;
        Flip();
        yield return new WaitForSeconds(timeToWait);
        isWaiting= false;
        anim.SetBool("idle", false);
        Flip();
    }
    public void Flip()//Funcion que hace cambiar de direccion al enemigo
    {
        walksRight = !walksRight;       //si anda a la derecha...
        transform.localScale *= new Vector2(-1, transform.localScale.y);//cambia de dirección
    }
}
