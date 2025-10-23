using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���E�ړ��iX���̂݁j
        float move = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(move * moveSpeed, velocity.y, 0f);

        // �n�ʔ���
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // �W�����v
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0f);
        }
    }
}
