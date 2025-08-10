using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private bool flag = true;
    public Rigidbody rb;
    public float forceAmount = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (flag)
        {
            // ������֘A���I�ɗ͂�������
            rb.AddForce(Vector3.up * forceAmount);
        }
        else
        {
            // �������֘A���I�ɗ͂�������
            rb.AddForce(Vector3.down * forceAmount);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PositionA")
        {
            flag = false;
            Debug.Log("UP");
        }
        else if (other.gameObject.tag == "PositionB")
        { 
            flag = true;
        }   
    }
}
