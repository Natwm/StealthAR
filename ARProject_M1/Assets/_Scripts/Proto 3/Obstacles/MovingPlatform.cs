using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [Space]
    [Header("Movement Param")]
    [SerializeField] private List<Transform> m_ListOfPosition;
    [SerializeField] private float m_MovementSpeed;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == m_ListOfPosition[index].position)
        {
            index++;
            if (index >= m_ListOfPosition.Count)
                index = 0;
            
        }
        Debug.Log(m_ListOfPosition.Count);
        transform.position = Vector3.MoveTowards(transform.position, m_ListOfPosition[index].position, m_MovementSpeed * Time.deltaTime);
        
    }
}
