using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header ("Indicator")]
    [SerializeField] GameObject IndicatorGO;
    [SerializeField] float IndicatorGOTime = 2f;

    [Space]
    [Header ("List of Spawner")]
    [SerializeField] private SpawnerBehaviours[] m_ListOfSpawner ;


    [Space]
    [Header("variation Spawner")]
    [SerializeField] private float timeBtwSpawn = 2;
    [SerializeField] private float startTimeBtwSpawn = 8;
    [SerializeField] private float startTimer = -1;
    [SerializeField] private float minTimeBtwSpawn = 1.5f;
    [SerializeField] private float deacreseStartTimeBtwSpawn = 0.2f;

    [Space]
    [Header ("Animation Curves")]
    [SerializeField] private AnimationCurve difficultyCurveSpawner;
    [SerializeField] private AnimationCurve difficultyCurveBullets;

    [Space]
    [Header("Animation Curves Values")]
    [SerializeField] private int nbSpawner;
    [SerializeField] private int nbBullets;

    // Start is called before the first frame update
    void Start()
    {
        m_ListOfSpawner = FindObjectsOfType<SpawnerBehaviours>();
        Debug.Log("Spawner Manager have " + m_ListOfSpawner.Length + " spawner");
    }

    // Update is called once per frame
    void Update()
    {

        
        if(FindObjectOfType<ARPlayerBehaviours>() && FindObjectOfType<ARPlayerBehaviours>().gameObject.GetComponent<BoxCollider>().enabled)
        {
            
            //Debug.Log(" there is a player : " + FindObjectOfType<ARPlayerBehaviours>() + " is enabled  :" + FindObjectOfType<ARPlayerBehaviours>().gameObject.GetComponent<BoxCollider>().enabled);
            if (startTimer < 60)
            {
                nbSpawner = (int)difficultyCurveSpawner.Evaluate(startTimer);
                nbBullets = (int)difficultyCurveBullets.Evaluate(startTimer);
                startTimer += Time.deltaTime;
            }

            if (timeBtwSpawn <= 0 )
            {
                if(startTimeBtwSpawn > minTimeBtwSpawn)
                    startTimeBtwSpawn -= deacreseStartTimeBtwSpawn;

                timeBtwSpawn = startTimeBtwSpawn;

                for (int i = 0; i < nbBullets; i++)
                {
                    spawnObjectAtRandomSpawner();
                }
                
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
            //Debug.Log("time between spawn = " + timeBtwSpawn);
        }
        
        
    }

    void spawnObjectAtRandomSpawner()
    {
        int spawnerIndex = Random.Range(0, nbSpawner);
        m_ListOfSpawner[spawnerIndex].SpawnObject();
    }

    public void IndicateDirection(Vector3 pos)
    {
        Debug.LogWarning("Création indicateur");
        GameObject temp = Instantiate(IndicatorGO, pos, Quaternion.identity);
        Destroy(temp, IndicatorGOTime);
    }

    public void UpdateSpawnerPosition()
    {
        foreach (SpawnerBehaviours item in m_ListOfSpawner)
        {
            item.gameObject.transform.position.Set(item.gameObject.transform.position.x, FindObjectOfType<ARPlayerBehaviours>().transform.position.y, item.gameObject.transform.position.z);
        }
    }
}
