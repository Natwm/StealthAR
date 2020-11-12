using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickableObject : MonoBehaviour
{
    [SerializeField] private bool canRespawn;

    [Header("Init")]
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 scale;
    [SerializeField] private Quaternion rotation;
    private bool isPick = false;

    [Space]
    [Header("Animation variable")]
    [Min(-360)]
    [SerializeField] int m_RotattionInZValue = -180;
    [SerializeField] float m_MoveToPlayerDuration = 1f;
    [SerializeField] float m_RotationDuration = 1f;
    [SerializeField] float m_ScaleToZeroDuration = 0.7f;

    [Space]
    [Header("Respawn Timer")]
    [SerializeField] private float timeBeforeRespawn = 1f;
    [SerializeField] private float RespawnTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        scale = transform.localScale;
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPick)
        {
            if (timeBeforeRespawn < 0f)
            {
                ActivateObject();
                timeBeforeRespawn = RespawnTime;
            }
            else
                timeBeforeRespawn -= Time.deltaTime;
            //lancer un timer
        }
    }

    public void PickUp(Vector3 playerPosition)
    {
        PlayAnimation(playerPosition);
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(1f);
        if (!canRespawn)
            Destroy(gameObject);
        isPick = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.position = position;
        transform.localScale = scale;
        transform.rotation = rotation;
    }

    private void ActivateObject()
    {
        isPick = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<BoxCollider>().isTrigger = false;
        transform.GetChild(0).gameObject.SetActive(true);
        //GetComponent<MeshRenderer>().enabled = true;
    }

    public void PlayAnimation(Vector3 playerPosition)
    {
        GetComponent<BoxCollider>().isTrigger = true;
        //m_CollectSequence.Play();
        transform.DOMove(playerPosition, m_MoveToPlayerDuration);
        transform.DORotate(new Vector3(0, m_RotattionInZValue, 0), m_RotationDuration);
        transform.DOScale(Vector3.zero, m_ScaleToZeroDuration);
        StartCoroutine(DisableObject());
    }
}