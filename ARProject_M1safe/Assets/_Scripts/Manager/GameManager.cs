using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager canvas;
    [SerializeField] private LevelManager m_levelManager;
    [SerializeField] private DialogueManager m_DialogueManager;
    [SerializeField] private SoundManager m_SoundManager;
    [SerializeField] private Transform playerCheckPoint;
    [SerializeField] private GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<CanvasManager>();
        m_levelManager = GetComponent<LevelManager>();
        m_DialogueManager = FindObjectOfType<DialogueManager>();
        m_SoundManager = FindObjectOfType<SoundManager>();
    }

    public void NewDialogue(Dialogues dialogue)
    {
        m_DialogueManager.Conversation(dialogue);
    }

    public void playerGetKilled() {

        //Instantiate(playerGO, playerCheckPoint.position, Quaternion.identity);
        canvas.ShowGameOverScreen();
    }

    public void IsJumping( bool isHeGrounded)
    {
        canvas.PlayerJump(isHeGrounded);
    }

    public void PlayerPickAnObject(GameObject pickableObject, int amount)
    {
        Debug.Log(amount);
        canvas.IncreaseAmoutOfObject(pickableObject, amount);
    }

    public void NewObjectAppear(IPickable spawningObject)
    {
        canvas.SetNewObject(spawningObject);
    }

    public void wallSetUp()
    {
        canvas.DeleteNewWall();
    }

    public void CanInteract(bool can)
    {
        canvas.UseButton(can);
    }

    public static void PlaySoundStatic(Sound.m_SoundName name)
    {
        FindObjectOfType<SoundManager>().PlaySound(name);
    }

    public void PlaySound(Sound.m_SoundName name)
    {
        m_SoundManager.PlaySound(name);
    }

}
