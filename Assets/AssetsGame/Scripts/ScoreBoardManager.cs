using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class ScoreBoardManager : MonoBehaviour
{
    private List<Transform> listUser = new List<Transform>();


    public void ResetAllList()
    {
        for (int i = 0; i < listUser.Count; i++)
        {
            Destroy(listUser[i].gameObject);
        }
        listUser.Clear();
        Debug.Log("clear");
    }
    public void updateListUser()
    {
        foreach (Transform user in gameObject.GetComponentInChildren<Transform>())
        {
            listUser.Add(user);
        }
    }
}
