using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private CanvasManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        uiManager = FindObjectOfType<CanvasManager>();
    }
    public void Conversation(Dialogues dialogue)
    {
        sentences.Clear();
        if (dialogue.sentences.Length != 0)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            Debug.Log("the dialogue is empty");
            return;
        }
        uiManager.UpdateDialogueName(dialogue.name);
        StartCoroutine(DialogueEvent());
    }


    public IEnumerator DialogueEvent()
    {
        Debug.Log("DialogueEvent");
        while (sentences.Count != 0)
        {
            StartCoroutine(DisplayText(sentences.Dequeue()));
            yield return new WaitForSeconds(5);

        }
        EndDialogue();
    }

    IEnumerator DisplayText(string text)
    {
        string newText = "";
        foreach (char letter in text)
        {
            newText += letter;
            uiManager.UpdateDialogueText(newText);
            yield return new WaitForSeconds(.05f);
        }
    }

    void CleanTextField()
    {
        uiManager.ClearTextField();
    }

    void EndDialogue()
    {
        Debug.Log("this is over");
        StopAllCoroutines();
        CleanTextField();
    }
}
