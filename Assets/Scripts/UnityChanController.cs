using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前方向の速度
    private float velocityZ = 16f;
    //横方向の速度
    private float velocityX = 10f;
    //上方向の速度
    private float velocityY = 10f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減速させる係数
    private float coefficient = 0.99f;
    //ゲーム終了の判定
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    //スコアを表示するテキスト
    private GameObject scoreText;
    //得点
    private int score = 0;
    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
    //上ボタン押下の判定
    private bool isJButtonDown = false;

    
    void Start() {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1f);

        //RigidBodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のsateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update() {
        //ゲーム終了ならUnityちゃんの動きを減衰する
        if (this.isEnd) GameOver();
        //左移動キー
        bool isInputLeft = Input.GetKey(KeyCode.LeftArrow);
        //右移動キー
        bool isInputRight = Input.GetKey(KeyCode.RightArrow);
        //ジャンプキー
        bool isJump = Input.GetKeyDown(KeyCode.Space);
        //Jumpステートの場合はJumpにfalseをセットする
        bool isJumpSetFalse = this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        //移動が可能か判定
        bool canLeftMove = -this.movableRange < this.transform.position.x;
        bool canRightMove = this.transform.position.x < this.movableRange;
        bool canJump = this.transform.position.y < 0.5f;


        //横方向の入力による速度
        float inputVelocityX = 0f;

        //上方向の入力による速度
        float inputVelocityY = 0;


        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if (isInputLeft && canLeftMove || this.isLButtonDown && canLeftMove) inputVelocityX = -this.velocityX;
        if (isInputRight && canRightMove || this.isRButtonDown && canRightMove) inputVelocityX = this.velocityX;
        if (isJump && canJump || this.isJButtonDown && canJump) inputVelocityY = AddJumpSpeed(inputVelocityY);
        else inputVelocityY += this.myRigidbody.velocity.y;
        if (isJumpSetFalse) this.myAnimator.SetBool("Jump", false);

        //Unitychanに速度を与える
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);


    }

    private void OnTriggerEnter(Collider other) {

        //衝突判定
        bool carTag = other.gameObject.CompareTag("CarTag");
        bool trafficConeTag = other.gameObject.CompareTag("TrafficConeTag");
        bool goalTag = other.gameObject.CompareTag("GoalTag");
        bool coinTag = other.gameObject.CompareTag("CoinTag");

        //障害物に衝突した場合
        if (carTag || trafficConeTag) GameOverText();

        //ゴール地点に到達した場合
        if (goalTag) GoalText();

        //コインに衝突した場合
        if (coinTag) CoinCollision(other);
    }

    private void CoinCollision(Collider other) {

        //スコアを加算
        this.score += 10;
        //ScoreTextに獲得した点数を表示
        this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
        //パーティクルを再生
        GetComponent<ParticleSystem>().Play();
        //接触したコインのオブジェクトを破棄
        Destroy(other.gameObject);

    }

    //ジャンプ関数
    public float AddJumpSpeed(float inputVelocityY) {

        //ジャンプアニメを再生
        this.myAnimator.SetBool("Jump", true);
        inputVelocityY = inputVelocityY + this.velocityY;
        return inputVelocityY;
    }

    //ゲームオーバー関数
    public void GameOver() {
        this.velocityZ *= this.coefficient;
        this.velocityX *= this.coefficient;
        this.velocityY *= this.coefficient;
        this.myAnimator.speed *= coefficient;
    }

    //ゴール関数
    private void GoalText() {

        this.isEnd = true;
        this.stateText.GetComponent<Text>().text = "CLEAR!!";
    }

    //ゲームオーバー関数
    private void GameOverText() {

        this.isEnd = true;
        this.stateText.GetComponent<Text>().text = "GAME OVER";
    }

    //ジャンプボタンを押した場合の判定
    public void GetMyJumpButtonDown() {

        this.isJButtonDown = true;
    }

    //ジャンプボタンを離した場合の判定
    public void GetMyJumpButtonUp() {

        this.isJButtonDown = false;

    }

    //左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown() {

        this.isLButtonDown = true;

    }

    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp() {

        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown() {

        this.isRButtonDown = true;
    }

    //右ボタンを離した場合の処理
    public void GetMyRightButtonUp() {

        this.isRButtonDown = false;
    }
}
