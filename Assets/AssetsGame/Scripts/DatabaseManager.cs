using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Unity.VisualScripting;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private ScoreBoardManager scoreBoardManager;
    [SerializeField] private TMP_InputField name;
    [SerializeField] private TextMeshProUGUI resultTimer;
    [SerializeField] private TextMeshProUGUI resultScore;
    [SerializeField] private GameObject scoreElement;
    private int scoreCurrent;
    private string time;
    DatabaseReference reference;

    private string userId;

    private void Awake()
    {
        RunScoreBoard();
    }

    void Start()
    {
        userId = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    internal void CreateNewUser()
    {

        if (name.text != "")
        {
            time = resultTimer.text.ToString();
            scoreCurrent = int.Parse(Regex.Replace(resultScore.text.ToString(), "\\$", ""));
            User newUser = new User(name.text, scoreCurrent, time);
            string json = JsonUtility.ToJson(newUser);
            reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
            Debug.Log("created new user");

        }
        RunScoreBoard();
    }
    public IEnumerator GetName(Action<string> onCallBack)
    {
        Debug.Log(reference.Child("users"));
        var name = reference.Child("users").Child(userId).Child("username").GetValueAsync();
        yield return new WaitUntil(predicate: () => name.IsCompleted);
        if (name.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {name.Exception}");
        }
        else
        {
            //Auth username is now updated
        }

    }
    public IEnumerator GetScore(Action<int> onCallBack)
    {
        var score = reference.Child("users").Child(userId).Child("score").GetValueAsync();
        yield return new WaitUntil(predicate: () => score.IsCompleted);
        if (score != null)
        {
            DataSnapshot dataSnapshot = score.Result;
            onCallBack.Invoke(int.Parse(dataSnapshot.Value.ToString()));
        }

    }
    public IEnumerator GetTime(Action<string> onCallBack)
    {
        var time = reference.Child("users").Child(userId).Child("time").GetValueAsync();
        yield return new WaitUntil(predicate: () => time.IsCompleted);
        if (time.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {time.Exception}");
        }
        else
        {
            //Auth username is now updated
        }

    }
    private IEnumerator LoadScoreboardData()
    {
        scoreBoardManager.ResetAllList();
        //Get all the users data ordered by kills amount
        Task<DataSnapshot> DBTask = FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            int count = 0;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                bool scoreParse = int.TryParse(childSnapshot.Child("score").Value.ToString(), out int score);
                if (!scoreParse) StartCoroutine(LoadScoreboardData());
                string time = childSnapshot.Child("time").Value.ToString();
                GameObject scoreboardElement = Instantiate(scoreElement, scoreBoardManager.transform);
                scoreboardElement.GetComponent<UserScordBorad>().NewScoreElement(username, score, time);
                count++;
            }

            scoreBoardManager.updateListUser();


        }
    }
    public void RunScoreBoard()
    {
        StartCoroutine(LoadScoreboardData());
    }

}


