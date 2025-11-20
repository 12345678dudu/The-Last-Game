using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuKershek : MonoBehaviour
{
    public Button[] button;
    public GameObject pause;
    public GameObject gameOver;
    public GameObject vitoria;
    public GameObject hud;
    private bool onGameOver = false;
    private bool onVitoria = false;
    private bool onHud;
    private bool onPause = false;
    public VidaKershek vidaKershek;
    void Start()
    {
        Time.timeScale = 1;
        button[0].onClick.AddListener(Voltar);
        button[1].onClick.AddListener(Recomecar);
        button[2].onClick.AddListener(Recomecar);
    }
    void Update()
    {
        ControleHud();
    }
    public void Voltar()
    {
        Time.timeScale = 1;
        pause.SetActive(onPause && !onGameOver && !onVitoria);
        onHud = !onHud;
        onPause = false;
    }
    public void Recomecar()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }

    void ControleHud()
    {
        pause.SetActive(onPause && !onGameOver && !onVitoria);
        gameOver.SetActive(!onPause && onGameOver && !onVitoria);
        vitoria.SetActive(!onPause && !onGameOver && onVitoria);
        hud.SetActive(!onPause && !onGameOver && !onVitoria);
        if (Input.GetKeyDown(KeyCode.P))
        {
            onPause = true;
            onHud = !onHud;
            Time.timeScale = 0;
        }
        if ( vidaKershek.vidaTotal<= 0)
        {
            onVitoria = true;
            onHud = false;
            Time.timeScale = 0; 
        }
        if (Vida.vidaPerdida <= 0)
        {
            StartCoroutine(MorreuAnimacao());
        }
    }
    void Morreu()
    {
        onGameOver = true;
        onHud = false;
        Time.timeScale = 0;
    }
    IEnumerator MorreuAnimacao()
    {
        yield return new WaitForSeconds(1.53f);
        Morreu();
    }
}

