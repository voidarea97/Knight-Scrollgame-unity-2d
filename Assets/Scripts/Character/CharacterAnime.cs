using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnime : MonoBehaviour {

    Animator characterAnimator;

	void Start () {
        characterAnimator = gameObject.GetComponent<Animator>();
	}

    public void StartRun()
    {
        characterAnimator.SetBool("Run", true);
    }

    public void EndRun()
    {
        characterAnimator.SetBool("Run", false);
    }
    public void StartSkill(string skillName)
    {
        characterAnimator.SetBool(skillName, true);
        characterAnimator.Play(skillName);
    }
    public void EndSkill(string skillName)
    {
        characterAnimator.SetBool(skillName, false);
    }
    public void TriggerBehit()
    {
        characterAnimator.SetTrigger("BeHit");
    }

    public void PlayAnime(string name)
    {
        characterAnimator.Play(name);
    }
    //public void EndBehit()
    //{
    //    heroAnimator.SetBool("Behit", false);
    //}
}
