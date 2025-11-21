using System;
using UnityEngine;

public class Armadilha : MonoBehaviour
{
   public  Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player"))
        {
        rb.bodyType =RigidbodyType2D.Dynamic;
        rb.mass=10f;
        }
    }
}
