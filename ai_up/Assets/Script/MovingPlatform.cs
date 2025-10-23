using UnityEngine;

public class MovingPlatformRandom : MonoBehaviour
{
    public float minDistance = 2f;   // ���E�ړ��̍ŏ�����
    public float maxDistance = 5f;   // ���E�ړ��̍ő勗��
    public float minSpeed = 1f;      // �ړ����x�ŏ�
    public float maxSpeed = 3f;      // �ړ����x�ő�

    private Vector3 startPos;
    private Vector3 targetPos;
    private float moveDistance;
    private float moveSpeed;
    private bool movingRight;

    void Start()
    {
        startPos = transform.position;

        // �����_���ɋ����Ƒ��x��ݒ�
        moveDistance = Random.Range(minDistance, maxDistance);
        moveSpeed = Random.Range(minSpeed, maxSpeed);

        // �����_���ɍ��E��������
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

    // �v���C���[���v���b�g�t�H�[���ɒǏ]������
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
