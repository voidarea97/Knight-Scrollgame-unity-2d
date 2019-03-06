using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpace : MonoBehaviour {

    public static readonly float NumRecycleTime = 1.5f;
	// Use this for initialization
	void Start () {
        GameMessage.Instance.ShowNumEvent += new GameMessage.ShowNumHandler(ShowDamage);
    }
	
    public void ShowDamage(GameObject obj, float num)      //显示伤害数字
    {
        //GameObject damageNum = Instantiate(damageNumPrefab, pos.transform.position, gameObject.transform.rotation);
        GameObject damageNum = ObjectPool.Instance.GetObject("damageNum");
        damageNum.transform.SetParent(gameObject.transform);
        damageNum.transform.localScale = new Vector3(1, 1, 1);
        damageNum.transform.localEulerAngles = new Vector3(0, 0, 0);

        damageNum.transform.position = obj.transform.position;

        Vector3 trans = damageNum.transform.position;
        trans.y += obj.GetComponent<SpriteRenderer>().bounds.size.y / 1.141f;
        //trans.y += 0.6f;
        damageNum.transform.position = trans;

        //damageNum.GetComponent<DamageNumFollow>().Getcharacter(pos);
        damageNum.GetComponent<Text>().text = num.ToString();

        damageNum.SetActive(true);

        StartCoroutine(RecycleDamageNum(damageNum));
    }

    protected IEnumerator RecycleDamageNum(GameObject damNum) //延时销毁伤害数字
    {
        yield return new WaitForSeconds(NumRecycleTime);
        ObjectPool.Instance.RecycleObject(damNum);
    }
}
