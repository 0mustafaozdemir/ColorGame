using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public List<Vector2> pointsList = new List<Vector2>();
    public GameObject ball;
    public GameObject FinishPanel, wrongPanel;
    public ParticleSystem wrongPartical;
    public static GameControl Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        //fixed update yerine update yap tamam mı?
        Debug.Log("Ben Just Update");
    }

    private void FixedUpdate()
    {
        ball.transform.position = new Vector2(ball.transform.position.x + 0.03f, ball.transform.position.y);
        Debug.Log("Ben Fixed Update");
    }

}
