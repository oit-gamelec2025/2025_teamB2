using UnityEngine;
using UnityEngine.InputSystem; // �VInput System�i���ݖ��g�p�j
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player1Script : MonoBehaviour
{
    //�ړ��֌W
    private Vector2 moveInput; // �������ǉ�: �X�e�B�b�N�̓��͒l��ێ�����

    //SE
    private AudioSource audioSource;
    public AudioClip RockSound;
    public AudioClip ScissorsSound;
    public AudioClip PaperSound;
    public AudioClip DrawSound;
    public AudioClip SpeedUP;
    public AudioClip ShieldSound;
    public AudioClip ChangeSound;

    //�v���C���[����֘A�ϐ�
    Rigidbody player1RigidBody;
    public GameObject Camera;
    public GameObject Born; //�v���C���[����
    float speed = 20.0f;
    int respron = 0;

    private PlayerInput playerInput;
    public enum PlayerIndex
    {
        Player1 = 0, // 1P
        Player2 = 1  // 2P
    };
    [Header("�v���C���[�ԍ�"), SerializeField] PlayerIndex playerIndex = PlayerIndex.Player1;

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

    //�A�C�e��
    float Itemtime = 0f;
    float reItemtime = 0f;
    public int ShieldFlag = 0;
    public GameObject ShiledTag;
    
    //GameManager�ƘA�g
    public GameObject gameManager;
    GameManager gameManagerScript; // GameManager�̃X�N���v�g���L���b�V������ϐ�

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g�擾
        playerInput = GetComponent<PlayerInput>();

        // �p�t�H�[�}���X����̂��߁AGameManager�̃X�N���v�g����x�����擾���ăL���b�V�����Ă���
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }

        audioSource = gameObject.AddComponent<AudioSource>(); //�ϐ��uaudioSource�v��AudioSource�R���|�l���g�����܂�

        var gamepads = Gamepad.all;
        var index = (int)playerIndex;

        if (gamepads.Count < index)
        {
            Debug.LogWarning($"�v���C���[�Ɋ��蓖�Ă�Q�[���R���g���[����������܂��� {index}");
            return;
        }

        // �ڑ�����Ă���R���g���[���[���v���C���[���͂֊��蓖��
        playerInput.SwitchCurrentControlScheme(gamepads[index]);

        ShiledTag.SetActive(false);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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

        // �������y�������炪�C���ӏ��ł��z������

        // �X�e�B�b�N�̓���(moveInput)���AXZ���ʂ�3D�x�N�g���ɕϊ�
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        // �X�e�B�b�N���킸���ł��|����Ă���ꍇ�̂ݏ���
        if (move.magnitude > 0.1f) // 0.1f�̓f�b�h�]�[���B�X�e�B�b�N�̗V�т𖳎�����臒l
        {
            // �X�e�B�b�N��|���������ɃL�����N�^�[�̌����iBorn�I�u�W�F�N�g�j���X���[�Y�Ɍ�����
            Born.transform.rotation = Quaternion.LookRotation(move);
        }
        else
        {
            // �X�e�B�b�N���|����Ă��Ȃ���Έړ��ʂ�0�ɂ���
            move = Vector3.zero;
        }

        // �ړ��x�N�g���𐳋K���i������1�Ɂj���A�X�s�[�h���|���čŏI�I�ȑ��x���v�Z
        Vector3 newVelocity = move.normalized * speed;
        // Y���i���������j�̑��x�́A�d�͂Ȃǂ̉e�����ێ����邽�߂Ɍ��݂̒l��ێ�
        newVelocity.y = player1RigidBody.velocity.y;
        player1RigidBody.velocity = newVelocity;

        // �A�j���[�^�[�ɕ��s���x��n��
        // ���������̑��x�̑傫���imagnitude�j���v�Z
        float MoveSpeed = new Vector3(player1RigidBody.velocity.x, 0, player1RigidBody.velocity.z).magnitude;
        animator.SetFloat("Walk", MoveSpeed);

        // ����������������������������������������������
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

        if(Itemtime > 0)
        {
            Itemtime -= Time.deltaTime;
            reItemtime = (int)Itemtime;
            if (reItemtime > 0)
            {
                speed = 35.0f;
            }
            else
            {
                speed = 20.0f;
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
                StartCoroutine(PlayRockSoundMultipleTimes(5, 0.5f));
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
            if (ShieldFlag == 0)
            {
                flags[4] = 1;
                animator.SetInteger(animations[4], flags[4]);
                StopFlag = 1;
                respron = 1;
                Debug.Log("Lose");
            }
            else
            {
                ShiledTag.SetActive(false);
                ShieldFlag = 0;
            }
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
            audioSource.PlayOneShot(ChangeSound);
        }
        if (other.gameObject.tag == "Hand_Scissors")
        {
            gameManagerScript.ChangeScissors01();
            audioSource.PlayOneShot(ChangeSound);
        }
        if (other.gameObject.tag == "Hand_Paper")
        {
            gameManagerScript.ChangePaper01();
            audioSource.PlayOneShot(ChangeSound);
        }
        if (other.gameObject.tag == "Speed")
        {
            Itemtime = 5f;
            audioSource.PlayOneShot(SpeedUP);
        }
        if (other.gameObject.tag == "Shield")
        {
            ShieldFlag = 1;
            ShiledTag.SetActive(true);
            audioSource.PlayOneShot(ShieldSound);
        }
        if (other.gameObject.tag == "Camera")
        {
            Camera.SetActive(true);
        }
    }

    private IEnumerator PlayRockSoundMultipleTimes(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            audioSource.PlayOneShot(RockSound);
            yield return new WaitForSeconds(interval); // ���̍Đ��܂ł̊Ԋu
        }
    }
}