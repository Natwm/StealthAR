using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum State
    {
        Moving,
        Stop
    }

    [SerializeField] private State m_MovingState = State.Stop;

    [Space]
    [Header("Movement Param")]
    [SerializeField] private List<Vector3> m_ListOfPosition;
    [SerializeField] private float m_MovementSpeed;
    public int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            m_ListOfPosition.Add(transform.GetChild(0).GetChild(i).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MovingState == State.Moving)
        {
            if (transform.position == m_ListOfPosition[index])
            {
                index++;
                if (index >= m_ListOfPosition.Count)
                    index = 0;

            }
            Debug.Log(m_ListOfPosition.Count);
            transform.position = Vector3.MoveTowards(transform.position, m_ListOfPosition[index], m_MovementSpeed * Time.deltaTime);
        }
    }

    #region GETTER && SETTER
    public State MovingState { get => m_MovingState; set => m_MovingState = value; }
    #endregion
}
