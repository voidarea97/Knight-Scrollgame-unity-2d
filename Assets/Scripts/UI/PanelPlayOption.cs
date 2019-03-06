using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPlayOption : UIBase {

    public override string ParentName()
    {
        return "UIPlay";
    }

    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject MusicOn;
    public GameObject MusicOff;
    // Use this for initialization
    void Awake()
    {
        Name = "PanalPlayOption";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnEntering()
    {
        //Time.timeScale = 0;
        
        gameObject.SetActive(true);

        MusicOn.SetActive(AudioManager.Instance.bMusic);
        MusicOff.SetActive(!AudioManager.Instance.bMusic);
        SoundOn.SetActive(AudioManager.Instance.bSound);
        SoundOff.SetActive(!AudioManager.Instance.bSound);
    }

    public override void OnPausing()
    {

    }

    public override void OnResuming()
    {

    }

    public override void OnExitng()
    {
        //Time.timeScale = 1;
        gameObject.SetActive(false);
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

    public void ContinueGame()
    {
        //Time.timeScale = 1;
        UIManager.Instance.PopUIPanel();
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Time.timeScale = 0;

        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIMainMenu");
        UIManager.Instance.PushUIPanel("PanelReady");

        GameSceneManager.Instance.UnLoadChapter();
        //ObjectPool.Instance.Clean();
        //Statistic.Instance.ResetAllTimer();
    }

    //public void unloadtest()
    //{
    //    GameSceneManager.Instance.UnLoadChapter();
    //}

}
