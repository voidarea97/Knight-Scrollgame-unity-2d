using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo {

    private static GameInfo instance;
    private GameInfo()
    {
        Reset();
    }
    public static GameInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameInfo();
            }
            return instance;
        }
    }

    public static readonly int maxchapter = 1;
    
    public int chapterProcess;  //当前可进入的关卡
    public int heroLevel;
    public int atk;
    public int health;
    public int point;
    public int skill1;
    public int skill2;
    public int skill3;

    public void Reset() //重置角色属性和关卡进度
    {
        chapterProcess = 1;
        heroLevel = 1;
        atk = 10;
        health = 100;
        point = 0;
        skill1 = 1;
        skill2 = 1;
        skill3 = 1;
    }

    public void Read()
    {
        chapterProcess = PlayerPrefs.GetInt("Chapter",2);
        heroLevel = PlayerPrefs.GetInt("Level",1);
        health = PlayerPrefs.GetInt("Health",100);
        atk = PlayerPrefs.GetInt("Attack",10);
        point = PlayerPrefs.GetInt("Point", 5);
        skill1= PlayerPrefs.GetInt("Skill1", 1);
        skill2= PlayerPrefs.GetInt("Skill2", 1);
        skill3= PlayerPrefs.GetInt("Skill3", 1);
    }

    public void Store()
    {
        PlayerPrefs.SetInt("Chapter", chapterProcess);
        PlayerPrefs.SetInt("Level", heroLevel);
        PlayerPrefs.SetInt("Attack", atk);
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Point", point);
        PlayerPrefs.SetInt("Skill1", skill1);
        PlayerPrefs.SetInt("Skill2", skill2);
        PlayerPrefs.SetInt("Skill3", skill3);
    }
}
