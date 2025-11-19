
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
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

        barraVida.fillAmount= vidaPerdida/vidaTotal;
    }
}
