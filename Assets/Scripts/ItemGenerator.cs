using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    //carPrefab������
    [SerializeField] private GameObject carPrefab;
    //coinPrefab������
    [SerializeField] private GameObject coinPrefab;
    //cornPrefab������
    [SerializeField] private GameObject conePrefab;
    //Unity����������
    private GameObject unityChan;

    //�X�^�[�g�n�_
    private int startPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //Unity������������
        unityChan = GameObject.Find("unitychan");

       /* //���̋������ƂɃA�C�e���𐶐�
        for (int i = startPos; i < goalPos; i += 15) {
            //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
            bool twoLessThan = Random.Range(1, 11) <= 2;
            //�R�[��
            bool coneProbability = Random.Range(1, 11) <= 2;
            if (coneProbability) ConeInstantiate(i);
            else LaneItemInstantiate(i, twoLessThan);
        }*/

    }

    void Update() {

        //Unity�����̑O��40m
        float unitychanPosFront = unityChan.transform.position.z + 50f;
        //�R�[�������̊���
        bool twoLessThan = Random.Range(1, 11) <= 2;
        if (startPos <= goalPos)
        if (startPos <= unitychanPosFront) LaneItemInstantiate(startPos, twoLessThan);
    }


    //�R�[���𐶐�����֐�
    private void ConeInstantiate(int i) {
        for (float j = -1f; j <= 1f; j += 0.4f) {
            //�R�[����x�������Ɉ꒼���ɐ���
            GameObject cone = Instantiate(conePrefab);
            cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
        }
    }

    //�R�C���𐶐�����֐�
    private void CoinInstantiate(int i,int j,int offsetZ) {
        GameObject coin = Instantiate(coinPrefab);
        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
    }

    //�Ԃ𐶐�����
    private void CarInstantiate(int i, int j,int offsetZ) {
        //�Ԃ𐶐�
        GameObject car = Instantiate(carPrefab);
        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
    }

    //���[�����ƂɃA�C�e���𐶐�����֐�
    private void LaneItemInstantiate(int i,bool twoLessThan) {
        startPos += 15;
        Debug.Log(startPos);
        if (twoLessThan) ConeInstantiate(i);
        else {

            for (int j = -1; j <= 1; j++) {
                //�A�C�e���̎�ނ����߂�
                bool coinProbability = 1 <= Random.Range(1, 11) && Random.Range(1, 11) <= 6;
                bool carProbability = 7 <= Random.Range(1, 11) && Random.Range(1, 11) <= 9;
                //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                int offsetZ = Random.Range(-5, 6);
                //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                if (coinProbability) CoinInstantiate(i, j,offsetZ);
                else if (carProbability) CarInstantiate(i, j,offsetZ);
            }
        }
    }
}
