using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
        CollectiblesManager.instance.ResetAll();
    }
}
