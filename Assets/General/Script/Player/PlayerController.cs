using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI playMail;
    public TextMeshProUGUI playName;
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
      playName.text = PlayerPrefs.GetString("UserName");
    }

    public void LogOut(){
      PlayerPrefs.DeleteKey("mail");
      PlayerPrefs.DeleteKey("AuthID");
      PlayerPrefs.DeleteKey("UserName");
      LogOutPanel.SetActive(true);      
      playerPanel.SetActive(false);
    }
}
