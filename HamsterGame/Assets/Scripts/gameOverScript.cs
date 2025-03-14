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
    }

    public void GameOver()
    {
        gameOverActive = true;
        gameOverScreen.SetActive(true);
        sadHampter.Play();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        FindObjectOfType<SeedSpawner>().DestroyAllSeeds();
    }
}
