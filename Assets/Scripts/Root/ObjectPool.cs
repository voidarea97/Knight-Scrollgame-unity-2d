using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    
    private static ObjectPool instance;
    private ObjectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
    }
    public static ObjectPool Instance
    {
        get{
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    // 对象池
    private Dictionary<string, List<GameObject>> pool;
    // 预设体
    private Dictionary<string, GameObject> prefabs;

    // 从对象池中获取对象
    public GameObject GetObject(string name)
    {
        //结果对象
        GameObject result = null;
        //判断是否有该名字的对象池
        if (pool.ContainsKey(name))
        {
            //对象池里有对象
            if (pool[name].Count > 0)
            {
                //获取结果
                result = pool[name][0];

                if (result != null) //防止返回无效对象
                {
                    //激活对象
                    //result.SetActive(true);
                    //从池中移除该对象
                    pool[name].Remove(result);
                    //返回结果
                    return result;
                }
                else
                {
                    pool[name].Remove(result);
                    Debug.Log("对象池含有失效对象");
                }
            }
        }
        //如果没有该名字的对象池或者该名字对象池没有对象

        GameObject prefab = null;
        //如果已经加载过该预设体
        if (prefabs.ContainsKey(name))
        {
            prefab = prefabs[name];
        }
        else     //如果没有加载过该预设体
        {
            //加载预设体
            prefab = Resources.Load<GameObject>("PrefabForPool/" + name);
            //更新字典
            prefabs.Add(name, prefab);
        }

        //生成
        result = UnityEngine.Object.Instantiate(prefab);
        //改名（去除 Clone）
        result.name = name;
        //result.SetActive(true);
        //返回
        return result;
    }

    /// <summary>
    /// 回收对象到对象池
    /// </summary>
    /// <param name="objName"></param>
    public void RecycleObject(GameObject obj)
    {
        //设置为非激活
        obj.SetActive(false);
        //判断是否有该对象的对象池
        if (pool.ContainsKey(obj.name))
        {
            //放置到该对象池
            pool[obj.name].Add(obj);
        }
        else
        {
            //创建该类型的池子，并将对象放入
            pool.Add(obj.name, new List<GameObject>() { obj });
        }

    }
    
    public void Clean() //清理失效对象
    {
        foreach(var keyValuePair in pool)
        {
            for(int i= keyValuePair.Value.Count-1; i>=0 ;i--)
            {
                var obj = keyValuePair.Value[i];
                if (obj == null)
                    keyValuePair.Value.Remove(obj);
            }

        }
    }
}
