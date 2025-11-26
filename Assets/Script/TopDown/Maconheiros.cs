using UnityEngine;

public class Maconheiros : MonoBehaviour
{
    public int comparacaoCahve;
    public bool clicou;
    public Transform posicaoNova;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) clicou=true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.gameObject.TryGetComponent<PersonagemTopDown>(out var inventario))
        {
            if(inventario.TemChave(comparacaoCahve)&& clicou==true)
            {
                transform.position = posicaoNova.position;
            }
        }
    }
    
}
