using UnityEngine;
using UnityEngine.InputSystem; // �VInput System

public class Player1Script : MonoBehaviour
{
    Rigidbody player1RigidBody;
    float speed = 3.0f;
    Vector2 moveInput; // ���X�e�B�b�N�̓��͂�ۑ�����
    public GameObject Camera;
    private Vector3 _velocity;

    void Start()
    {
        player1RigidBody = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();

        // �ړ����x��ێ�
        _velocity = new Vector3(axis.x, 0, axis.y);
    }

    private void Update()
    {
        // �I�u�W�F�N�g�ړ�
        transform.position += _velocity * Time.deltaTime;
    }
}
