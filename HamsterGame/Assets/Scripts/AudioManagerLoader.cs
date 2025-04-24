using UnityEngine;

public class AudioManagerLoader : MonoBehaviour
{
    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            GameObject audioManagerPrefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            Instantiate(audioManagerPrefab);
        }
    }
}
