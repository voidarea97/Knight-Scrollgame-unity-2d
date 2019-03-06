using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorInfo : MonoBehaviour {

    public GameObject healthText;
    public GameObject healthBar;
    public HeroCharacter heroCharacter;


    private RectTransform rectT;    //血量条rect
    private float healthDefaultWidth;   //血量条初始宽度

    
    private string health;
	
	void Awake () {

        
    }
    private void Start()
    {
        rectT = healthBar.GetComponent<RectTransform>();
        healthDefaultWidth = rectT.sizeDelta.x;

        heroCharacter = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroCharacter>();
    }
    void OnEnable()
    {
        Invoke("FindHero", 0.1f);
        //heroCharacter = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroCharacter>();
    }
    


    void FixedUpdate () {
        //更新血量
        health = "   "+ heroCharacter.health.ToString() + "/" + heroCharacter.defaultHealth.ToString();
        healthText.GetComponent<Text>().text = health;

        //调整血条,插值平滑变化
        //rectT.sizeDelta = new Vector2(healthDefaultWidth *
            //(heroCharacter.health / heroCharacter.defaultHealth), rectT.sizeDelta.y);
        rectT.sizeDelta = Vector2.Lerp(rectT.sizeDelta, new Vector2(healthDefaultWidth *
            (heroCharacter.health / heroCharacter.defaultHealth), rectT.sizeDelta.y),0.1f);

    }
    void FindHero()
    {
        heroCharacter = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroCharacter>();
    }
}
