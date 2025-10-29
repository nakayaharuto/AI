using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public float moveRange = 1.5f;
    public float moveSpeed = 1.5f;
    private Vector3 startPos;

    void Start() => startPos = transform.position;

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}
