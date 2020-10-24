using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoads : MonoBehaviour
{

    [SerializeField] private List<Transform> roads;
    [SerializeField] private float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Debug.Log(CanDropTile());
    }

    private bool CanDropTile()
    {
        foreach (var item in roads)
        {
            if (!item.gameObject.GetComponent<RoadBehaviour>().CanConnectPath)
                return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform item in roads)
        {
            Gizmos.DrawSphere(item.position, radius);
        }
    }
}
