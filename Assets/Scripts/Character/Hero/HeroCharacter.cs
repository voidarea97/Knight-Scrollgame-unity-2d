using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : Character {

    private CharacterAnime heroAnime;
    private CharacterSkill heroSkill;
    //private Statistic statistic;


    public override void BeHit(BulletBase bulletBase)
    {
        health -= bulletBase.damage;    //损失生命值等于子弹伤害
        heroAnime.TriggerBehit();
        Statistic.Instance.BeHitAcc();  //统计被击数
        //statistic.behits++;
    }

    protected override void Start () {
 
        heroAnime = gameObject.GetComponent<CharacterAnime>();
        heroSkill = GetComponent<CharacterSkill>();


        //更新英雄属性
        level = GameInfo.Instance.heroLevel;
        defaultHealth = GameInfo.Instance.health;
        phyAtk = GameInfo.Instance.atk;
        if(heroSkill)
        {
            heroSkill.skills[0].level = GameInfo.Instance.skill1;
            heroSkill.skills[1].level = GameInfo.Instance.skill2;
            heroSkill.skills[2].level = GameInfo.Instance.skill3;
        }

        base.Start();
        //statistic = gameObject.GetComponent<Statistic>();
        //Camera.main.gameObject.GetComponent<CameraFollow>().FollowHero(gameObject);
    }

    //   protected override void Update () {
    //       base.Update();

    //}

    protected override void OnDie() //死亡
    {
        //base.OnDie();
        alive = false;
        action = false;

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        if(rigidbody2D!=null)
        {
            rigidbody2D.velocity = new Vector2(0, 0);
        }
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        if(boxCollider2D!=null)
        {
            boxCollider2D.enabled = false;
        }

        if (heroAnime)
        {
            heroAnime.PlayAnime("Die");
        }

        die.Invoke();
        StartCoroutine(Common.Common.WaitTime(1f, GameLose));
    }

    protected void GameLose()
    {
        UIManager.Instance.PushUIPanel("PanelLose");
    }

    public void Revive()    //复活
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = true;
        }

        health = defaultHealth;
        immune = true;
        action = true;
        alive = true;
        if(heroAnime)
        {
            heroAnime.PlayAnime("Ide");
        }
        StartCoroutine(Common.Common.WaitTime(1f, EndImmune));
    }
    protected void EndImmune()
    {
        immune = false;
    }
}
