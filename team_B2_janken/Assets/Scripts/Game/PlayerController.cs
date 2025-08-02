using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// プレイヤー番号
/// </summary>
public enum PlayerIndex
{
    Player1 = 0, // 1P
    Player2 = 1  // 2P
};

/// <summary>
/// プレイヤーキャラクター制御
/// </summary>
public class PlayerScript : MonoBehaviour
{
    [Header("プレイヤー番号"), SerializeField] PlayerIndex playerIndex = PlayerIndex.Player1;
    [Header("移動速度"), SerializeField] float speed = 3.0f;
    [Header("カメラ"), SerializeField] GameObject Camera;
    [Header("入力"), SerializeField] PlayerInput playerInput;
    private Rigidbody playerRigidBody;
    private Vector2 moveInput; // 左スティックの入力を保存する
    private Vector3 _velocity; // 移動値

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        // コントローラの割り当て

        // 接続されているコントローラ取得
        var gamepads = Gamepad.all;
        var index = (int)playerIndex;
        if (gamepads.Count <= index)
        {
            Debug.LogWarning($"プレイヤーに割り当てるゲームコントローラが見つかりません {index}");
            return;
        }

        // 接続されているコントローラーをプレイヤー入力へ割り当て
        playerInput.SwitchCurrentControlScheme(gamepads[index]);
    }

    /// <summary>
    /// PlayerInputからの入力イベント
    /// </summary>
    /// <param name="value"></param>
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
