using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallPosCalculator : MonoBehaviour
{
    public Transform transformBall;
    public GameObject ball;
    private void Start()
    {
        ball = GameControl.Instance.ball;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Ball")
        {
             
            
            BallMain.ınstance.isColor = true;
            ball.transform.position = transformBall.position;
            BallMain.ınstance.camera.transform.DOMove(new Vector3(0, BallMain.ınstance.camera.transform.position.y + 1, -10), .50f);
        }
    }

  
}
