using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public int starCount;

    public TextMeshProUGUI starText;

    public static Shop Instance;
    public GameObject touchButtonBall, buyPanel, notBuyPanel, circlePrefab;
    public List<GameObject> buyBallList = new List<GameObject>();
    public List<GameObject> correctList = new List<GameObject>();

    private void Start()
    {
        StarCheck();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    public void StarCheck()
    {
        starCount = PlayerPrefs.GetInt("StarCount");
        starText.text = starCount.ToString();
    }

    public void CheckShop()
    {
        if (starCount >= 100 && !buyBallList.Contains(touchButtonBall))
        {
            buyPanel.SetActive(true);
        }
        if (starCount <= 100 && !buyBallList.Contains(touchButtonBall))
        {
            notBuyPanel.SetActive(true);
        }


        if (buyBallList.Contains(touchButtonBall))
        {
            SellCheck();
        }

    }

    public void ClickObjectDetected(GameObject go)
    {
        touchButtonBall = go;
    }

    public void SellCheck()
    {
        for (int i = 0; i < correctList.Count; i++)
        {
            correctList[i].SetActive(false);
        }
        touchButtonBall.GetComponent<Value>().buyImage.SetActive(true);
        touchButtonBall.GetComponent<Value>().coinImage.gameObject.SetActive(false);
        if (buyBallList.Contains(touchButtonBall))
        {
            circlePrefab.GetComponent<SpriteRenderer>().sprite = touchButtonBall.GetComponent<Value>().ball.GetComponent<Image>().sprite;
            buyBallList.Add(touchButtonBall);
        }
        else
        {
            starCount -= 100;
            PlayerPrefs.SetInt("StarCount", starCount);
            starText.text = starCount.ToString();

            circlePrefab.GetComponent<SpriteRenderer>().sprite = touchButtonBall.GetComponent<Value>().ball.GetComponent<Image>().sprite;
            buyBallList.Add(touchButtonBall);

            touchButtonBall.GetComponent<Value>().priceText.text = string.Empty;
        }
        buyPanel.SetActive(false);
    }
}
