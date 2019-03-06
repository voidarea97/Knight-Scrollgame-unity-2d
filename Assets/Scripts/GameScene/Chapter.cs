using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour {

    public int number;  //章节序号
    private static readonly float WaitTime= 2f;   //过关等待时间
    GameObject hero;

	// Use this for initialization
	void Start () {
        hero = GameObject.FindWithTag("Hero");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Statistic.Instance.StartTiming();
        
    }

    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {

    }

    public void Settle()    //过关结算
    {
        Statistic.Instance.EndTiming();
        if (number + 1 > GameInfo.Instance.chapterProcess&&number+1<=GameInfo.maxchapter)
            GameInfo.Instance.chapterProcess = number + 1;

        hero.GetComponent<Character>().action = false;
        hero.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (hero.GetComponent<Animator>())
            hero.GetComponent<Animator>().Play("Win");
        Time.timeScale = 0;
        StartCoroutine(Common.Common.WaitTime(WaitTime,SettleLate));   
    }

    private void SettleLate()
    {
        UIManager.Instance.PushUIPanel("PanelWin");
    }
    //IEnumerator Wait(float time)
    //{
    //    yield return new WaitForSecondsRealtime(time);
    //}
}
