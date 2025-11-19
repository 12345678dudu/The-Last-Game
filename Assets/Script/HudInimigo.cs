using UnityEngine;
using UnityEngine.UI;
public class HudInimigo : MonoBehaviour
{
    
    public  float vidaTotal;
    public static float vidaPerdidas ;
    public Image barraVida;

    void Awake()
    {
        vidaPerdidas = vidaTotal;
    }
    void Update()
    {
        
        barraVida.fillAmount= vidaPerdidas/vidaTotal;
    }
}
