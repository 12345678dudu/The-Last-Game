using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArvoreInimigo : MonoBehaviour
{
    public float velocidade = 3f;
    public float distancia = 3f;

    [Header("Configuração")]
    public bool comecarEsquerda = false; // se marcado, começa indo para a esquerda

    private Rigidbody2D rb;
    private Vector2 posInicial;
    private int direcao = 1;
    public  float dano;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posInicial = rb.position;

        // se o inimigo deve começar indo pra esquerda
        if (comecarEsquerda)
        {
            direcao = -1;
            VirarSprite();
        }
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        Vector2 movimento = new Vector2(direcao * velocidade * Time.fixedDeltaTime, 0);
        rb.MovePosition(rb.position + movimento);

        // limite direita
        if (direcao == 1 && rb.position.x >= posInicial.x + distancia)
            TrocarDirecao();

        // limite esquerda
        if (direcao == -1 && rb.position.x <= posInicial.x - distancia)
            TrocarDirecao();
    }

    void TrocarDirecao()
    {
        direcao *= -1;
        VirarSprite();
    }

    void VirarSprite()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
