using UnityEngine;
using UnityEngine.InputSystem; // �VInput System
using UnityEngine.UI;


public class Player1Script : MonoBehaviour
{
    //�v���C���[����֘A�ϐ�
    Rigidbody player1RigidBody;
    public GameObject Camera;
    public GameObject Born; //�v���C���[����
    float speed = 20.0f;

    //�������œ]��ł���N���オ��̎���
    float totaltime = 1.5f;
    int retime = 0;

    //Animator�Ƃ̘A�g
    Animator animator;
    int DrawFlag = 0;

    //GameManager�ƘA�g
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g�擾
    }

    private void Update()
    {
        Vector3 move = Vector3.zero;

        //W�L�[�őO�ړ�
        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward;
            Born.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        //S�L�[�Ō��ړ�
        if (Input.GetKey(KeyCode.S))
        {
            move -= transform.forward;
            Born.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        //D�L�[�ŉE�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right;
            Born.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            if(Input.GetKey(KeyCode.W))
            {
                Born.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Born.transform.rotation = Quaternion.Euler(0f, 135f, 0f);
            }
        }
        //A�L�[�ō��ړ�
        if (Input.GetKey(KeyCode.A))
        {
            move -= transform.right;
            Born.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            if (Input.GetKey(KeyCode.W))
            {
                Born.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Born.transform.rotation = Quaternion.Euler(0f, -135f, 0f);
            }
        }

        // �m�[�}���C�Y���Ĉ�葬�x�ɕۂ�
        player1RigidBody.velocity = move.normalized * speed; //Vextor�^�����璼�œn���Ȃ�
        float MoveSpeed = player1RigidBody.velocity.magnitude; //velocity�̑傫���𐔒l�ɕς���
        animator.SetFloat("Walk", MoveSpeed);

        if(DrawFlag == 1)
        {
            totaltime -= Time.deltaTime;
            retime = (int)totaltime;
            if (retime < 0)
            {
                DrawFlag = 0;
                animator.SetInteger("DrawFlag", DrawFlag);
            }
        }
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
            script01.Resporn_test();//�����X�|�[��
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
            DrawFlag = 1;
            animator.SetInteger("DrawFlag", DrawFlag);
            totaltime = 3;
            retime = 0;
            Debug.Log("Draw");
        }
    }
}
