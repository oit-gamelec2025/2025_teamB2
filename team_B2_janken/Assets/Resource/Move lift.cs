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
            // 上方向へ連続的に力を加える
            rb.AddForce(Vector3.up * forceAmount);
        }
        else
        {
            // 下方向へ連続的に力を加える
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
