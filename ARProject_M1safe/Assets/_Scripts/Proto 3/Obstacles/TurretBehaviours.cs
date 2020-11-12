using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviours : MonoBehaviour
{

    //[SerializeField] private float stopDurationBeforeFollow = 1f;
    //[SerializeField] private float startDurationBeforFollow;
    [SerializeField] private bool isStatic = false;


    [Header("Animation")]
    [SerializeField] private Animator m_Animator;

    [Space]
    [Header("Shooting GameObject")]
    [SerializeField] private GameObject bulletGO;
    [SerializeField] private Transform canonPosition;

    [Space]
    [Header("Shooting Time")]
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] private float startTimeBtwSpawn;

    [Space]
    [Header("Projectile Param")]
    [SerializeField] private int m_Damage;
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

    private GameObject target;
    private Vector3 targetPosition;

    public bool canTurn = true;

    // Start is called before the first frame update
    void Start()
    {

        m_ListOfRotation.Clear();
        m_ListOfPosition.Clear();

        m_TurretFOV = transform.GetChild(0).GetComponent<FieldOfView>();
        m_Animator = transform.GetChild(2).GetComponent<Animator>();

        if (!isStatic)
        {
            for (int i = 0; i < m_PositionParent.transform.childCount; i++)
            {
                m_ListOfPosition.Add(m_PositionParent.transform.GetChild(i).position);
                m_ListOfRotation.Add(m_PositionParent.transform.GetChild(i).rotation);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canTurn && m_TurretFOV.VisibleGameobject.Count <= 0)
            canTurn = true;

        if (canTurn && !isStatic)
        {
            
            CalculeRotation(m_ListOfPosition[index]);

            if (transform.position == m_ListOfPosition[index])
            {
                index++;
                if (index >= m_ListOfPosition.Count)
                    index = 0;

                if(index == 0)
                    TurretRotation(m_ListOfPosition.Count);
                else
                    TurretRotation(index-1);
            }
            TurretMovement(index);
        }
        if (m_TurretFOV.VisibleGameobject.Count > 0)
        {
            m_Animator.SetBool("stop", true);
            canTurn = false;
            try
            {
                target = GetTarget();
                targetPosition = target.transform.position;
                Shoot(target);
            }
            catch(Exception e)
            {
                target = null;
                canTurn = true;
            }
            
        }
        else if (target!= null && !isStatic)
        {
            m_Animator.SetBool("stop", false);
            //WaitBeforefollow();
            CalculeRotation(targetPosition);
            MoveToPosition(targetPosition);
            if (targetPosition == transform.position)
            {
                target = null;
                canTurn = true;
            }

        }
        if(!isStatic)
            m_Animator.SetBool("IsWalking", true);
        /*if (m_TurretFOV.VisibleGameobject.Count > 0)
        {
            canTurn = false;
            target = GetTarget();
            Shoot(target);
        }
        else if (target != null)
        {
            MoveToPosition(target.transform.position);
        }*/
    }

    /*bool WaitBeforefollow()
    {
        stopDurationBeforeFollow -= Time.deltaTime;
        return stopDurationBeforeFollow <= 0.0f ? true : WaitBeforefollow();
    }*/

    void Shoot(GameObject target)
    {

        if (timeBtwSpawn <= 0)
        {
            GameManager.PlaySoundStatic(Sound.m_SoundName.TurretShoot);
            /*GameObject bullet = Instantiate(bulletGO, canonPosition.position, Quaternion.identity);
            bullet.GetComponent<BulletBehaviours>().Projection(target, projectileSpeed);*/
            Debug.Log(target.name);
            target.GetComponent<IDamageable<int>>().Damage(m_Damage);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }

    }

    void CalculeRotation(Vector3 position)
    {
        Quaternion TargetRotation = Quaternion.LookRotation(position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, m_MovementSpeed * Time.deltaTime);
    }
    IEnumerator WaitUntilRotationDone()
    {
        canTurn = false;
        yield return new WaitForSeconds(stopRotationDuration);
        /*index++;
        if (index >= m_ListOfPosition.Count)
            index = 0;*/

        canTurn = true;
    }

    void MoveToPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, m_MovementSpeed * Time.deltaTime);
    }

    void TurretMovement(int index)
    {
        //transform.LookAt(m_ListOfPosition[index]);
        MoveToPosition(m_ListOfPosition[index]);   
    }
    void TurretRotation(int index)
    {
        StartCoroutine(WaitUntilRotationDone());
    }

    GameObject GetTarget()
    {
        GameObject thereIsAPlayer = null;
        //if(m_TurretFOV.VisibleGameobject.Count > 0)
        //{
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
        //}
        return thereIsAPlayer;
    }

}
