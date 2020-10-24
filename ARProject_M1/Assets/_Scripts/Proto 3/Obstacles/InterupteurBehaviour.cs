using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InterupteurBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] m_UpWall;
    [SerializeField] private GameObject[] m_DownWall;

    public void Interaction()
    {
        MoveWall();
    }

    void MoveWall()
    {
        MoveDown();
        MoveUp();
    }

    void MoveUp()
    {
        foreach (GameObject wall in m_UpWall)
        {
            wall.GetComponent<DoorBehaviours>().OpenDoor();
        }
    }

    void MoveDown()
    {
        foreach (GameObject wall in m_DownWall)
        {
            wall.GetComponent<DoorBehaviours>().CloseDoor();
        }
    }
}
