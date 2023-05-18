using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;



public class LoginAndSign : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    FirebaseFirestore db;
    public GameObject panel;
    public FirebaseUser user;



    public TMP_InputField EMailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField userNameInput;

    public TMP_InputField emailInputRegister;
    public TMP_InputField PasswrodInputRegister;

    public bool isFauled, testing;
    public GameObject loadingPanel;
    public string userMail;
    public string playerID;
    public GameObject false_Pop_Up, emailVerificionPanel;
    public TextMeshProUGUI emailVerifiticonText;

    public static LoginAndSign Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
    }
    private void Update()
    {
        if (isFauled == true)
        {
            false_Pop_Up.SetActive(true);
            clear();
            isFauled = false;
        }
        if (testing == true)
        {
            panel.SetActive(false);
            PlayerPrefs.SetString("mail", userMail);
            testing = false;
        }
    }

    public void SignIn()
    {
        string email, password, userName;
        email = EMailInput.text;
        password = passwordInput.text;
        userName = userNameInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email.ToString(), password.ToString()).ContinueWith(async task =>
        {

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                isFauled = true;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                isFauled = true;
                return;
            }



            Debug.Log("Sign up");

            Firebase.Auth.FirebaseUser newUser = task.Result.User;
            playerID = newUser.UserId;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);

            var player = new PlayerValue();
            string mail = EMailInput.text;
            player.Mail = mail;
            player.Password = passwordInput.text;
            player.AuthID = newUser.UserId;
            player.UserName = userName;
            player.CastleLevelCount = 0;



            await db.Collection("Userss").Document(newUser.UserId).SetAsync(player).ContinueWithOnMainThread(task =>
            {
                PlayerPrefs.SetString("mail", mail);
                PlayerPrefs.SetString("AuthID", newUser.UserId);
                PlayerPrefs.SetString("UserName", userName);
                panel.SetActive(false);
                userMail = player.Mail;
                Debug.Log("+");

                Debug.Log(player.Mail);
                
            });

            Debug.Log("Kapandı");

        });

    }

    public void Register()
    {
        string email, password;
        email = emailInputRegister.text;
        password = PasswrodInputRegister.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                isFauled = true;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                isFauled = true;
                return;
            }
            userMail = email;
            testing = true;


            Firebase.Auth.FirebaseUser newUser = task.Result.User;
            playerID = newUser.UserId;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);


        });


    }
    public void clear()
    {
        isFauled = false;
        emailInputRegister.text = string.Empty;
        PasswrodInputRegister.text = string.Empty;
        EMailInput.text = string.Empty;
        passwordInput.text = string.Empty;
        //loadingPanel.SetActive(true);
        //StartCoroutine(FauledScene());
    }
    IEnumerator FauledScene()
    {
        yield return new WaitForSeconds(2f);
        loadingPanel.SetActive(false);
    }

   

 

}
