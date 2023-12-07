using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager Instance;

    public Text lnameText;

    public GameObject lNamePanel;

    public Text coinsText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
}
