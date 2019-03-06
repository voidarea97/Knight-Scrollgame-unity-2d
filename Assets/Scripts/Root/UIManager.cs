using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager {

    //单例模式
    private static UIManager _instance = new UIManager();
    private UIManager() {}
    public static UIManager Instance
    {
        get
        {
            //if (_instance == null)
                //_instance = new UIManager();
            return _instance;
        }
    }

    private Stack<UIBase> UIStack = new Stack<UIBase>();    //UI栈
    private Dictionary<string, GameObject> UIPrefabDict = new Dictionary<string, GameObject>(); //已加载资源目录
    private Dictionary<string, UIBase> UIDict = new Dictionary<string, UIBase>(); //  已创建的UI目录

    private Transform UIParent; //所有UI的父物体
    private string ResourcesDir = "UI";  //资源路径

    public void LoadAllUIResouces()    //从资源目录加载现有UI资源
    {
        string path;
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/" + ResourcesDir;
#elif UNITY_ANDROID
        path = Application.streamingAssetsPath + "/Resources/" + ResourcesDir;
#endif
        DirectoryInfo folder = new DirectoryInfo(path);
        foreach (FileInfo file in folder.GetFiles("*.Prefab"))
        {
            int index = file.Name.LastIndexOf(".");
            string UIName = file.Name.Substring(0, index);
            string UIPath = ResourcesDir + "/" + UIName;
            GameObject UIObject = Resources.Load<GameObject>(UIPath);
            UIPrefabDict.Add(UIName, UIObject);
        }
    }

    public void PushUIPanel(string UIName)  //UI入栈
    {

        if (UIStack.Count > 0) //栈不为空
        {
            if (UIStack.Peek().Name == UIName)
                return;
            UIBase OldTopUI = UIStack.Peek();
            OldTopUI.OnPausing();
        }
        UIBase NewTopUI = GetUIBase(UIName);
        UIStack.Push(NewTopUI);
        NewTopUI.OnEntering();


    }

    public void PopUIPanel()    //UI出栈
    {
        if (UIStack.Count == 0)
        {
            return;
        }
        UIBase oldTopUI = UIStack.Pop();
        oldTopUI.OnExitng();
        if (UIStack.Count > 0)
        {
            UIStack.Peek().OnResuming();
        }
    }
    public void PopAll()
    {
        while (UIStack.Count > 0)
        {
            UIBase oldTopUI = UIStack.Pop();
            oldTopUI.OnExitng();
            if (UIStack.Count > 0)
                UIStack.Peek().OnResuming();
        }
        return;
    }
    private UIBase GetUIBase(string UIName)
    {
        foreach (var name in UIDict.Keys)    //该UI面板已创建
        {
            if (name == UIName)
            {
                return UIDict[name];
            }
        }

        //从Prefab创建UI面板
        if (!UIPrefabDict.ContainsKey(UIName))
        {
            LoadResouce(UIName);
        }
        GameObject UIPrefab = UIPrefabDict[UIName];
        GameObject UIObject = GameObject.Instantiate<GameObject>(UIPrefab);

        UIBase uiBase = UIObject.GetComponent<UIBase>();
        if (uiBase.ParentName() != "")
        {
            UIParent = (GameObject.Find(uiBase.ParentName())).transform;

            UIObject.transform.SetParent(UIParent, false);
        }

        UIObject.name = UIName;


        UIDict.Add(UIName, uiBase);
        return uiBase;
    }

    public void LoadResouce(string name)    //加载资源
    {
        string UIPath = ResourcesDir + "/" + name;
        GameObject UIObject = Resources.Load<GameObject>(UIPath);
        if (UIObject)
            UIPrefabDict.Add(name, UIObject);
    }
}
