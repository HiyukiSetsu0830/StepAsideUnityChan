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
        //�A�C�e����Z���W���擾
        float itemPos = this.gameObject.transform.position.z;
        //Unity������Z���W���擾
        float unitychanPos = unitychan.transform.position.z;
        //Unity����񂪃I�u�W�F�N�g���O�ɏo����
        bool deletePos = unitychanPos >= itemPos;
        if (deletePos)Destroy(this.gameObject, 0.5f); 
    }
}
