using UnityEngine;

public class MovingPlatformRandom : MonoBehaviour
{
    public float minDistance = 2f;   // 左右移動の最小距離
    public float maxDistance = 5f;   // 左右移動の最大距離
    public float minSpeed = 1f;      // 移動速度最小
    public float maxSpeed = 3f;      // 移動速度最大

    private Vector3 startPos;
    private Vector3 targetPos;
    private float moveDistance;
    private float moveSpeed;
    private bool movingRight;

    void Start()
    {
        startPos = transform.position;

        // ランダムに距離と速度を設定
        moveDistance = Random.Range(minDistance, maxDistance);
        moveSpeed = Random.Range(minSpeed, maxSpeed);

        // ランダムに左右初期方向
        movingRight = (Random.value > 0.5f);

        UpdateTargetPos();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            movingRight = !movingRight;
            UpdateTargetPos();
        }
    }

    void UpdateTargetPos()
    {
        if (movingRight)
            targetPos = startPos + Vector3.right * moveDistance;
        else
            targetPos = startPos + Vector3.left * moveDistance;
    }

    // プレイヤーをプラットフォームに追従させる
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.position += (targetPos - transform.position) * Time.deltaTime;
            }
        }
    }
}
