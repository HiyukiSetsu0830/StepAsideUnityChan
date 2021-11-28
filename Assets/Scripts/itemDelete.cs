using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDelete : MonoBehaviour
{

    private GameObject unitychan;

    void Start() {

        unitychan = GameObject.Find("unitychan");


    }
    // Update is called once per frame
    void Update()
    {
        //アイテムのZ座標を取得
        float itemPos = this.gameObject.transform.position.z;
        //UnityちゃんのZ座標を取得
        float unitychanPos = unitychan.transform.position.z;
        //Unityちゃんがオブジェクトより前に出たら
        bool deletePos = unitychanPos >= itemPos;
        if (deletePos)Destroy(this.gameObject, 0.5f); 
    }
}
