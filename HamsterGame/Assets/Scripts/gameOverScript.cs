using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public bool gameOverActive = false;
    public void RestartGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }

    public void GameOver()
    {
        gameOverActive = true;
        gameOverScreen.SetActive(true);

        AudioManager.instance.PlayMusic(AudioManager.instance.gameOverMusic);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level2")
        {
            FindObjectOfType<SeedSpawner>().DestroyAllSeeds();
        }

        CollectiblesManager.instance.ResetPointsAndSeedsOnDeath();
    }
}
