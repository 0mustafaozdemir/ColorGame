using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController_Track : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public ParticleSystem ballEffect, ballEffectTwo;
    public bool touch;
    public GameObject Camera;
    public GameObject completePanel, wrongPanel;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.GetComponent<SpriteRenderer>().color != other.gameObject.GetComponent<SpriteRenderer>().color)
        {
            Debug.Log("-");
            ParticleController();
            wrongPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (other.gameObject.tag == "MainCamera")
        {
            ParticleController();
            Debug.Log("--");
            wrongPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (other.gameObject.tag == "checkpoint")
        {
            completePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.02f, gameObject.transform.position.y);
        Camera.transform.DOMove(new Vector3(gameObject.transform.position.x, 0, -10), .50f);
        Debug.Log("Ben Fixed Update");
         if (Input.GetMouseButtonDown(0))
        {
            ballEffectTwo.Play();
        }
        if (Input.GetMouseButton(0))
        {
            touch = true;
            rb.gravityScale = 0f;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f);
        }
        else
        {
            touch = false;
            ballEffectTwo.Stop();
           rb.gravityScale = 0.5f;
        }
       


    }

    public void ParticleController()
    {
        ballEffect.transform.position = gameObject.transform.position;
        ballEffect.Play();
    }


}
