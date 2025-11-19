using System.Collections;
using UnityEngine;

public class BalaAerea : MonoBehaviour
{
    private Animator animator;
    public float danos = 10f;

    private bool jaColidiu = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Destruir automaticamente após 7 segundos se não colidir com nada
        Invoke(nameof(DestruirAutomaticamente), 7f);
    }
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Chão") || collision.CompareTag("PlayerOriginal"))
    {
        if (jaColidiu) return;
        jaColidiu = true;
        animator.SetBool("Merda", true);
        StartCoroutine(DestruirDepoisDaAnimacao());
    }
}


    // Destrói depois de um tempo para a animação acontecer
    private IEnumerator DestruirDepoisDaAnimacao()
    {
        yield return new WaitForSeconds(0.5f); // tempo da animação "Merda"
        Destroy(gameObject);
    }

    private void DestruirAutomaticamente()
    {
        if (!jaColidiu) // Só destrói automaticamente se não colidiu antes
        {
            Destroy(gameObject);
        }
    }
}

