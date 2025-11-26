using UnityEngine;

public class Porta : MonoBehaviour
{
    public int comparacaoCahve;
    public bool clicou;
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
                Destroy(gameObject);
            }
        }
    }
    
}
