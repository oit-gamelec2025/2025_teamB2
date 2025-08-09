using UnityEngine;

public class LockPosition : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // ゲーム開始時の位置と回転を記録
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // もしRigidbodyコンポーネントがあれば、それを取得
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Rigidbodyの物理演算を無効化
            rb.isKinematic = true;
        }
    }

    void LateUpdate()
    {
        // 毎フレーム、初期位置と初期回転を強制的に適用
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}