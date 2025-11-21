using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlataformaVoando : MonoBehaviour
{
    public float velocidade = 3f;
    public float distancia = 3f;

    [Header("Configuração")]
    public bool comecarBaixo = false; // se marcado, começa indo para baixo

    private Rigidbody2D rb;
    private Vector2 posInicial;
    private int direcao = 1; 
    public float dano;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posInicial = rb.position;

        // se a plataforma deve começar indo pra baixo
        if (comecarBaixo)
        {
            direcao = -1;
        }
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        // movimento vertical no eixo Y
        Vector2 movimento = new Vector2(0, direcao * velocidade * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movimento);

        // limite superior
        if (direcao == 1 && rb.position.y >= posInicial.y + distancia)
            TrocarDirecao();

        // limite inferior
        if (direcao == -1 && rb.position.y <= posInicial.y - distancia)
            TrocarDirecao();
    }

    void TrocarDirecao()
    {
        direcao *= -1;
    }
}
