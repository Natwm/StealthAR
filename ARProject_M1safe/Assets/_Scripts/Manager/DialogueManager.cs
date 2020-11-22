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
        StartCoroutine(DialogueEvent(dialogue));
    }

    public void StopDialogue()
    {
        EndDialogue();
    }


    public IEnumerator DialogueEvent(Dialogues dialogue)
    {
        EndDialogue();
        Debug.Log("DialogueEvent");
        while (sentences.Count != 0)
        {
            StartCoroutine(DisplayText(sentences.Dequeue(), dialogue.image));
            yield return new WaitForSeconds(5);

        }
        
    }

    IEnumerator DisplayText(string text, Sprite speakerImage)
    {
        string newText = "";
        foreach (char letter in text)
        {
            newText += letter;
            uiManager.UpdateDialogueText(newText);
            uiManager.UpdateUIImages(speakerImage);
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
