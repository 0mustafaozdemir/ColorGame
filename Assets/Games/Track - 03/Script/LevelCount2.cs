using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

public class LevelCount2 : MonoBehaviour
{
    FirebaseFirestore db;

    public int count;


    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
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

                count = int.Parse(city["TrackLevelCount"].ToString());


            }
        });
    }

    public void LevelCountCheck(int levelCount)
    {
        if (levelCount > count)
        {

            Debug.Log("+");


            DocumentReference cityRef = db.Collection("Userss").Document(PlayerPrefs.GetString("AuthID"));
            Dictionary<string, object> updates = new Dictionary<string, object>
{
        { "TrackLevelCount", levelCount }
};

            cityRef.UpdateAsync(updates).ContinueWithOnMainThread(task =>
            {
                Debug.Log(
                        "Updated the Capital field of the new-city-id document in the cities collection.");
            });

        }

    }
}
