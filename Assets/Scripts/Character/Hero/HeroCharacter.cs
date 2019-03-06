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

    protected override void Update () {
        base.Update();
        
	}

    public void Death()
    {
        UIManager.Instance.PushUIPanel("PanelEnd");
    }
}
