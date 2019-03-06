using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStateMachine : MonoBehaviour {


    protected int state1;
    public float resetTime1 = 2f; //state1重置时间
    protected float resetTimer1;

    protected void Awake()
    {
        state1 = 0;
    }
    protected void FixedUpdate()
    {
        if (resetTimer1 > 0)
        {
            resetTimer1 -= Time.deltaTime;
            if (resetTimer1 <= 0)
                state1 = 0;
        }
    }

    public int JudgeState(int num)  //输入当前技能指令，判断状态，返回应释放的技能
    {
        if (num == 1)
        {
            if (state1 == 2)
            {
                //state1 = 0;
                return 3;
            }
            //state1++;
            //resetTimer1 = resetTime1;
        }
        return num;
    }

    public void TransformState(int num)  //根据已施放技能转换状态
    {
        if (num == 1)
        {
            state1++;
            resetTimer1 = resetTime1;
        }
        if (num !=1)
            state1 = 0;
    }
}
