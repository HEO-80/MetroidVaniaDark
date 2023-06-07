using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{

    //Script de camara avanzada, para que la camara siga al player por rooms
    public void OnTriggerEnter2D(Collider2D collision)//detecta si el player ha entrado en el room
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CamaraController.instance.activeRoom = transform.GetChild(0);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)//detecta si el player se mantiene en el room

    {
        if (collision.CompareTag("Player"))
        {
            CamaraController.instance.activeRoom = transform.GetChild(0);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)//detecta si el player se sale del room
    {
        if (collision.CompareTag("Player"))//si lo que sele de ese trigger algo con tag de player..
        {
            transform.GetChild(0).gameObject.SetActive(false);//voy a poner el gameObject del hijo como falso.

        }
    }

}
