using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviours : ActionableObjects
{
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
