using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretBehaviours : MonoBehaviour
{
    
    public enum Status
    {
        NORMAL,
        PATROL,
        ATTACK
    }

    [SerializeField] private bool isStatic = false;
    [SerializeField] private Status m_status = Status.NORMAL;

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
    [SerializeField] private List<Transform> m_ListOfPosition;
    [SerializeField] private List<Quaternion> m_ListOfRotation;
    [SerializeField] private float m_MovementSpeed;
    public int index = 0;
    [SerializeField] private float stopRotationDuration = 1f;

    private FieldOfView m_TurretFOV;

    [Space]
    [Header("Effect")]
    [SerializeField] private ParticleSystem m_ShootEffectRight;
    [SerializeField] private ParticleSystem m_ShootEffectLeft;

    private GameObject target;
    private Vector3 targetPosition;
    private Quaternion TargetRotation = Quaternion.identity;

    [Space]
    [Header("Flag")]
    [SerializeField] private bool isCinema = false;
    [SerializeField] private bool canTurn = true;
    [SerializeField] private bool detection = false;

    public bool IsCinema { get => isCinema; set => isCinema = value; }

    // Start is called before the first frame update


    void Start()
    {
        
        m_ListOfRotation.Clear();
        m_ListOfPosition.Clear();

        m_TurretFOV = transform.GetChild(0).GetComponent<FieldOfView>();
        m_Animator = transform.GetChild(2).GetComponent<Animator>();

        m_status = Status.NORMAL;
        m_TurretFOV.ChangeColor(m_status);

        if (!isStatic)
        {
            for (int i = 0; i < m_PositionParent.transform.childCount; i++)
            {
                m_ListOfPosition.Add(m_PositionParent.transform.GetChild(i));
                m_ListOfRotation.Add(m_PositionParent.transform.GetChild(i).rotation);
            }
        }
        m_PositionParent.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!canTurn && m_TurretFOV.VisibleGameobject.Count <= 0)
            canTurn = true;*/
        if (!isCinema)
        {
            TurretMovement();

            CanShootOnObject();
        }
            

        /**/
    }

    /*bool WaitBeforefollow()
    {
        stopDurationBeforeFollow -= Time.deltaTime;
        return stopDurationBeforeFollow <= 0.0f ? true : WaitBeforefollow();
    }*/

    void TurretMovement()
    {
        if (canTurn && !isStatic)
        {
            CalculeRotation(m_ListOfPosition[index].position);

            if (transform.position == m_ListOfPosition[index].position)
            {
                TargetRotation = Quaternion.identity;
                StartCoroutine(WaitUntilRotationDone());
            }
            TurretMovement(index);
        }
    }

    void CanShootOnObject()
    {
        if (m_TurretFOV.VisibleGameobject.Count > 0)
        {
            if(detection == false)
            {
                detection = true;
                GameManager.PlaySoundOneShotStatic(Sound.m_SoundName.TurretDetection);
            }
                
            m_Animator.SetBool("stop", true);
            canTurn = false;
            FindTarget();
        }

        else if (target != null && !isStatic)
        {
            m_Animator.SetBool("stop", false);
            m_status = Status.PATROL;
            m_TurretFOV.ChangeColor(m_status);
            detection = false;
            GoToplayerLastPosition();
        }
        else
        {
            detection = false;
            m_status = Status.NORMAL;
            m_TurretFOV.ChangeColor(m_status);
        }
    }

    void GoToplayerLastPosition()
    {
        CalculeRotation(targetPosition);
        Debug.Log(targetPosition);
        MoveToPosition(targetPosition);
        if (targetPosition == transform.position)
        {
            Debug.Log("test");
            target = null;
            canTurn = true;
            m_status = Status.NORMAL;
            m_TurretFOV.ChangeColor(m_status);
        }
    }

    void FindTarget()
    {
        try
        {
            target = GetTarget();
            m_status = Status.ATTACK;
            m_TurretFOV.ChangeColor(m_status);
            targetPosition = new Vector3 (target.transform.position.x,transform.position.y, target.transform.position.z);
            Shoot(target);
            
        }
        catch (Exception e)
        {
            target = null;
            canTurn = true;
        }
    }

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

            int val = UnityEngine.Random.RandomRange(1, 3);

            if (!isStatic)
            {
                if (val == 1)
                {
                    m_ShootEffectLeft.Play();
                }
                else
                {
                    m_ShootEffectRight.Play();
                }
            }
            else
            {
                Debug.Log("rotation");
                CalculeRotationShoot(m_ShootEffectLeft.gameObject, target.transform.position);
                m_ShootEffectLeft.Play();
            }

            
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }

    }

    public void CalculeRotation(Vector3 position)
    {
        if(TargetRotation == Quaternion.identity)
            TargetRotation = Quaternion.LookRotation(position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, m_MovementSpeed * Time.deltaTime);
    }

    void CalculeRotationShoot(GameObject shoot,Vector3 position)
    {
        shoot.transform.LookAt(position);
        /*if (TargetRotation == Quaternion.identity)
            TargetRotation = Quaternion.LookRotation(position - shoot.transform.position);

        shoot.transform.rotation = Quaternion.Slerp(shoot.transform.rotation, TargetRotation, m_MovementSpeed * Time.deltaTime);*/
    }

    IEnumerator WaitUntilRotationDone()
    {
        canTurn = false;
        index++;
        if (index >= m_ListOfPosition.Count)
            index = 0;
        yield return new WaitForSeconds(stopRotationDuration);
        
        canTurn = true;
    }

    void MoveToPosition(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, m_MovementSpeed * Time.deltaTime);
        //transform.DOMove(position, m_MovementSpeed);
    }

    void TurretMovement(int index)
    {
        //transform.LookAt(m_ListOfPosition[index]);
        MoveToPosition(m_ListOfPosition[index].position);   
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

    #region Gizmo
    private void OnDrawGizmos()
    {
        if (m_PositionParent != null)
        {
            for (int i = 1; i < m_PositionParent.transform.childCount; i++)
            {
                Gizmos.DrawLine(m_PositionParent.transform.GetChild(i - 1).position, m_PositionParent.transform.GetChild(i).position);
            }
        }
    }
    #endregion

}
