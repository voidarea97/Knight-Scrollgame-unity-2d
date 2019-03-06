using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlay : UIBase {

    //private static readonly float NumShowTime = 1.5f;
    //public GameObject damageNumPrefab;
    public GameObject addButton;

    public override string ParentName()
    {
        return "UI";
    }

    void Awake()
    {
        Name = "UIPlay";

        //damageNumPrefab= Resources.Load<GameObject>("UI/DamageNum");
        //Mediator.Instance.SetUIPlay(this);      //设置与场景通信的中介
        //GameMessage.Instance.ShowNumEvent += new GameMessage.ShowNumHandler(ShowDamage);
    }

    public override void OnEntering()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        gameObject.SetActive(true);
        Time.timeScale = 1;

    }

    public override void OnExitng()
    {
        gameObject.SetActive(false);
    }

    public override void OnPausing()
    {
        //gameObject.SetActive(false);
    }

    public override void OnResuming()
    {
        //gameObject.SetActive(true);
    }

    public void BackToStart()
    {
        UIManager.Instance.PopUIPanel();
        UIManager.Instance.PushUIPanel("UIMainMenu");

    }
    public void GoToPlayOption()
    {
        Time.timeScale = 0;
        UIManager.Instance.PushUIPanel("PanelPlayOption");
    }
    public void GoToSelect()
    {
        UIManager.Instance.PushUIPanel("PanelSelect");
    }
    public void AddEnemy()
    {
        UIManager.Instance.PushUIPanel("PanelEnemyAdder");
    }

     
    //public void ShowDamage(GameObject obj, float num)      //显示伤害数字
    //{
    //    //GameObject damageNum = Instantiate(damageNumPrefab, pos.transform.position, gameObject.transform.rotation);
    //    GameObject damageNum = ObjectPool.Instance.GetObject("damageNum");
    //    damageNum.transform.SetParent(gameObject.transform);
    //    damageNum.transform.localScale = new Vector3(1, 1, 1);
    //    damageNum.transform.localEulerAngles = new Vector3(0, 0, 0);
    //    Vector3 trans = obj.transform.position;
    //    trans.y += obj.GetComponent<SpriteRenderer>().bounds.size.y / 2;      
    //    damageNum.transform.position = trans;

    //    //damageNum.GetComponent<DamageNumFollow>().Getcharacter(pos);
    //    damageNum.GetComponent<Text>().text = num.ToString();
    //    StartCoroutine(RecycleDamageNum(damageNum));
    //}

    //protected IEnumerator RecycleDamageNum(GameObject damNum) //延时销毁伤害数字
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    ObjectPool.Instance.RecycleObject(damNum);
    //}

    //public void SetAddBtn(bool b)   //是否显示添加敌人按钮
    //{
    //    addButton.SetActive(b);
    //}






}
