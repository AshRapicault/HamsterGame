using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] PlayerMovement player;
    [SerializeField] BossHealth boss;

    private CollectiblesManager cm;
    public static PauseMenu instance;
    public bool isPaused { get; private set; }
    void Start()
    {
        instance = this;
        cm = CollectiblesManager.instance;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
    }

    public void purchaseSpeedUpgrade()
    {
        if (cm.countPoints >= 10)
        {
            cm.countPoints -= 10;
            player.ActivateSpeedBoost();
        }
        else
        {
            Debug.Log("You do not have enough points to do this purchase.");
        }
    }

    public void purchaseDamageUpgrade()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            Debug.Log("Damage upgrades are not avaible in this scene");
        }
        else
        {
            if (cm.countPoints >= 20)
            {
                cm.countPoints -= 20;
                StartCoroutine(DamageUpgradeTimer());
            }
            else
            {
                Debug.Log("You do not have enough points to do this purchase." + cm.countPoints);
            }
        }

    }

    private IEnumerator DamageUpgradeTimer()
    {
        boss.damageAmount = 2;
        yield return new WaitForSeconds(10f);
        boss.damageAmount = 1;
    }

}
