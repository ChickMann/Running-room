using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [SerializeField] GameObject menuSetting;
    [SerializeField] Toggle toggleMusic;
    [SerializeField] Toggle toggleEffect;
    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip clickButton;
    [SerializeField] AudioClip clickToggle;
    [SerializeField] AudioSource audioBG;
    [SerializeField] AudioSource audioEffect;
    

    private void Start()
    {

        if (PlayerPrefs.HasKey("music"))
        {
            LoadMusic();
        }
        else if (!PlayerPrefs.HasKey("music"))
        {
            SetMusic();
        }
        if (PlayerPrefs.HasKey("effect"))
        {
            LoadEffect();
        }
        else if (!PlayerPrefs.HasKey("effect"))
        {
            SetEffect();
        }
    }
    internal void PlayMainMusic()
    {
  
        audioBG.clip = mainMusic;
        audioBG.Play();
    }
    internal bool WhenPauseGame()
    {
        ClickButtonSoundEffect();
        menuSetting.SetActive(!menuSetting.activeSelf);
        if (!menuSetting.activeSelf)
        {
            return false;
        }
        else
        {
            return true;

        }
    }
    internal void ClickButtonSoundEffect()
    {
        audioEffect.PlayOneShot(clickButton);
    }
    private void SetMusic()
    {
       audioBG.mute = !toggleMusic.isOn;
        PlayerPrefs.SetInt("music", toggleMusic.isOn? 1:0);
    }
    private void LoadMusic()
    {
        toggleMusic.isOn = PlayerPrefs.GetInt("music") == 1? true: false;
        SetMusic();
    }

    private void SetEffect()
    {
        
        audioEffect.mute = !toggleEffect.isOn;
        PlayerPrefs.SetInt("effect", toggleEffect.isOn ? 1 : 0);
    }
    private void LoadEffect()
    {
        toggleEffect.isOn = PlayerPrefs.GetInt("effect") == 1 ? true : false;
        SetMusic();
    }
    public void ClickToggleMusic()
    {
        audioEffect.PlayOneShot(clickToggle);
        SetMusic();
    }
    public void ClickToggleEffect()
    {
        audioEffect.PlayOneShot(clickToggle);
        SetEffect();
    }
}
