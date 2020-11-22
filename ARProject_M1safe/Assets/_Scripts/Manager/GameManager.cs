using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager canvas;
    [SerializeField] private LevelManager m_levelManager;
    [SerializeField] private DialogueManager m_DialogueManager;
    [SerializeField] private SoundManager m_SoundManager;
    [SerializeField] private Transform playerCheckPoint;
    [SerializeField] private GameObject playerGO;

    [Space]
    [SerializeField] private float timeToRespawn;

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

    public void StopDialogue()
    {
        m_DialogueManager.StopDialogue();
    }

    public IEnumerator playerGetKilled(GameObject player) {
        JoystickCharacterControler controller = player.GetComponent<JoystickCharacterControler>();

        player.transform.DOMove(playerCheckPoint.position, 0.2f);
        player.transform.DOScale(Vector3.zero,0.1f);
        player.GetComponent<JoystickCharacterControler>().enabled = false;

        yield return new WaitForSeconds(timeToRespawn);

        player.GetComponent<JoystickCharacterControler>().enabled = true;
        controller.Visuel.SetActive(true);
        controller.SpawnEffect.Play();
        controller.LifePoint = 1;
        player.transform.DOScale(controller.StartScale, .8f);
    }

    public void IsJumping( bool isHeGrounded)
    {
        canvas.PlayerJump(isHeGrounded);
    }

    public void PlayerPickAnObject(GameObject pickableObject, int amount)
    {
        Debug.Log(amount);
        canvas.UpdateAmoutOfObject(pickableObject, amount);
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

    public static void PlaySoundOneShotStatic(Sound.m_SoundName name)
    {
        FindObjectOfType<SoundManager>().PlaySoundOneShot(name);
    }

    

    public Sound GetSound(Sound.m_SoundName name)
    {
        return m_SoundManager.GetSound(name);
    }

    public void PlaySound(Sound.m_SoundName name)
    {
        m_SoundManager.PlaySound(name);
    }

}
