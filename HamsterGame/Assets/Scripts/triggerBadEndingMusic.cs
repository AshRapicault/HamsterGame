using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBadEndingMusic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.finalMusicBadEnding);
        }
    }
}
