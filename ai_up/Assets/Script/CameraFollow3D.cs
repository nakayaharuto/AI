using UnityEngine;

public class CameraFollowVertical : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    void LateUpdate()
    {
        if (!player) return;

        Vector3 targetPos = new Vector3(player.position.x, player.position.y + offset.y, offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
    }
}
