using UnityEngine;

public class RubberBandBouncer : MonoBehaviour
{
    // 吹き飛ばす力の強さをインスペクターから設定
    public float bounceForce = 50.0f;

    // AudioSourceコンポーネントへの参照
    private AudioSource audioSource;

    void Start()
    {
        // ゲーム開始時にAudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // プレイヤーと衝突したときに呼ばれるメソッド
    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手がプレイヤーであるか確認
        if (collision.gameObject.CompareTag("Player"))
        {
            // 効果音を再生
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // 以下は以前の吹き飛ばす処理
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 normal = collision.contacts[0].normal;
                Vector3 incomingVelocity = playerRigidbody.velocity;
                Vector3 bounceDirection = Vector3.Reflect(incomingVelocity.normalized, normal);

                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(bounceDirection.normalized * bounceForce, ForceMode.Impulse);
            }
        }
    }
}