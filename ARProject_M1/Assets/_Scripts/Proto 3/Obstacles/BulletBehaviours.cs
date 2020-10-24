using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviours : MonoBehaviour
{

    public void Projection(GameObject direction, float speed)
    {
        GetComponent<Rigidbody>().AddForce((direction.transform.position - transform.position) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            other.gameObject.GetComponent<JoystickCharacterControler>().GetDammage(1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("PlayerWall"))
        {
            other.gameObject.GetComponent<WallBehaviours>().GetDammage(1);
            Destroy(gameObject);
        }
    }
}
