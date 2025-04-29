using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] gameOverScript gameOver;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManage.instance != null)
            {
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    SceneManage.instance.NextLevel();
                }
                else if (SceneManager.GetActiveScene().name == "Level2" && gameOver.gameOverCount == 0)
                {
                    SceneManage.instance.GoodEnding();
                }
                else if (SceneManager.GetActiveScene().name == "Level2" && gameOver.gameOverCount > 0)
                {
                    SceneManage.instance.BadEnding();
                }
            }
        }
    }
}

