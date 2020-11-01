﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBehaviours : MonoBehaviour, IActionableObjects
{
    [SerializeField] private GameManager m_GameManager;

    [Header("Text")]
    [SerializeField] private Dialogues dialogue;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>(); 
    }

    #region Interface
    public void Interaction()
    {
        Debug.Log("Interact");
        m_GameManager.NewDialogue(dialogue);
    }
    #endregion
}
