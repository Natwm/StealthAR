using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorBehaviours : MonoBehaviour
{
    private enum doorState
    {
        OPEN,
        WAIT,
        CLOSE
    }

    [SerializeField] private doorState m_State;
    [SerializeField] private Vector3 closePos;
    [SerializeField] private Vector3 openPos;
    [SerializeField] private float movementSpeed;

    [SerializeField] private Animator m_Animator;

    [SerializeField] private BoxCollider doorCollider;


    // Start is called before the first frame update
    void Start()
    {
        doorCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        m_Animator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        closePos = transform.GetChild(1).transform.GetChild(0).position;
        openPos = transform.GetChild(1).transform.GetChild(1).position;

        if (m_State == doorState.CLOSE)
        {
            m_Animator.SetBool("IsOpen", false);
            m_Animator.SetBool("IsClose", true);
        }
        else if (m_State == doorState.OPEN)
        {
            m_Animator.SetBool("IsOpen", true);
            m_Animator.SetBool("IsClose", false);
        }
    }

    // Update is called once per frame
    void Update()
    {}

    public void OpenDoor()
    {
        GameManager.PlaySoundStatic(Sound.m_SoundName.DoorOpen);
        m_State = doorState.OPEN;
        m_Animator.SetBool("IsOpen", true);
        m_Animator.SetBool("IsClose", false);

        doorCollider.enabled = false;
        
        //transform.DOMove(openPos, movementSpeed, false);
    }

    public void CloseDoor()
    {
        GameManager.PlaySoundStatic(Sound.m_SoundName.CloseDoor);
        m_Animator.SetBool("IsOpen", false);
        m_Animator.SetBool("IsClose", true);
        m_State = doorState.CLOSE;

        doorCollider.enabled = true;

        //transform.DOMove(closePos, movementSpeed, false);
    }
}
