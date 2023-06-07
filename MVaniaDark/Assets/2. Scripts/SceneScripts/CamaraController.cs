using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform player;
    public Transform activeRoom;
    public float dampSpeed;
  

    public static CamaraController instance;

    [Range(-5,5)]
    public float minModX = 3.5f, maxModX = -3.5f, minModY = 2f , maxModY = -2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //player = PlayerController.instance.gameObject.transform;
        activeRoom = player;
        transform.position = new Vector3(player.position.x, player.position.y, -1);
    }

    // Update is called once per frame
    void Update()
    {
        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + minModY;//defindo el limite de la camara
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y + maxModY;
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x + maxModX;

      

        Vector3 clampedPos = new Vector3(  //zona donde vamos a encerrar la camara
            Mathf.Clamp(player.position.x, minPosX,maxPosX),
            Mathf.Clamp(player.position.y, minPosY, maxPosY),
            Mathf.Clamp(player.position.z, -10f,-10f)
            );


        Vector3 smoothPos = Vector3.Lerp(transform.position, clampedPos, dampSpeed * Time.deltaTime);
        transform.position = smoothPos;

       
    }
}
