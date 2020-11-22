using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JoystickCharacterControler : MonoBehaviour, IDamageable<int>
{
    #region Param
    [SerializeField] private GameManager m_GameManager;

    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Space]
    [Header ("turn")]
    [SerializeField] private float turnShmoothTime = 0.1f;
    [SerializeField] private float turnShmoothVelocity;

    [Space]
    [Header("Player Status")]
    [SerializeField] private int m_LifePoint = 3;

    [Space]
    [Header("Player Component")]
    [SerializeField] private CharacterController characterController;
    public Camera cam;
    [SerializeField] private GameObject m_Visuel;

    [Space]
    [Header("Environment Check Properties")]
    [Tooltip("Position of the gameObject who check if the player touch the ground")]
    public Transform groundCheck;

    [Tooltip("the layer of the ground")]
    public LayerMask groundMask;

    [Tooltip("how far the player can check beside his feet")]
    [SerializeField] private float groundDistance;

    [Tooltip("Boolean variable how indicate if the player touch the ground or not")]
    [SerializeField] private bool isGrounded;

    [Space]
    [Header("Movement Variable")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speedMouvement;
    [SerializeField] private float m_JumpPower = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Space]
    [Header("Canvas")]
    [SerializeField] private Joystick joystick;

    [Space]
    [Header("Spawner")]
    [SerializeField] private Transform Objectspawner;

    [Space]
    [Header("Inventory")]
    [SerializeField] private int m_AmountOfWall;
    [SerializeField] private int m_AmountOfPlatform;
    [SerializeField] private int m_AmountOfCube;

    [Space]
    [Header("Inventory GameObject")]
    [SerializeField] private GameObject m_WallGO;
    [SerializeField] private GameObject m_PlatformGO;
    [SerializeField] private GameObject m_CubeGO;


    [Space]
    [Header("Interact")]
    [SerializeField] GameObject interactGO;
    [SerializeField] Transform interactTransform;
    [SerializeField] Vector3 castGizmoCube;
    [SerializeField] float castRadius;
    [SerializeField] LayerMask interactLayer;


    [Space]
    [Header("Particule")]
    [SerializeField] ParticleSystem m_SpawnEffect;
    [SerializeField] GameObject m_DeathEffect;

    [Space]
    [Header("Audio")]
    [SerializeField] AudioSource m_AudioSource;

    [Space]
    [Header("Animation variable")]
    [Min(-360)]
    [SerializeField] int m_RotattionInZValue = -180;
    [SerializeField] float m_MoveToObjectDuration = 1f;
    [SerializeField] float m_RotationDuration = 1f;
    [SerializeField] float m_ScaleToZeroDuration = 0.7f;

    [Space]
    [Header("Flag")]
    [SerializeField] private bool isCinema = false;

    private Vector3 m_StartScale;

    #endregion

    #region FIXEUPDATE || UPDATE || LATEUPDATE || START
    void Start()
    {
        //cam = Camera.main; 
        m_StartScale = transform.localScale;
        joystick = FindObjectOfType<Joystick>();
        m_Animator = transform.GetChild(3).GetComponent<Animator>();
        characterController = FindObjectOfType<CharacterController>();
        groundCheck = transform.GetChild(0);
        m_GameManager = FindObjectOfType<GameManager>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded && !IsCinema)
            TpsMove();
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        physicsCheck();
    }
    #endregion

    AudioClip SetSound(Sound.m_SoundName name)
    {
        Sound mySound = m_GameManager.GetSound(name);

        m_AudioSource.clip = mySound.Clip;

        m_AudioSource.volume = mySound.Volume;
        m_AudioSource.pitch = mySound.Pitch;

        return m_AudioSource.clip;
    }

    #region Player Actions
    void TpsMove()
    {
        float moveVertical = joystick.Vertical;
        float moveHorizontal = joystick.Horizontal;

        Vector3 mouvement = new Vector3(moveHorizontal, 0, moveVertical).normalized;//(transform.right * moveHorizontal + transform.forward * moveVertical) * speedMouvement;

        Debug.Log(moveHorizontal + "  " + moveVertical);
        
        if (mouvement.magnitude >= 0.1f)
        {
            m_Animator.SetBool("IsWalking", true);
            //m_GameManger.PlaySound(Sound.m_SoundName.PlayerMovement);

            //m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerMovement));

            if (moveVertical >= 0.65f || moveHorizontal >= 0.65f || moveVertical <= -0.65f || moveHorizontal <= -0.65f)
            {
                Debug.Log("run");
                m_Animator.SetBool("IsRunning", true);
            }
            else
            {
                Debug.Log("Walk");
                m_Animator.SetBool("IsRunning", false);
            }
                

            Debug.Log(m_Animator.GetBool("IsRunning"));

            float targetAngle = Mathf.Atan2(mouvement.x, mouvement.z) * Mathf.Rad2Deg + cam.gameObject.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnShmoothVelocity, turnShmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speedMouvement * Time.deltaTime);
        }
        else
        {
            m_Animator.SetBool("IsWalking", false);
            m_Animator.SetBool("IsRunning", false);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            m_GameManager.IsJumping(false);
            velocity.y = Mathf.Sqrt(m_JumpPower * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
            m_Animator.SetTrigger("Jump");
            characterController.Move(velocity * Time.deltaTime);
        }
        
    }


    public void Interaction()
    {
        if (interactGO.GetComponent<IActionableObjects>() != null)
            interactGO.GetComponent<IActionableObjects>().Interaction();
        else
            Collect();
    }

    void Collect()
    {
        m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerPickUp));
        Debug.Log(interactGO.name);
        switch (interactGO.tag)
        {
            case "PlayerWall":
                Debug.Log("PlayerWall");
                m_AmountOfWall++;
                m_GameManager.PlayerPickAnObject(interactGO, m_AmountOfWall);
                break;

            case "Platform":
                Debug.Log("Platform");
                m_AmountOfPlatform++;
                m_GameManager.PlayerPickAnObject(interactGO, m_AmountOfPlatform);
                break;

            case "Cube":
                Debug.Log("Cube");
                m_AmountOfCube++;
                m_GameManager.PlayerPickAnObject(interactGO, m_AmountOfCube);
                break;

            default:
                return;
                break;
        }

        interactGO.GetComponent<PickableObject>().PickUp(transform.position);
        //Destroy(interactGO,1f);
    }
    #endregion

    #region SpawnObjects
    public void SpawnWall()
    {
        if(m_AmountOfWall > 0)
        {
            m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerCreateObject));
            m_AmountOfWall--;

            GameObject playerObject = Instantiate(m_WallGO, Objectspawner.position, transform.rotation/*Quaternion.identity*/);
            Vector3 scale = playerObject.transform.localScale;
            playerObject.transform.localScale = Vector3.zero;
            playerObject.transform.parent = Objectspawner;

            playerObject.GetComponent<BoxCollider>().isTrigger = true;
            m_GameManager.NewObjectAppear(playerObject.GetComponent<WallBehaviours>());

            m_GameManager.PlayerPickAnObject(playerObject, m_AmountOfWall);

            playerObject.transform.DOMove(Objectspawner.transform.position, m_MoveToObjectDuration);
            playerObject.transform.DORotate(new Vector3(0, m_RotattionInZValue, 0), m_RotationDuration);
            playerObject.transform.DOScale(scale, m_ScaleToZeroDuration);
        }
        
    }

    public void SpawnCube()
    {
        if(m_AmountOfCube > 0)
        {
            m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerCreateObject));
            m_AmountOfCube--;

            GameObject playerObject = Instantiate(m_CubeGO, transform.position, Quaternion.identity);
            Vector3 scale = playerObject.transform.localScale;
            playerObject.transform.localScale = Vector3.zero;
            playerObject.transform.parent = Objectspawner;
            

            playerObject.GetComponent<BoxCollider>().isTrigger = true;
            m_GameManager.NewObjectAppear(playerObject.GetComponent<BoxBehaviours>());

            m_GameManager.PlayerPickAnObject(playerObject, m_AmountOfCube);

            playerObject.transform.DOMove(Objectspawner.transform.position, m_MoveToObjectDuration);
            playerObject.transform.DORotate(new Vector3(0, m_RotattionInZValue, 0), m_RotationDuration);
            playerObject.transform.DOScale(scale, m_ScaleToZeroDuration);
        }
        
    }

    public void SpawnPlatform()
    {
        if (m_AmountOfPlatform > 0)
        {
            m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerCreateObject));
            m_AmountOfPlatform--;

            GameObject playerObject = Instantiate(m_PlatformGO, Objectspawner.position, Quaternion.identity);
            Vector3 scale = playerObject.transform.localScale;
            playerObject.transform.localScale = Vector3.zero;
            playerObject.transform.parent = Objectspawner;

            playerObject.GetComponent<BoxCollider>().isTrigger = true;

            m_GameManager.NewObjectAppear(playerObject.GetComponent<PlatformBehaviours>());

            m_GameManager.PlayerPickAnObject(playerObject, m_AmountOfPlatform);

            playerObject.transform.DOMove(Objectspawner.transform.position, m_MoveToObjectDuration);
            playerObject.transform.DORotate(new Vector3(0, m_RotattionInZValue, 0), m_RotationDuration);
            playerObject.transform.DOScale(scale, m_ScaleToZeroDuration);
        }

    }

    public void SpawnObject()
    {
        transform.GetChild(1).GetChild(0).GetComponent<IPickable>().ValidationSpawn();
    }
    #endregion

    #region Physics
    private void physicsCheck()
    {
       Collider[] listOfInteract = Physics.OverlapSphere(interactTransform.position, castRadius, interactLayer);
        if(listOfInteract.Length != 0)
        {
            interactGO = listOfInteract[0].gameObject;
            m_GameManager.CanInteract(true);
        }
        else
        {
            interactGO = null;
            m_GameManager.CanInteract(false);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
            m_GameManager.IsJumping(isGrounded);
        }
    }
    #endregion

    #region Interface
    public void Damage(int damageTake)
    {
        m_LifePoint -= damageTake;
        if (m_LifePoint <= 0)
            Kill();
    }

    public void Kill()
    {
        m_AudioSource.PlayOneShot(SetSound(Sound.m_SoundName.PlayerDied));
        Visuel.SetActive(false);
        Instantiate(m_DeathEffect,new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        StartCoroutine(m_GameManager.playerGetKilled(this.gameObject));


        //Destroy(gameObject);
    }
    #endregion

    #region GETTER && SETTER

    public void GetDammage(int damage)
    {
        m_LifePoint -= damage;

        if (m_LifePoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    public GameObject Visuel { get => m_Visuel; set => m_Visuel = value; }
    public ParticleSystem SpawnEffect { get => m_SpawnEffect; set => m_SpawnEffect = value; }
    public GameObject DeathEffect { get => m_DeathEffect; set => m_DeathEffect = value; }
    public Vector3 StartScale { get => m_StartScale; set => m_StartScale = value; }
    public int LifePoint { get => m_LifePoint; set => m_LifePoint = value; }
    public bool IsCinema { get => isCinema; set => isCinema = value; }
    public Animator Animator { get => m_Animator; set => m_Animator = value; }

    #endregion

    #region Gizmo
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(interactTransform.position, castRadius);
    }
    #endregion
}
