using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviours : SpawningObjects, IPickable
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

    public void RotateObject()
    {
        transform.Rotate(new Vector3(transform.rotation.x, angle, transform.rotation.z));
    }

    #endregion

    #region TRIGGER && COLLISION
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformSpawn"))
        {
            canPlant = true;
            //GetComponent<Renderer>().material = canPlantMat;
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("blabla pas bon");
            canPlant = false;
            //GetComponent<Renderer>().material = cantPlantMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("blabla sortie");
        canPlant = false;
        //GetComponent<Renderer>().material = cantPlantMat;
    }
    #endregion
}