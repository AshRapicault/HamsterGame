using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;

    public int countPoints;
    public int countAttackSeeds;
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
}