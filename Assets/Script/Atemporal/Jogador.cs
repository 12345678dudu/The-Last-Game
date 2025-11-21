using System;
using System.Collections;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float danoJogador;
    private Rigidbody2D rb;
    public float velocidade;
    public float horizontal;
    [SerializeField] private GameObject bico;
    private Animator animator;
    private bool estaDireita = false;
    private bool estaAtaque = false;
    private bool morto=false;
    private bool estaDano;
    public AudioClip[] audioClip;
    private AudioSource audioSource;
    private GameObject gameObjects;
    [Header("Funções do Pulo")]
    private bool pulandoduplo;
    [SerializeField] private float forcaPulo;
    public float quantidadeMaxPulo;
    private float quantidadePulo;

    [Header("Detecção do Chão")]
    [SerializeField] private bool noChao;
    public Transform encostandoChao;
    public float areaChecaChao;
    public LayerMask checaChao;
    public Jardelas jardelas;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Pulo();
        DetectarAtaque();
        Animacacoes();
    }

    void FixedUpdate()
    {
        Deteccao();
        Moviment();
    }

    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && quantidadePulo > 0)
        {
            quantidadePulo--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            AudioManager(0);
        }

        // Controla a queda mais leve segurando tecla E
        rb.gravityScale = Input.GetKey(KeyCode.L) && rb.linearVelocity.y < 0 ? 0.3f : 1.5f;
    }

    void Deteccao()
    {
        noChao = Physics2D.OverlapCircle(encostandoChao.position, areaChecaChao, checaChao);
        if (noChao && rb.linearVelocityY <= 0)
        {
            quantidadePulo = quantidadeMaxPulo;
        }
    }

    void Moviment()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * velocidade, rb.linearVelocity.y);
        if (horizontal > 0 && estaDireita) Virar();
        if (horizontal < 0 && !estaDireita) Virar();

    }

    private void OnDrawGizmosSelected()
    {
        if (encostandoChao != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(encostandoChao.position, areaChecaChao);
        }
    }

    void DetectarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.O) && !estaAtaque)
        {// ativa animação
            StartCoroutine(Ataque());
        }
    }

    IEnumerator Ataque()
    {
        bico.SetActive(true);
        estaAtaque = true;
        yield return new WaitForSeconds(0.45f);
        bico.SetActive(false);
        estaAtaque = false;
    }

    void Virar()
    {
        estaDireita = !estaDireita;
        transform.Rotate(0, 180, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bala"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<Bala>().dano;
            AudioManager(1);
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }
        if (other.CompareTag("BalaAerea"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<BalaAerea>().danos;
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }
        if (other.CompareTag("Patrulha"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<PatrulhaDano>().dano;
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }

        if (other.CompareTag("Jardelas"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<Jardelas>().dano;
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }

    }
    private IEnumerator TomarDano()
    {
        estaDano = true;
        animator.Play("Dano");
        yield return new WaitForSeconds(1.03f); // tempo visível da animação
        estaDano = false;
        animator.Play("Dano");
    }

    void AudioManager(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }
    void Animacacoes()
    {
      
         if (Vida.vidaPerdida <= 0)
        {
         StartCoroutine(Morreu());
        }
        else if(estaAtaque)animator.Play("Bicada");
          else   if (rb.linearVelocityY > 0 &&  !morto && !estaDano) animator.Play("Pulando");
        else if (horizontal == 0 && !morto && !estaDano) animator.Play("ParadoPlataforma");
        else if (horizontal != 0 &&  !morto && !estaDano) animator.Play("Andando");
    }
      private IEnumerator Morreu()
    {
           morto = true;
            animator.Play("Morreu");
            yield return new WaitForSeconds(1.53f);
            this.enabled = false;
    }
}
