using UnityEngine;

public class VitoriaAtivacaoChave : MonoBehaviour
{public GameObject chaveJardelas;
    void Update()
    {
            bool ganhouPlataforma = PlayerPrefs.GetInt("GanhouPlataforma", 0) == 1;
            if (ganhouPlataforma) chaveJardelas.SetActive(true);

    }
}
