using UnityEngine;

public class RobotController : MonoBehaviour
{
    // 突進する力
    public float dashForce = 20.0f;
    // 突進のクールダウン時間
    public float dashCooldown = 1.0f;
    // 突進が可能な状態か
    private bool canDash = true;

    private Rigidbody rb;
    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the robot!");
        }
    }

    void Update()
    {
        // プレイヤーの移動入力（今回は突進のみなので省略）
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Bボタン（joystick button 1）が押されたときに突進
        // Input ManagerでDashという名前で設定した場合
        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartDash();
        }
    }

    void StartDash()
    {
        // 突進中は次の突進を無効化
        canDash = false;
        
        // 突進の方向を決定
        Vector3 dashDirection = transform.forward; // ロボットの向いている方向
        if (movementDirection.magnitude > 0)
        {
            dashDirection = movementDirection; // 移動方向に入力があればその方向に
        }

        // Rigidbodyに力を加える
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // クールダウンを開始
        Invoke("ResetDash", dashCooldown);
    }

    void ResetDash()
    {
        // 突進を再び可能にする
        canDash = true;
    }
}