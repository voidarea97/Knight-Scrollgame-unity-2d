using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPoint : MonoBehaviour {

    public Chapter chapter;
    //private void Awake()
    //{
    //    gameObject.SetActive(false);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.GetComponent<Character>() != null)
            if(obj.GetComponent<Character>().group==1)
            {
                chapter.Settle();
            }
        gameObject.SetActive(false);
    }
}
