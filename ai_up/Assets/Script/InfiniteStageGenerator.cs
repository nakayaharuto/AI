using UnityEngine;
using System.Collections.Generic;

public class InfiniteStageGenerator : MonoBehaviour
{
    [Header("����Prefab")]
    public GameObject[] platformPrefabs; // 0:�ʏ�, 1:����, 2:����

    [Header("�����ݒ�")]
    public float minYGap = 1.5f;
    public float maxYGap = 2.5f;
    public float minX = -5f;
    public float maxX = 5f;
    public int initialPlatforms = 20; // �ŏ��ɐ������鐔

    [Header("��Փx�ݒ�")]
    [Range(0f, 1f)] public float fallRatio = 0.2f;
    [Range(0f, 1f)] public float moveRatio = 0.2f;

    public Transform player;

    private float highestY = 0f;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        float lastY = 0f;

        for (int i = 0; i < initialPlatforms; i++)
        {
            lastY = GeneratePlatform(lastY);
        }
    }

    void Update()
    {
        // �v���C���[�㏸�ɉ����đ���𐶐�
        while (highestY < player.position.y + 10f) // ��ʏ�ɑ��ꂪ��ɂ���悤��
        {
            highestY = GeneratePlatform(highestY);
        }

        // ��ʉ��̑����j��
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < player.position.y - 10f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }

    float GeneratePlatform(float lastY)
    {
        float yGap = Random.Range(minYGap, maxYGap);
        float yPos = lastY + yGap;
        float xPos = Random.Range(minX, maxX);

        // ����^�C�v����
        float rand = Random.value;
        GameObject prefab;
        if (rand < fallRatio)
            prefab = platformPrefabs[1];
        else if (rand < fallRatio + moveRatio)
            prefab = platformPrefabs[2];
        else
            prefab = platformPrefabs[0];

        GameObject platform = Instantiate(prefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        platforms.Add(platform);

        return yPos;
    }
}
