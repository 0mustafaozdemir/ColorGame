using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Extensions;

public class MyFireBaseRemoteConfigs : MonoBehaviour
{
   /* void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        System.Collections.Generic.Dictionary<string, object> defaults =
            new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server
        // yet, or if we ask for values that the server doesn't have:
        defaults.Add("config_test_string", "default local string");
        defaults.Add("config_test_int", 1);
        defaults.Add("config_test_float", 1.0);
        defaults.Add("config_test_bool", false);

        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
        Debug.Log("Remote config ready!");
    }
    public void FetchFireBase()
    {
        FetchDataAsync();
    }
    public void ShowData()
    {
        //   DebugLog("config_test_string: " +
        //        Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("config_test_string").StringValue);
        Debug.Log("maxCountToShowAdmob: " +
            Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("maxCountToShowAdmob").LongValue);
        //   DebugLog("config_test_float: " +
        //            Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("config_test_float").DoubleValue);
        //   DebugLog("config_test_bool: " +
        //            Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("config_test_bool").BooleanValue);
    }

    // Start a fetch request.
    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        // FetchAsync only fetches new data if the current data is older than the provided
        // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
        // By default the timespan is 12 hours, and for production apps, this is a good
        // number.  For this example though, it's set to a timespan of zero, so that
        // changes in the console will always show up immediately.
        System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWith(FetchComplete);
    }

    void FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch canceled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
        }

        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                    info.FetchTime));
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending.");
                break;
        }
    }*/
}