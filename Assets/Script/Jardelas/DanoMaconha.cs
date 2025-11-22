using UnityEngine;

public class DanoMaconha : MonoBehaviour
{
    private Vector2 direcaoLocal; // <- direção guardada internamente
    public float velocidade = 8f;
    public float tempoVida = 3f;
    public float dano;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirecao(Vector2 dir)
    {
        direcaoLocal = dir.normalized; // <- guarda para sempre
    }

    void Start()
    {
        Destroy(gameObject, tempoVida);

        // Aplica velocidade PERMANENTE
        rb.linearVelocity = direcaoLocal * velocidade;
    }
}
