using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f; // âìåiÇŸÇ«è¨Ç≥Ç¢íl
    private Vector3 lastCameraPos;
    private float spriteHeight;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPos = cameraTransform.position;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            spriteHeight = sr.bounds.size.y;
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(0, delta.y * parallaxEffect, 0);
        lastCameraPos = cameraTransform.position;

        // åJÇËï‘Çµ
        if (cameraTransform.position.y - transform.position.y > spriteHeight)
        {
            transform.position += new Vector3(0, spriteHeight * 2f, 0);
        }
    }
}
