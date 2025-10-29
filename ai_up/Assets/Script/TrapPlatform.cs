using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private bool triggered = false;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(Drop), 0.5f);
        }
    }

    void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;
        Destroy(gameObject, 3f);
    }
}
