using UnityEngine;

public class StageGeneratorAdvanced : MonoBehaviour
{
    [Header("足場Prefab")]
    public GameObject[] platformPrefabs; // 0:通常, 1:落下, 2:動く

    [Header("生成設定")]
    public int platformCount = 100;      // 総生成数
    public float minYGap = 1.5f;         // 最小Y間隔
    public float maxYGap = 2.5f;         // 最大Y間隔
    public float minX = -5f;             // X範囲
    public float maxX = 5f;

    [Header("難易度設定")]
    [Range(0f, 1f)] public float fallRatio = 0.2f;  // 落下足場割合
    [Range(0f, 1f)] public float moveRatio = 0.2f;  // 動く足場割合

    void Start()
    {
        float lastY = 0f;

        for (int i = 0; i < platformCount; i++)
        {
            // 次の足場の位置
            float yGap = Random.Range(minYGap, maxYGap);
            lastY += yGap;
            float xPos = Random.Range(minX, maxX);

            // 足場タイプ判定
            float rand = Random.value;
            GameObject prefab;

            if (rand < fallRatio)
                prefab = platformPrefabs[1];   // 落下足場
            else if (rand < fallRatio + moveRatio)
                prefab = platformPrefabs[2];   // 動く足場
            else
                prefab = platformPrefabs[0];   // 通常足場

            Instantiate(prefab, new Vector3(xPos, lastY, 0f), Quaternion.identity);
        }
    }
}
