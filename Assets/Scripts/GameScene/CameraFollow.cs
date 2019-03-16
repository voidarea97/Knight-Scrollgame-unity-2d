using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform heroTransform;
    float heroInitialY;
    Vector3 trans;

    public Vector3 cameraInitialPos;    //相机初始位置
    public float xOffset;  //镜头偏移
    public float yCoefficient;
    Vector3 posBuffer;

    public void FollowHero(GameObject hero)
    {
        //transform.position = cameraInitialPos;
        heroTransform = hero.transform;
        heroInitialY = heroTransform.position.y;
        
        Vector3 cameraPos = cameraInitialPos;
        cameraPos.x = heroTransform.position.x + xOffset;
        transform.position = cameraPos;
    }

    // Use this for initialization
    void Start()
    {
        cameraInitialPos = transform.position;
        //posBuffer = cameraInitialPos;
        //heroTransform = GameObject.FindWithTag("Hero").transform;
        //xDis = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroTransform!=null)
        {
            Vector3 vTrans = heroTransform.position;
            if (heroTransform.gameObject.GetComponent<Character>().xDirection)
            {
                if (Math.Abs(transform.position.x - heroTransform.position.x - xOffset) > 0.2)
                {
                    vTrans.x += xOffset;
                    //if (vTrans.x < 0)
                    //    vTrans.x = 0;
                    if (Math.Abs(transform.position.x - heroTransform.position.x - xOffset) > 2)
                        trans = Vector3.Lerp(transform.position, vTrans, 0.025f);
                    else
                        trans = Vector3.Lerp(transform.position, vTrans, 0.035f);
                    trans.y = transform.position.y;
                    trans.z = transform.position.z;
                    transform.position = trans;
                }
            }
            else
            {
                if (Math.Abs(transform.position.x - heroTransform.position.x + xOffset) > 0.2)
                {
                    //避免移出边界
                    vTrans.x -= xOffset;   //相机目标位置
                    //if (vTrans.x < 0)
                    //    vTrans.x = 0;
                    //大幅移动相机时更平滑
                    if (Math.Abs(transform.position.x - heroTransform.position.x + xOffset) > 2)
                        trans = Vector3.Lerp(transform.position, vTrans, 0.025f);
                    else
                        trans = Vector3.Lerp(transform.position, vTrans, 0.035f);

                    trans.y = transform.position.y;
                    trans.z = transform.position.z;
                    transform.position = trans;

                }
            }

            //if (Math.Abs(heroTransform.position.y - heroInitialY) > 0.01f)
            {
                float temp = (heroTransform.position.y - heroInitialY) * yCoefficient;
                //posBuffer = trans;
                trans.y = cameraInitialPos.y + temp;
                trans.z = cameraInitialPos.z - temp;
                //trans = posBuffer;
                transform.position = trans;
            }

        }
    }
}
