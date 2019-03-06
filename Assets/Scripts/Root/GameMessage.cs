using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessage
{
    //消息,事件管理器

    //单例
    private static GameMessage _instance = new GameMessage();
    private GameMessage() { }
    public static GameMessage Instance
    {
        get
        {
            //if (_instance == null)
            //_instance = new UIManager();
            return _instance;
        }
    }

    //player控制器 移动消息
    public delegate void ControllerMoveHandler(float x, float y);
    public event ControllerMoveHandler ControllerMoveEvent = delegate { };
    public void OnControllerMove(float x,float y)
    {
        ControllerMoveEvent(x, y);
    }
    //player控制器 按钮消息
    public delegate void ControllerButtonHandler(int num);
    public event ControllerButtonHandler ControllerButtonEvent = delegate { };
    public void OnControllerButton(int num)
    {
        ControllerButtonEvent(num);
    }

    public delegate void ShowNumHandler(GameObject obj, float num);
    public event ShowNumHandler ShowNumEvent = delegate { };
    public void OnShowNum(GameObject obj,float num)
    {
        ShowNumEvent(obj, num);
    }
}
