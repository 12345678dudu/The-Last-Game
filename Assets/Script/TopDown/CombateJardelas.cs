using UnityEngine;
using UnityEngine.SceneManagement;

public class CombateJardelas : MonoBehaviour
{
    public int comparacaoCahve;
    public int comparacaoCahve1;
    public int comparacaoCahve2;
    public bool clicou;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            clicou = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
                if (other.CompareTag("Player"))
                {
                    // SALVA A POSIÇÃO
                    PlayerPrefs.SetFloat("PlayerX", other.transform.position.x);
                    PlayerPrefs.SetFloat("PlayerY", other.transform.position.y);
                    PlayerPrefs.SetFloat("PlayerZ", other.transform.position.z);

                    // FORÇA O SALVAMENTO IMEDIATO
                    PlayerPrefs.Save();  
                }
        if (other.TryGetComponent<PersonagemTopDown>(out var inventario))
        {
            if (inventario.TemChave(comparacaoCahve) &&
                inventario.TemChave(comparacaoCahve1) &&
                inventario.TemChave(comparacaoCahve2) &&
                clicou)
            {

                // AGORA SIM troca de cena
                SceneManager.LoadScene("ExplicaçãoJardelas");

                // Destrói só DEPOIS
                Destroy(gameObject);
            }
        }
    }
}
