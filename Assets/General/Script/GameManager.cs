using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject logOutPanel;



    // Update is called once per frame
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("AuthID"));
        if (PlayerPrefs.GetString("AuthID") != "")
        {
            logOutPanel.SetActive(false);
        }
        else
        {
            logOutPanel.SetActive(true);
        }
    }
}

