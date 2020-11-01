using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private FadeScreen fader;
    // Start is called before the first frame update
    void Start()
    {
        fader = FindObjectOfType<FadeScreen>();
    }

    public void ReloadLevel()
    {
        StartCoroutine("ReloadScene");
    }

    public IEnumerator ReloadScene()
    {
        //menuManager.FadOutMusic();
        yield return fader.FadOutCore();
        LevelLoader.LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadNextScene()
    {
        //menuManager.FadOutMusic();
        yield return fader.FadOutCore();
        LevelLoader.LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex+1);
    }
}
