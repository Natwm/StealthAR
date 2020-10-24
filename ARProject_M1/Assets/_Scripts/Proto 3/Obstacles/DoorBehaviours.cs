using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorBehaviours : MonoBehaviour
{
    private enum doorState
    {
        OPEN,
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        transform.DOMove(openPos, movementSpeed, false);
    }

    public void CloseDoor ()
    {
        transform.DOMove(closePos, movementSpeed, false);
    }
}
