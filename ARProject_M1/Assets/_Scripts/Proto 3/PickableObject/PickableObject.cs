using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickableObject : MonoBehaviour
{
    [Min(-360)]
    [SerializeField] int m_RotattionInZValue = 180;

    [SerializeField] float m_MoveToPlayerDuration = 1f;
    [SerializeField] float m_RotationDuration = 1f;
    [SerializeField] float m_ScaleToZeroDuration = 0.7f;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(Vector3 playerPosition)
    {
        GetComponent<BoxCollider>().isTrigger = true;
        //m_CollectSequence.Play();
        transform.DOMove(playerPosition, m_MoveToPlayerDuration);
        transform.DORotate(new Vector3(0, m_RotattionInZValue, 0), m_RotationDuration);
        transform.DOScale(Vector3.zero, m_ScaleToZeroDuration);
    }
}
