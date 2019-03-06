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
        MusicOn.SetActive(AudioManager.Instance.bMusic);
        MusicOff.SetActive(!AudioManager.Instance.bMusic);
        SoundOn.SetActive(AudioManager.Instance.bSound);
        SoundOff.SetActive(!AudioManager.Instance.bSound);
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
        AudioManager.Instance.bMusic = !AudioManager.Instance.bMusic;
        MusicOn.SetActive(AudioManager.Instance.bMusic);
        MusicOff.SetActive(!AudioManager.Instance.bMusic);
    }

    public void SoundOnOff()
    {
        AudioManager.Instance.bSound = !AudioManager.Instance.bSound;
        SoundOn.SetActive(AudioManager.Instance.bSound);
        SoundOff.SetActive(!AudioManager.Instance.bSound);
    }

    public void BackToStart()
    {
        UIManager.Instance.PopUIPanel();
    }

    public void GoToAbout()
    {

    }
}
