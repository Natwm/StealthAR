using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public int SceneBuildIndexToLoad { private get; set; }

    private FadeScreen fader;
    //private MenuManager menuManager;

    protected void Start()
    {
        //menuManager = FindObjectOfType<MenuManager>();
        fader = FindObjectOfType<FadeScreen>();
        AddListener(LoadSceneWithName);
    }

    public void SetLevelName(string name) => buttonText.text = $"{name}";

    private void AddListener(UnityAction method) => button.onClick.AddListener(method);

    private void LoadSceneWithName() => StartCoroutine(LoadSceneWithNameCore());

    private IEnumerator LoadSceneWithNameCore()
    {
        //menuManager.FadOutMusic();
        yield return fader.FadOutCore();
        LevelLoader.LoadLevelByIndex(SceneBuildIndexToLoad);
    }
}
