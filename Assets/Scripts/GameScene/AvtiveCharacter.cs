using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvtiveCharacter : MonoBehaviour {

    public GameObject[] Aims;   //待激活角色
    public int trrigerGroup;  //触发该机关的角色阵营
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())
        {
            if (collision.gameObject.GetComponent<Character>().group == trrigerGroup)
            {
                foreach (GameObject item in Aims)
                {
                    item.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
