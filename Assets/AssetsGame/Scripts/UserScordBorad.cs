using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserScordBorad : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void NewScoreElement(string _username, int score, string time)
    {
        usernameText.text = _username;
        scoreText.text = score.ToString()+"$";
        timeText.text = time;
    }

}
