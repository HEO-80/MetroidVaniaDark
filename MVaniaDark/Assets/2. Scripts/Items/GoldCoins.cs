using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour
{
    public float cashToGive;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BankAcount.Instance.Money(cashToGive);
            AudioManager.instance.PlayAudio(AudioManager.instance.diamon);//sonido de coger diamante
            Destroy(gameObject);
        }
    }
}
