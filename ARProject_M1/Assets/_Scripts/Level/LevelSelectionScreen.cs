using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private Transform levelButtonParent;

    private string sceneName;

    protected void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (sceneName.Contains("Level"))
            {
                LevelButton currentLevelButton = Instantiate(levelButtonPrefab, levelButtonParent);
                currentLevelButton.SetLevelName(sceneName.Replace("Level", ""));
                currentLevelButton.SceneBuildIndexToLoad = i;
            }
        }
    }
}
