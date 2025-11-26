using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    [Header("Falas Normais")]
    public string[] dialogoNormal;

    [Header("Falas Depois da Plataforma")]
    public string[] dialogoPosPlataforma;

    [Header("Sistema Atual")]
    public string[] dialogo;
    public int dialogueindex;
    public GameObject dialoguepanel;
    public TextMeshProUGUI dialoguetext;
    public Image image;
    public Sprite spriteNPC;

    public  bool ready;
    public  bool start=false;

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private PersonagemTopDown player;

    void Start()
    {
        dialoguepanel.SetActive(false);
        player = FindAnyObjectByType<PersonagemTopDown>();

        // 👉 ESCOLHE QUAL FALA VAI USAR 👈
        bool ganhouPlataforma = PlayerPrefs.GetInt("GanhouPlataforma", 0) == 1;

        if (ganhouPlataforma)
        {
            dialogo = dialogoPosPlataforma;
        }
        else
        {
            dialogo = dialogoNormal;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ready)
        {
            if (!start)
            {
                player.velocidade = 0f;
                Dialogue();
            }
            else
            {
                if (isTyping)
                {
                    StopCoroutine(typingCoroutine);
                    dialoguetext.text = dialogo[dialogueindex];
                    isTyping = false;
                }
                else
                {
                    Nextdialogue();
                }
            }
        }
    }

    void Dialogue()
    {
        dialoguetext.text = "Marcos";
        image.sprite = spriteNPC;
        start = true;
        dialogueindex = 0;
        dialoguepanel.SetActive(true);
        typingCoroutine = StartCoroutine(ShowDiaLogue());
    }

    IEnumerator ShowDiaLogue()
    {
        isTyping = true;
        dialoguetext.text = "";

        foreach (char letter in dialogo[dialogueindex])
        {
            dialoguetext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    void Nextdialogue()
    {
        dialogueindex++;

        if (dialogueindex < dialogo.Length)
        {
            typingCoroutine = StartCoroutine(ShowDiaLogue());
        }
        else
        {
            dialogueindex = 0;
            dialoguepanel.SetActive(false);
            start = false;
            player.velocidade =8;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ready = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ready = false;
    }
}
