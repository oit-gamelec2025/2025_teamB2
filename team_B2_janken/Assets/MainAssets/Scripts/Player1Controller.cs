using UnityEngine;
using UnityEngine.InputSystem; // 新Input System
using UnityEngine.UI;


public class Player1Script : MonoBehaviour
{
    //プレイヤー操作関連変数
    Rigidbody player1RigidBody;
    public GameObject Camera;
    public GameObject Born; //プレイヤー方向
    float speed = 20.0f;
    int respron = 0;

    //あいこで転んでから起き上がりの時間
    float totaltime = 0f;
    int retime = 0;

    //Animatorとの連携
    Animator animator;
    string[] animations = new string[] { "DrawFlag", "RockFlag", "ScissorsFlag", "PaperFlag", "DownFlag" };
    int[] flags = new int[] { 0, 0, 0, 0, 0 };
    //flags[0]←DrawFlag
    //flags[1]←RockFlag
    //flags[2]←ScissorsFlag
    //flags[3]←PaperFlag
    //flags[4]←DownFlag

    //操作停止
    int StopFlag = 0;
    float stoptime = 1f;
    float restoptime = 0f;

    //GameManagerと連携
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animatorコンポーネント取得
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
                    GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
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

            //Wキーで前移動
            if (Input.GetKey(KeyCode.W))
            {
                move += transform.forward;
                Born.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            //Sキーで後ろ移動
            if (Input.GetKey(KeyCode.S))
            {
                move -= transform.forward;
                Born.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            //Dキーで右移動
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
            //Aキーで左移動
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

            // ノーマライズして一定速度に保つ
            player1RigidBody.velocity = move.normalized * speed; //Vextor型だから直で渡せない
            float MoveSpeed = player1RigidBody.velocity.magnitude; //velocityの大きさを数値に変える
            animator.SetFloat("Walk", MoveSpeed);
        }
    }

    void OnCollisionEnter(Collision other) //当たり判定での処理
    {
        //GameManager script01 = gameManager～～～が各所にあるのは今後、GameManager側で変数が増えたときにそれらを一度に取得できるようにしているためである（テスト段階だから気にしないで）

        stoptime = 1f;
        restoptime = 0;
        totaltime = 1f;
        retime = 0;

        //じゃんけんで勝利するときの処理欄
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Scissors")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            flags[1] = 1;
            animator.SetInteger(animations[1], flags[1]);
            StopFlag = 1;
            Debug.Log("Win");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            flags[2] = 1;
            animator.SetInteger(animations[2], flags[2]);
            StopFlag = 1;
            Debug.Log("Win");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Rock")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            flags[3] = 1;
            animator.SetInteger(animations[3], flags[3]);
            StopFlag = 1;
            Debug.Log("Win");
        }

        //じゃんけんで敗北するときの処理欄
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
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

        //じゃんけんでドローになるときの処理
        if(gameObject.tag == other.gameObject.tag)
        {
            flags[0] = 1;
            animator.SetInteger(animations[0], flags[0]);
            StopFlag = 1;
            Debug.Log("Draw");
        }
    }
}
