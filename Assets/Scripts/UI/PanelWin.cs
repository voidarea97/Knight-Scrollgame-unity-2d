using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelWin : UIBase {
    public override string ParentName()
    {
        return "UIPlay";
    }

    public Text textKills;
    public Text textBeHit;
    public Text textCombo;
    public Text textTime;

 //   // Use this for initialization
 //   void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public override void OnEntering()
    {
        textKills.text = Statistic.Instance.Kills.ToString();
        textBeHit.text = Statistic.Instance.BeHit.ToString();
        textCombo.text = Statistic.Instance.MaxCombo.ToString();
        textTime.text = Statistic.Instance.GameTimer.ToString();

        gameObject.SetActive(true);
    }
    public override void OnResuming()
    {

    }
    public override void OnPausing()
    {

    }
    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public void Next()
    {
        Time.timeScale = 0;

        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIMainMenu");
        UIManager.Instance.PushUIPanel("PanelReady");

        GameSceneManager.Instance.UnLoadChapter();
        //ObjectPool.Instance.Clean();
        Statistic.Instance.ResetAllTimer();

    }

    public void MainMenu()
    {
        Time.timeScale = 0;

        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIMainMenu");
        UIManager.Instance.PushUIPanel("PanelStart");

        GameSceneManager.Instance.UnLoadChapter();

    }

    
}
