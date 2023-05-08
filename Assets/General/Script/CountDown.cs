using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    public static CountDown Instance;
    public bool currentBool;
    float currentTime;
    float startingTime = 3f;
    public GameObject countDownPanel;
    [SerializeField] TextMeshProUGUI countDownText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentTime = startingTime;
        countDownPanel.SetActive(true);
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        countDownText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0;

        }
        if (currentTime <= 0)
        {
            currentBool = true;
            countDownPanel.SetActive(false);
        }


    }

   


}
