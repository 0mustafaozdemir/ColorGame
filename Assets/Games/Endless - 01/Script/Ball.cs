using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using Firebase.Firestore;
using System;

public class Ball : MonoBehaviour
{
    public Color touchColor;
    public Color ball;

    public bool ballsLife = true;


    public ParticleSystem wrongParticle;
    public ParticleSystem starParticle;

    public int score;
    public int highScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultScoreText;
    public TextMeshProUGUI HighScoreText;

    public List<GameObject> CircleInstantiateList = new List<GameObject>();
    public List<GameObject> circle = new List<GameObject>();
    public List<GameObject> allObject = new List<GameObject>();

    public GameObject balls;
    public GameObject randomColor;
    public GameObject checkPoint;

    public static Ball Instance;
    public GameObject checkPosition;

    public Button starButton;
    Firebase.Auth.FirebaseAuth auth;
    FirebaseFirestore db;


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

    private void Start()
    {

        balls.GetComponent<SpriteRenderer>().color = GameController.Instances.colorList[UnityEngine.Random.Range(0, RandomRange.instance.circleList.Count)];
        ball = gameObject.GetComponent<SpriteRenderer>().color;

        Debug.Log(PlayerPrefs.GetString("AuthID"));
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;

        HighScoresChange();



    }

    public void HighScoresChange()
    {
        DocumentReference docRef = db.Collection("Userss").Document(PlayerPrefs.GetString("AuthID"));

        docRef.GetSnapshotAsync().ContinueWith((task) =>
 {
     var snapshot = task.Result;
     if (snapshot.Exists)
     {
         Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
         PlayerValue player = snapshot.ConvertTo<PlayerValue>();
         HighScoreText.text = player.hıghScore.ToString();

     }
     else
     {
         Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
     }
 });
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "border")
        {

            DeadBall();
        }
        if (collision.gameObject.tag == "Object")
        {
            GameController.Instances.checkLife();
            touchColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Debug.Log(collision.gameObject);

            Check();
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (collision.gameObject.tag == "RandomColor")
        {
            Destroy(collision.gameObject);
            balls.GetComponent<SpriteRenderer>().color = GameController.Instances.colorList[UnityEngine.Random.Range(0, 3)];
            ball = balls.GetComponent<SpriteRenderer>().color;
        }
        if (collision.gameObject.tag == "Star")
        {
            Destroy(collision.gameObject);
            starParticle.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y);
            starParticle.Play();
            GameController.Instances.starCount++;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (collision.gameObject.tag == "checkpoint")
        {
            checkPosition = collision.gameObject;
            checkPosition.GetComponent<SpriteRenderer>().color = Color.green;
            GameObject collisionGO = collision.gameObject;
        }
    }

    public void starController()
    {
        for (int j = 0; j < allObject.Count; j++)
        {
            Destroy(allObject[j]);
            allObject.Remove(allObject[j]);
        }
        balls.transform.position = checkPosition.transform.position;
        balls.GetComponent<Transform>().localScale = new Vector3(0.35f, 0.35f, 0.35f);
        GameController.Instances.ballIsLife = true;
        GameController.Instances.wrongPanel.SetActive(false);
        for (int i = 0; i < CircleInstantiateList.Count; i++)
        {
            Destroy(CircleInstantiateList[i]);
        }
        CircleInstantiateList.Clear();
        var go = Instantiate(circle[UnityEngine.Random.Range(0, circle.Count)], new Vector2(0, balls.transform.position.y + 4.5f), Quaternion.identity);
        CircleInstantiateList.Add(go);
        GameController.Instances.count -= 30;
        PlayerPrefs.SetInt("StarCount", GameController.Instances.count);
        GameController.Instances.starCountText.text = GameController.Instances.count.ToString();
    }

    public void Check()
    {
        if (GameController.Instances.ballIsLife == true)
        {
            if (touchColor == ball)
            {
                Debug.Log("TRUE");
                mods();
                var go = Instantiate(circle[UnityEngine.Random.Range(0, circle.Count)], new Vector2(0, CircleInstantiateList[CircleInstantiateList.Count - 1].transform.position.y + 4.5f), Quaternion.identity);
                CircleInstantiateList.Add(go);
                score += 1;
                scoreText.text = score.ToString();
            }
            else
            {

                DeadBall();
            }
        }
    }

    public void mods()
    {
        if (CircleInstantiateList.Count % 3 == 0)
        {
            var go = Instantiate(checkPoint, new Vector2(0, CircleInstantiateList[CircleInstantiateList.Count - 1].transform.position.y + 2.25f), Quaternion.identity);
        }
        if (CircleInstantiateList.Count % 2 == 0)
        {
            var go = Instantiate(randomColor, new Vector2(0, CircleInstantiateList[CircleInstantiateList.Count - 1].transform.position.y + 2.25f), Quaternion.identity);
            allObject.Add(go);
        }
    }

    public void HighScores()
    {

        var player = new PlayerValue();
        player.AuthID = PlayerPrefs.GetString("AuthID");
        player.Mail = PlayerPrefs.GetString("mail");
        player.hıghScore = int.Parse(HighScoreText.text);

        if (score > player.hıghScore)
        {

            Debug.Log("+");
            player.hıghScore = score;

            HighScoreText.text = score.ToString();
            DocumentReference cityRef = db.Collection("Userss").Document(player.AuthID);
            Dictionary<string, object> updates = new Dictionary<string, object>
{
        { "hıghScore", player.hıghScore }
};

            cityRef.UpdateAsync(updates).ContinueWithOnMainThread(task =>
            {
                Debug.Log(
                        "Updated the Capital field of the new-city-id document in the cities collection.");
            });

        }

    }

    public void DeadBall()
    {
        if (GameController.Instances.count <= 30 && checkPosition != null)
        {
            starButton.interactable = true;
        }


        GameController.Instances.ballIsLife = false;
        balls.GetComponent<Transform>().localScale = Vector3.zero;
        GameController.Instances.playAnim();
        Debug.Log("FALSE");
        wrongParticle.GetComponent<ParticleSystem>().startColor = ball;
        wrongParticle.transform.position = gameObject.transform.position;
        wrongParticle.Play();
        resultScoreText.text = scoreText.text;
        balls.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        HighScores();
        GameController.Instances.StarCount();
    }


}






