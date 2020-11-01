using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBehaviours : SpawningObjects, IPickable<int>
{

    #region MoveWall
    public void MoveForward()
    {
        Debug.Log("MoveForward");
        transform.Translate(Vector3.forward * speed);
    }
    public void MoveBackward()
    {
        Debug.Log("MoveBackward");
        transform.Translate(Vector3.back * speed);
    }
    public void RotateLeft()
    {
        Debug.Log("RotateLeft");
        transform.Rotate(Vector3.up * angle);
    }
    public void RotateRight()
    {
        Debug.Log("RotateRight");
        transform.Rotate(Vector3.down * angle);
    }
    #endregion

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

    public void Damage(int damageTake)
    {
        m_LifePoint -= damageTake;

        if (m_LifePoint <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
    #endregion

    #region TRIGGER && COLLISION
    private void OnTriggerEnter(Collider other)
    {
        canPlant = false;
        GetComponent<Renderer>().material = cantPlantMat;
    }

    private void OnTriggerExit(Collider other)
    {
        canPlant = true;
        GetComponent<Renderer>().material = canPlantMat;
    }
    #endregion
}