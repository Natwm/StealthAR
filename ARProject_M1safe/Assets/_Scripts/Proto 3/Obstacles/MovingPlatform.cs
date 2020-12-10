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
        GameManager.PlaySoundStatic(Sound.m_SoundName.Propulsion);
        Debug.Log(transform.GetChild(1).name);
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            m_ListOfPosition.Add(transform.GetChild(1).GetChild(i).transform.position);
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
            transform.position = Vector3.MoveTowards(transform.position, m_ListOfPosition[index], m_MovementSpeed * Time.deltaTime);
        }
    }

    #region GETTER && SETTER
    public State MovingState { get => m_MovingState; set => m_MovingState = value; }
    #endregion

    #region TRIGGER && COLLIDER

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            collision.gameObject.transform.parent = this.gameObject.transform;
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (transform.GetChild(1).gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            transform.GetChild(1).parent = null;
        }
            
    }*/
    #endregion
}
