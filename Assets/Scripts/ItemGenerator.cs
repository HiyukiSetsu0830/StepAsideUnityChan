using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    //carPrefabを入れる
    [SerializeField] private GameObject carPrefab;
    //coinPrefabを入れる
    [SerializeField] private GameObject coinPrefab;
    //cornPrefabを入れる
    [SerializeField] private GameObject conePrefab;
    //Unityちゃんを入れる
    private GameObject unityChan;

    //スタート地点
    private const int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //アイテム生成位置
    private int GenerateItemPos = startPos;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //Unityちゃんを見つける
        unityChan = GameObject.Find("unitychan");

    }

    void Update() {

        //Unityちゃんの前方40m
        float unitychanPosFront = unityChan.transform.position.z + 50f;
        //コーン生成の割合

        if (GenerateItemPos < goalPos && GenerateItemPos <= unitychanPosFront){
            
            LaneItemInstantiate(GenerateItemPos);
            GenerateItemPos += 15;
        }
    }


    //コーンを生成する関数
    private void ConeInstantiate(int i,int dice) {
        for (float j = -1f; j <= 1f; j += 0.4f) {
            
            //コーンをx軸方向に一直線に生成
            GameObject cone = Instantiate(conePrefab);
            cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
        }
    }

    //コインを生成する関数
    private void CoinInstantiate(int i,int j,int offsetZ) {
        GameObject coin = Instantiate(coinPrefab);
        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
    }

    //車を生成する
    private void CarInstantiate(int i, int j,int offsetZ) {
        //車を生成
        GameObject car = Instantiate(carPrefab);
        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
    }

    //レーンごとにアイテムを生成する関数
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
            //アイテムの種類を決める
            bool coinProbability = 1 <= coinOrCarDaice && coinOrCarDaice <= 6;
            bool carProbability = 7 <= coinOrCarDaice && coinOrCarDaice <= 9;
            //アイテムを置くZ座標のオフセットをランダムに設定
            int offsetZ = Random.Range(-5, 6);
            //60%コイン配置:30%車配置:10%何もなし
            if (coinProbability) CoinInstantiate(z, j, offsetZ);
            else if (carProbability) CarInstantiate(z, j, offsetZ);
        }

    }
}
