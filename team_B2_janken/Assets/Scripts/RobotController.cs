using UnityEngine;
using UnityEngine.InputSystem; // 新しいInput Systemを使うために必要

public class RobotController : MonoBehaviour
{
    // ----- 突進関連の変数 -----
    [Header("Dash Settings")]
    public float dashForce = 20.0f; // 突進する力
    public float dashCooldown = 1.0f; // 突進のクールダウン時間
    private bool canDash = true; // 突進が可能な状態か

    // ----- バネに弾かれた後の回転ロックと操作無効化の変数 -----
    [Header("Lock Settings")]
    public float lockDuration = 1.0f; // 角度を固定し、操作を無効化する時間
    private bool isLocked = false; // ロック状態か

    // ----- その他 -----
    private Rigidbody rb;
    private Vector3 movementDirection; // 突進方向決定用

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the robot!");
        }
    }

    // 毎フレーム、Z軸回転を0に固定する
    void LateUpdate()
    {
        // Y軸回転のみを維持し、X軸とZ軸を0に固定
        // これにより、ロボットは横転せず直立を維持する
        Quaternion currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
    }
    
    // ----- Input Systemからのイベントハンドラー -----

    // Dashボタンの入力を受け取るためのメソッド
    // Input Actionsファイルで、Dashアクションにバインドする
    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("OnDash");
        // ロック中は突進を無効化
        if (isLocked)
        {
            return;
        }

        // ボタンが押された瞬間だけ処理を実行し、クールダウン中ではないか確認
        if (context.performed && canDash)
        {
            StartDash();
        }
    }
    
    // ----- ロボットの挙動を制御するメソッド -----

    void StartDash()
    {
        canDash = false;
        
        Vector3 dashDirection = transform.forward; // ロボットの向いている方向を突進方向とする
        
        // Rigidbodyに瞬間的な力を加えて突進させる
        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        // クールダウンを開始
        Invoke("ResetDash", dashCooldown);
    }

    void ResetDash()
    {
        // 突進を再び可能にする
        canDash = true;
    }

    // ----- バネのスクリプトから呼び出されるメソッド -----

    // バネに弾かれた際に呼び出され、回転ロックと操作無効化を開始する
    public void StartInvincibilityAndRotationLock()
    {
        isLocked = true;
        // 指定時間後にロックを解除する
        Invoke("EndRotationLock", lockDuration);
    }

    void EndRotationLock()
    {
        // ロックを解除し、操作を再び可能にする
        isLocked = false;
    }
}