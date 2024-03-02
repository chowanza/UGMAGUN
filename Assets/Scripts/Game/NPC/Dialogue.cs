using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject DialogueMark;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private bool isLineShowing; // Nueva variable
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartRandomDialogue();
            }
            else if (!isLineShowing && DialogueText.text == dialogueLines[lineIndex]) // Modificado aquí
            {
                NextDialogueLine();
            }
        }
    }

    private void StartRandomDialogue()
    {
        didDialogueStart = true;
        DialoguePanel.SetActive(true);
        DialogueMark.SetActive(false);

        // Obtener un índice de línea de diálogo aleatorio
        lineIndex = Random.Range(0, dialogueLines.Length);

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            StartCoroutine(HideDialogueAfterDelay(5f)); // Esperar 5 segundos antes de ocultar el diálogo
        }
    }

    private IEnumerator ShowLine()
    {
        isLineShowing = true; // Modificado aquí
        DialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            DialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        isLineShowing = false; // Modificado aquí
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        didDialogueStart = false;
        DialoguePanel.SetActive(false);
        DialogueMark.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StartCoroutine(HideDialogueAfterDelay(8f));
        }
    }
}
