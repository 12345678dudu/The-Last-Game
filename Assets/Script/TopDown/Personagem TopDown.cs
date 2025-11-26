using UnityEngine;
using System.Collections.Generic;

public class PersonagemTopDown : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 6f;
    private bool olhandoEsquerda = true;
    private Vector2 ultimaDirecao;

    private Animator animator;
    private Rigidbody2D rb;

    [Header("Colisões")]
    public LayerMask obstaculos;

    public List<int> chaves = new List<int>();

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    
    }

    void Update()
    {
        Vector2 input = LerDirecao();
        Mover(input);
        AtualizarAnimacao(input);
    }

    // ---------------- MOVIMENTO LIVRE -----------------

    void Mover(Vector2 input)
    {
        rb.linearVelocity = input * velocidade;
    }

    // --------------- LEITURA DE DIREÇÃO -----------------

    Vector2 LerDirecao()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W)) y = 1;
        if (Input.GetKey(KeyCode.S)) y = -1;
        if (Input.GetKey(KeyCode.A)) x = -1;
        if (Input.GetKey(KeyCode.D)) x = 1;

        Vector2 dir = new Vector2(x, y).normalized;

        if (dir != Vector2.zero)
            ultimaDirecao = dir;

        return dir;
    }

    // ---------------- ANIMAÇÃO -----------------

    void AtualizarAnimacao(Vector2 input)
    {
        // reset
        animator.SetBool("AndandoFrente", false);
        animator.SetBool("AndandoBaixo", false);
        animator.SetBool("AndandoLado", false);

        if (input == Vector2.zero)
        {
            TocarIdle();
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // andando
        if (input.y > 0)
        {
            animator.SetBool("AndandoFrente", true);
        }
        else if (input.y < 0)
        {
            animator.SetBool("AndandoBaixo", true);
        }
        else
        {
            animator.SetBool("AndandoLado", true);

            if (input.x < 0 && !olhandoEsquerda) VirarLado();
            else if (input.x > 0 && olhandoEsquerda) VirarLado();
        }
    }

    void TocarIdle()
    {
        animator.ResetTrigger("ParadoFrente");
        animator.ResetTrigger("ParadoBaixo");
        animator.ResetTrigger("ParadoLado");

        if (ultimaDirecao == Vector2.up)
            animator.SetTrigger("ParadoFrente");
        else if (ultimaDirecao == Vector2.down)
            animator.SetTrigger("ParadoBaixo");
        else
            animator.SetTrigger("ParadoLado");
    }

    void VirarLado()
    {
        olhandoEsquerda = !olhandoEsquerda;
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * (olhandoEsquerda ? 1 : -1),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    // ----------------- CHAVES -----------------

    public void ColetarChave(int id)
    {
        chaves.Add(id);
    }

    public bool TemChave(int id)
    {
        return chaves.Contains(id);
    }
    
    
}

