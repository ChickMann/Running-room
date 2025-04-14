using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void Exiting()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
