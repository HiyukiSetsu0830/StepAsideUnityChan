using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Unity�������ړ�������R���|�[�l���g������
    private Rigidbody myRigidbody;
    //�O�����̑��x
    private float velocityZ = 16f;
    //�������̑��x
    private float velocityX = 10f;
    //������̑��x
    private float velocityY = 10f;
    //���E�̈ړ��ł���͈�
    private float movableRange = 3.4f;
    //����������������W��
    private float coefficient = 0.99f;
    //�Q�[���I���̔���
    private bool isEnd = false;
    //�Q�[���I�����ɕ\������e�L�X�g
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g
    private GameObject scoreText;
    //���_
    private int score = 0;
    //���{�^�������̔���
    private bool isLButtonDown = false;
    //�E�{�^�������̔���
    private bool isRButtonDown = false;
    //��{�^�������̔���
    private bool isJButtonDown = false;

    
    void Start() {

        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        this.myAnimator.SetFloat("Speed", 1f);

        //RigidBody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody>();

        //�V�[������sateText�I�u�W�F�N�g���擾
        this.stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update() {
        //�Q�[���I���Ȃ�Unity�����̓�������������
        if (this.isEnd) GameOver();
        //���ړ��L�[
        bool isInputLeft = Input.GetKey(KeyCode.LeftArrow);
        //�E�ړ��L�[
        bool isInputRight = Input.GetKey(KeyCode.RightArrow);
        //�W�����v�L�[
        bool isJump = Input.GetKeyDown(KeyCode.Space);
        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����
        bool isJumpSetFalse = this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        //�ړ����\������
        bool canLeftMove = -this.movableRange < this.transform.position.x;
        bool canRightMove = this.transform.position.x < this.movableRange;
        bool canJump = this.transform.position.y < 0.5f;


        //�������̓��͂ɂ�鑬�x
        float inputVelocityX = 0f;

        //������̓��͂ɂ�鑬�x
        float inputVelocityY = 0;


        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������
        if (isInputLeft && canLeftMove || this.isLButtonDown && canLeftMove) inputVelocityX = -this.velocityX;
        if (isInputRight && canRightMove || this.isRButtonDown && canRightMove) inputVelocityX = this.velocityX;
        if (isJump && canJump || this.isJButtonDown && canJump) inputVelocityY = AddJumpSpeed(inputVelocityY);
        else inputVelocityY += this.myRigidbody.velocity.y;
        if (isJumpSetFalse) this.myAnimator.SetBool("Jump", false);

        //Unitychan�ɑ��x��^����
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);


    }

    private void OnTriggerEnter(Collider other) {

        //�Փ˔���
        bool carTag = other.gameObject.CompareTag("CarTag");
        bool trafficConeTag = other.gameObject.CompareTag("TrafficConeTag");
        bool goalTag = other.gameObject.CompareTag("GoalTag");
        bool coinTag = other.gameObject.CompareTag("CoinTag");

        //��Q���ɏՓ˂����ꍇ
        if (carTag || trafficConeTag) GameOverText();

        //�S�[���n�_�ɓ��B�����ꍇ
        if (goalTag) GoalText();

        //�R�C���ɏՓ˂����ꍇ
        if (coinTag) CoinCollision(other);
    }

    private void CoinCollision(Collider other) {

        //�X�R�A�����Z
        this.score += 10;
        //ScoreText�Ɋl�������_����\��
        this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
        //�p�[�e�B�N�����Đ�
        GetComponent<ParticleSystem>().Play();
        //�ڐG�����R�C���̃I�u�W�F�N�g��j��
        Destroy(other.gameObject);

    }

    //�W�����v�֐�
    public float AddJumpSpeed(float inputVelocityY) {

        //�W�����v�A�j�����Đ�
        this.myAnimator.SetBool("Jump", true);
        inputVelocityY = inputVelocityY + this.velocityY;
        return inputVelocityY;
    }

    //�Q�[���I�[�o�[�֐�
    public void GameOver() {
        this.velocityZ *= this.coefficient;
        this.velocityX *= this.coefficient;
        this.velocityY *= this.coefficient;
        this.myAnimator.speed *= coefficient;
    }

    //�S�[���֐�
    private void GoalText() {

        this.isEnd = true;
        this.stateText.GetComponent<Text>().text = "CLEAR!!";
    }

    //�Q�[���I�[�o�[�֐�
    private void GameOverText() {

        this.isEnd = true;
        this.stateText.GetComponent<Text>().text = "GAME OVER";
    }

    //�W�����v�{�^�����������ꍇ�̔���
    public void GetMyJumpButtonDown() {

        this.isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̔���
    public void GetMyJumpButtonUp() {

        this.isJButtonDown = false;

    }

    //���{�^���������������ꍇ�̏���
    public void GetMyLeftButtonDown() {

        this.isLButtonDown = true;

    }

    //���{�^���𗣂����ꍇ�̏���
    public void GetMyLeftButtonUp() {

        this.isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏���
    public void GetMyRightButtonDown() {

        this.isRButtonDown = true;
    }

    //�E�{�^���𗣂����ꍇ�̏���
    public void GetMyRightButtonUp() {

        this.isRButtonDown = false;
    }
}
