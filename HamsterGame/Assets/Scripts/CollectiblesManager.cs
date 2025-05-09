using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;

    public int countPoints;
    public int countAttackSeeds;
    public int savedPoints;//to keep this value in a new level instead of farm seeds or start from 0
    public int savedAttackSeeds;//to keep this value in a new level instead of farm seeds or start from 0
    public Text pointsText;
    public Text AttackSeedText;
    bool isLevel2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pointsText = GameObject.Find("TextPoints")?.GetComponent<Text>();
        AttackSeedText = GameObject.Find("TextAttack")?.GetComponent<Text>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pointsText = GameObject.Find("TextPoints")?.GetComponent<Text>();
        AttackSeedText = GameObject.Find("TextAttack")?.GetComponent<Text>();

        if (scene.buildIndex != 0)
        {
            savedPoints = countPoints;
            savedAttackSeeds = countAttackSeeds;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        isLevel2 = SceneManager.GetActiveScene().name == "Level2";
        if (pointsText != null)
        {
            pointsText.text = " : " + countPoints.ToString();
        }

        if (AttackSeedText != null)
        {
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

    public void ResetPointsAndSeedsOnDeath()
    {
        countPoints = savedPoints;
        countAttackSeeds = savedAttackSeeds;
    }

    public void ResetAll()
    {
        countPoints = 0;
        countAttackSeeds = 0;
        savedPoints = 0;
        savedAttackSeeds = 0;
    }
}
