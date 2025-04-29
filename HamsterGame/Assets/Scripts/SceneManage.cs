using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;
    public CollectiblesManager cm;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("SceneManager initialized.");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Duplicate SceneManage instance found. Destroying...");
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (CollectiblesManager.instance != null)
            {
                CollectiblesManager.instance.savedPoints = CollectiblesManager.instance.countPoints;
                CollectiblesManager.instance.savedAttackSeeds = CollectiblesManager.instance.countAttackSeeds;
            }

            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }

    public void GoodEnding()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }

    public void BadEnding()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 2;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (CollectiblesManager.instance != null)
            {
                CollectiblesManager.instance.savedPoints = CollectiblesManager.instance.countPoints;
                CollectiblesManager.instance.savedAttackSeeds = CollectiblesManager.instance.countAttackSeeds;
            }

            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }
}

