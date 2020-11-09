using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public enum m_SoundName
    {
        LoopMusique,
        PlayerMovement,
        PlayerDied,
        PlayerSpawn,
        PlayerPickUp,
        PlayerCreateObject,
        PlayerCreateObjectCant,
        TurretStatic,
        TurretMovement,
        TurretDetection,
        TurretShoot,
        DoorOpen,
        InterupteurAction,
        PressurePlaqueAction,
        SpaceShipCome,
        SpaceShip,
        DroidMouvement,
        Explosion,
        Propulsion,
        UI
    }

    [SerializeField] private m_SoundName name;

    [SerializeField] private AudioClip m_Clip;

    [Range(0f,1f)]
    [SerializeField] private float m_Volume;
    [Range(1f, 3f)]
    [SerializeField] private float m_Pitch;

    [SerializeField] private bool m_Loop;
    [SerializeField] private bool m_PlayOnAwake;

    [HideInInspector]
    private AudioSource sourceSound;

    #region Getter && Setter
    public m_SoundName Name { get => name; set => name = value; }
    public AudioClip Clip { get => m_Clip; set => m_Clip = value; }
    public float Volume { get => m_Volume; set => m_Volume = value; }
    public float Pitch { get => m_Pitch; set => m_Pitch = value; }
    public AudioSource SourceSound { get => sourceSound; set => sourceSound = value; }
    public bool Loop { get => m_Loop; set => m_Loop = value; }
    public bool PlayOnAwake { get => m_PlayOnAwake; set => m_PlayOnAwake = value; }

    #endregion
}
