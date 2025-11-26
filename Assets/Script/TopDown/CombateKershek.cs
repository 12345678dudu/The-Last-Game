using UnityEngine;
using UnityEngine.SceneManagement;

public class CombateKershek : MonoBehaviour
{
    public int comparacaoCahve;
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
               
                clicou)
            {

                // AGORA SIM troca de cena
                SceneManager.LoadScene("ExplicaçãoKershek");

                // Destrói só DEPOIS
                Destroy(gameObject);
            }
        }
    }
}

