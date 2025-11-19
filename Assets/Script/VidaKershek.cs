using UnityEngine;
using UnityEngine.UI;
public class VidaKershek : MonoBehaviour
{
  
    public float vidaTotal;
    public static float vidaPerdida ;
    public Image barraVida;

    void Awake()
    {
        vidaPerdida = vidaTotal;
    }
    void Update()
    {
        vidaPerdida-=Time.deltaTime*1f;
        barraVida.fillAmount= vidaPerdida/vidaTotal;
    }
}
