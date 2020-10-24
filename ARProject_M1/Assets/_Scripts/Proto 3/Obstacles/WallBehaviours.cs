using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBehaviours : MonoBehaviour
{
    [SerializeField] private GameManager m_GameManager;

    [Tooltip("Life point of this wall. It'll lose 1 point each time it gets touch by a turret projectile")]
    [SerializeField] private int m_LifePoint = 5;

    [Space]
    [Header("Movement Variables")]
    [Tooltip("Movement Speed of this wall")]
    [SerializeField] private float speed = 1f;

    [Tooltip("Rotation Angle of this wall")]
    [Range(1,180)]
    [SerializeField] private int angle = 1;

    [Space]
    [Header("Verification Variable")]
    [SerializeField] private bool canPlant = true;

    [Space]
    [Header("Feedbacks materials")]
    [SerializeField] private Material cantPlantMat;
    [SerializeField] private Material canPlantMat;


    private void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        Material myMat = canPlant? GetComponent<Renderer>().material = canPlantMat : GetComponent<Renderer>().material = cantPlantMat;
    }

    public void GetDammage(int damage)
    {
        m_LifePoint -= damage;

        if (m_LifePoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    #region MoveWall
    public void MoveForward()
    {
        Debug.Log("MoveForward");
        transform.Translate(Vector3.forward * speed);
    }
    public void MoveBackward()
    {
        Debug.Log("MoveBackward");
        transform.Translate(Vector3.back * speed );
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
    public void ValidationSpawnWall()
    {
        //change les components qui faut
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

    #region TRIGGER && COLLISION
    private void OnTriggerEnter(Collider other)
    {
        canPlant = false;
        GetComponent<Renderer>().material = canPlantMat;
    }

    private void OnTriggerExit(Collider other)
    {
        canPlant = true;
        GetComponent<Renderer>().material = canPlantMat;
    }
    #endregion
}
