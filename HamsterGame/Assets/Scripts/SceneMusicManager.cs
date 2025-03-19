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

        if (AudioManager.instance == null)
        {
            return;
        }

        if (scene.name == "MainMenu")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
        }
        else if (scene.name == "Level1" || scene.name == "Level2")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.gameplayMusic);
        }
    }
}
