using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static GameManager;

public class IntroScript : MonoBehaviour
{
    [SerializeField] internal GameObject tutorial;
    [SerializeField] internal GameObject buttonStart;
    [SerializeField] internal GameObject buttonControl;
    private PlayableDirector intro;
    [SerializeField] private PlayableDirector Awake;
    public bool isIntro;
    private void Start()
    {
        intro = GetComponent<PlayableDirector>();
    }
    public void RunIntro()
    {
        Awake.Stop();
        intro.Play();
        isIntro = true;
    }
    public void ButtonTutorial()
    {
        settingScript_Instance.ClickButtonSoundEffect();
        tutorial.SetActive(false);
        buttonStart.SetActive(true);
        PlayerPrefs.SetInt("tutorial", 0);
    }
    internal void checkTutorial()
    {
        if (!PlayerPrefs.HasKey("tutorial"))
        {
            tutorial.SetActive(true);
        }
        else
        {
            buttonStart.SetActive(true);
        }
    }



}
