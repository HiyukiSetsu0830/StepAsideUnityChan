using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //��]���J�n����p�x��ݒ�
        this.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
    }

    // Update is called once per frame
    void Update()
    {

        //��]
        this.transform.Rotate(0f, 3f, 0f);
    }
}
