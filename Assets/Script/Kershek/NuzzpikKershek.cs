using System;
using System.Collections;
using UnityEngine;

public class NuzzpikKershek : MonoBehaviour
{
    public static Vector2 ultimoPuloPos; // ponto onde player pulou
    public static bool pulouAgora;
    public Rigidbody2D rb;
    public static float velocidade;
    public float maxVelocidade;
    public float horizontal;
    private Animator animator;
    private bool estaDireita = false;
    public bool morto = false;
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
    public Vida vida;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        Pulo();
        Animacacoes();
    }
    void LateUpdate()
    {
        pulouAgora = false;
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
            ultimoPuloPos = transform.position;
            pulouAgora = true;
        }

        // Controla a queda mais leve segurando tecla E
        rb.gravityScale = Input.GetKey(KeyCode.L) && rb.linearVelocity.y < 0 ? 0.5f : 1.5f;
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
        if (Mathf.Abs(horizontal) > 0.1f) velocidade += Time.deltaTime * 1.7f;
        else if (Mathf.Abs(horizontal) > 0.1f) velocidade -= Time.deltaTime * -2f;

        if (velocidade >= maxVelocidade) velocidade = maxVelocidade;
    }

    private void OnDrawGizmosSelected()
    {
        if (encostandoChao != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(encostandoChao.position, areaChecaChao);
        }
    }

    void Virar()
    {
        estaDireita = !estaDireita;
        transform.Rotate(0, 180, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Banana"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<Bala>().dano;
            AudioManager(1);
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }
        if (other.CompareTag("Kershek"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<Kershek>().danos;
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }
        if (other.CompareTag("Grama"))
        {
            velocidade = velocidade - 2f;
        }
        if (other.CompareTag("Arvore"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<ArvoreInimigo>().dano;
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
                velocidade = velocidade - 0.5f;
            }
        }
        if (other.CompareTag("Agua"))
        {
            Vida.vidaPerdida -= vida.vidaTotal;
        }
        if (other.CompareTag("Bala"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<BalaDireita>().dano;
            AudioManager(1);
            if (Vida.vidaPerdida > 0)
            {
                StartCoroutine(TomarDano());
            }
        }
        if(other.CompareTag("Armadilha"))
        {
            Vida.vidaPerdida += other.gameObject.GetComponent<DanoArmadilha>().dano;
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
    public float CurrentHorizontalSpeed
    {
        get
        {
            if (rb == null) return 0f;
            return Mathf.Abs(rb.linearVelocity.x); // velocidade horizontal atual
        }
    }

    void Animacacoes()
    {
        if (Vida.vidaPerdida <= 0)
        {
            StartCoroutine(Morreu());
        }
        else if (rb.linearVelocityY > 0 && !morto && !estaDano) animator.Play("Pulando");
        else if (horizontal == 0 && !morto && !estaDano) animator.Play("ParadoPlataforma");
        else if (horizontal != 0 && !morto && !estaDano) animator.Play("Andando");

    }
    private IEnumerator Morreu()
    {
        morto = true;
        animator.Play("Morreu");
        this.enabled = false;
        yield return new WaitForSeconds(1.6f);
    }
}
