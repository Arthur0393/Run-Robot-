using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBridge : MonoBehaviour
{
    public string leveltoUnlock;
    void Start()
    {
        if (PlayerPrefs.GetInt(leveltoUnlock + "_unlocked") == 0)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
