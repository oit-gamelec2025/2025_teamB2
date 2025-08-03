using UnityEngine;
using UnityEngine.InputSystem; // �VInput System

public class Player1Script : MonoBehaviour
{
    Rigidbody player1RigidBody;
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
