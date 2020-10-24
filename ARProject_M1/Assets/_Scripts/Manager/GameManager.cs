using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager canvas;
    [SerializeField] private Transform playerCheckPoint;
    [SerializeField] private GameObject playerGO;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void playerGetKilled() {
        Instantiate(playerGO, playerCheckPoint.position, Quaternion.identity);
    }

    public void IsJumping( bool isHeGrounded)
    {
        canvas.PlayerJump(isHeGrounded);
    }

    public void PlayerPickAnObject(GameObject pickableObject, int amount)
    {
        Debug.Log(amount);
        canvas.IncreaseAmoutOfObject(pickableObject, amount);
    }

    public void NewWallAppear(GameObject wall)
    {
        canvas.SetNewWall(wall);
    }

    public void wallSetUp()
    {
        canvas.DeleteNewWall();
    }

    public void CanInteract(bool can)
    {
        canvas.UseButton(can);
    }

}
