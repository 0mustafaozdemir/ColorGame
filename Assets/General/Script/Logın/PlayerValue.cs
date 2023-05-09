using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;

[FirestoreData]
public class PlayerValue
{

    [FirestoreProperty]
    public string Mail { get; set; }

    [FirestoreProperty]
    public string AuthID { get; set; }

    [FirestoreProperty]
    public string Password { get; set; }
    [FirestoreProperty]
    public int hıghScore { get; set; }
     [FirestoreProperty]
    public string ID { get; set; }
}
