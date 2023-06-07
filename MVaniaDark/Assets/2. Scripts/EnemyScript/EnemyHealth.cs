using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public bool isDamaged; //para arreglar triger del arma
    public GameObject deathEffect;
    SpriteRenderer sprite;
    Blink material;
    Rigidbody2D rb;


    private void Start()
    
    {
        enemy = GetComponent<Enemy>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        material = GetComponent<Blink>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon") && !isDamaged)
        {
            enemy.healthPoints -= 2f;
            //AudioManager.instance.Playaudio(AudioManager.instance.enemyHit) //activa sonido de golpear al enemigo

            if (collision.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(enemy.nockbackForceX, enemy.nockbackForceY), ForceMode2D.Force);//movimiento de golpe al enemigo

            } else
            {
                rb.AddForce(new Vector2(-enemy.nockbackForceX, enemy.nockbackForceY), ForceMode2D.Force);//movimiento de golpe al enemigo del otro lado


            }


            StartCoroutine(Damager());//para que el daño no se repita varias veces

            if (enemy.healthPoints <= 0)
            {

                Instantiate(deathEffect, transform.position, Quaternion.identity);
                AudioManager.instance.PlayAudio(AudioManager.instance.enemyDeath);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Damager()
    {
        isDamaged = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.5f);//para que se pueda volver a atacar en ese tiempo sin repetirse
        isDamaged = false;
        sprite.material = material.original;
    }

}
