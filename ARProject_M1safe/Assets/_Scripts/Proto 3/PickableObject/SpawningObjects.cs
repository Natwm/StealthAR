using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    [SerializeField] protected GameManager m_GameManager;

    [Tooltip("Life point of this wall. It'll lose 1 point each time it gets touch by a turret projectile")]
    [SerializeField] protected int m_LifePoint = 5;

    [Space]
    [Header("Movement Variables")]
    [Tooltip("Movement Speed of this wall")]
    [SerializeField] protected float speed = 1f;

    [Tooltip("Rotation Angle of this wall")]
    [Range(1, 180)]
    [SerializeField] protected int angle = 1;

    [Space]
    [Header("Verification Variable")]
    [SerializeField] protected bool canPlant = true;

    [Space]
    [Header("Feedbacks materials")]
    [SerializeField] protected Material cantPlantMat;
    [SerializeField] protected Material canPlantMat;


    private void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //Material myMat = canPlant ? GetComponent<Renderer>().material = canPlantMat : GetComponent<Renderer>().material = cantPlantMat;
    }
}