using UnityEngine;
using UnityEngine.InputSystem; // 新Input System

public class Player1Script : MonoBehaviour
{
    Rigidbody player1RigidBody;
    float speed = 3.0f;
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
}
