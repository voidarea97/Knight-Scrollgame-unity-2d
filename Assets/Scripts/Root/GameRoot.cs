using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {

    private void Awake()
    {
        //UIManager.Instance.LoadAllUIResouces();
    }
    // Use this for initialization
    void Start () {
        UIManager.Instance.PushUIPanel("UIMainMenu");
        UIManager.Instance.PushUIPanel("PanelStart");
        //AudioManager.Instance.PlayBGM("Audio/bgm.mp3");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
