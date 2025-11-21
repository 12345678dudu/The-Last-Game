using UnityEngine;

public class Jardelas : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dano;
    private Animator animator;
    public GameObject Limite;
    public GameObject correndo;
    [Header("Funções da Movimentação")]
    private Vector2 posicaoInicial;
    public static bool estaDireita = false;
    private float tempoCorrida;
    public float tempoCorridaVariavel;
    public float velocidade;
    public float posicaoMax;
    private bool estaAndando;
    public float direcao;
    public AudioClip[] audioClip;
    private AudioSource audioSource;
    [Header("Funções do Pulo")]
    public float quantidadePulo;
    private float tempoparaPular;
    private float tempoPulo;
    private float tempoMaxPulo;
    [SerializeField] private float forcaPulo;
    private bool estaPulando;

    [Header("Detecção do Chão")]
    [SerializeField] private bool noChao;
    public Transform encostandoChao;
    public float areaChecaChao;
    public LayerMask checaChao;
    [Header("Ignorar colisão")]
    public Collider2D zonaIgnorar;
    public Collider2D colJogador;
    private Collider2D colInimigo;
    [Header("Fase 2")]
    private bool faseTwo = false;
    public Jogador jogador;
    public float tempoBala;
    private float contabilizadorBala;
    public GameObject maconha;
    public Transform posicao;

    void Start()
    {
        posicaoInicial = transform.position;
        transform.localScale = new Vector3(-1, 1, 1);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colInimigo = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Pulo();
        Moviment();
        FaseTwo();
        Morreu();
    }

    void FixedUpdate()
    {
        Deteccao();
    }


    // ----------------- PULO -----------------
    void Pulo()
    {
        if (!faseTwo)
        {

            tempoparaPular += Time.deltaTime;

            if (tempoparaPular >= 5f && tempoPulo < quantidadePulo)
            {
                tempoMaxPulo += Time.deltaTime;
                if (tempoMaxPulo >= 1f)
                {
                    rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
                    tempoPulo++;
                    tempoMaxPulo = 0f;
                }

                if (tempoPulo >= quantidadePulo)
                {
                    tempoparaPular = 0f;
                    tempoPulo = 0f;
                }
            }

        }

    }


    // ----------------- DETECÇÃO DO CHÃO -----------------
    void Deteccao()
    {
        noChao = Physics2D.OverlapCircle(encostandoChao.position, areaChecaChao, checaChao);
        estaPulando = estaPulando ? rb.linearVelocityY != 0 : !estaPulando;
        estaAndando = estaAndando ? rb.linearVelocityX != 0 : !estaAndando;

    }

    private void OnDrawGizmosSelected()
    {
        if (encostandoChao != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(encostandoChao.position, areaChecaChao);
        }
    }


    // ----------------- MOVIMENTAÇÃO -----------------
    void Moviment()
    {
        if (!faseTwo)
        {
            tempoCorrida += Time.deltaTime;

            if (tempoCorrida >= tempoCorridaVariavel)
            {
                direcao = estaDireita ? 1f : -1f;
                rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocityY);
                if (!estaDireita && transform.position.x <= posicaoInicial.x - posicaoMax)
                {
                    Limite.SetActive(true);
                    Virar();
                }
                if (estaAndando) correndo.SetActive(true);
            }
              if (!estaAndando) correndo.SetActive(false);
        }
    }
    void Atirar()
    {
        if(tempoCorrida <= tempoCorridaVariavel)
        {
            contabilizadorBala+=Time.deltaTime;
            if(tempoBala>=contabilizadorBala)
            {
                  Instantiate(maconha,posicao.position, Quaternion.identity);
            }
        }
    }

    void Virar()
    {
        estaDireita = !estaDireita;
        transform.localScale = new Vector3(estaDireita ? 1 : -1, 1, 1);
    }



    // ----------------- COLISÕES -----------------
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Limite"))
        {
            rb.linearVelocityX = 0;
            Virar();
            tempoCorrida = 0;
        }

        if (coll.CompareTag("Player"))
        {
            HudInimigo.vidaPerdidas += jogador.danoJogador;
        }
        if (coll == colJogador)
        {
            Physics2D.IgnoreCollision(colInimigo, colJogador, true);
        }
        if (coll == zonaIgnorar)
        {
            Physics2D.IgnoreCollision(colInimigo, zonaIgnorar, true);
        }

    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll == colJogador)
        {
            Physics2D.IgnoreCollision(colInimigo, colJogador, false);
        }
        if (coll == zonaIgnorar)
        {
            Physics2D.IgnoreCollision(colInimigo, zonaIgnorar, true);
        }

    }

    void FaseTwo()
    {
        if (HudInimigo.vidaPerdidas <= 150 && !faseTwo)
        {
            faseTwo = true;
            tempoCorrida = 0;
            tempoparaPular = 0;
        }

        if (faseTwo)
        {

            
        }
    }
    void Morreu()
    {
        if (HudInimigo.vidaPerdidas <= 0)
        {
            this.enabled = false;
        }
    }
    void AudioManager(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }
}
