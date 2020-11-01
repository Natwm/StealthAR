using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JoystickCharacterControler : MonoBehaviour
{
    [SerializeField] private GameManager m_GameManger;
    [Space]
    [SerializeField] private float turnShmoothTime = 0.1f;
    [SerializeField] private float turnShmoothVelocity;

    [Space]
    [Header("Player Status")]
    [SerializeField] private int m_LifePoint = 3;

    [Space]
    [Header("Player Component")]
    [SerializeField] private CharacterController characterController;
    public Camera cam;

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
    [Header("TEST")]
    [SerializeField] private GameObject Wall;
    [SerializeField] private Transform Wallspawner;

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



    //Sequence m_CollectSequence = DOTween.Sequence();

    void Start()
    {
        cam = Camera.main;
        //joystick = FindObjectOfType<Joystick>();
        characterController = FindObjectOfType<CharacterController>();
        groundCheck = transform.GetChild(0);
        m_GameManger = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_LifePoint <= 0)
        {
            m_GameManger.playerGetKilled();
            Destroy(gameObject);
        }

        //if(isGrounded)
            TpsMove();

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnCube(); 
        }

    }

    private void FixedUpdate()
    {
        physicsCheck();

    }

    #region Player Actions
    void TpsMove()
    {
        float moveVertical = joystick.Vertical;
        float moveHorizontal = joystick.Horizontal;

        Vector3 mouvement = new Vector3(moveHorizontal, 0, moveVertical).normalized;//(transform.right * moveHorizontal + transform.forward * moveVertical) * speedMouvement;

        if (mouvement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(mouvement.x, mouvement.z) * Mathf.Rad2Deg + cam.gameObject.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnShmoothVelocity, turnShmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speedMouvement * Time.deltaTime);
        }
        
        

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            m_GameManger.IsJumping(false);
            velocity.y = Mathf.Sqrt(m_JumpPower * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
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
        Debug.Log(interactGO.name);
        switch (interactGO.tag)
        {
            case "PlayerWall":
                Debug.Log("PlayerWall");
                m_AmountOfWall++;
                m_GameManger.PlayerPickAnObject(interactGO, m_AmountOfWall);
                break;

            case "Platform":
                Debug.Log("Platform");
                m_AmountOfPlatform++;
                m_GameManger.PlayerPickAnObject(interactGO, m_AmountOfPlatform);
                break;

            case "Cube":
                Debug.Log("Cube");
                m_AmountOfCube++;
                m_GameManger.PlayerPickAnObject(interactGO, m_AmountOfCube);
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
            m_AmountOfWall--;

            GameObject playerObject = Instantiate(m_WallGO, Wallspawner.position, transform.rotation/*Quaternion.identity*/);
            playerObject.transform.parent = Wallspawner;
            playerObject.GetComponent<BoxCollider>().isTrigger = true;
            m_GameManger.NewObjectAppear(playerObject.GetComponent<WallBehaviours>());

            m_GameManger.PlayerPickAnObject(playerObject, m_AmountOfWall);
        }
        
    }

    public void SpawnCube()
    {
        if(m_AmountOfCube > 0)
        {
            m_AmountOfCube--;

            GameObject playerObject = Instantiate(m_CubeGO, Wallspawner.position, Quaternion.identity);
            playerObject.transform.parent = Wallspawner;
            playerObject.GetComponent<BoxCollider>().isTrigger = true;
            m_GameManger.NewObjectAppear(playerObject.GetComponent<BoxBehaviours>());

            m_GameManger.PlayerPickAnObject(playerObject, m_AmountOfWall);
        }
        
    }

    public void SpawnPlatform()
    {
        if (m_AmountOfPlatform > 0)
        {
            m_AmountOfPlatform--;

            GameObject playerObject = Instantiate(m_PlatformGO, Wallspawner.position, Quaternion.identity);
            playerObject.transform.parent = Wallspawner;
            playerObject.GetComponent<BoxCollider>().isTrigger = true;
            m_GameManger.NewObjectAppear(playerObject.GetComponent<PlatformBehaviours>());

            m_GameManger.PlayerPickAnObject(playerObject, m_AmountOfWall);
        }

    }

    public void SpawnObject()
    {
        transform.GetChild(1).GetChild(0).GetComponent<IPickable<int>>().ValidationSpawn();
    }
    #endregion

    

    #region Physics
    private void physicsCheck()
    {
       Collider[] listOfInteract = Physics.OverlapSphere(interactTransform.position, castRadius, interactLayer);
        if(listOfInteract.Length != 0)
        {
            interactGO = listOfInteract[0].gameObject;
            m_GameManger.CanInteract(true);
        }
        else
        {
            interactGO = null;
            m_GameManger.CanInteract(false);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
            m_GameManger.IsJumping(isGrounded);
        }
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
    #endregion

    #region Gizmo
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(interactTransform.position, castRadius);
    }
    #endregion
}
