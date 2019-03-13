using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour {

    //protected Transform heroTransform;
    protected Rigidbody2D herorigidbody;
    protected CharacterAnime heroAnime;
    protected CharacterSkill heroSkill;

    //protected float xAxis, yAxis;
    //protected bool btn1, btn2, btn3;

    private Vector3 rotPositive;    //x轴正向
    private Vector3 rotNegative;    //x反向

    virtual protected void Start () {
        //heroTransform = gameObject.GetComponent<Transform>();
        herorigidbody = gameObject.GetComponent<Rigidbody2D>();
        heroAnime = gameObject.GetComponent<CharacterAnime>();
        heroSkill = gameObject.GetComponent<CharacterSkill>();

        rotPositive = new Vector3(-45, 0, 0);
        rotNegative = new Vector3(45, 180, 0);
        gameObject.GetComponent<Character>().xDirection = true;

        //joystick = GameObject.Find("UIPlay/Control/JoystickBack").GetComponent<PlayerController>();

        //初始化相机位置
        //Vector3 cameraPos = Camera.main.transform.position;
        //cameraPos.x =gameObject.transform.position.x + 3.5f;
        //Camera.main.transform.position = cameraPos;
        Camera.main.gameObject.GetComponent<CameraFollow>().FollowHero(gameObject);

        GameMessage.Instance.ControllerMoveEvent += new GameMessage.ControllerMoveHandler(ControllerMoveListener);
        GameMessage.Instance.ControllerButtonEvent += new GameMessage.ControllerButtonHandler(ControllerButtonListener);
    }

    private void OnDestroy()
    {
        GameMessage.Instance.ControllerMoveEvent -= ControllerMoveListener;
        GameMessage.Instance.ControllerButtonEvent -= ControllerButtonListener;
    }
    //   virtual protected void Update () {

    //}

    //   virtual protected void FixedUpdate()
    //   {

    //   }

    //virtual protected void CastSkill(Transform skillTrans)    //施放指定技能
    //{

    //    if (skillTrans.GetComponent<SkillProperty>().flashing <= 0) //未在冷却           
    //    {
    //        skillTrans.GetComponent<SkillProperty>().flashing = skillTrans.GetComponent<SkillProperty>().flash; //重置冷却
    //        gameObject.GetComponent<Hero1Skill>().Skill
    //            (skillTrans.gameObject.GetComponent<SkillProperty>().skillName);  //施放skill

    //        //后摇僵直
    //        GetComponent<Character>().action = false;
    //        herorigidbody.velocity = new Vector2(0, 0);
    //        //重置后摇时间
    //        skillTrans.GetComponent<SkillProperty>().staying = skillTrans.GetComponent<SkillProperty>().stay;

    //    }
    //}
    //virtual protected void CastSkill(SkillProperty skillProperty)    //施放指定技能
    //{
    //        heroSkill.Skill
    //            (skillProperty);  //施放skill
    //}

    virtual protected void ControllerMoveListener(float xAxis,float yAxis) //监听角色移动消息
    {
        //调整控制精度
        #region 
        if (xAxis >= 0.8)
            xAxis = 1;
        else if (xAxis <= 0.2 && xAxis >= -0.2)
            xAxis = 0;
        else if (xAxis <= -0.8)
            xAxis = -1;

        if (yAxis >= 0.8)
            yAxis = 1;
        else if (yAxis <= 0.2 && yAxis >= -0.2)
            yAxis = 0;
        else if (yAxis <= -0.8)
            yAxis = -1;
        #endregion
        if (Time.timeScale != 0&&gameObject.GetComponent<Character>().action)    //可行动
        {
            if (xAxis >= 0)
            {
                herorigidbody.velocity = new Vector2
                    (gameObject.GetComponent<Character>().speedX * xAxis, herorigidbody.velocity.y);


                if (xAxis > 0)  //向右走
                {
                    //调整身体方向
                    gameObject.transform.eulerAngles = rotPositive;
                    gameObject.GetComponent<Character>().xDirection = true;
                    //行走动画
                    heroAnime.StartRun();
                }

            }
            else    //向左走
            {
                herorigidbody.velocity = new Vector2
                    (gameObject.GetComponent<Character>().speedX * xAxis, herorigidbody.velocity.y);
                gameObject.transform.eulerAngles = rotNegative;
                gameObject.GetComponent<Character>().xDirection = false;
                heroAnime.StartRun();
            }
            //y轴移动
            if (yAxis >= 0)
            {

                herorigidbody.velocity = new Vector2
                    (herorigidbody.velocity.x, gameObject.GetComponent<Character>().speedY * yAxis);

                gameObject.GetComponent<Character>().yDirection = true;
                if (yAxis == 0)
                {
                    if (xAxis == 0) 
                    heroAnime.EndRun();
                }
                else
                    heroAnime.StartRun();
            }
            else
            {
                herorigidbody.velocity = new Vector2
                    (herorigidbody.velocity.x, gameObject.GetComponent<Character>().speedY * yAxis);
                //heroAnime.StartRun();
                gameObject.GetComponent<Character>().yDirection = false;
                heroAnime.StartRun();
            }

        }
    }
    virtual protected void ControllerButtonListener(int num)
    {
        heroSkill.Cast(num);
    }
}
