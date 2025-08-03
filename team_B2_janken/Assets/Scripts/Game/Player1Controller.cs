using UnityEngine;
using UnityEngine.InputSystem; // 新Input System

public class Player1Script : MonoBehaviour
{
    Rigidbody player1RigidBody;
    Vector2 moveInput; // 左スティックの入力を保存する
    public GameObject Camera;
    private Vector3 _velocity;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();

        // 移動速度を保持
        _velocity = new Vector3(axis.x, 0, axis.y);
    }

    private void Update()
    {
        // オブジェクト移動
        transform.position += _velocity * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "Rock" && other.gameObject.tag == "Scissors")
        {
            Debug.Log("Win");
        }
        if (gameObject.tag == "Paper" && other.gameObject.tag == "Rock")
        {
            Debug.Log("Win");
        }
        if (gameObject.tag == "Scissors" && other.gameObject.tag == "Paper")
        {
            Debug.Log("Win");
        }

        if (gameObject.tag == "Rock" && other.gameObject.tag == "Paper")
        {
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

        if(gameObject.tag == other.gameObject.tag)
        {
            Debug.Log("Draw");
        }

    }
}
