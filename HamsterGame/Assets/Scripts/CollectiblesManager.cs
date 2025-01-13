using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesManager : MonoBehaviour
{
    public int countPoints;
    public int countAttackSeeds;
    public Text pointsText;
    public Text AttackSeedText;

    // Update is called once per frame
    void Update()
    {
        pointsText.text = " : " + countPoints.ToString();
        AttackSeedText.text = " : " + countAttackSeeds.ToString() + " /3";
    }
}
