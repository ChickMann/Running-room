using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;
using static UnityEngine.Random;

public class DoorScript : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;

    [SerializeField] public Material[] color;
    public List<Transform> doorsRight = new List<Transform>();
    public List<Transform> doorsLeft = new List<Transform>();
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Transform door in doorRight.GetComponentInChildren<Transform>())
        {
            doorsRight.Add(door);

        }
        foreach (Transform gate in doorLeft.GetComponentInChildren<Transform>())
        {
            doorsLeft.Add(gate);

        }
        RandomDoorOneTime();
    }

    internal void RandomDoorOneTime()
    {
        Shuffle(doorsLeft);
        Shuffle(doorsRight);
        for (int i = 0; i < doorsLeft.Count; i++)
        {
            doorsLeft[i].GetComponent<GateContronller>().SetColor(color[i], i);
            doorsRight[i].GetComponent<GateContronller>().SetColor(color[i], i);
        }

    }
    internal void RandomDoorManyTimes()
    {
        Shuffle(doorsLeft);
        Shuffle(doorsRight);
        for (int i = 0; i < doorsLeft.Count; i++)
        {
            doorsLeft[i].GetComponent<GateContronller>().SetColor(color[i], i);
            doorsRight[i].GetComponent<GateContronller>().SetColor(color[i], i);
        }
        Invoke("RandomDoorManyTimes", 0.5f);

    }
    internal void CancelRandomDoorManyTimes()
    {

        CancelInvoke("RandomDoorManyTimes");

    }

    internal Transform TransDoor(int DirDoor, int colorNumber)
    {
        if (DirDoor > 0)
        {
            foreach (Transform door in doorsRight)
            {
                if (door.GetComponent<GateContronller>().GetColorNumber() == colorNumber)
                {
                    return door.GetComponent<GateContronller>().transform;
                }
            }
        }
        else if (DirDoor < 0)
        {
            foreach (Transform door in doorsLeft)
            {
                if (door.GetComponent<GateContronller>().GetColorNumber() == colorNumber)
                {
                    return door.GetComponent<GateContronller>().transform;
                }
            }
        }
        return transform;
    }
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}

