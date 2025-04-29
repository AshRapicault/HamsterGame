using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene geladen: " + scene.name); // Geeft aan welke scene geladen wordt

        if (scene.name == "Main Menu")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
        }
        else if (scene.name == "Level1" || scene.name == "Level2")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
        }
        else if (scene.name == "Good ending")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.goodEndingMusic);
        }
        else if (scene.name == "Bad Ending")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.badEndingMusic);
        }
    }
}
