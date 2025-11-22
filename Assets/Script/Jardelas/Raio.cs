using System;
using System.Collections;
using UnityEngine;

public class Raio : MonoBehaviour
{public float dano;
    void Start()
    {
        StartCoroutine(Raios());
            }
   private IEnumerator Raios()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
            }
}
