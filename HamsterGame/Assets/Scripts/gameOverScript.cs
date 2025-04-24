using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public AudioSource sadHampter;
    public bool gameOverActive = false;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (AudioManager.instance != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "MainMenu")
            {
                AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
            }
            else if (sceneName == "Level1" || sceneName == "Level2")
            {
                AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
            }
        }
    }

    public void GameOver()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }

        gameOverActive = true;
        gameOverScreen.SetActive(true);
        sadHampter.Play();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        FindObjectOfType<SeedSpawner>().DestroyAllSeeds();

        CollectiblesManager.instance.ResetPointsAndSeedsOnDeath();
    }
}
