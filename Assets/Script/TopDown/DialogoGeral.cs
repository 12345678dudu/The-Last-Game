using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoGeral : MonoBehaviour
{
    [Header("Texto")]
    public string[] dialogo;          // falas
    public int[] quemFala;            // 0 = personagem | 1 = vilão

    public int dialogueIndex;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Imagens (lado da fala)")]
    public GameObject imgPersonagem;
    public GameObject imgVilao;

    [Header("Configurações")]
    public string proximaCena = "NomeDaCena";

    private bool isTyping;
    private bool dialogueActive;

    private PersonagemTopDown player;

    void Start()
    {
        dialoguePanel.SetActive(false);
        player = FindAnyObjectByType<PersonagemTopDown>();

        StartDialogue();
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTyping)
                NextDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogueActive = true;

        if (player != null)
            player.velocidade = 0f;

        AtualizarFalante();
        StartCoroutine(TypeDialogue(dialogo[dialogueIndex]));
    }

    IEnumerator TypeDialogue(string frase)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in frase)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogo.Length)
        {
            AtualizarFalante();
            StartCoroutine(TypeDialogue(dialogo[dialogueIndex]));
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueActive = false;

            if (player != null)
                player.velocidade = 5f;

            SceneManager.LoadScene(proximaCena);
        }
    }

    void AtualizarFalante()
    {
        if (quemFala[dialogueIndex] == 0) // personagem
        {
            imgPersonagem.SetActive(true);
            imgVilao.SetActive(false);
        }
        else // vilão
        {
            imgPersonagem.SetActive(false);
            imgVilao.SetActive(true);
        }
    }
}
