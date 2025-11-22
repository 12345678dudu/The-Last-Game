using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Kershek : MonoBehaviour
{
    public float danos;

    [Header("Movimento")]
    public float velocidade = 6f;
    public float forcaPulo = 7f;
    public float distanciaPerseguir = 30f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Tiro")]
    public GameObject bananaPrefab;
    public Transform pontoTiro;
    public float intervaloTiro = 1.5f;
    public float forcaTiro = 8f;
    public float distanciaTiro = 12f;

    private Rigidbody2D rb;
    private Transform jogador;
    private float tempoDesdeUltimoTiro;
    private bool noChao;
    private bool encostouJogador=false;
    private Animator animator;
    public float tempoPulo;
    private float contabilizadorPulo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) jogador = playerObj.transform;
        animator= GetComponent<Animator>();
    }


    void Update()
    {
        if (jogador == null) return;

        float distancia = Vector2.Distance(transform.position, jogador.position);
        if (distancia > distanciaPerseguir) return;

        // ► PULAR NO MESMO FRAME QUE O PLAYER (SEM DELAY)
        CopiarPuloInstantaneo();
  float dirX = Mathf.Sign(jogador.position.x - transform.position.x);
        // ► Movimentação horizontal perseguindo
        if(!encostouJogador){
        rb.linearVelocity = new Vector2(dirX * velocidade, rb.linearVelocity.y);
        }

        // ► Flip
        if (dirX != 0)
            transform.localScale = new Vector3(Mathf.Sign(dirX), 1f, 1f);

        // ► Detecta chão
        noChao = Physics2D.OverlapCircle(groundCheck.position, 0.12f, groundLayer);

        // ► Sistema de tiro
        tempoDesdeUltimoTiro += Time.deltaTime;
        if (tempoDesdeUltimoTiro >= intervaloTiro && distancia <= distanciaTiro)
        {
            AtirarNaDirecaoDoJogador();
            tempoDesdeUltimoTiro = 0f;
        }
    }
 

    // ===========================================================
    //       SISTEMA DE PULO INSTANTÂNEO — SEM ATRASO
    // ===========================================================
    void CopiarPuloInstantaneo()
    {contabilizadorPulo+= Time.deltaTime;
        // Se o player pulou neste frame → inimigo pula no mesmo frame
        if (NuzzpikKershek.pulouAgora && noChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            contabilizadorPulo=0;
        }
        if(tempoPulo<=contabilizadorPulo)
        {
           rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse); 
             contabilizadorPulo=0;
        }
    }


    // ===========================================================
    //                       TIRO
    // ===========================================================
    void AtirarNaDirecaoDoJogador()
    {
        if (bananaPrefab == null || pontoTiro == null || jogador == null) return;

        Vector2 direcao = (jogador.position - pontoTiro.position).normalized;
        GameObject banana = Instantiate(bananaPrefab, pontoTiro.position, Quaternion.identity);
        Rigidbody2D rbBanana = banana.GetComponent<Rigidbody2D>();

        if (rbBanana != null)
        {
            rbBanana.linearVelocity = Vector2.zero;
            rbBanana.AddForce(direcao * forcaTiro, ForceMode2D.Impulse);
        }
    }


    // ===========================================================
    //                    GIZMOS DE DEBUG
    // ===========================================================
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, 0.12f);
        }

        if (pontoTiro != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pontoTiro.position, 0.08f);
        }
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            encostouJogador=true;
        }
    }
     void OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            encostouJogador=false;
        }
    }
  
}
