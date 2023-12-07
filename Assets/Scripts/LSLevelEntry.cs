using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public string levelName, levelToCheck, displayName;

    public bool canLoadLevel;

    public GameObject mapPointActive, mapPointInactive;

    private bool _levelUnlocked;

    private bool _levelLoading;

    private void Start()
    {
        if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
        {
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            _levelUnlocked = true;
        }
        else
        {
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            _levelUnlocked = false;
        }
        if(PlayerPrefs.GetString("currentLevel") == levelName)
        {
            PlayerController.instance.transform.position = transform.position;
            LSResetPlayer.Instance.respawnPosition = transform.position;
        }
    }

    private void Update()
    {
        if(Input.GetButton("Jump") && canLoadLevel && _levelUnlocked  && !_levelLoading) 
        {
            StartCoroutine("LevelLoadWaiter");
            _levelLoading = true;


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canLoadLevel = true;

            LSUIManager.Instance.lNamePanel.SetActive(true);
            LSUIManager.Instance.lnameText.text = displayName;

            if(PlayerPrefs.HasKey(levelName + "_coins"))
            {
                LSUIManager.Instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
            } else
            {
                LSUIManager.Instance.coinsText.text = "0";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = true;
            LSUIManager.Instance.lNamePanel.SetActive(false);

        }
    }

    public IEnumerator LevelLoadWaiter()
    {
        PlayerController.instance.stopMove = true;
        UIManager.Instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel", levelName);
    }
}
