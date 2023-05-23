using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCheck : MonoBehaviour
{
    public GameObject internetErrorPanel;
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internetErrorPanel.SetActive(true);           
        }
    }

    public void InternetVoid()
    {
        Application.Quit();
    }
}
