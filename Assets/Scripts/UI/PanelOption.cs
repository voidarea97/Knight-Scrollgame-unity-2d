using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : UIBase
{
    public override string ParentName()
    {
        return "UIMainMenu";
    }
    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject MusicOn;
    public GameObject MusicOff;

    private void Awake()
    {
        Name = "PanelOption";
    }

    public override void OnEntering()
    {
        
        gameObject.SetActive(true);

        //设置音乐开关初始状态
        MusicOn.SetActive(AudioManager.Instance.BMusic);
        MusicOff.SetActive(!AudioManager.Instance.BMusic);
        SoundOn.SetActive(AudioManager.Instance.BSound);
        SoundOff.SetActive(!AudioManager.Instance.BSound);
    }

    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public override void OnPausing()
    {
        gameObject.SetActive(false);
    }

    public override void OnResuming()
    {
        gameObject.SetActive(true);
    }


    public void MusicOnOff()
    {
        AudioManager.Instance.BMusic = !AudioManager.Instance.BMusic;
        MusicOn.SetActive(AudioManager.Instance.BMusic);
        MusicOff.SetActive(!AudioManager.Instance.BMusic);
    }

    public void SoundOnOff()
    {
        AudioManager.Instance.BSound = !AudioManager.Instance.BSound;
        SoundOn.SetActive(AudioManager.Instance.BSound);
        SoundOff.SetActive(!AudioManager.Instance.BSound);
    }

    public void BackToStart()
    {
        UIManager.Instance.PopUIPanel();
    }

    public void GoToAbout()
    {

    }
}
