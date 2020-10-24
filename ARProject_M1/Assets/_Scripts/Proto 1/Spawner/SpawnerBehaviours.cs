using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviours : MonoBehaviour
{
    [SerializeField] private Transform spawnerTransform ;
    [SerializeField] private GameObject bulletGO;


    // Start is called before the first frame update
    void Start()
    {
        spawnerTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        Instantiate(bulletGO, spawnerTransform.position, Quaternion.identity);
        Debug.Log("point");
    }

    
}
