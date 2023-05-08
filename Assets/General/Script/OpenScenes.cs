using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScenes : MonoBehaviour
{
    public GameObject touchPanel;
    public GameObject Panel;

    public void TouchOpen(GameObject panel)
    {
        touchPanel = panel;
        panel.SetActive(true);      
    }

    public void ClosePanel(GameObject panel)
    {
        
            panel.SetActive(false);
        
        touchPanel = null;
    }

    public void OpenScene(int sceneInt)
    {

        SceneManager.LoadScene(sceneInt);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);

    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
}
