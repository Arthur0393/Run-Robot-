using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invincCounter;

    public Sprite[] healtBarImages;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ResetHealth();
    }

    void Update()
    {
        if(invincCounter >0)
        {
            invincCounter -= Time.deltaTime;


            for(int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }
                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
            
        }
    }

    public void Hurt()
    {
        if(invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.Instance.Respawn();
            }
            else
            {
                PlayerController.instance.Knockback();
                invincCounter = invincibleLength;
            }
        }
        UpdateUI();

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.Instance.healthImage.enabled = true;
        UpdateUI();

    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.healthText.text = currentHealth.ToString();
    
        switch(currentHealth)
        {
            case 5:
                UIManager.Instance.healthImage.sprite = healtBarImages[4];
                break;
            case 4:
                UIManager.Instance.healthImage.sprite = healtBarImages[3];
                break;
            case 3:
                UIManager.Instance.healthImage.sprite = healtBarImages[2];
                break;
            case 2:
                UIManager.Instance.healthImage.sprite = healtBarImages[1];
                break;
            case 1:
                UIManager.Instance.healthImage.sprite = healtBarImages[0];
                break;
            case 0:
                UIManager.Instance.healthImage.enabled = false;
                break;
        }
    }

    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
