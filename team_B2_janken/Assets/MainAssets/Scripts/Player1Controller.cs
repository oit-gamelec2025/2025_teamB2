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

    //あいこで転んでから起き上がりの時間
    float totaltime = 1.5f;
    int retime = 0;

    //Animatorとの連携
    Animator animator;
    int DrawFlag = 0;

    //GameManagerと連携
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Animatorコンポーネント取得
    }

    private void Update()
    {
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
            if(Input.GetKey(KeyCode.W))
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

    void OnCollisionEnter(Collision other) //当たり判定での処理
    {
        //GameManager script01 = gameManager～～～が各所にあるのは今後、GameManager側で変数が増えたときにそれらを一度に取得できるようにしているためである（テスト段階だから気にしないで）

        //じゃんけんで勝利するときの処理欄
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Scissors")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            Debug.Log("Win");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Rock")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            Debug.Log("Win");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.AddScore_1(1);
            Debug.Log("Win");
        }

        //じゃんけんで敗北するときの処理欄
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Paper")
        {
            GameManager script01 = gameManager.GetComponent<GameManager>(); //GameManaerにアタッチしているスクリプトを取得
            script01.Resporn_test();//仮リスポーン
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

        //じゃんけんでドローになるときの処理
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
