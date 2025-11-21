
using UnityEngine;

public class Atemporal : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject Limite;
    public bool estaAtaque = false;
    [Header("Funções da Movimentação")]
    private Vector2 posicaoInicial;
    public static bool estaDireita = false;
    private float tempoCorrida;
    public float tempoCorridaVariavel;
    public float velocidade;
    public float posicaoMax;
    private bool estaAndando;
    public AudioClip[] audioClip;
    private AudioSource audioSource;

    [Header("Funções da Bala")]
    public GameObject prefabBala;
    public Transform spawnBala;
    private float tempoBala;
    public float intervalo;
    public float intervaloFaseDois;
    private float criandoBala;
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
    public float forcaPuloInicial;  // Pulo mais alto
    public float forcaPuloLoop;      // Pulo mais leve, contínuo
    public float intervaloPulo;      // Intervalo entre pulos
    public int quantidadeMaxPulo;   // Ou outro limite, se quiser
    float tempoParaProximoPulo;
    public Jogador jogador;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posicaoInicial = transform.position;
        tempoBala = 1.5f;
        colInimigo = GetComponent<Collider2D>();
        GameObject passaro = GameObject.Find("ControleInimigoPatrulha");
        if (passaro != null)
        {
            var pc = passaro.GetComponent<Patrulha>();
            if (pc != null) pc.enabled = true;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Pulo();
        Moviment();
        Ataque();
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

            if (tempoparaPular >= 3f && tempoPulo < quantidadePulo)
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
        animator.SetBool("Pulando", rb.linearVelocityY != 0 && !estaAtaque);
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

            animator.SetBool("ParadoInimigo", rb.linearVelocityX == 0 && noChao);
            animator.SetBool("AtirandoInimigo", rb.linearVelocityX == 0 && estaAtaque);

            if (tempoCorrida >= tempoCorridaVariavel)
            {
                float direcao = estaDireita ? 1f : -1f;
                rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocityY);
                if (!estaDireita && transform.position.x <= posicaoInicial.x - posicaoMax)
                {
                    Limite.SetActive(true);
                    Virar();
                }

            }
        }
    }

    void Virar()
    {
        estaDireita = !estaDireita;
        transform.eulerAngles = estaDireita ? Vector3.up * 180 : Vector3.zero;
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

        if (coll.CompareTag("PlayerOriginal"))
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

    void Ataque()
    {
        if (tempoCorrida >= tempoCorridaVariavel)
        {

            if (criandoBala < 10)
            {
                estaAtaque = true;
                tempoBala += Time.deltaTime * 1.5f;

                if (tempoBala >= intervalo)
                {
                    Instantiate(prefabBala, spawnBala.position, transform.rotation);
                    criandoBala++;
                    tempoBala = 0;
                }
            }
            criandoBala = 0;
            estaAtaque = false;
        }


        if (tempoparaPular >= 5f && !estaDireita)
        {
            if (criandoBala < 10)
            {
                estaAtaque = true;
                tempoBala += Time.deltaTime * 1.5f;

                if (tempoBala >= intervalo)
                {
                    Instantiate(prefabBala, spawnBala.position, Quaternion.identity);
                    criandoBala++;
                    tempoBala = 0;
                }
            }
            criandoBala = 0;
            estaAtaque = false;
        }
    }
    void FaseTwo()
    {
        if (HudInimigo.vidaPerdidas <= 150 && !faseTwo && !estaAndando && !estaPulando)
        {
            print("d");
            faseTwo = true;
          
        }

        if (faseTwo)
        {
            // Contador para controlar intervalo exato dos pulo

            tempoParaProximoPulo -= Time.deltaTime;

            if (tempoParaProximoPulo <= 0f)
            {
                rb.AddForce(Vector2.up * forcaPuloLoop, ForceMode2D.Impulse);
                tempoParaProximoPulo = intervaloPulo; // reseta o tempo de pulo
            }

            if (criandoBala < 7)
            {
                estaAtaque = true;
                tempoBala += Time.deltaTime * 0.5f;

                if (tempoBala >= intervaloFaseDois)
                {
                    Instantiate(prefabBala, spawnBala.position, Quaternion.identity);
                    criandoBala++;
                    tempoBala = 0;
                }
            }
            criandoBala = 0;
            estaAtaque = false;


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


