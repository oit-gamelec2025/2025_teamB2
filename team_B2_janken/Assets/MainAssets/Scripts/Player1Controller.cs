using UnityEngine;
using UnityEngine.InputSystem; // 新Input System
using UnityEngine.UI;


public class Player1Script : MonoBehaviour
{
    //プレイヤー操作関連変数
    Rigidbody player1RigidBody;
    public GameObject Camera;
    float speed = 6.0f;

    //GameManagerと連携
    public GameObject gameManager;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        Vector3 move = Vector3.zero;

        //Wキーで前移動
        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward;
        }
        //Sキーで後ろ移動
        if (Input.GetKey(KeyCode.S))
        {
            move -= transform.forward;
        }
        //Dキーで右移動
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right;
        }
        //Aキーで左移動
        if (Input.GetKey(KeyCode.A))
        {
            move -= transform.right;
        }

        // ノーマライズして一定速度に保つ
        player1RigidBody.velocity = move.normalized * speed;
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

        //じゃんけんでドローになるときの処理
        if(gameObject.tag == other.gameObject.tag)
        {
            Debug.Log("Draw");
        }
    }
}
