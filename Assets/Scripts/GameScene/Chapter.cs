using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour {

    //public GameObject Heros;
    public int number;  //章节序号
    public int point;   //过关获得点数
    private static readonly float WaitTime= 2f;   //过关等待时间
    GameObject hero;
    public Transform startPoint;

	// Use this for initialization
	void Start () {
        //hero = GameObject.FindWithTag("Hero");
        Initiallize();
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}

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

    public void Initiallize()
    {
        //Quaternion rotation = Quaternion.Euler(-45, 0, 0);
        GameObject heroPrefab = Resources.Load("Character/Hero/Hero1/Hero1") as GameObject;
        hero = Instantiate(heroPrefab,startPoint.position,startPoint.rotation,gameObject.transform);
        hero.SetActive(true);
        Camera.main.gameObject.GetComponent<CameraFollow>().FollowHero(hero);
    }

    public void Settle()    //过关结算
    {
        Statistic.Instance.EndTiming();

        hero.GetComponent<Character>().action = false;
        hero.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (hero.GetComponent<Animator>())
            hero.GetComponent<Animator>().Play("Win");
        Time.timeScale = 0;

        StartCoroutine(Common.Common.WaitTime(WaitTime, SettleLate));

        if (number + 1 > GameInfo.Instance.chapterProcess && number + 1 <= GameInfo.maxchapter)
            GameInfo.Instance.chapterProcess = number + 1;
        GameInfo.Instance.point += point;

        GameInfo.Instance.Store();
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
