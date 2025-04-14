using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class User 
{
        public string username;
        public int score;
        public string time;

    public User(string username, int scores,string time)
    {
        this.username = username;
        this.score = scores;
        this.time = time;
    }
    
   
}
