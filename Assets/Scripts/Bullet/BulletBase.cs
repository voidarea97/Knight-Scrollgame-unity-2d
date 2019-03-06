using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBase : MonoBehaviour {

    public enum BulletKind
    {
        MeleeF = 0, //近战跟随
        FlyF,   //飞行跟随
        SceneI, //环境独立
        FlyI    //飞行独立
    }

    private Rigidbody2D bulletRigidbody;


    public float speedX;
    public float speedY;
    public BulletKind kind;    //0:跟随(Local)  1：滞留|独立飞行(Gloabal)
    public float damage;
    public float survivalTime;  //生存时间
    public float survivalCollision; //可碰撞次数
    public bool xDirection;

    public float remainTime; //生存时间到后视觉残存时间，用于播放消散动画
    public float collisionRemainTime;   //碰撞后残存时间

    public bool noDestroy; //设置active而非销毁对象

    public float repelDis;  //击退距离
    public float slowTime;  //减速时间
    public float paralyseTime;  //麻痹时间

    public Vector2 direction;

    public GameObject belong;   //所属


	void Awake () {
        bulletRigidbody = gameObject.GetComponent<Rigidbody2D>();
        //if (bulletRigidbody)
        //    if (xDirection)
        //        bulletRigidbody.velocity = new Vector2(speedX, speedY); //初始化子弹速度与方向
        //    else
        //        bulletRigidbody.velocity = new Vector2(-speedX, speedY);
    }
    private void OnEnable()
    {        
        if (bulletRigidbody)
            if (xDirection)
                bulletRigidbody.velocity = new Vector2(speedX, speedY); //初始化子弹速度与方向
            else
                bulletRigidbody.velocity = new Vector2(-speedX, speedY);
    }

    virtual protected void FixedUpdate()
    {
        //survivalTime -= Time.deltaTime;
        ////bulletRigidbody.velocity = new Vector2(speedX, speedY);
        //if (survivalCollision<=0||survivalTime<=0)   //子弹碰撞次数或生存时间耗尽，销毁子弹
        //{
        //    gameObject.SetActive(false);
        //    if(!noDestroy)
        //    Destroy(gameObject);
        //}
        
    }
    virtual public void DestroyBullet()  //销毁子弹
    {
        gameObject.SetActive(false);
        if (!noDestroy)
        {
            Destroy(gameObject);
            return;
        }
        ObjectPool.Instance.RecycleObject(gameObject);

    }
}
