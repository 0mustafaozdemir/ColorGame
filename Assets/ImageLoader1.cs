using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;


public class ImageLoader1 : MonoBehaviour
{
    public static ImageLoader1 Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public RawImage rawImage;

    FirebaseStorage storage;

    StorageReference storageReference;

    IEnumerator LoadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    public void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();
        //StartCoroutine(LoadImage("https://firebasestorage.googleapis.com/v0/b/testingproject-adf6a.appspot.com/o/test.jpg?alt=media&token=0f3cf9d4-e433-4bf2-917f-4c81b3c6ab69"));
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://testingproject-adf6a.appspot.com/");

        StorageReference image = storageReference.Child(PlayerPrefs.GetString("AuthID"));

        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadImage(Convert.ToString(task.Result)));
            }
            else
            {
                Debug.Log(task.Exception);
            }


        });
    }

    


}
