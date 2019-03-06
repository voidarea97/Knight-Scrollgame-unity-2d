using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProperty : MonoBehaviour {

    public string skillName;

    public float[] flash; //冷却时间
    public float flashing;  //当前剩余冷却时间

    public int level; //等级
    public int maxLevel;

    public float prestay;   //前摇
    public float stay;  //后摇
    //public float staying;
    public float charactorAnimeTime; //角色释放技能动画持续时间
    //public float EndAnimeStartTime; //技能结束效果动画开始时间
    public int damageType;  //0:real    1:phy   2:mag


    public float phyCoefficient;    //物理加成系数
    //public float magCoefficient;    //魔法加成
    public float[] baseCoefficient;   //基础伤害

    public float damage;

    public float xOffset;
    public float yOffset;

    public float repelDis;  //击退距离
    public float paralyseTime;  //麻痹时间

    private void Awake()
    {
        //baseCoefficient = new float[maxLevel];
    }


    void FixedUpdate()
    {
        if (flashing > 0)   //冷却中
            flashing -= Time.deltaTime;
        //if (staying > 0)    //后摇中
        //{
        //    staying -= Time.deltaTime;
        //    if (staying <= 0)
        //        transform.parent.GetComponent<Character>().action = true;
        //}
    }

    public float CalculateDamage()
    {
        return baseCoefficient[level - 1] + phyCoefficient * transform.parent.GetComponent<Character>().phyAtk;
    }
}
