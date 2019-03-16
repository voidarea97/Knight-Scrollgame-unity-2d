using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenetateCharactor : MonoBehaviour {

    public string charactorName;
    public int num;
    public Vector2[] offsets;
    public GameObject parent;

	// Use this for initialization
	void Start () {
        parent = GameObject.FindGameObjectWithTag("Enemys");

        for (int i = 0; i < num; i++)
        {
           
            Vector3 offset = gameObject.transform.position;            
                offset.x += offsets[i].x;

            offset.y += offsets[i].y;

            GameObject obj = ObjectPool.Instance.GetObject(charactorName);
            obj.SetActive(false);
            //obj.GetComponent<Character>().group = -1;
            obj.transform.position = offset;
            obj.transform.SetParent(parent.transform);
            obj.SetActive(true);
        }
        Destroy(gameObject);
	}

}
