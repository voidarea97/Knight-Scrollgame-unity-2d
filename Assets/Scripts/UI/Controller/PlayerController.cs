using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : ScrollRect
{
    //public float axisX;
    //public float axisY;
    //public bool btn1, btn2, btn3;

    //public HeroControl heroControl;
    private Vector2 moveVector;
    private bool keyboardControl;
    //摇杆能移动的半径
    private float _mRadius = 0f;

    //private Vector2 moveBackPos;

    //摇杆能移动的距离参数
    private readonly static float Dis = 0.8f;

    protected override void Start()
    {
        base.Start();
        moveVector = new Vector2(0, 0);
        keyboardControl = false;
        //heroControl = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroControl>();
        //heroControl.joystick = this;
        //能移动的半径 = 摇杆的宽 * Dis
        _mRadius = content.sizeDelta.x * Dis;

        //moveBackPos = transform.parent.transform.position;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        //获取摇杆，根据锚点的位置
        var contentPosition = content.anchoredPosition;

        //判断摇杆的位置是否大于半径
        if (contentPosition.magnitude > _mRadius)
        {
            //设置摇杆最远的位置
            contentPosition = contentPosition.normalized * _mRadius;
            SetContentAnchoredPosition(contentPosition);
        }

        // 最后 v2.x/y 就跟 Input中的 Horizontal Vertical 获取的值一样 
        Vector2 v2 = content.anchoredPosition.normalized;

        //axisX = v2.x;
        //axisY = v2.y;

        //Vector2 oppsitionVec = eventData.position;
        //oppsitionVec=oppsitionVec.normalized;
        //axisX = oppsitionVec.x;
        GameMessage.Instance.OnControllerMove(v2.x, v2.y);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SetContentAnchoredPosition(new Vector2(0, 0));

        //axisX = 0;
        //axisY = 0;
        GameMessage.Instance.OnControllerMove(0, 0);
    }

    public void OnButton(int num)
    {
        GameMessage.Instance.OnControllerButton(num);
    }

    private void Update()
    {
        keyboardControl = false;
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveVector.y = 0;
            keyboardControl = true;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveVector.x = 0;
            keyboardControl = true;
        }


        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y=1;
            keyboardControl = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y = -1;
            keyboardControl = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x = 1;
            keyboardControl = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x = -1;
            keyboardControl = true;
        }
        if(keyboardControl)
            GameMessage.Instance.OnControllerMove(moveVector.x,moveVector.y);

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameMessage.Instance.OnControllerButton(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameMessage.Instance.OnControllerButton(2);
        }
    }
}