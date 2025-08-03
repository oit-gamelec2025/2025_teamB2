using UnityEngine;
using UnityEngine.InputSystem; // �VInput System
using UnityEngine.UI;


public class Player1Script : MonoBehaviour
{
    //�v���C���[����֘A�ϐ�
    Rigidbody player1RigidBody;
    public GameObject Camera;
    float speed = 6.0f;

    //GameManager�ƘA�g
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        Vector3 move = Vector3.zero;

        //W�L�[�őO�ړ�
        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward;
        }
        //S�L�[�Ō��ړ�
        if (Input.GetKey(KeyCode.S))
        {
            move -= transform.forward;
        }
        //D�L�[�ŉE�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right;
        }
        //A�L�[�ō��ړ�
        if (Input.GetKey(KeyCode.A))
        {
            move -= transform.right;
        }

        // �m�[�}���C�Y���Ĉ�葬�x�ɕۂ�
        player1RigidBody.velocity = move.normalized * speed;
    }

    void OnCollisionEnter(Collision other) //�����蔻��ł̏���
    {
        //GameManager script01 = gameManager�`�`�`���e���ɂ���͍̂���AGameManager���ŕϐ����������Ƃ��ɂ�������x�Ɏ擾�ł���悤�ɂ��Ă��邽�߂ł���i�e�X�g�i�K������C�ɂ��Ȃ��Łj

        //����񂯂�ŏ�������Ƃ��̏�����
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Scissors")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            Debug.Log("Win");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Rock")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            Debug.Log("Win");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            Debug.Log("Win");
        }

        //����񂯂�Ŕs�k����Ƃ��̏�����
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.Resporn_test();
            Debug.Log("Lose");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Scissors")
        {
            Debug.Log("Lose");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Rock")
        {
            Debug.Log("Lose");
        }

        //����񂯂�Ńh���[�ɂȂ�Ƃ��̏���
        if(gameObject.tag == other.gameObject.tag)
        {
            Debug.Log("Draw");
        }
    }
}
