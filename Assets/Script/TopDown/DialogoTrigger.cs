using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogo;
    public GameObject dialoguepanel;
    public TextMeshProUGUI dialoguetext;

    private bool isTyping;
    private bool dialogueActive;
    private int dialogueindex;
    private PersonagemTopDown player;

    private void Start()
    {
        dialoguepanel.SetActive(false);
        player = FindAnyObjectByType<PersonagemTopDown>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueActive)
        {
            StartDialogue();
        }
    }

    void Update()
    {
        if (dialogueActive && Input.GetMouseButtonDown(0))
        {
            if (!isTyping)
            {
                NextDialogue();
            }
        }
    }

    void StartDialogue()
    {
        dialogueindex = 0;
        dialoguepanel.SetActive(true);
        dialogueActive = true;

        if (player != null)
            player.velocidade = 0f;

        StartCoroutine(TypeDialogue(dialogo[dialogueindex]));
    }

    IEnumerator TypeDialogue(string frase)
    {
        isTyping = true;
        dialoguetext.text = "";

        foreach (char letter in frase)
        {
            dialoguetext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    void NextDialogue()
    {
        dialogueindex++;

        if (dialogueindex < dialogo.Length)
        {
            StartCoroutine(TypeDialogue(dialogo[dialogueindex]));
        }
        else
        {
            dialoguepanel.SetActive(false);
            dialogueActive = false;

            if (player != null)
                player.velocidade = 8f;

              Destroy(gameObject);
        // Remove o trigger após o diálogo, opcional
        }
    }
}


