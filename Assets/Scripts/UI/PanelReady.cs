using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelReady : UIBase {

    public override string ParentName()
    {
        return "UIMainMenu";
    }

    private int chapterNum;
    public Text textChapterNum;
    private int chapterProcess;

    public Text textNumHealth;
    private int health;
    private int healthTemp;
    public Text textNumAttack;
    private int attack;
    private int attackTemp;
    public Text textNumSkill1;
    private int skill1;
    private int skill1Temp;
    public Text textNumSkill2;
    private int skill2;
    private int skill2Temp;
    public Text textNumSkill3;
    private int skill3;
    private int skill3Temp;

    public Text textPoint;
    private int point;
    private int pointTemp;

    private int healthDelta;   //1 point对应的health
    private int attackDelta;

    private void Awake()
    {
        Name = "PanelSelect";
    }

    // Use this for initialization
    void Start()
    {
        
        //临时
        healthDelta = 10;
        attackDelta = 5;      
    }

    public override void OnEntering()
    {
        
        chapterProcess = GameInfo.Instance.chapterProcess;
        chapterNum = chapterProcess;

        health = GameInfo.Instance.health;
        healthTemp = health;
        attack = GameInfo.Instance.atk;
        attackTemp = attack;
        point = GameInfo.Instance.point;
        pointTemp = point;
        skill1 = GameInfo.Instance.skill1;
        skill1Temp = skill1;
        skill2 = GameInfo.Instance.skill2;
        skill2Temp = skill2;
        skill3 = GameInfo.Instance.skill3;
        skill3Temp = skill3;

        textNumHealth.text = health.ToString();
        textNumAttack.text = attack.ToString();
        textNumSkill1.text = skill1.ToString();
        textNumSkill2.text = skill2.ToString();
        textNumSkill3.text = skill3.ToString();
        textPoint.text = point.ToString();
        textChapterNum.text = chapterNum.ToString();

        gameObject.SetActive(true);
    }
    public override void OnPausing()
    {
        
    }
    public override void OnResuming()
    {
        
    }
    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public void GoToPlay()
    {
        GameSceneManager.Instance.LoadChapter(chapterNum,ToPlay);
    }

    private void ToPlay()
    {
        UIManager.Instance.PopAll();
        UIManager.Instance.PushUIPanel("UIPlay");
        Time.timeScale = 1;
    }

    public void ChangeChapter(int b)
    {
        if (chapterNum + b > 0 && chapterNum + b <= chapterProcess)
        {
            chapterNum += b;
            textChapterNum.text = chapterNum.ToString();
        }
    }

    public void ChangeProrerty(int num)
    {
        if (pointTemp > 0)  //剩余点数大于0，可以加点
        {
            switch (num)
            {
                case 1:
                    healthTemp += healthDelta;
                    textNumHealth.text = healthTemp.ToString();
                    break;
                case 2:
                    attackTemp += attackDelta;
                    textNumAttack.text = attackTemp.ToString();
                    break;
                case 3:
                    skill1Temp += 1;
                    textNumSkill1.text = skill1Temp.ToString();
                    break;
                case 4:
                    skill2Temp += 1;
                    textNumSkill2.text = skill2Temp.ToString();
                    break;
                case 5:
                    skill3Temp += 1;
                    textNumSkill3.text = skill3Temp.ToString();
                    break;
            }
            pointTemp--;
            textPoint.text = pointTemp.ToString();
        }
    }

    public void OnConfirm() //确认加点
    {
        health = healthTemp;
        attack = attackTemp;
        skill1 = skill1Temp;
        skill2 = skill2Temp;
        skill3 = skill3Temp;
        point = pointTemp;

        GameInfo.Instance.health = health;
        GameInfo.Instance.atk = attack;
        GameInfo.Instance.skill1 = skill1;
        GameInfo.Instance.skill2 = skill2;
        GameInfo.Instance.skill3 = skill3;
        GameInfo.Instance.point = point;
    }
    public void ResetPoint()    
    {
        healthTemp = health;
        attackTemp = attack;
        skill1Temp = skill1;
        skill2Temp = skill2;
        skill3Temp = skill3;
        pointTemp = point;

        textNumHealth.text = health.ToString();
        textNumAttack.text = attack.ToString();
        textNumSkill1.text = skill1.ToString();
        textNumSkill2.text = skill2.ToString();
        textNumSkill3.text = skill3.ToString();
        textPoint.text = point.ToString();
    }
}
