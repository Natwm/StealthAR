using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cinema : MonoBehaviour
{
    [SerializeField] JoystickCharacterControler player;
    //[SerializeField] GameObject turretstatic;
    [SerializeField] GameObject movingTurret;
    //[SerializeField] GameObject turretinPlay;
    [SerializeField] GameObject canvas;

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<TurretBehaviours> listTurret = new List<TurretBehaviours>();

    [SerializeField] GameObject spaceShip;
    [SerializeField] GameObject particulespaceShip;
    [SerializeField] GameObject spaceShipPos;
    [SerializeField] GameObject spaceShipParticulePos;


    [SerializeField] GameObject ExplosionParticule;

    GameObject spaceShipScene;

    bool stop = false;
    bool can = false;
    float timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<JoystickCharacterControler>();
        canvas = FindObjectOfType<CanvasManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (can)
        {
            if (!stop)
            {
                if (timer <= 0)
                {
                    spaceShipScene = Instantiate(spaceShip, spaceShipPos.transform.position, Quaternion.identity);
                    turn();
                    stop = true;
                    timer = 6f;
                }
                else
                    timer -= Time.deltaTime;
            }
            else
            {
                if (timer <= 0)
                {
                    spaceShipScene.GetComponent<shittyScale>().attaque(listTurret);
                    foreach (var item in listTurret)
                    {
                        Instantiate(ExplosionParticule, item.transform.position, Quaternion.identity);
                        Destroy(item.gameObject);
                        GameManager.PlaySoundStatic(Sound.m_SoundName.PlayerDied);
                    }
                }
                else
                    timer -= Time.deltaTime;
            }
        }

        
        
    }

    void cinema()
    {
        player.IsCinema = true;
        player.Animator.SetBool("IsWalking", false);
        player.Animator.SetBool("IsRunning", false);

        canvas.SetActive(false);
        //SpawnTurret();
        SpawnTurret();
        Instantiate(particulespaceShip, spaceShipParticulePos.transform.position, Quaternion.identity);
        can = true;
        //GetComponent<ComputerBehaviours>().Interaction();
    }

    void turn()
    {
        Vector3 pos = new Vector3 (spaceShipPos.transform.position.x, spaceShipPos.transform.position.y,0);
        foreach (var item in listTurret)
        {
            //item.transform.rotation = Quaternion.LookRotation(spaceShipPos.transform.position);//LookAt(spaceShipPos.transform);
            item.transform.DORotate(pos, 1f);
        }
    }

    void SpawnTurret()
    {
        foreach (var item in spawnPoints)
        {
            GameObject turretMove = Instantiate(movingTurret, item.position, Quaternion.identity);
            turretMove.GetComponentInChildren<Animator>().enabled = false;
            turretMove.transform.LookAt(player.transform);
            turretMove.GetComponent<TurretBehaviours>().IsCinema = true;
            listTurret.Add(turretMove.GetComponent<TurretBehaviours>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cinema();
        }
    }
}
