using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviours : MonoBehaviour
{
    [Header("Shooting GameObject")]
    [SerializeField] private GameObject bulletGO;
    [SerializeField] private Transform canonPosition;

    [Space]
    [Header("Shooting Time")]
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] private float startTimeBtwSpawn;

    [Space]
    [Header("Projectile Param")]
    [SerializeField] private float projectileSpeed;

    [SerializeField] private GameObject m_PositionParent;

    [Space]
    [Header("Movement Param")]
    [SerializeField] private List<Vector3> m_ListOfPosition;
    [SerializeField] private List<Quaternion> m_ListOfRotation;
    [SerializeField] private float m_MovementSpeed;
    public int index = 0;
    [SerializeField] private float stopRotationDuration = 1f;

    private FieldOfView m_TurretFOV;

    public bool canTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        m_ListOfRotation.Clear();
        m_ListOfPosition.Clear();

        m_TurretFOV = transform.GetChild(0).transform.GetChild(0).GetComponent<FieldOfView>();
        for (int i = 0; i < m_PositionParent.transform.childCount; i++)
        {
            m_ListOfPosition.Add(m_PositionParent.transform.GetChild(i).position);
            m_ListOfRotation.Add(m_PositionParent.transform.GetChild(i).rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canTurn)
        {
            if (transform.position == m_ListOfPosition[index])
            {
                TurretRotation(index);
            }
            TurretMovement(index);
        }
        

        
    }

    void Shoot()
    {
        if (m_TurretFOV.VisibleGameobject.Count > 0)
        {
            GameObject target = GetTarget();

            if (timeBtwSpawn <= 0)
            {
                GameObject bullet = Instantiate(bulletGO, canonPosition.position, Quaternion.identity);
                bullet.GetComponent<BulletBehaviours>().Projection(target, projectileSpeed);
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }

    IEnumerator WaitUntilRotationDone()
    {
        canTurn = false;
        yield return new WaitForSeconds(stopRotationDuration);
        index++;
        if (index >= m_ListOfPosition.Count)
            index = 0;
            
        canTurn = true;
    }

    void TurretMovement(int index)
    {
        transform.position = Vector3.MoveTowards(transform.position, m_ListOfPosition[index], m_MovementSpeed * Time.deltaTime);
    }
    void TurretRotation(int index)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, m_ListOfRotation[index], m_MovementSpeed * Time.deltaTime);
        if(transform.rotation == m_ListOfRotation[index])
            StartCoroutine(WaitUntilRotationDone());
    }

    GameObject GetTarget()
    {
        GameObject thereIsAPlayer = null;
        foreach (var item in m_TurretFOV.VisibleGameobject)
        {
            if (item.CompareTag("Player"))
            {
                thereIsAPlayer = item;
                break;
            }
            else
            {
                thereIsAPlayer = item;
            }
        }
        return thereIsAPlayer;
    }

}
