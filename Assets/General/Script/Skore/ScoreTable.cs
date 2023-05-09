using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using TMPro;

public class ScoreTable : MonoBehaviour
{

    FirebaseFirestore db;
    public int PlayerCount;
    public GameObject playerPrefab;
    public GameObject parentObject;
    public List<TextMeshProUGUI> playerText = new List<TextMeshProUGUI>();
    public List<string> playerMails = new List<string>();
    public List<int> playerCount = new List<int>();
    public List<TextMeshProUGUI> playerCountText = new List<TextMeshProUGUI>();
    public static Player instance;
    
    


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
                    playerCount.Add(int.Parse(city["hıghScore"].ToString()));
                    
                }
            }
        });
        for (int i = 0; i < playerText.Count; i++)
        {
            CollectionReference citiesRef = db.Collection("cities");
            citiesRef.Document(playerMails[i].ToString()).SetAsync(new Dictionary<string, object>()
            {
                {"Mail", "asd"},
            }
            );
        }

    }

    public void PlayerInstantiate()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < PlayerCount; i++)
        {
            var player = Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);

            player.transform.parent = parentObject.transform;
            playerText.Add(player.GetComponent<ID>().texts);
            playerText[i].text = playerMails[i].ToString();
            playerCountText.Add(player.GetComponent<ID>().textmesh);
            playerCount.Sort();
            playerCount.Reverse();
            playerCountText[i].text = playerCount[i].ToString();
        }

    }


}
