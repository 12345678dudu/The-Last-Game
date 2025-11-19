using UnityEngine;

public class Paralax : MonoBehaviour
{
    [Header("Configurações")]
    public float intensidade = 0.5f; // controla quanto o fundo se move
    public NuzzpikKershek jogador;   // arraste o jogador aqui no Inspector

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (jogador == null) return;

        // velocidade horizontal real do jogador (no Rigidbody)
        float velX = jogador.rb.linearVelocity.x;

        // só faz o parallax se o jogador estiver realmente se movendo
        if (Mathf.Abs(velX) >= 7f)
        {
            // proporcional à velocidade máxima
            float velocidadeRelativa = velX / jogador.maxVelocidade;

            // move o fundo no sentido oposto ao movimento
            offset.x += velocidadeRelativa * intensidade * Time.deltaTime;

            rend.material.mainTextureOffset = offset;
        }
    }
}

