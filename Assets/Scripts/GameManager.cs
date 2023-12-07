using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int currentCoins;

    public int levelEndMusic;

    public string levelToLoad;

    public bool isRespawning;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpase();
        }
    }

    public void Respawn()
    {
        StartCoroutine("RespanwnWaiter");
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespanwnWaiter()
    {
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.cmBrain.enabled = false;

        UIManager.Instance.fadeToBlack = true;

        isRespawning = true;

        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f,1f,0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        UIManager.Instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition;
        
        CameraController.instance.cmBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);

        HealthManager.instance.ResetHealth();

        isRespawning = false;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn set");
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.Instance.coinText.text = "" + currentCoins;
    }

    public void PauseUnpase()
    {
        if (UIManager.Instance.pauseScreen.activeInHierarchy)
        {
            UIManager.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            UIManager.Instance.pauseScreen.SetActive(true);
            UIManager.Instance.CloseOptions();
            Time.timeScale = 0f;



            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public IEnumerator LevelEndWaiter()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(3f);


        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            if(currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins",currentCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);

        }

        SceneManager.LoadScene(levelToLoad);

    }
}
