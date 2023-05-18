using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool ballIsDead;
    public ParticleSystem wrongParticle;
    public GameObject wrongPanel, finishPanel;
    public static BallController Instance;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ballIsDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (ballIsDead == false)
            {
                Debug.Log("Test");
                other.gameObject.GetComponent<Collider2D>().enabled = false;
                transform.gameObject.GetComponent<Transform>().localScale = Vector3.zero;
                wrongParticle.transform.position = gameObject.transform.position;
                wrongParticle.Play();
                StartCoroutine(waitForSecondBall());
            }
            ballIsDead = true;
        }

        if (other.gameObject.tag == "checkpoint")
        {

        }
    }


    IEnumerator waitForSecondBall()
    {
        yield return new WaitForSeconds(1f);
        
        wrongPanel.SetActive(true);
    }
}
