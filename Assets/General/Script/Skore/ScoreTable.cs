using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using TMPro;
using System.Linq;

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
    public List<ID> gameObj = new List<ID>();
    public List<GameObject> players = new List<GameObject>();
    public GameObject parentOBJ;

    public TextMeshProUGUI ınfoText;




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
                    playerMails.Add(city["UserName"].ToString());
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
        if (playerCount.Count <= 9)
        {
            parentObject.transform.parent = parentOBJ.transform;
        }

    }

    public void PlayerInstantiate()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (playerCount.Count <= 50)
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                var player = Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity).GetComponent<ID>();
                players.Add(player.gameObject);

                gameObj.Add(player);
                playerText.Add(player.GetComponent<ID>().texts);
                playerText[i].text = playerMails[i].ToString();
                playerCountText.Add(player.GetComponent<ID>().textmesh);
                playerCountText[i].text = playerCount[i].ToString();
                player.GetComponent<ID>().score = int.Parse(player.GetComponent<ID>().textmesh.text);
            }
            SortList();
            test();        }


    }


    public void SortList()
    {
        gameObj.Sort((obj1, obj2) => obj2.score.CompareTo(obj1.score));
        //gameObj.Reverse();
        for (int i = 0; i < gameObj.Count; i++)
        {
            gameObj[i].transform.parent = parentObject.transform;
        }
        for (int i = 0; i < gameObj.Count; i++)
        {
            gameObj[i].GetComponent<ID>().scorePosCount = i + 1;
            gameObj[i].GetComponent<ID>().scorePosText.text = gameObj[i].GetComponent<ID>().scorePosCount.ToString();
        }


    }
    public void test()
    {
        for (int i = 0; i < playerMails.Count; i++)
        {
            if (playerMails[i] ==     (PlayerPrefs.GetString("UserName")))
            {
                var go = players[i];
                Debug.Log(go.GetComponent<ID>().scorePosCount);
                ınfoText.text = go.GetComponent<ID>().scorePosCount.ToString();
            }
        }
    }


}
