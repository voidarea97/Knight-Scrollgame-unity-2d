using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {
    //单例
    private GameSceneManager() { }
    public static GameSceneManager Instance { get; private set; }

    //public string resourceDir;
    private ResourceRequest request;
    private Dictionary<int, GameObject> chapterPrefabDic;

    private void Awake()
    {
        Instance = this;
        chapterPrefabDic = new Dictionary<int, GameObject>();
    }

    public delegate void CallbackFunc();    //加载完成时调用
    public void LoadChapter(int num,CallbackFunc func)   //异步加载游戏场景，完成时回调传入函数
    {
        if(chapterPrefabDic.ContainsKey(num))
        {
            GameObject obj = Instantiate(chapterPrefabDic[num]);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(true);
            Statistic.Instance.ResetAllTimer(); //重置各种计数
            func();
        }
        else
            StartCoroutine(Load(num,func));
    }
	
    IEnumerator Load(int num,CallbackFunc func)
    {
        request = Resources.LoadAsync("Chapter/Chapter" + num.ToString());
        yield return request;

        //Debug.Log("异步加载完成了！");
        chapterPrefabDic.Add(num, request.asset as GameObject);
        GameObject obj = Instantiate(chapterPrefabDic[num]);
        obj.transform.SetParent(gameObject.transform);
        obj.SetActive(true);
        func();
    }

    public void UnLoadChapter()
    {
        Transform child = gameObject.transform.GetChild(0);
        child.gameObject.SetActive(false);
        Destroy(child.gameObject);
        StartCoroutine(LateClean());
        Statistic.Instance.ResetAllTimer(); //重置统计数据
    }
    IEnumerator LateClean() //等游戏场景销毁后清理对象池
    {
        yield return 2;
        ObjectPool.Instance.Clean();
    }
}
