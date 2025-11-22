using System;
using System.Collections;
using UnityEngine;

public class Maconha : MonoBehaviour
{
    Animator animator;

    [Header("Movimento da Bala Principal")]
    public float velocidade;
    public float tempoExplosao;
    private float contabilizadorExplosao;

    [Header("Fragmentação")]
    public Transform[] posicaoFragmentosMaconha;
    public GameObject[] fragmentosMaconha;
    public Vector2[] direcoesFragmentos; // <- direções de cada fragmento
    public float velocidadeFragmento = 8f;

    public float dano;

    private bool jaExplodiu = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!jaExplodiu)animator.Play("Voando");
        transform.position += -transform.right * velocidade * Time.deltaTime;

        contabilizadorExplosao += Time.deltaTime;

        if (!jaExplodiu && contabilizadorExplosao >= tempoExplosao)
        {
            StartCoroutine(Explosao());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!jaExplodiu && collision.CompareTag("Player"))
        {
            StartCoroutine(Explosao());
        }
    }

   private IEnumerator Explosao()
{
    jaExplodiu = true;

    animator.Play("Explosao");

    yield return new WaitForSeconds(0.83f);

    for (int i = 0; i < posicaoFragmentosMaconha.Length; i++)
    {
        GameObject frag = Instantiate(
            fragmentosMaconha[i],
            posicaoFragmentosMaconha[i].position,
            Quaternion.identity
        );

        DanoMaconha script = frag.GetComponent<DanoMaconha>();

        if (script != null)
        {
            script.velocidade = velocidadeFragmento;
            script.SetDirecao(direcoesFragmentos[i]);
        }
    }

    Destroy(gameObject);
}

}
