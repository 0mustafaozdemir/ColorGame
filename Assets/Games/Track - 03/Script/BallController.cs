using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    public GameObject m_PlayerObj;
    public List<Transform> transformObject = new List<Transform>();
    public bool downBool;
    public float value;
    float timer = 2;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            value += 1;
            Vector2 pos = transformObject[(int)value].transform.position;
            m_PlayerObj.transform.DOMove(pos, .5f).SetEase(Ease.InBack);
            downBool = false;
        }
        else
        {
            if (timer < Time.time)
            {


                timer = Time.time + 1;
            }

        }

    }

    private void OnDrawGizmos()
    {

    }
}

