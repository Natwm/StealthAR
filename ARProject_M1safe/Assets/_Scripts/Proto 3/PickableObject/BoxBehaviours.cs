using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviours : SpawningObjects, IPickable
{
    #region Interfaces
    public void ValidationSpawn()
    {
        if (canPlant)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            transform.parent = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            m_GameManager.wallSetUp();
        }
        else
        {
            Debug.Log("Can plant the wall");
        }

    }
    #endregion

    #region TRIGGER && COLLISION
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PressurePlaque"))
        {
            canPlant = false;
            //GetComponent<Renderer>().material = cantPlantMat;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        canPlant = true;
        //GetComponent<Renderer>().material = canPlantMat;
    }
    #endregion
}