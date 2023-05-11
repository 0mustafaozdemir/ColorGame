using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using Firebase.Firestore;

public class LevelManagers : MonoBehaviour
{
    FirebaseFirestore db;
    public List<Button> LevelButton = new List<Button>();

    public int levelCount;

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        LevelCountOpen();
    }

    public void LevelCountOpen()
    {
       Query allCitiesQuery = db.Collection("Userss");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            

            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {

                //Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    // Debug.Log(String.Format("{0}: {1}", , pair.Value));                    
                }
               
                    levelCount = int.Parse(city["CastleLevelCount"].ToString());                    
                

            }
        });
    }

    public void LevelInteractableOpen()
    {
        for (int i = 0; i <= levelCount; i++)
        {
            LevelButton[i].interactable = true;
        }
    }

}
