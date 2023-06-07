using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    public float secondsToDestroy;


    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }

   
}