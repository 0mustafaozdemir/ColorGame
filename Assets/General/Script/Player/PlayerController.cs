using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI playMail;
    public GameObject LogOutPanel;
    public GameObject playerPanel;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
         Instance = this;
        }
    }
    public void ChangeMail()
    {
      playMail.text = PlayerPrefs.GetString("mail");
    }

    public void LogOut(){
      PlayerPrefs.DeleteKey("mail");
      PlayerPrefs.DeleteKey("AuthID");
      LogOutPanel.SetActive(true);      
      playerPanel.SetActive(false);
    }
}
