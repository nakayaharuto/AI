using UnityEngine;

public class FallingPlatform3D : MonoBehaviour
{
    public float fallDelay = 0.5f;
    private Rigidbody rb;
    private bool triggered;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(StartFalling), fallDelay);
        }
    }

    void StartFalling()
    {
        rb.isKinematic = false;
        Destroy(gameObject, 4f);
    }
}
