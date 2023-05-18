using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallPosition : MonoBehaviour
{
    public GameObject m_PlayerObj;
    public List<Transform> transformObject = new List<Transform>();
    public bool downBool;
    public float value;
    float timer = 2;
    public float touchSecond;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            value += 1;
            PosBall();
            downBool = false;
        }
        else
        {
            if (Time.time - touchSecond > 0.5f)
            {
                StartCoroutine(WaitForSecondBall());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchSecond = Time.time;
        }
        if (value == transformObject.Count)
        {
            BallController.Instance.finishPanel.SetActive(true);
        }

    }

    private void OnDrawGizmos()
    {

    }

    public void PosBall()
    {
        Vector2 pos = transformObject[(int)value].transform.position;
        m_PlayerObj.transform.DOMove(pos, .5f).SetEase(Ease.OutBack);
    }

    IEnumerator WaitForSecondBall()
    {
        for (int i = (int)value; i >= 0; i--)
        {
            value = i;
            PosBall();
            yield return new WaitForSeconds(0.2f);
        }

    }
}

