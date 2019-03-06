using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelStart : UIBase
{
    public GameObject btnNewGame;
    public GameObject btnContinue;

    public override string ParentName()
    {
        return "UIMainMenu";
    }

    private void Awake()
    {
        Name = "PanelStart";

    }
    public override void OnEntering()
    {
        //GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
    }

    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public override void OnPausing()
    {
        btnNewGame.GetComponent<Button>().enabled = false;
        btnContinue.GetComponent<Button>().enabled = false;
        //gameObject.SetActive(false);
    }

    public override void OnResuming()
    {
        btnNewGame.GetComponent<Button>().enabled = true;
        btnContinue.GetComponent<Button>().enabled = true;
        gameObject.SetActive(true);
    }

    //public void GoToOption()
    //{
    //    UIManager.Instance.PushUIPanel("PanelOption");
    //}
    public void GoToReady()
    {
        GameInfo.Instance.Read();
        UIManager.Instance.PopUIPanel();
        UIManager.Instance.PushUIPanel("PanelReady");
    }
    public void GoToPlay()
    {
        GameInfo.Instance.Reset();
        GameSceneManager.Instance.LoadChapter(1, ToPlay);
    }

    private void ToPlay()
    {
        //Debug.Log("toplay");
        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIPlay");
        Time.timeScale = 1;
    }
}
