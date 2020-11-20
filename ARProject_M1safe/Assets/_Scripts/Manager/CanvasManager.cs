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
    [SerializeField] private GameObject m_DialoguePanel;
    [SerializeField] private GameObject m_ValidationSpawnPanel;

    [Header("Wall Buttons")]
    [SerializeField] private Button m_UseButton;
    [SerializeField] private Button m_ValidationButton;

    [Header("Settings Buttons")]
    [SerializeField] private Button m_ExitButton;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_JumpButton;
    [SerializeField] private Button m_TryAgainButton;
    [SerializeField] private Button m_ExitConfirmeButton;
    [SerializeField] private Button m_ExitUnconfirmeButton;
    [SerializeField] private Button m_RotateObjectButton;

    [Space]
    [Header("Settings Buttons")]
    [SerializeField] private Button m_SpawnButtonWall;
    [SerializeField] private Button m_SpawnButtonPlatform;
    [SerializeField] private Button m_SpawnButtonCube;

    [Header("Text Dialogue")]
    public TMP_Text dialogueText;
    public TMP_Text dialogueName;
    public Sprite imageSpeaker;

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

        m_JumpButton.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().Jump);
        m_UseButton.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().Interaction);
        
        m_SpawnButtonWall.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().SpawnWall);
        m_SpawnButtonPlatform.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().SpawnPlatform);
        m_SpawnButtonCube.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().SpawnCube);

        if (m_SettingsPanel.active)
        {
            m_SettingsPanel.SetActive(false);
        }

        if (m_GameOverPanel.active)
        {
            m_GameOverPanel.SetActive(false);
        }

        if (m_ValidationSpawnPanel.active)
        {
            m_ValidationSpawnPanel.SetActive(false);
        }
    }

    //void SetUpNewPlayer();

    #region Dialogues
    public void UpdateDialogueText(string sentences)
    {
        if(!m_DialoguePanel.active)
            m_DialoguePanel.SetActive(true);
        dialogueText.text = sentences;
    }

    public void UpdateDialogueName(string sentences)
    {
        if (!m_DialoguePanel.active)
            m_DialoguePanel.SetActive(true);
        dialogueName.text = sentences;
    }

    public void ClearTextField()
    {
        dialogueText.text = "";
        m_DialoguePanel.SetActive(false);
    }
    #endregion

    #region SpawnObject
    public void SpawnObject()
    {
        m_ValidationButton.onClick.AddListener(GameObject.FindObjectOfType<JoystickCharacterControler>().SpawnWall);
    }

    public void SetNewObject(IPickable spawningObject)
    {
        m_RotateObjectButton.onClick.RemoveAllListeners();

        m_UseButton.gameObject.SetActive(false);
        m_ValidationSpawnPanel.gameObject.SetActive(true);

        m_ValidationButton.onClick.RemoveAllListeners();
        m_RotateObjectButton.onClick.RemoveAllListeners();

        m_ValidationButton.onClick.AddListener(FindObjectOfType<JoystickCharacterControler>().SpawnObject);
        m_RotateObjectButton.onClick.AddListener(spawningObject.RotateObject);
    }

    public void DeleteNewWall()
    {
        m_ValidationSpawnPanel.gameObject.SetActive(false);
        //m_ValidationButton.gameObject.SetActive(false);
        m_UseButton.gameObject.SetActive(true);
    }

    #endregion

    #region Setting
    public void ShowSetting()
    {
        if (m_SettingsPanel.active)
        {
            m_SettingsPanel.SetActive(false);
        }else
            m_SettingsPanel.SetActive(true);
        //pauser le jeu
    }

    public void ShowExitPanel()
    {
        m_ExitPanel.SetActive(true);
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

    #endregion

    public void ShowGameOverScreen()
    {
        m_GameOverPanel.SetActive(true);
    }

    public void UpdateAmoutOfObject(GameObject pickableObject, int amount)
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

    public void UseButton( bool active)
    {
        m_UseButton.interactable = active;
    }

    public void PlayUISound()
    {
        GameManager.PlaySoundStatic(Sound.m_SoundName.UI);
    }
}
