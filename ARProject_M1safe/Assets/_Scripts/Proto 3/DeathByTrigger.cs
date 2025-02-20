﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<JoystickCharacterControler>().Kill();
        }
    }
}
