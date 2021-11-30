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
    private const int startPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    //�A�C�e�������ʒu
    private int GenerateItemPos = startPos;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //Unity������������
        unityChan = GameObject.Find("unitychan");

    }

    void Update() {

        //Unity�����̑O��40m
        float unitychanPosFront = unityChan.transform.position.z + 50f;
        //�R�[�������̊���

        if (GenerateItemPos < goalPos && GenerateItemPos <= unitychanPosFront){
            
            LaneItemInstantiate(GenerateItemPos);
            GenerateItemPos += 15;
        }
    }


    //�R�[���𐶐�����֐�
    private void ConeInstantiate(int i,int dice) {
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
    private void LaneItemInstantiate(int z) {
        int dice = Random.Range(1, 11);
        bool coneGenerate = dice <= 2;
        if (coneGenerate) ConeInstantiate(z,dice);
        else {
            GenarateCoinOrCar(z); 
        }
    }

    private void GenarateCoinOrCar(int z) {

        for (int j = -1; j <= 1; j++) {

            int coinOrCarDaice = Random.Range(1, 11);
            //�A�C�e���̎�ނ����߂�
            bool coinProbability = 1 <= coinOrCarDaice && coinOrCarDaice <= 6;
            bool carProbability = 7 <= coinOrCarDaice && coinOrCarDaice <= 9;
            //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
            int offsetZ = Random.Range(-5, 6);
            //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
            if (coinProbability) CoinInstantiate(z, j, offsetZ);
            else if (carProbability) CarInstantiate(z, j, offsetZ);
        }

    }
}
