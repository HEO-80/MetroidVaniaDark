using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;

    private Vector2 offset;

    private Material material;
    public Transform player;
    private Rigidbody2D rb;


    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        offset = (rb.velocity.x * 0.1f) * velocidadMovimiento * Time.deltaTime;
        //offset =  velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
