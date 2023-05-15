using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    public GameObject m_PlayerObj;
    public Transform[] positionPoint;
    [Range(0,1)]
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(iTween.PathLength(positionPoint));
    }
    float tempTime;
    // Update is called once per frame
    void Update()
    {
        
        if (value < 2)
        {
            value += Time.deltaTime / 10;
        }
        iTween.PutOnPath(m_PlayerObj, positionPoint, value);
    }
    private void OnDrawGizmos()
    {
        
        iTween.DrawPath(positionPoint,Color.green);
    }
}

