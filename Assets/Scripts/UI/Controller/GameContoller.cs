using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameContoller : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,
    IEndDragHandler,IPointerExitHandler
{

    RectTransform joyBack;
    public RectTransform canvas;
    public RectTransform joyMove;
    public RectTransform parentRect;

    Vector2 originalPos;
    Vector2 offset;
    public float moveRange;


    private Vector2 moveVector;
    private bool keyboardControl;

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标

        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);
        if (isRect)
        {
            //offset = uguiPos - joyBack.anchoredPosition;

            offset.x = uguiPos.x - (joyBack.anchoredPosition.x + parentRect.rect.width * (joyBack.anchorMax.x-0.5f));
            offset.y = uguiPos.y - (joyBack.anchoredPosition.y + parentRect.rect.height * (joyBack.anchorMax.y - 0.5f));
        }

        if (offset.x * offset.x + offset.y * offset.y > moveRange * moveRange)
        {
            offset.Normalize();
            offset.x *= moveRange;
            offset.y *= moveRange;
        }
        joyMove.anchoredPosition = offset + originalPos;

        offset.Normalize();
        GameMessage.Instance.OnControllerMove(offset.x, offset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
        joyMove.anchoredPosition = offset + originalPos;
        GameMessage.Instance.OnControllerMove(0, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset.x = mouseUguiPos.x - (joyBack.anchoredPosition.x + parentRect.rect.width * (joyBack.anchorMax.x - 0.5f));
            offset.y = mouseUguiPos.y - (joyBack.anchoredPosition.y + parentRect.rect.height * (joyBack.anchorMax.y - 0.5f));
            //offset = mouseUguiPos - joyBack.anchoredPosition;
        }

        if (offset.x * offset.x + offset.y * offset.y > moveRange * moveRange)
        {
            offset.Normalize();
            offset.x *= moveRange;
            offset.y *= moveRange;
        }
        joyMove.anchoredPosition = offset + originalPos;

        offset.Normalize();
        GameMessage.Instance.OnControllerMove(offset.x, offset.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
        joyMove.anchoredPosition = offset + originalPos; joyMove.anchoredPosition = offset + originalPos;
        GameMessage.Instance.OnControllerMove(0, 0);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        offset = Vector2.zero;
        joyMove.anchoredPosition = offset + originalPos; joyMove.anchoredPosition = offset + originalPos;
        GameMessage.Instance.OnControllerMove(0, 0);
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
            moveVector.y = 1;
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
        if (keyboardControl)
            GameMessage.Instance.OnControllerMove(moveVector.x, moveVector.y);

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameMessage.Instance.OnControllerButton(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameMessage.Instance.OnControllerButton(2);
        }
    }

    // Use this for initialization
    void Start()
    {
        joyBack = GetComponent<RectTransform>();
        offset = new Vector2(0, 0);
        originalPos = joyMove.anchoredPosition;

    }

    public void OnButton(int num)
    {
        GameMessage.Instance.OnControllerButton(num);
    }

}
