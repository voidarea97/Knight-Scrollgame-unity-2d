using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : Character {

    private CharacterAnime heroAnime;
    //private Statistic statistic;


    public override void BeHit(BulletBase bulletBase)
    {
        health -= bulletBase.damage;    //损失生命值等于子弹伤害
        heroAnime.TriggerBehit();
        Statistic.Instance.BeHitAcc();
        //statistic.behits++;
    }

    protected override void Start () {
        base.Start();
        heroAnime = gameObject.GetComponent<CharacterAnime>();
        //statistic = gameObject.GetComponent<Statistic>();
        Camera.main.gameObject.GetComponent<CameraFollow>().FollowHero(gameObject);
    }

    //   protected override void Update () {
    //       base.Update();

    //}

    protected override void OnDie()
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
        die.Invoke();
        StartCoroutine(Common.Common.WaitTime(1f, GameLose));
    }

    protected void GameLose()
    {
        UIManager.Instance.PushUIPanel("PanelLose");
    }

    public void Revive()
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
        StartCoroutine(Common.Common.WaitTime(1f, EndImmune));
    }
    protected void EndImmune()
    {
        immune = false;
    }
}
