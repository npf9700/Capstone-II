using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene(float time)
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex + 1);
        StartCoroutine(LoadAfterTime(time));
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        //Time.timeScale = 1;
    }

    IEnumerator LoadAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

