using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public Sound[] m_Sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound audio in m_Sounds)
        {
            audio.SourceSound = gameObject.AddComponent<AudioSource>();
            audio.SourceSound.clip = audio.Clip;

            audio.SourceSound.volume = audio.Volume;
            audio.SourceSound.pitch = audio.Pitch;

            audio.SourceSound.playOnAwake = false;

            audio.SourceSound.loop = audio.Loop;
            audio.SourceSound.playOnAwake = audio.PlayOnAwake;
        }
    }

    public void PlaySound(Sound.m_SoundName name )
    {
        foreach (Sound audio in m_Sounds)
        {
            if(audio.Name == name)
            {
                audio.SourceSound.Play();
                Debug.Log("Can");
            }
        }
    }
}
