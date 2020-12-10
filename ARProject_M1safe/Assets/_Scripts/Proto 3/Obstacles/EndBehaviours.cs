using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBehaviours : MonoBehaviour
{

    private FadeScreen fader;
    // Start is called before the first frame update
    void Start()
    {
        fader = FindObjectOfType<FadeScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoadSceneWithNameCore()
    {
        yield return fader.FadOutCore();
        LevelLoader.LoadLevelByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(LoadSceneWithNameCore());
    }
}
