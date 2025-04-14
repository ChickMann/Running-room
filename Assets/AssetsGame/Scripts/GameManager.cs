using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private SettingScript settingScript;
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private IntroScript introScript;
    [SerializeField] private Loader loaderScript;
    [SerializeField] private SpawnManager spawnManagerScript;
    [SerializeField] private AdmodAdsScript admodAdsScript;
    [SerializeField] private Timer timerScript;
    [SerializeField] private DatabaseManager databaseManagerScript;
    [SerializeField] private PlayerContronller playerContronllerScript;
    [SerializeField] private DoorScript doorScript;

    public static GameManager GameManager_Instance { get; private set; }
    public static SettingScript settingScript_Instance { get; private set; }
    public static ScoreScript scoreScript_Instance { get; private set; }
    public static IntroScript introScript_Instance { get; private set; }
    public static Loader loaderScript_Instance { get; private set; }
    public static SpawnManager spawnManager_Instance { get; private set; }
    public static AdmodAdsScript admodAdsScript_Instance { get; private set; }
    public static Timer timer_Instance { get; private set; }
    public static DatabaseManager databaseManager_Instance { get; private set; }
    public static PlayerContronller playerContronller_Instance { get; private set; }
    public static DoorScript doorScript_Instance { get; private set; }

    private Animator animatorCam;
    private bool hasClick = true;
    private bool _isGameOver;
    public bool isGameOver
    {
        get { return _isGameOver; }

    }
    private bool _hasStart;
    public bool hasStart
    {
        get { return _hasStart; }
    }

    private bool _isPaused;
    public bool isPaused
    {
        get { return _isPaused; }
    }


    private void Awake()
    {
        GameManager_Instance = this;
        settingScript_Instance = settingScript;
        scoreScript_Instance = scoreScript;
        introScript_Instance = introScript;
        loaderScript_Instance = loaderScript;
        spawnManager_Instance = spawnManagerScript;
        admodAdsScript_Instance = admodAdsScript;
        timer_Instance = timerScript;
        databaseManager_Instance = databaseManagerScript;
        playerContronller_Instance = playerContronllerScript;
        doorScript_Instance = doorScript;

    }
    private void Start()
    {

        introScript.checkTutorial();
        animatorCam = Camera.main.GetComponent<Animator>();
        _isPaused = false;
        _hasStart = false;

    }
    private void LateUpdate()
    {

    }

    #region State Game
    public void StartGame()
    {
        if (!_isPaused)
        {
            introScript.RunIntro();
            introScript.buttonStart.SetActive(false);
            animatorCam.SetInteger("Cam_int", 2);
            StartCoroutine(CoolDown(() =>
            {
                timer_Instance.StartTimer();
                introScript.buttonControl.SetActive(true);
                settingScript.PlayMainMusic();
                spawnManagerScript.SpawnItemNow();
                spawnManagerScript.SpawnStarTime(7);
                timerScript.SliderRunning();
                _isGameOver = false;
                _hasStart = true;
                scoreScript.UpdateScore(0);
                introScript.isIntro = false;
            }, 2f));

        }

    }
    public void PauseGame()
    {

        if (!_isGameOver && !introScript.isIntro)
        {
            _isPaused = settingScript.WhenPauseGame();
            if (_isPaused)
            {
                admodAdsScript.LoadBannerAD();
                Time.timeScale = 0;
            }
            else
            {
                admodAdsScript.DestroyBannerAD();
                Time.timeScale = 1;
            }

        }

    }
    public void ShowGameBoard()
    {
        scoreScript.ShowResult();
        timerScript.StopTimer();
        timerScript.StopSlider();
        admodAdsScript.LoadBannerAD();
        databaseManagerScript.RunScoreBoard();
        Debug.Log("Game Over");
    }
    public void RestatGame()
    {
        settingScript.ClickButtonSoundEffect();
        loaderScript.Loading(0);
        admodAdsScript.DestroyBannerAD();
    }
    #endregion
    #region Functions

    /// <summary>
    /// Cool down time then do something
    /// </summary>
    public IEnumerator CoolDown(Action modifySpeed, float time)
    {

        yield return new WaitForSeconds(time);
        modifySpeed.Invoke();
    }
    public void IncreaseScore(int value)
    {
        scoreScript.UpdateScore(value);
    }

    public void SpawnItems()
    {
        spawnManagerScript.SpawnItem();
    }
    public void SaveResult()
    {
        if (hasClick)
        {
            settingScript.ClickButtonSoundEffect();
            databaseManagerScript.CreateNewUser();
            hasClick = false;
            StartCoroutine(CoolDown(() => { hasClick = true; }, 1));
        }

    }
    public void GameOVer()
    {
        _isGameOver = true;
    }
    #endregion



}
