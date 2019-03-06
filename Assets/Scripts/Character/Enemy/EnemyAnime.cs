using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnime : MonoBehaviour {
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void StartRun()
    {
        animator.SetBool("Run", true);
    }

    public void EndRun()
    {
        animator.SetBool("Run", false);
    }
    public void StartSkill(string skillName)
    {
        animator.SetBool(skillName, true);
    }
    public void EndSkill(string skillName)
    {
        animator.SetBool(skillName, false);
    }
    public void TriggerBehit()
    {
        animator.SetTrigger("BeHit");
    }
    public void TriggerAtk()
    {
        animator.SetTrigger("Atk");
    }
}
