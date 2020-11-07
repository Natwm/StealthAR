using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableObjects : MonoBehaviour
{

    [Tooltip(" all door in this list will be open when this object is use")]
    [SerializeField] protected List<GameObject> m_OpenWall;

    [Tooltip(" all door in this list will be close when this object is use")]
    [SerializeField] protected List<GameObject> m_CloseWall;

    [Tooltip(" all platform in this list will be moving when this object is use")]
    [SerializeField] protected List<GameObject> m_ActionPlatform;

    [Tooltip(" all platform in this list will stop moving when this object is use")]
    [SerializeField] protected List<GameObject> m_StopActionPlatform;

    protected void MoveWall()
    {
        OpenDoor();
        CloseDoor();
        changeListDoor();
    }

    protected void ActionPlatform()
    {
        MovePlatform();
        StopPlatform();
        changeListMovingPlatform();
    }

    protected void OpenDoor()
    {
        if (m_OpenWall.Count > 0)
        {
            Debug.Log("Open");
            foreach (GameObject wall in m_OpenWall)
            {
                wall.GetComponent<DoorBehaviours>().OpenDoor();
            }
        }
    }

    protected void CloseDoor()
    {
        if (m_CloseWall.Count > 0)
        {
            Debug.Log("m_CloseWall.Count" + m_CloseWall.Count);
            Debug.Log("Close");
            foreach (GameObject wall in m_CloseWall)
            {
                wall.GetComponent<DoorBehaviours>().CloseDoor();
            }

        }
    }

    protected void MovePlatform()
    {
        if (m_ActionPlatform.Count > 0)
        {
            Debug.Log("Move");
            foreach (GameObject platform in m_ActionPlatform)
            {
                platform.GetComponent<MovingPlatform>().MovingState = MovingPlatform.State.Moving;
            }
        }
    }

    protected void StopPlatform()
    {
        if (m_StopActionPlatform.Count > 0)
        {
            Debug.Log("Stopaction");
            foreach (GameObject platform in m_StopActionPlatform)
            {
                platform.GetComponent<MovingPlatform>().MovingState = MovingPlatform.State.Stop;
            }
        }
    }

    void changeListDoor()
    {
        List<GameObject> tempCloseList = m_CloseWall;
        List<GameObject> tempOpenList = m_OpenWall;

        m_CloseWall = tempOpenList;
        m_OpenWall = tempCloseList;
    }

    void changeListMovingPlatform()
    {
        List<GameObject> tempStopPlatform = m_StopActionPlatform;
        List<GameObject> tempGoPlatform = m_ActionPlatform;

        m_StopActionPlatform = tempGoPlatform;
        m_ActionPlatform = tempStopPlatform;
    }
}
