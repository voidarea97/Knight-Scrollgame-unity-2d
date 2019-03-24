using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour {

    public SkillProperty []skills;
    protected CharacterAnime characterAnime;
    protected CharacterAudio characterAudio;
    protected Rigidbody2D characterRigidbody;
    public GameObject globalBullet;
    public SkillStateMachine stateMachine;

    //public SkillProperty skillProperty1;
    //public SkillProperty skillProperty2;
    //public SkillProperty skillProperty3;

    //protected Dictionary<string, GameObject> skillDict
    //    = new Dictionary<string, GameObject>();  //技能子弹名称与其预制体对应字典

    //protected string ResourcesDir = "Character/Hero/";  //资源路径


    //public void Skill(SkillProperty skillProperty)
    //{

    //    if (skillProperty.flashing <= 0) //未在冷却           
    //    {
    //        skillProperty.flashing = skillProperty.flash; //重置冷却
    //        {
    //            //技能动画
    //            //heroAnime.StartSkill(skillProperty.skillName);

    //            //产生技能子弹               
    //            Vector3 offset = gameObject.transform.position;
    //                //调整子弹位置
    //            bool xDir = gameObject.GetComponent<Character>().xDirection;
    //            if (xDir)
    //                offset.x += skillProperty.xOffset;
    //            else
    //                offset.x -= skillProperty.xOffset;
    //            offset.y += skillProperty.yOffset;
    //            GameObject skillBullet = Instantiate(skillDict[skillProperty.skillName + "Bullet"],
    //                offset, transform.rotation);


    //            if(skillBullet.GetComponent<BulletBase>().kind == BulletBase.BulletKind.FlyI||
    //                skillBullet.GetComponent<BulletBase>().kind==BulletBase.BulletKind.SceneI)
    //                skillBullet.transform.SetParent(gloableBullet.transform, true);
    //            else
    //                skillBullet.transform.SetParent(gameObject.transform, true);
    //            //确定方向
    //            //float x = -1;
    //            //if (gameObject.GetComponent<Character>().xDirection)
    //            //    x = 1;
    //            //skill1Bullet.GetComponent<BulletBase>().direction = new Vector2(x, 0);
    //            skillBullet.GetComponent<BulletBase>().xDirection = xDir;
    //            skillBullet.GetComponent<BulletBase>().belong = gameObject;
    //            //确定子弹技能伤害
    //            skillBullet.GetComponent<BulletBase>().damage = skillProperty.damage;
    //            ////设置播放技能效果结束动画时间
    //            //Animator animator = skillBullet.GetComponent<Animator>();
    //            //if (animator)
    //            //{
    //            //    StartCoroutine(EndSkillAnime(animator, skillProperty.EndAnimeStartTime));
    //            //    //animator.SetBool("End", true);
    //            //}
    //            skillBullet.SetActive(true);
    //        }

    //        //后摇僵直
    //        GetComponent<Character>().action = false;
    //        herorigidbody.velocity = new Vector2(0, 0);
    //        //重置后摇时间
    //        skillProperty.staying = skillProperty.stay;

    //        //延时结束技能动画
    //        StartCoroutine(SkillAnimeEnd(skillProperty.skillName,skillProperty.charactorAnimeTime));
    //    }

    //}

    public void Cast(int num)
    {
        if (num > skills.Length)
            return;
        if(stateMachine!=null)
            num=stateMachine.JudgeState(num);  //通过状态机现在状态判断当前应该释放的技能
        SkillProperty skillProperty = skills[num - 1];
        if (skillProperty == null)
            return;

        if (GetComponent<Character>().action==true && skillProperty.flashing <= 0) //技能未在冷却
        {
            skillProperty.flashing = skillProperty.flash[skillProperty.level-1]; //重置冷却
            {

                //释放技能成功，状态机转换
                if (stateMachine != null)
                    stateMachine.TransformState(num);
                //技能动画
                if (characterAnime != null)
                {
                    characterAnime.EndRun();
                    characterAnime.StartSkill(skillProperty.skillName);
                }
                if(characterAudio!=null)
                {
                    if(AudioManager.Instance.BSound)
                        characterAudio.PlayAudio(skillProperty.skillName);
                }

                
                Vector3 offset = gameObject.transform.position;
                //调整子弹位置
                bool xDir = gameObject.GetComponent<Character>().xDirection;
                if (xDir)
                    offset.x += skillProperty.xOffset;
                else
                    offset.x -= skillProperty.xOffset;
                offset.y += skillProperty.yOffset;
                //GameObject skillBullet = Instantiate(skillDict[skillProperty.skillName + "Bullet"],
                //    offset, transform.rotation);

                GameObject skillBullet = ObjectPool.Instance.GetObject(skillProperty.skillName + "Bullet");
                skillBullet.SetActive(false);
                BulletBase bulletBase = skillBullet.GetComponent<BulletBase>();
                if (bulletBase != null)
                {
                    //确定方向
                    bulletBase.xDirection = xDir;
                    bulletBase.belong = gameObject;
                    //确定子弹技能伤害和效果
                    bulletBase.damage = skillProperty.CalculateDamage();
                    bulletBase.repelDis = skillProperty.repelDis;
                    bulletBase.paralyseTime = skillProperty.paralyseTime;

                }
                //skillBullet.SetActive(true);
                StartCoroutine(GenerateBullet(skillProperty.prestay,skillBullet,offset));    //前摇结束后子弹生效

                
            }

            //后摇僵直
            GetComponent<Character>().action = false;
            characterRigidbody.velocity = new Vector2(0, 0);
            //重置后摇时间
            //skillProperty.staying = skillProperty.stay;

            //延时结束技能动画
            //StartCoroutine(SkillAnimeEnd(skillProperty.skillName, skillProperty.charactorAnimeTime));
            //延时结束后摇
            StartCoroutine(SkillStayEnd(skillProperty.stay));
        }
    }

    virtual protected void Awake()
    {
        
    }

    virtual protected void Start()
    {
        characterAnime = gameObject.GetComponent<CharacterAnime>();
        characterAudio = gameObject.GetComponent<CharacterAudio>();
        characterRigidbody = gameObject.GetComponent<Rigidbody2D>();
        globalBullet = GameObject.FindGameObjectWithTag("GlobalBullet");
    }

    //virtual protected void CastStateMechine(ref int num)    //技能释放状态机，用于连续技
    //{

    //}

    //protected void LoadResouce(string name)    //加载资源
    //{
    //    string Path = ResourcesDir + "/" + name;
    //    GameObject Object = Resources.Load<GameObject>(Path);
    //    if (Object)
    //        skillDict.Add(name, Object);
    //}

    protected IEnumerator SkillAnimeEnd(string name, float time) //延时结束角色释放技能动画
    {
        yield return new WaitForSeconds(time);
        characterAnime.EndSkill(name);

    }
    protected IEnumerator SkillStayEnd(float time) //延时结束后摇
    {
        yield return new WaitForSeconds(time);
        GetComponent<Character>().action = true;

    }
    protected IEnumerator GenerateBullet(float num,GameObject skillBullet,Vector3 offset)   //前摇结束子弹生效
    {
        yield return new WaitForSeconds(num);

        if(skillBullet.GetComponent<BoxCollider2D>()!=null)
            skillBullet.GetComponent<BoxCollider2D>().enabled = true;
        skillBullet.SetActive(true);
        skillBullet.GetComponent<Transform>().position = offset;
        skillBullet.GetComponent<Transform>().rotation = transform.rotation;

        BulletBase bulletBase = skillBullet.GetComponent<BulletBase>();
        if (bulletBase != null)
        {
            if (bulletBase.kind == BulletBase.BulletKind.FlyI ||
                bulletBase.kind == BulletBase.BulletKind.SceneI)
                skillBullet.transform.SetParent(globalBullet.transform, true);
            else
                skillBullet.transform.SetParent(gameObject.transform, true);
        }
        else
        {
            skillBullet.transform.SetParent(gameObject.transform, true);
        }
        //子弹动画
        Animator bulletAnime = skillBullet.GetComponent<Animator>();
        if (bulletAnime)
        {
            bulletAnime.SetTrigger("Start");
        }
    }

    //protected IEnumerator EndSkillAnime(Animator animator,float time) //开始播放技能停止动画,
    //{
    //    yield return new WaitForSeconds(time);
    //    animator.SetTrigger("End");
    //}
}
