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
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    
    
    // Start is called before the first frame update
    void Start()
    {

        //Unityちゃんを見つける
        unityChan = GameObject.Find("unitychan");

       /* //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15) {
            //どのアイテムを出すのかをランダムに設定
            bool twoLessThan = Random.Range(1, 11) <= 2;
            //コーン
            bool coneProbability = Random.Range(1, 11) <= 2;
            if (coneProbability) ConeInstantiate(i);
            else LaneItemInstantiate(i, twoLessThan);
        }*/

    }

    void Update() {

        //Unityちゃんの前方40m
        float unitychanPosFront = unityChan.transform.position.z + 50f;
        //コーン生成の割合
        bool twoLessThan = Random.Range(1, 11) <= 2;
        if (startPos <= goalPos)
        if (startPos <= unitychanPosFront) LaneItemInstantiate(startPos, twoLessThan);
    }


    //コーンを生成する関数
    private void ConeInstantiate(int i) {
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
    private void LaneItemInstantiate(int i,bool twoLessThan) {
        startPos += 15;
        Debug.Log(startPos);
        if (twoLessThan) ConeInstantiate(i);
        else {

            for (int j = -1; j <= 1; j++) {
                //アイテムの種類を決める
                bool coinProbability = 1 <= Random.Range(1, 11) && Random.Range(1, 11) <= 6;
                bool carProbability = 7 <= Random.Range(1, 11) && Random.Range(1, 11) <= 9;
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (coinProbability) CoinInstantiate(i, j,offsetZ);
                else if (carProbability) CarInstantiate(i, j,offsetZ);
            }
        }
    }
}
