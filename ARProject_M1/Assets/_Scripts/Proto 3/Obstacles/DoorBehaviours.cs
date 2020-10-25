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


    // Start is called before the first frame update
    void Start()
    {
        closePos = transform.GetChild(0).position;
        openPos = transform.GetChild(1).position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(m_State == doorState.WAIT)
        {
            m_State = transform.position == openPos || transform.position == closePos ? doorState.CLOSE : doorState.WAIT;
        }*/
    }

    public void OpenDoor()
    {
        m_State = doorState.OPEN;
        transform.DOMove(openPos, movementSpeed, false);
    }

    public void CloseDoor ()
    {
        m_State = doorState.CLOSE;
        transform.DOMove(closePos, movementSpeed, false);
    }
}
