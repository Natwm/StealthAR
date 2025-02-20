﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviours : ActionableObjects, IActionableObjects
{
    #region Interface
    public void Interaction()
    {
        GameManager.PlaySoundStatic(Sound.m_SoundName.PressurePlaqueAction);
        MoveWall();
        ActionPlatform();
        Debug.Log("interaction");
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Cube") )/* && (!other.GetComponent<BoxCollider>().isTrigger || other.GetComponent<CharacterController>() != null)*/)
            Interaction();
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Cube")) /*&& (!other.GetComponent<BoxCollider>().isTrigger || other.GetComponent<CharacterController>()!= null) */)
            Interaction();
    }
}
