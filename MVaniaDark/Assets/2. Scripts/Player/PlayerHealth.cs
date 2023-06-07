using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Image HealthImg;
    bool isInmune;
    public float inmunityTime;
    Blink material;
    SpriteRenderer sprite;
    public float nockBackForceX;
    public float nockBackForceY;
    Rigidbody2D rb;
    public GameObject deathEffect;
    public GameObject gameOverImg;


    // Start is called before the first frame update
    void Start()
    {
        gameOverImg.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        health = maxHealth;
        material.original = sprite.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthImg.fillAmount = health/100;
            
        if (health > maxHealth)
        {
            health = maxHealth;//la salud solo puede tener el maximo de salud
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isInmune)//si es golpeado por un enemigo y no es inmune
        {
            health -= collision.GetComponent<Enemy>().damageToGive;//cuando nos golpee un enemigo se va a restar el daño
            StartCoroutine(Inmunity());



            if (collision.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(-nockBackForceX, nockBackForceY), ForceMode2D.Force);//movimiento de golpe al enemigo

            }
            else
            {
                rb.AddForce(new Vector2(nockBackForceX, nockBackForceY), ForceMode2D.Force);//movimiento de golpe al enemigo del otro lado


            }

            if (health < 0)
            {
                Time.timeScale= 0;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                AudioManager.instance.PlayAudio(AudioManager.instance.playerDeath);
                //Destroy(gameObject);
                //Mostrar pantalla de Game Over
                gameOverImg.SetActive(true);
                AudioManager.instance.backGroundMusic.Stop();
                AudioManager.instance.PlayAudio(AudioManager.instance.gameOver);
            }
        }
    }

    IEnumerator Inmunity()
    {

        isInmune = true;
        sprite.material = material.blink;//al ser golpeado se pone el personaje blanco
        yield return new WaitForSeconds(inmunityTime);//esperamos unos segundos antes de que nos puedan vovolver a quitar vida.
        sprite.material = material.original;//buelve a su estado de color original
        isInmune = false;

    }
}
