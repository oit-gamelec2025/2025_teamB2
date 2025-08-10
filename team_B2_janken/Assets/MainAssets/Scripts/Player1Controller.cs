using UnityEngine;
using UnityEngine.InputSystem; // �VInput System�i���ݖ��g�p�j
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player1Script : MonoBehaviour
{
    //SE
    private AudioSource audioSource;
    public AudioClip RockSound;
    public AudioClip ScissorsSound;
    public AudioClip PaperSound;
    public AudioClip LoseSound;
    public AudioClip DrawSound;

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

    //�����~
    int StopFlag = 0;
    float stoptime = 1f;
    float restoptime = 0f;

    //GameManager�ƘA�g
    public GameObject gameManager;
    GameManager gameManagerScript; // GameManager�̃X�N���v�g���L���b�V������ϐ�

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g�擾

        // �p�t�H�[�}���X����̂��߁AGameManager�̃X�N���v�g����x�����擾���ăL���b�V�����Ă���
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }

        audioSource = gameObject.AddComponent<AudioSource>(); //�ϐ��uaudioSource�v��AudioSource�R���|�l���g�����܂�
    }

    // Rigidbody������������FixedUpdate�ōs���̂���������܂�
    void FixedUpdate()
    {
        // StopFlag��1�̂Ƃ��A�܂���gameManagerScript���Ȃ��ꍇ�͈ړ��������s��Ȃ�
        if (StopFlag == 1 || gameManagerScript == null)
        {
            // ��~���͑��x��0�ɂ���i�K�v�ɉ����āj
            player1RigidBody.velocity = new Vector3(0, player1RigidBody.velocity.y, 0);
            animator.SetFloat("Walk", 0);
            return;
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

        // �������y�d�v�z�������C���_�ł� ������
        // ���������̑��x���v�Z���A���݂�Y�����x�͈ێ�����
        Vector3 newVelocity = move.normalized * speed;
        newVelocity.y = player1RigidBody.velocity.y;
        player1RigidBody.velocity = newVelocity;
        // ����������������������������������������������

        float MoveSpeed = new Vector3(player1RigidBody.velocity.x, 0, player1RigidBody.velocity.z).magnitude;
        animator.SetFloat("Walk", MoveSpeed);
    }

    void Update()
    {
        // ���Ԍo�߂Ɋւ��鏈����Update�Ɏc��
        if (StopFlag == 1)
        {
            stoptime -= Time.deltaTime;
            restoptime = (int)stoptime;
            if (restoptime < 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    flags[i] = 0;
                    animator.SetInteger(animations[i], flags[i]);
                }
                if (respron == 1 && gameManagerScript != null)
                {
                    gameManagerScript.Resporn_01();
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
        }
    }


    void OnCollisionEnter(Collision other) //�����蔻��ł̏���
    {
        if (gameManagerScript == null) return; // GameManager���Ȃ���Ή������Ȃ�

        stoptime = 1f;
        restoptime = 0;
        totaltime = 1f;
        retime = 0;

        //����񂯂�ŏ�������Ƃ��̏�����
        if ((gameObject.tag == "Rock" && other.gameObject.tag == "Scissors") ||
            (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper") ||
            (gameObject.tag == "Paper" && other.gameObject.tag == "Rock"))
        {
            gameManagerScript.AddScore_1(1);
            if (gameObject.tag == "Rock")
            {
                flags[1] = 1;
                audioSource.PlayOneShot(RockSound);
            }
            if (gameObject.tag == "Scissors")
            {
                flags[2] = 1;
                audioSource.PlayOneShot(ScissorsSound);
            }
            if (gameObject.tag == "Paper")
            {
                flags[3] = 1;
                audioSource.PlayOneShot(PaperSound);
            }

            for (int i = 1; i < 4; i++) animator.SetInteger(animations[i], flags[i]);

            StopFlag = 1;
            Debug.Log("Win");
        }

        //����񂯂�Ŕs�k����Ƃ��̏�����
        else if ((gameObject.tag == "Rock" && other.gameObject.tag == "Paper") ||
            (gameObject.tag == "Paper" && other.gameObject.tag == "Scissors") ||
            (gameObject.tag == "Scissors" && other.gameObject.tag == "Rock"))
        {
            flags[4] = 1;
            audioSource.PlayOneShot(LoseSound);
            animator.SetInteger(animations[4], flags[4]);
            StopFlag = 1;
            respron = 1;
            Debug.Log("Lose");
        }

        //����񂯂�Ńh���[�ɂȂ�Ƃ��̏���
        else if (gameObject.tag == other.gameObject.tag)
        {
            audioSource.PlayOneShot(DrawSound);
            flags[0] = 1;
            animator.SetInteger(animations[0], flags[0]);
            StopFlag = 1;
            Debug.Log("Draw");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameManagerScript == null) return; // GameManager���Ȃ���Ή������Ȃ�

        if (other.gameObject.tag == "Hand_Rock")
        {
            gameManagerScript.ChangeRock01();
        }
        if (other.gameObject.tag == "Hand_Scissors")
        {
            gameManagerScript.ChangeScissors01();
        }
        if (other.gameObject.tag == "Hand_Paper")
        {
            gameManagerScript.ChangePaper01();
        }
    }
}