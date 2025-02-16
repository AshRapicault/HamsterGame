using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public AudioSource sadHampter;
    public void RestartGame()
    {
        Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        sadHampter.Play();
    }
}
