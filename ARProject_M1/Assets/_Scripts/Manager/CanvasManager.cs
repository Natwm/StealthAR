using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    
    [Header("Panel")]
    [SerializeField] private GameObject m_SettingsPanel;
    [SerializeField] private GameObject m_ExitPanel;
    [SerializeField] private GameObject m_GameOverPanel;

    [Header("Wall Buttons")]
    [SerializeField] private Button m_UseButton;
    [SerializeField] private Button m_ValidationButton;

    [Header("Settings Buttons")]
    [SerializeField] private Button m_ExitButton;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_JumpButton;
    [SerializeField] private Button m_ExitConfirmeButton;
    [SerializeField] private Button m_ExitUnconfirmeButton;

    [Space]
    [Header("Settings Buttons")]
    [SerializeField] private string m_InteractionText = "USE";
    [SerializeField] private string m_SpawnWallText = "SPAWN WALL";
    [SerializeField] private string m_ValidSpawnWallText = "VALID";

    [Space]
    [Header("Inventory Info")]
    [SerializeField] private TMP_Text m_AmountOfWall;
    [SerializeField] private TMP_Text m_AmountOfPlatform;
    [SerializeField] private TMP_Text m_AmountOfCube;

    // Start is called before the first frame update
    void Start()
    {
        m_AmountOfCube.text = "0";
        m_AmountOfWall.text = "0";
        m_AmountOfPlatform.text = "0";

        if (m_SettingsPanel.active)
        {
            m_SettingsPanel.SetActive(false);
        }
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        m_ValidationButton.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().SpawnWall);
    }

    public void SetNewObject(IPickable<int> spawningObject)
    {
        m_UseButton.gameObject.SetActive(false);
        m_ValidationButton.gameObject.SetActive(true);


        m_ValidationButton.onClick.RemoveAllListeners(); //= null;
        //m_ButtonPanel.SetActive(true);
        m_ValidationButton.transform.GetChild(0).GetComponent<TMP_Text>().text = m_ValidSpawnWallText;
        m_ValidationButton.onClick.AddListener(spawningObject.ValidationSpawn);
    }

    public void DeleteNewWall()
    {
        m_ValidationButton.gameObject.SetActive(false);
        m_UseButton.gameObject.SetActive(true);
        /*m_UseButton.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().Interaction);
        m_UseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = m_InteractionText;*/
    }

    public void ShowSetting()
    {
        if (m_SettingsPanel.active)
        {
            m_SettingsPanel.SetActive(false);
        }else
            m_SettingsPanel.SetActive(true);
        //pauser le jeu
    }

    public void ShowGameOverScreen()
    {
        m_GameOverPanel.SetActive(true);
    }

    public void IncreaseAmoutOfObject(GameObject pickableObject, int amount)
    {
        Debug.Log(pickableObject.name);
        switch (pickableObject.tag)
        {
            case "PlayerWall":
                Debug.Log("PlayerWall");
                m_AmountOfWall.text = amount.ToString();
                break;

            case "Platform":
                Debug.Log("Platform");
                m_AmountOfPlatform.text = amount.ToString();
                break;

            case "Cube":
                Debug.Log("Cube");
                m_AmountOfCube.text = amount.ToString();
                break;

            default:
                return;
                break;
        }
    }

    public void PlayerJump(bool isHeGrounded)
    {
        m_JumpButton.interactable = isHeGrounded;
    }

    public void ShowExitPanel()
    {
        m_ExitPanel.SetActive(true);
    }

    public void UseButton( bool active)
    {
        m_UseButton.interactable = active;
    }

    public void DontQuitGame()
    {
        m_ExitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("END OF THE GAME");
        Application.Quit();
    }
}
