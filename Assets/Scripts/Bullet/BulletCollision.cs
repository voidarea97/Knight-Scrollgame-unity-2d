using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletCollision : MonoBehaviour {

    public UnityEvent onCollision;
    private BulletBase bulletBase;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //获取碰撞两方物体或角色数据
        GameObject objSelf = bulletBase.belong;
        GameObject objOther = collision.gameObject;
        Character characterSelf = objSelf.GetComponent<Character>();
        Character characterOther = objOther.GetComponent<Character>();

        if(objOther.tag=="Transparent")
        {
            return;
        }
        if(objOther.tag=="Hard")    //碰撞墙等硬实体阻挡物
        {
            if (gameObject.GetComponent<BulletBase>().kind == BulletBase.BulletKind.FlyI && (gameObject.GetComponent<Rigidbody2D>().velocity.x != 0
                || gameObject.GetComponent<Rigidbody2D>().velocity.y != 0))
            //bulletBase.survivalCollision = 0;   
            //直接销毁子弹
            {
                GetComponent<Collider2D>().enabled = false;
                Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
                if (rigidbody)
                {
                    rigidbody.velocity = new Vector2(0, 0);
                }
                Animator animator = GetComponent<Animator>();
                //播放碰撞消散动画
                if (animator)
                {
                    animator.SetTrigger("Collision");
                }
                StartCoroutine(Common.Common.WaitTime(bulletBase.collisionRemainTime, DestroyBullet));
            }

            return;
        }
        if (characterOther)
        {
            //如果子弹碰撞不同阵营角色
            if ((characterSelf.kind == 0 && (characterOther.kind > 100 && characterOther.kind < 200))
                || ((characterSelf.kind > 100 && characterSelf.kind < 200) && characterOther.kind == 0))
            {
                if (!characterOther.immune) //不处于免疫状态
                {
                    characterOther.BeHit(bulletBase);   //被击中角色的处理方法
                    if(--bulletBase.survivalCollision<=0)    //子弹剩余碰撞次数-1
                    {
                        GetComponent<Collider2D>().enabled = false;
                        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
                        if(rigidbody)
                        {
                            rigidbody.velocity = new Vector2(0, 0);
                        }
                        Animator animator = GetComponent<Animator>();
                        //播放碰撞消散动画
                        if (animator)
                        {
                            animator.SetTrigger("Collision");
                        }

                        //StartCoroutine(DelayDestroy(bulletBase.collisionRemainTime));     
                        //使用Common类的等待接口
                        StartCoroutine(Common.Common.WaitTime(bulletBase.collisionRemainTime, DestroyBullet));

                        //bulletBase.DestroyBullet();

                    }

                    if (characterSelf.kind == 0)   //计算连击
                    {
                        Statistic.Instance.ComboAcc();
                    }
                    onCollision.Invoke();   //附加碰撞事件
                }
            }
           
        }
        else
        {

        }
    }

    void Awake () {
        bulletBase = gameObject.GetComponent<BulletBase>();
        
    }
    private void OnEnable()
    {
        StartCoroutine(AutoDestroyBullet(bulletBase.survivalTime));  //延时销毁
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    //void Update () {

    //}

    IEnumerator AutoDestroyBullet(float waitTime) //生存时间到后销毁子弹
    {
        yield return new WaitForSeconds(waitTime);
        //碰撞体失效
        GetComponent<Collider2D>().enabled = false;        

        Animator animator = GetComponent<Animator>();
        //播放消散动画
        if(animator)
        {
            animator.SetTrigger("End");
        }
        //yield return StartCoroutine(DelayDestroy(bulletBase.remainTime));
        yield return StartCoroutine(Common.Common.WaitTime(bulletBase.remainTime, DestroyBullet));
        ////销毁子弹
        //GetComponent<Transform>().position = new Vector3(0, 100, 0);
        //bulletBase.DestroyBullet();
    }

    //IEnumerator DelayDestroy(float TimeDelay)   //延时销毁
    //{
    //    yield return new WaitForSeconds(TimeDelay);
    //    //销毁子弹
    //    GetComponent<Transform>().position = new Vector3(0, 100, 0);
    //    bulletBase.DestroyBullet();
    //}
    private void DestroyBullet()
    {
        GetComponent<Transform>().position = new Vector3(0, 100, 0);
        bulletBase.DestroyBullet();
    }
}
