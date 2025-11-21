using UnityEngine;

public class PersonagemTopDown : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float tamanhoTile = 1f;
    public float velocidade = 5f;

    private bool estaAndando = false;
    private bool olhandoEsquerda = true;
    private Vector3 destino;
    private Vector2 ultimaDirecao;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        destino = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if (!estaAndando)
        {
            Vector2 direcao = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direcao = Vector2.up;
                ultimaDirecao = direcao;
                
                animator.SetBool("AndandoFrente", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direcao = Vector2.down;
                ultimaDirecao = direcao;
               
                animator.SetBool("AndandoBaixo", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direcao = Vector2.left;
                ultimaDirecao = direcao;
                
                animator.SetBool("AndandoLado", true);
                if (!olhandoEsquerda) VirarLado();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direcao = Vector2.right;
                ultimaDirecao = direcao;
               
                animator.SetBool("AndandoLado", true);
                if (olhandoEsquerda) VirarLado();
            }

            if (direcao != Vector2.zero)
            {
                destino = transform.position + (Vector3)(direcao * tamanhoTile);
                StartCoroutine(MoverAte(destino));
            }
            else
            {
                TocarIdle();
            }
        }
    }

    System.Collections.IEnumerator MoverAte(Vector3 destinoFinal)
    {
        estaAndando = true;

        while (Vector3.Distance(transform.position, destinoFinal) > 0.01f)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, destinoFinal, velocidade * Time.deltaTime));
            yield return null;
        }

        rb.MovePosition(destinoFinal);
        estaAndando = false;
        TocarIdle();
    }

    void VirarLado()
    {
        olhandoEsquerda = !olhandoEsquerda;
        transform.Rotate(0, 180, 0);
    }

    void TocarIdle()
    {
        ResetarParado();

        if (ultimaDirecao == Vector2.up)
        {
            animator.SetTrigger("ParadoFrente");
            animator.SetBool("AndandoFrente", false);
        }
        else if (ultimaDirecao == Vector2.down)
        {
            animator.SetTrigger("ParadoBaixo");
            animator.SetBool("AndandoBaixo", false);
        }
        else
        {
            animator.SetTrigger("ParadoLado");
              animator.SetBool("AndandoLado", false);
        }
    }

   

    void ResetarParado()
    {
        animator.ResetTrigger("ParadoFrente");
        animator.ResetTrigger("ParadoBaixo");
        animator.ResetTrigger("ParadoLado");
    }
}
