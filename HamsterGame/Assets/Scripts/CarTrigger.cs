using System.Collections;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    public GameObject car;             
    public GameObject badEndingPanel;  

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            car.GetComponent<CarController>().StartDriving(); 
            StartCoroutine(ShowBadEnding());
            AudioManager.instance.PlayMusic(AudioManager.instance.finalMusicBadEnding2);
        }
    }

    IEnumerator ShowBadEnding()
    {
        yield return new WaitForSeconds(1.5f); 
        badEndingPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
}
