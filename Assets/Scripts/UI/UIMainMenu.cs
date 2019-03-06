using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UIBase
{

    public override string ParentName()
    {
        return "UI";
    }
    private void Awake()
    {
        Name = "UIMainMenu";
    }

    public override void OnEntering()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        GetComponent<Canvas>().worldCamera = Camera.main;

        //ObjectPool.Instance.Clean();
        //UIManager.Instance.PushUIPanel("PanelStart");
        //AudioManager.Instance.PlayBGM("bgm");
    }

    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public override void OnPausing()
    {
        //gameObject.SetActive(false);
    }

    public override void OnResuming()
    {
        //gameObject.SetActive(true);
    }
    public void GoToOption()
    {
        UIManager.Instance.PushUIPanel("PanelOption");
    }
}
