
using UnityEngine;

public class RobotDash1 : MonoBehaviour
{
    public float dashForce = 20f;       // 突進の力
    public float dashDuration = 0.2f;   // 突進の持続時間
    public float dashCooldown = 1f;     // クールダウン時間
    public string dashButton = "P1_Dash"; // プレイヤーごとに設定

    private bool canDash = true;
    private Rigidbody rb;
    private Quaternion dashRotation; // 突進中の固定向き

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canDash && Input.GetButtonDown(dashButton))
        {
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        canDash = false;

        // 突進開始時の向きを保存
        dashRotation = transform.rotation;

        // 向いている方向のXZ平面ベクトルを取得
        Vector3 dashDirection = transform.forward;
        dashDirection.y = 0f;
        dashDirection.Normalize();

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            // 向きを固定
            transform.rotation = dashRotation;

            // 突進中の移動
            rb.velocity = dashDirection * dashForce;
            yield return null;
        }

        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

