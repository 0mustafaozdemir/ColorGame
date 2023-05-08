using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRange : MonoBehaviour
{
    public List<GameObject> circleList = new List<GameObject>();
    public static RandomRange instance;
    public bool isCheckCollider = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {       
        colorsChange();   
    }
   
   public void colorsChange()
   {
        for (int i = 0; i < circleList.Count; i++)
        {
            circleList[i].GetComponent<SpriteRenderer>().color = GameController.Instances.colorList[i];
        }       
   }

    
}
