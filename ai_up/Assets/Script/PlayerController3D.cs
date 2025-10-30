using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public AudioClip jumpSound;      // ジャンプ音を設定（インスペクタで指定）
    private AudioSource audioSource; // 音を鳴らす用
    public float moveSpeed = 6f;
    public float jumpForce = 6f;
    public int maxJumps = 2; // 二段ジャンプ
    private int jumpCount = 0;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        UpdateAnimator();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0);
        rb.linearVelocity = move;

        // 左右反転処理
        if (moveX != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = moveX > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            // ジャンプトリガーを送る
            anim.SetTrigger("isJumping");

            audioSource.PlayOneShot(jumpSound);
        }
    }

    void UpdateAnimator()
    {
        // Speedパラメータ更新
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // isGrounded更新
        anim.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }


}
