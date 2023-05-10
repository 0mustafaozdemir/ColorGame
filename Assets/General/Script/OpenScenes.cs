using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OpenScenes : MonoBehaviour
{
    public GameObject touchPanel;
    public GameObject Panel;
    public CanvasGroup canvasGroup;
    RectTransform rectTransformTouchPanel;
    public float fadeTime;
    public List<GameObject> items = new List<GameObject>();
    public static OpenScenes Instance;


    public void TouchOpen(GameObject panel)
    {
        touchPanel = panel;

        rectTransformTouchPanel = touchPanel.GetComponent<RectTransform>();
        rectTransformTouchPanel.transform.localPosition = new Vector3(0, -400f, 0f);
        rectTransformTouchPanel.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);

        panel.SetActive(true);
    }
    private void Start()
    {

        StartCoroutine(ItemAnimation());
    }
    public void ClosePanel(GameObject panel)
    {
        rectTransformTouchPanel = touchPanel.GetComponent<RectTransform>();
        rectTransformTouchPanel.transform.localPosition = new Vector3(0, 0f, 0f);
        rectTransformTouchPanel.DOAnchorPos(new Vector2(0f, -4000f), fadeTime, false).SetEase(Ease.InOutQuint);



        touchPanel = null;
    }


    IEnumerator ItemAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach (var item in items)
        {
            item.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void ItemAnimVoid()
    {
       StartCoroutine(ItemAnimation());
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
