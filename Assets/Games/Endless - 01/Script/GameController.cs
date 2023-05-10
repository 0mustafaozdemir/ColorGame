using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    public bool ballIsLife;
    public GameObject wrongPanel;

    public float jumpForce = 0.5f;
    public float rotationSpeed;

    public Rigidbody2D rb;
    public float speed;
    public float fallSpeed;

    public GameObject pausePanel;
    public List<Color> colorList = new List<Color>();

    public GameObject Circle;
    public GameObject ball;

    public static GameController Instances;

    public int starCount;
    public TextMeshProUGUI starCountText;

    private bool canRise;
    public float riseTime = 1f;

    public int count;
    bool touchBool = true;

    Coroutine coroutine;

    private void Awake()
    {
        rb.gravityScale = 0;
        InstantiateCircle();

        if (Instances == null)
        {
            Instances = this;
        }

        Application.targetFrameRate = 120;
    }

    void Update()
    {
        if (ballIsLife == true )
        {
            if (Input.GetMouseButtonDown(0) && touchBool == true)
            {
                Debug.Log("+");
                jump();
                Jump();
                ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                touchBool = false;
                StartCoroutine(TouchSecond());
            }
            CircleLoop();
        }        
    }

    IEnumerator TouchSecond()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        touchBool = true;
    }

    private void Start()
    {
        starCountText.text = PlayerPrefs.GetInt("StarCount").ToString();
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    public void checkLife()
    {
        if (ballIsLife == false)
        {


        }
        else
        {
            CircleLoop();
        }
    }

    public void jump()
    {
        rb.gravityScale = 0.5f;
    }

    public void ListShuffle()
    {
        int n = colorList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = colorList[k];
            colorList[k] = colorList[n];
            colorList[n] = value;
        }
    }

    public void InstantiateCircle()
    {
        ListShuffle();
    }

    public void Restart(int count)
    {
        SceneManager.LoadScene(count);
    }

    public void CircleLoop()
    {
        var circleList = Ball.Instance.CircleInstantiateList;
        for (int i = 0; i < circleList.Count; i++)
        {
            circleList[i].transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);

    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    IEnumerator PlayWrong()
    {
        yield return new WaitForSeconds(0.9f);
        wrongPanel.SetActive(true);
        ballIsLife = true;
    }

    public void playAnim()
    {
        StartCoroutine(PlayWrong());
    }

    public void StarCount()
    {
        count = int.Parse(starCountText.text);
        count += starCount;
        PlayerPrefs.SetInt("StarCount", count);
        starCountText.text = count.ToString();
    }

}

