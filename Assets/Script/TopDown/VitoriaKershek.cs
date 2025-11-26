using UnityEngine;

public class VitoriaKershek : MonoBehaviour
{public GameObject chaveJardelas;
    void Update()
    {
            bool ganhouPlataforma = PlayerPrefs.GetInt("GanhouPlataformaKershek", 0) == 1;
            if (ganhouPlataforma) chaveJardelas.SetActive(true);
    }
}
