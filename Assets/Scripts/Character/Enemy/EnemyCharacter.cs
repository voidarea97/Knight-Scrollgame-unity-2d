using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {
    public enum EnemyStrategy
    {
        Melle=0,    //近战
        Remote,     //远程
        Hide        //远离目标
    }

    public GameObject gloableBullet;
    //public SkillProperty[] skills;

    protected Rigidbody2D selfRigidbody;
    protected EnemyAnime anime;
    protected Vector2 aimPos;   //目标位置
    protected Transform heroTransform;

    public float searchTimeDelay;    //搜寻目标延时
    //public float searchTime;
    //private float atkTimeDelay;     //攻击延时
    //public float atkTime;

    public float[] skillTimeDelay;
    private float[] skillTimer;
    public Vector2[] skillCastDis;

    private Vector3 rotPositive;    //x轴正向
    private Vector3 rotNegative;    //x反向

    public CharacterSkill characterSkill; 
    public CharacterAnime characterAnime;


    protected override void Awake()
    {
        skillTimer = new float[skillTimeDelay.Length];
        rotPositive = new Vector3(-45, 0, 0);
        rotNegative = new Vector3(45, 180, 0);
    }

    protected override void Start()
    {
        base.Start();
        anime = GetComponent<EnemyAnime>();
        heroTransform = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        characterSkill = GetComponent<CharacterSkill>();

        if (selfRigidbody != null)
            StartCoroutine(FollowAim());
    }

    protected override void OnEnable()
    {
        health = defaultHealth;
        alive = true;
        action = true;

        for (int i = 0; i < skillTimer.Length; i++)
        {
            skillTimer[i] = skillTimeDelay[i];
        }

        if(selfRigidbody != null)
            StartCoroutine(FollowAim());

    }

    protected override void FixedUpdate()
    {
        for(int i=0;i<skillTimer.Length;i++)    //技能施放间隔计时，按时施放
        {
            if (skillTimer[i] > 0)
            {
                skillTimer[i] -= Time.deltaTime;
            }
            else
            {
                if (action)
                {
                    if (Math.Abs(transform.position.x - heroTransform.position.x) < skillCastDis[i].x
                        && Math.Abs(transform.position.y - heroTransform.position.y) < skillCastDis[i].y)
                    {
                        characterSkill.Cast(i + 1);

                    }
                    skillTimer[i] = skillTimeDelay[i];
                }
            }
        }
        if(!action)
        {
            if (selfRigidbody != null)
                selfRigidbody.velocity = new Vector2(0, 0);
        }

    }



    public override void BeHit(BulletBase bulletBase)
    {
        if (anime)
        {
            anime.TriggerBehit();
        }
        //击退
        if (bulletBase.repelDis > 0)
        {
            if (xDirection)
                transform.position = new Vector3(transform.position.x - bulletBase.repelDis, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + bulletBase.repelDis, transform.position.y, transform.position.z);
        }

        //麻痹
        if (bulletBase.paralyseTime > 0)
        {
            StopCoroutine("Paralyse");
            StartCoroutine(Paralyse(bulletBase.paralyseTime));
        }

        //减速


        health -= bulletBase.damage;    //损失生命值等于子弹伤害
        GameMessage.Instance.OnShowNum(gameObject, bulletBase.damage);
        if (health <= 0)
            OnDie();
    }
    override protected void OnDie()
    {
        if (selfRigidbody != null)

        {
            selfRigidbody.velocity = new Vector2(0, 0);
            StopCoroutine("FollowAim");
        }

        Statistic.Instance.KillsAcc();
        die.Invoke();
        gameObject.SetActive(false);


        ObjectPool.Instance.RecycleObject(gameObject);
    }

    virtual protected IEnumerator FollowAim()   //获取目标位置
    {
        yield return new WaitForSeconds(searchTimeDelay);
        aimPos = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>().position;

        if (Math.Abs(transform.position.x - heroTransform.position.x) < 0.1)
            selfRigidbody.velocity = new Vector2(0, selfRigidbody.velocity.y);
        //0.001使为使子弹生成后碰撞正常生效，移动物体，待优化
        if (Math.Abs(transform.position.y - heroTransform.position.y) < 0.1)
            selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, 0);

        if (transform.position.x - heroTransform.position.x < -0.1)
        {
            selfRigidbody.velocity = new Vector2(speedX, selfRigidbody.velocity.y);
            //调整身体方向
            gameObject.transform.eulerAngles = rotPositive;
            xDirection = true;
        }
        else if (transform.position.x - heroTransform.position.x > 0.1)
        {
            selfRigidbody.velocity = new Vector2(-speedX, selfRigidbody.velocity.y);
            //调整身体方向
            gameObject.transform.eulerAngles = rotNegative;
            xDirection = false;
        }
        //else
        //    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        if (transform.position.y - heroTransform.position.y < -0.1)
        {
            selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, speedY);
        }
        else if (transform.position.y - heroTransform.position.y > 0.1)
        {
            selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, -speedY);
        }

        yield return StartCoroutine(FollowAim());  
    }

    protected IEnumerator Paralyse(float num)
    {
        action = false;
        yield return new WaitForSeconds(num);
        action = true;
    }
    //void Atk()
    //{
    //    characterSkill.Cast(1);
    //}
    //virtual protected IEnumerator AtkAim()   //攻击目标
    //{
    //    yield return new WaitForSeconds(searchTimeDelay);
    //    aimPos = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>().position;

    //    if (Math.Abs(transform.position.x - heroTransform.position.x) < 0.1)
    //        selfRigidbody.velocity = new Vector2(0.001f, selfRigidbody.velocity.y);
    //    //0.001使为使子弹生成后碰撞正常生效，移动物体，待优化
    //    if (Math.Abs(transform.position.y - heroTransform.position.y) < 0.1)
    //        selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, 0.001f);

    //    if (transform.position.x - heroTransform.position.x < -0.1)
    //    {
    //        selfRigidbody.velocity = new Vector2(speedX, selfRigidbody.velocity.y);
    //        //调整身体方向
    //        gameObject.transform.eulerAngles = rotPositive;
    //        xDirection = true;
    //    }
    //    else if (transform.position.x - heroTransform.position.x > 0.1)
    //    {
    //        selfRigidbody.velocity = new Vector2(-speedX, selfRigidbody.velocity.y);
    //        //调整身体方向
    //        gameObject.transform.eulerAngles = rotNegative;
    //        xDirection = false;
    //    }
    //    //else
    //    //    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

    //    if (transform.position.y - heroTransform.position.y < -0.1)
    //    {
    //        selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, speedY);
    //    }
    //    else if (transform.position.y - heroTransform.position.y > 0.1)
    //    {
    //        selfRigidbody.velocity = new Vector2(selfRigidbody.velocity.x, -speedY);
    //    }

    //    yield return StartCoroutine(FollowAim());
    //}
}
