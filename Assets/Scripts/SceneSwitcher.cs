using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject promptSprite;
    public int sceneIndex;

    void Start()
    {
        if (promptSprite != null)
        {
            promptSprite.SetActive(false); // Pastikan sprite tidak terlihat saat memulai
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Play()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
