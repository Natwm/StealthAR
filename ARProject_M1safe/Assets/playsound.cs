using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsound : MonoBehaviour
{
    void PlaySoundStep()
    {
        GameManager.PlaySoundOneShotStatic(Sound.m_SoundName.PlayerMovement);
    }
}
