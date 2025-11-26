using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTopDown : MonoBehaviour
{



    public Button[] button;
    public GameObject pause;
    public GameObject tutorial;
    private bool onPause = false;
    void Start()
    {
        Time.timeScale = 1;
        button[0].onClick.AddListener(Voltar);
        button[1].onClick.AddListener(Tutorial);
        button[2].onClick.AddListener(Sair);
        button[3].onClick.AddListener(VoltarTutorial);
    }
    void Update()
    {
        Pause();
      
    }
    public void Voltar()
    {
        Time.timeScale = 1;
        pause.SetActive(!onPause);
    }


    void Pause()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {  
        
            onPause=true;
            pause.SetActive(onPause);
            Time.timeScale = 0;
    
        }
    }
    void Sair()
    {
        Application.Quit();
    }
    void Tutorial()
    {
        pause.SetActive(false); 
            tutorial.SetActive(true);
            Time.timeScale = 0;

    }
    void VoltarTutorial()
    {
          pause.SetActive(true); 
            tutorial.SetActive(false);
            Time.timeScale = 0;
    }

}


