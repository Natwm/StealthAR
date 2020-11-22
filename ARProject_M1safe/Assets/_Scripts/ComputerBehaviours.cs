using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBehaviours : MonoBehaviour, IActionableObjects
{
    [SerializeField] private GameManager m_GameManager;

    [Header("Text")]
    [SerializeField] private Dialogues[] dialogue;

    [SerializeField] private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>(); 
    }

    #region Interface
    public void Interaction()
    {
        Debug.Log("Interact");

        if (index >= dialogue.Length)
        {
            m_GameManager.StopDialogue();
            index = 0;
        }
        else
            m_GameManager.NewDialogue(dialogue[index]);
    }
    #endregion

    public void NextDialogue()
    {
        //affiche le nouveau dialogue
        index++;
        Interaction();
    }
}
