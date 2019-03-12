using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLose : UIBase {
    public override string ParentName()
    {
        return "UIPlay";
    }

    public GameObject heroGameobject;

    public override void OnEntering()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        heroGameobject = GameObject.FindGameObjectWithTag("Hero");
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

    public void Continue()
    {
        heroGameobject.GetComponent<HeroCharacter>().Revive();
        UIManager.Instance.PopUIPanel();
        Time.timeScale = 1;

    }

    public void Back()
    {
        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIMainMenu");
        UIManager.Instance.PushUIPanel("PanelReady");

        GameSceneManager.Instance.UnLoadChapter();

    }
}
