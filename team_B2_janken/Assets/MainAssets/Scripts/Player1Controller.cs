using UnityEngine;
using UnityEngine.InputSystem; // 新Input System（現在未使用）
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player1Script : MonoBehaviour
{
    //移動関係
    private Vector2 moveInput; // ★これを追加: スティックの入力値を保持する

    //SE
    private AudioSource audioSource;
    public AudioClip RockSound;
    public AudioClip ScissorsSound;
    public AudioClip PaperSound;
    public AudioClip DrawSound;
    public AudioClip SpeedUP;
    public AudioClip ShieldSound;
    public AudioClip ChangeSound;

    //プレイヤー操作関連変数
    Rigidbody player1RigidBody;
    public GameObject Camera;
    public GameObject Born; //プレイヤー方向
    float speed = 20.0f;
    int respron = 0;

    private PlayerInput playerInput;
    public enum PlayerIndex
    {
        Player1 = 0, // 1P
        Player2 = 1  // 2P
    };
    [Header("プレイヤー番号"), SerializeField] PlayerIndex playerIndex = PlayerIndex.Player1;

    //あいこで転んでから起き上がりの時間
    float totaltime = 0f;
    int retime = 0;

    //Animatorとの連携
    Animator animator;
    string[] animations = new string[] { "DrawFlag", "RockFlag", "ScissorsFlag", "PaperFlag", "DownFlag" };
    int[] flags = new int[] { 0, 0, 0, 0, 0 };

    //操作停止
    int StopFlag = 0;
    float stoptime = 1f;
    float restoptime = 0f;

    //アイテム
    float Itemtime = 0f;
    float reItemtime = 0f;
    public int ShieldFlag = 0;
    public GameObject ShiledTag;
    
    //GameManagerと連携
    public GameObject gameManager;
    GameManager gameManagerScript; // GameManagerのスクリプトをキャッシュする変数

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animatorコンポーネント取得
        playerInput = GetComponent<PlayerInput>();

        // パフォーマンス向上のため、GameManagerのスクリプトを一度だけ取得してキャッシュしておく
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }

        audioSource = gameObject.AddComponent<AudioSource>(); //変数「audioSource」にAudioSourceコンポネントを入れます

        var gamepads = Gamepad.all;
        var index = (int)playerIndex;

        if (gamepads.Count < index)
        {
            Debug.LogWarning($"プレイヤーに割り当てるゲームコントローラが見つかりません {index}");
            return;
        }

        // 接続されているコントローラーをプレイヤー入力へ割り当て
        playerInput.SwitchCurrentControlScheme(gamepads[index]);

        ShiledTag.SetActive(false);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Rigidbodyを扱う処理はFixedUpdateで行うのが推奨されます
    void FixedUpdate()
    {
        // StopFlagが1のとき、またはgameManagerScriptがない場合は移動処理を行わない
        if (StopFlag == 1 || gameManagerScript == null)
        {
            // 停止中は速度を0にする（必要に応じて）
            player1RigidBody.velocity = new Vector3(0, player1RigidBody.velocity.y, 0);
            animator.SetFloat("Walk", 0);
            return;
        }

        // ▼▼▼【ここからが修正箇所です】▼▼▼

        // スティックの入力(moveInput)を、XZ平面の3Dベクトルに変換
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        // スティックがわずかでも倒されている場合のみ処理
        if (move.magnitude > 0.1f) // 0.1fはデッドゾーン。スティックの遊びを無視する閾値
        {
            // スティックを倒した方向にキャラクターの向き（Bornオブジェクト）をスムーズに向ける
            Born.transform.rotation = Quaternion.LookRotation(move);
        }
        else
        {
            // スティックが倒されていなければ移動量を0にする
            move = Vector3.zero;
        }

        // 移動ベクトルを正規化（長さを1に）し、スピードを掛けて最終的な速度を計算
        Vector3 newVelocity = move.normalized * speed;
        // Y軸（垂直方向）の速度は、重力などの影響を維持するために現在の値を保持
        newVelocity.y = player1RigidBody.velocity.y;
        player1RigidBody.velocity = newVelocity;

        // アニメーターに歩行速度を渡す
        // 水平方向の速度の大きさ（magnitude）を計算
        float MoveSpeed = new Vector3(player1RigidBody.velocity.x, 0, player1RigidBody.velocity.z).magnitude;
        animator.SetFloat("Walk", MoveSpeed);

        // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
    }

    void Update()
    {
        // 時間経過に関する処理はUpdateに残す
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


    void OnCollisionEnter(Collision other) //当たり判定での処理
    {
        if (gameManagerScript == null) return; // GameManagerがなければ何もしない

        stoptime = 1f;
        restoptime = 0;
        totaltime = 1f;
        retime = 0;

        //じゃんけんで勝利するときの処理欄
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

        //じゃんけんで敗北するときの処理欄
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

        //じゃんけんでドローになるときの処理
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
        if (gameManagerScript == null) return; // GameManagerがなければ何もしない

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
            yield return new WaitForSeconds(interval); // 次の再生までの間隔
        }
    }
}