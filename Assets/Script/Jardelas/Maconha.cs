using System;
using System.Collections;
using UnityEngine;

public class Maconha : MonoBehaviour
{
    Animator animator;
    public float tempoExplosao;
    private float contabilizadorExplosao;
    public Transform[] posicaoFragmentosMaconha;
    public GameObject[] fragmentosMaconha;
    void Start()
    {
       animator=GetComponent<Animator>();
    }

    void Update()
    {
        contabilizadorExplosao+=Time.deltaTime;
        if(tempoExplosao>=contabilizadorExplosao)
        {
             StartCoroutine(Explosao());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
         StartCoroutine(Explosao());
        }
    }
    private IEnumerator Explosao()
    {
        animator.Play("Explosao");
        yield return new WaitForSeconds(2);
        for (int i = 0; i<posicaoFragmentosMaconha.Length;i++)
        {
            Instantiate(fragmentosMaconha[i],posicaoFragmentosMaconha[i].position,Quaternion.identity);
        }
        Destroy(gameObject);
        
    }
}
