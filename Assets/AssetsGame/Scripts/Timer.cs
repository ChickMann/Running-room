using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameManager;
public class Timer : MonoBehaviour
{
    [SerializeField] Slider m_Slider;
    float amountCurrent;
    float sliderMax;
    private int time = 0;
    private float minutes;
    private float seconds;
    private bool isRunningTimer;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI resultTimer;

    private void Awake()
    {
        amountCurrent = m_Slider.maxValue;
        sliderMax = m_Slider.maxValue;
    }
    internal void StartTimer()
    {
        timeText.gameObject.SetActive(true);
        isRunningTimer = true;
        InvokeRepeating(nameof(TimerRunning), 0, 1f);
    }
    internal void StopTimer()
    {
        timeText.gameObject.SetActive(false);
        isRunningTimer = false;
        CancelInvoke(nameof(TimerRunning));
        ShowResultTimer();
    }

    private void ShowResultTimer()
    {
        resultTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    private void TimerRunning()
    {
        time += 1;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    internal void SliderRunning()
    {
        m_Slider.gameObject.SetActive(true);
        amountCurrent -= 0.5f * Time.deltaTime;
        m_Slider.value = amountCurrent;
        if (m_Slider.value <= 0)
        {
            doorScript_Instance.RandomDoorOneTime();
            doorScript_Instance.CancelRandomDoorManyTimes();
            amountCurrent = sliderMax;
        }
        Invoke(nameof(SliderRunning), 0);
    }

    internal void StopSlider()
    {
        CancelInvoke(nameof(SliderRunning));
        m_Slider.gameObject.SetActive(false);
    }
}
