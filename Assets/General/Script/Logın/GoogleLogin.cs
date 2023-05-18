using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using System.Net.Http;
using TMPro;

public class GoogleLogin : MonoBehaviour
{
    public string googleWebAPI = "";
    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public TextMeshProUGUI usernameText, UserEmailText;
    public Image UserProfilePic;
    public string imageURL;
    public GameObject LoginScreen, ProfileScreen;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = googleWebAPI,
            RequestIdToken = true,
        };
    }

    private void Start()
    {
        InitFireBase();
    }

    public void InitFireBase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void GoogleSıgnInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticedFinished);
    }

    void OnGoogleAuthenticedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.Log("Fault");
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Login Cancel");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Was Canceled");
                    return;
                }
                else if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                    return;
                }

                user = auth.CurrentUser;

                usernameText.text = user.DisplayName;
                UserEmailText.text = user.Email;

                LoginAndSign.Instance.false_Pop_Up.SetActive(false);
                
                StartCoroutine(LoadImage(CheckImageURL(user.PhotoUrl.ToString())));

            });
        }
    }

    private string CheckImageURL(string URL)
    {
      if (!string.IsNullOrEmpty(null))
      {
        return URL;
      }

      return imageURL;
    }
    
    IEnumerator LoadImage(string imageURL)
    {
        WWW www = new WWW(imageURL);
        yield return www;

          UserProfilePic.sprite = Sprite.Create(www.texture,  new Rect(0,0, www.texture.width, www.texture.height), new Vector2(0,0));


    }



}







