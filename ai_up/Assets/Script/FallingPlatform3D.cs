using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFalling = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    [Header("Falling Settings")]
    public float fallDelay = 0.5f;      // プレイヤーが乗ってから落ちるまで
    public float returnDelay = 3f;      // 落下後、戻り始めるまでの時間
    public float returnSpeed = 2f;      // 戻るスピード
    public float fallDuration = 2f;     // 落下し続ける時間（これを過ぎたら強制停止）

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndReturn());
        }
    }

    IEnumerator FallAndReturn()
    {
        isFalling = true;

        // 少し待ってから落下
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;

        // 一定時間だけ落下させる
        yield return new WaitForSeconds(fallDuration);
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;

        // 少し待ってから上昇開始
        yield return new WaitForSeconds(returnDelay);

        // ゆっくり元の位置へ戻る
        while (Vector3.Distance(transform.position, originalPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rb.isKinematic = true;
        isFalling = false;
    }
}