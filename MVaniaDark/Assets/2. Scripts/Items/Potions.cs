using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public float healthToGive;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().health += healthToGive;
            AudioManager.instance.PlayAudio(AudioManager.instance.heart);//sonido de coger heart
            Destroy(gameObject);    

        }
    }
}
