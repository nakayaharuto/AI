using UnityEngine;

public class StageGeneratorAdvanced : MonoBehaviour
{
    [Header("����Prefab")]
    public GameObject[] platformPrefabs; // 0:�ʏ�, 1:����, 2:����

    [Header("�����ݒ�")]
    public int platformCount = 100;      // ��������
    public float minYGap = 1.5f;         // �ŏ�Y�Ԋu
    public float maxYGap = 2.5f;         // �ő�Y�Ԋu
    public float minX = -5f;             // X�͈�
    public float maxX = 5f;

    [Header("��Փx�ݒ�")]
    [Range(0f, 1f)] public float fallRatio = 0.2f;  // �������ꊄ��
    [Range(0f, 1f)] public float moveRatio = 0.2f;  // �������ꊄ��

    void Start()
    {
        float lastY = 0f;

        for (int i = 0; i < platformCount; i++)
        {
            // ���̑���̈ʒu
            float yGap = Random.Range(minYGap, maxYGap);
            lastY += yGap;
            float xPos = Random.Range(minX, maxX);

            // ����^�C�v����
            float rand = Random.value;
            GameObject prefab;

            if (rand < fallRatio)
                prefab = platformPrefabs[1];   // ��������
            else if (rand < fallRatio + moveRatio)
                prefab = platformPrefabs[2];   // ��������
            else
                prefab = platformPrefabs[0];   // �ʏ푫��

            Instantiate(prefab, new Vector3(xPos, lastY, 0f), Quaternion.identity);
        }
    }
}
