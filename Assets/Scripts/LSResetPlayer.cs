using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSResetPlayer : MonoBehaviour
{
    public static LSResetPlayer Instance;

    public Vector3 respawnPosition;
    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = respawnPosition;
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
