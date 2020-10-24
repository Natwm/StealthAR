using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{

    [SerializeField] private LayerMask roadMask;
    [SerializeField] private float radius;

    [SerializeField] private Color color;
    [SerializeField] private Status m_RoadStatus;

    [SerializeField] private bool canConnectPath = true;

    public enum Status
    {
        Road,
        Empty
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PhysicsCheck();
    }

    

    void PhysicsCheck()
    {
        foreach (var box in Physics.OverlapSphere(transform.position, radius, roadMask))
        {
            if (box.gameObject.transform.parent != this.gameObject.transform.parent && box.gameObject.GetComponent<RoadBehaviour>().RoadStatus == m_RoadStatus)
                CanConnectPath = true;
            else if (box.gameObject.transform.parent == this.gameObject.transform.parent || box.gameObject.GetComponent<RoadBehaviour>().RoadStatus != m_RoadStatus)
                CanConnectPath = false;
                //gameObject.transform.parent.GetComponent<Renderer>().material.color = color;
        }
    }

    #region GETTER && SETTER
    public Status RoadStatus { get => m_RoadStatus; set => m_RoadStatus = value; }
    public bool CanConnectPath { get => canConnectPath; set => canConnectPath = value; }
    #endregion
}
