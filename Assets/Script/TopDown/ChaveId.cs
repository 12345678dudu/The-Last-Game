using UnityEngine;

public class ChaveId : MonoBehaviour
{
    public int numeroChave;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<PersonagemTopDown>(out var inventario))
        {
            inventario.ColetarChave(numeroChave);
        }

    }
}
