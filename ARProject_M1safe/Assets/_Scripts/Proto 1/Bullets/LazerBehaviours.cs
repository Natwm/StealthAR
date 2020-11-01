using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehaviours : MonoBehaviour
{
    [SerializeField] float m_TimeBeforLaunche = 2f;
    [SerializeField] float m_LifeTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLazer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnLazer()
    {
        //play anim
        Debug.Log("Play anim lazer");
        yield return new WaitForSeconds(m_TimeBeforLaunche);
        GetComponent<BoxCollider>().enabled = true;
        Debug.Log("lazer launch");
        Destroy(gameObject, m_LifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("onTrigger enter lazer");
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy destroy");
            Destroy(other.gameObject);
        }
    }
}
