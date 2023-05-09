using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    FirebaseFirestore db;
    public int PlayerCount;
    public GameObject playerPrefab;
    public GameObject parentObject;
    public List<TextMeshProUGUI> playerText = new List<TextMeshProUGUI>();
    public List<string> playerMails = new List<string>();
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        PlayerCheck();
    }

    public void PlayerCheck()
    {
        

        Query allCitiesQuery = db.Collection("Userss");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;


            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                PlayerCount = allCitiesQuerySnapshot.Count;
                //Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                   // Debug.Log(String.Format("{0}: {1}", , pair.Value));                    
                }
                for (int i = 0; i < 1; i++)
                {
                    playerMails.Add(city["Mail"].ToString());
                }
            }
        });
        for (int i = 0; i < playerText.Count; i++)
        {
            CollectionReference citiesRef = db.Collection("cities");
            citiesRef.Document(playerMails[i].ToString()).SetAsync(new Dictionary<string, object>()
            {

                {"Mail", playerMails[i].ToString()},

            }
            );
        }

    }

  

}
