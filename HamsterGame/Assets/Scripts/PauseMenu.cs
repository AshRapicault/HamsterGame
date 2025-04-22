using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] CollectiblesManager cm;
    [SerializeField] PlayerMovement player;
    [SerializeField] BossHealth boss;

    public Button upgradeDamageButton;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            upgradeDamageButton.gameObject.SetActive(false);
            boss = null;
            Debug.Log("Damage upgrades are not avaible in this scene");
        }
        else
        {
            upgradeDamageButton.gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
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
        if (cm.countPoints > 20)
        {
            cm.countPoints -= 20;
            player.ActivateSpeedBoost();
        }
        else
        {
            Debug.Log("You do not have enough points to do this purchase.");
        }
    }

    public void purchaseDamageUpgrade()
    {
        if (cm.countPoints > 30)
        {
            cm.countPoints -= 30;
            StartCoroutine(DamageUpgradeTimer());
        }
        else
        {
            Debug.Log("You do not have enough points to do this purchase.");
        }
    }

    private IEnumerator DamageUpgradeTimer()
    {
        boss.damageAmount = 2;
        yield return new WaitForSeconds(10f);
        boss.damageAmount = 1;
    }

}
