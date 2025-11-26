using UnityEngine;

public class ArvoreTransparente : MonoBehaviour
{
    [Range(0f, 1f)] public float transparencia = 0.3f; // Valor final da transparência
    public float velocidade = 2f; // Velocidade da transição
    public string tagDoPlayer = "Player";

    private SpriteRenderer spriteRenderer;
    private Color corOriginal;
    private Color corAlvo;
    private bool jogadorPerto = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
        corAlvo = corOriginal;
    }

    void Update()
    {
        Color corAtual = spriteRenderer.color;
        spriteRenderer.color = Color.Lerp(corAtual, corAlvo, Time.deltaTime * velocidade);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagDoPlayer))
        {
            jogadorPerto = true;
            corAlvo = new Color(corOriginal.r, corOriginal.g, corOriginal.b, transparencia);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagDoPlayer))
        {
            jogadorPerto = false;
            corAlvo = corOriginal;
        }
    }
}
