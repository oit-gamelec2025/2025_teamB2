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
    int respron = 0;

    //�������œ]��ł���N���オ��̎���
    float totaltime = 0f;
    int retime = 0;

    //Animator�Ƃ̘A�g
    Animator animator;
    string[] animations = new string[] { "DrawFlag", "RockFlag", "ScissorsFlag", "PaperFlag", "DownFlag" };
    int[] flags = new int[] { 0, 0, 0, 0, 0 };
    //flags[0]��DrawFlag
    //flags[1]��RockFlag
    //flags[2]��ScissorsFlag
    //flags[3]��PaperFlag
    //flags[4]��DownFlag

    //�����~
    int StopFlag = 0;
    float stoptime = 1f;
    float restoptime = 0f;

    //GameManager�ƘA�g
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g�擾
    }

    private void Update()
    {
        if (StopFlag == 1)
        {
            stoptime -= Time.deltaTime;
            restoptime = (int)stoptime;
            if (restoptime < 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    flags[i] = 0;
                    animator.SetInteger(animations[i],flags[i]);
                }
                if (respron == 1)
                {
                    GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
                    script01.Resporn_test();
                }
                StopFlag = 0;
                respron = 0;
            }
        }
        else
        {
            if (flags[0] == 1)
            {
                totaltime -= Time.deltaTime;
                retime = (int)totaltime;
                if (retime < 0)
                {
                    flags[0] = 0;
                    animator.SetInteger(animations[0], flags[0]);
                }
            }

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
                if (Input.GetKey(KeyCode.W))
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
        }
    }

    void OnCollisionEnter(Collision other) //�����蔻��ł̏���
    {
        //GameManager script01 = gameManager�`�`�`���e���ɂ���͍̂���AGameManager���ŕϐ����������Ƃ��ɂ�������x�Ɏ擾�ł���悤�ɂ��Ă��邽�߂ł���i�e�X�g�i�K������C�ɂ��Ȃ��Łj

        stoptime = 1f;
        restoptime = 0;
        totaltime = 1f;
        retime = 0;

        //����񂯂�ŏ�������Ƃ��̏�����
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Scissors")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            flags[1] = 1;
            animator.SetInteger(animations[1], flags[1]);
            StopFlag = 1;
            Debug.Log("Win");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            flags[2] = 1;
            animator.SetInteger(animations[2], flags[2]);
            StopFlag = 1;
            Debug.Log("Win");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Rock")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            script01.AddScore_1(1);
            flags[3] = 1;
            animator.SetInteger(animations[3], flags[3]);
            StopFlag = 1;
            Debug.Log("Win");
        }

        //����񂯂�Ŕs�k����Ƃ��̏�����
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaer�ɃA�^�b�`���Ă���X�N���v�g���擾
            flags[4] = 1;
            animator.SetInteger(animations[4], flags[4]);
            StopFlag = 1;
            respron = 1;
            Debug.Log("Lose");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Scissors")
        {
            flags[4] = 1;
            animator.SetInteger("DownFlag", flags[4]);
            StopFlag = 1;
            respron = 1;
            Debug.Log("Lose");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Rock")
        {
            flags[4] = 1;
            animator.SetInteger("DownFlag", flags[4]);
            StopFlag = 1;
            respron = 1;
            Debug.Log("Lose");
        }

        //����񂯂�Ńh���[�ɂȂ�Ƃ��̏���
        if(gameObject.tag == other.gameObject.tag)
        {
            flags[0] = 1;
            animator.SetInteger(animations[0], flags[0]);
            StopFlag = 1;
            Debug.Log("Draw");
        }
    }
}
