using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallMain : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public bool upBool = true;
    public bool finished = true;
    public GameObject camera;
    public static BallMain ınstance;
    public bool isColor = true;
    public Vector3 position;
    private void Awake()
    {
        if (ınstance == null)
        {
            ınstance = this;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && upBool == true && finished == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            //rb.AddForce(Vector2.up*jumpForce);
            upBool = false;
            StartCoroutine(BallCountDown());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            finished = false;
            transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            transform.DOScale(-new Vector2(15f, 15f), 2f);
            transform.GetComponent<CircleCollider2D>().enabled = false;
            transform.GetComponent<Transform>().position = Vector3.zero;

            StartCoroutine((OpenPanel(GameControl.Instance.FinishPanel)));

        }
        if (other.gameObject.tag == "border" && isColor == true)
        {
            
            transform.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<SpriteRenderer>().color;
        }
        if (other.gameObject.tag == "Enemies")
        {
            Debug.Log("false");
            finished = false;
            transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GameControl.Instance.wrongPartical.startColor = transform.GetComponent<SpriteRenderer>().color;
            transform.GetComponent<Transform>().localScale = Vector3.zero;
            GameControl.Instance.wrongPartical.transform.position = transform.position;
            GameControl.Instance.wrongPartical.Play();
            StartCoroutine(OpenPanel(GameControl.Instance.wrongPanel));
        }
    }


    IEnumerator BallCountDown()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        upBool = true;
    }

    IEnumerator OpenPanel(GameObject gameObject)
    {
        yield return new WaitForSecondsRealtime(0.6f);
        gameObject.SetActive(true);
    }
}
