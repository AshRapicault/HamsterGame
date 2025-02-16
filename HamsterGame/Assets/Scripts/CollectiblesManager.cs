using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectiblesManager : MonoBehaviour
{
    public int countPoints;
    public int countAttackSeeds;
    public Text pointsText;
    public Text AttackSeedText;
    bool isLevel2;

    // Update is called once per frame
    void Update()
    {
        isLevel2 = SceneManager.GetActiveScene().name == "Level2";
        pointsText.text = " : " + countPoints.ToString();

        if (isLevel2)
        {
            AttackSeedText.text = " : " + countAttackSeeds.ToString();
        }
        else
        {
            AttackSeedText.text = " : " + countAttackSeeds.ToString() + " /3";
        }
    }
}
