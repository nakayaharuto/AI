using UnityEngine;
using System.Collections.Generic;

public class InfiniteStageGenerator : MonoBehaviour
{
    public GameObject normalPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject fallingPlatformPrefab;

    public Transform player;
    public float spawnY = 0f;
    public float platformSpacingMin = 4f;
    public float platformSpacingMax = 6f;
    public float xRange = 5f;
    public int poolSize = 30;
    public float playerSafeZone = 6f;

    private List<GameObject> platforms = new List<GameObject>();
    private float highestY;

    void Start()
    {
        highestY = spawnY;

        // プール作成
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = GetRandomPlatformPrefab();
            GameObject platform = Instantiate(prefab, transform);
            platform.SetActive(false);
            platforms.Add(platform);
        }

        // ✅ 最初の数個は安全距離チェックなしで生成（開始地点を確保）
        for (int i = 0; i < 10; i++)
        {
            bool ignoreSafeZone = i < 3; // 最初の3つは近くでもOK
            SpawnPlatform(ignoreSafeZone);
        }
    }

    void Update()
    {
        if (player.position.y + 15f > highestY)
        {
            for (int i = 0; i < 5; i++)
                SpawnPlatform();
        }
    }

    void SpawnPlatform(bool ignoreSafeZone = false)
    {
        GameObject platform = GetInactivePlatform();
        if (platform == null) return;

        float y = highestY + Random.Range(platformSpacingMin, platformSpacingMax);
        float x = Random.Range(-xRange, xRange);

        // プレイヤー付近の床を避ける（初期生成以外のみ）
        if (!ignoreSafeZone && Mathf.Abs(y - player.position.y) < playerSafeZone)
        {
            y += playerSafeZone;
        }

        platform.transform.position = new Vector3(x, y, 0);
        platform.SetActive(true);
        highestY = y;
    }

    GameObject GetInactivePlatform()
    {
        foreach (var p in platforms)
        {
            if (!p.activeInHierarchy)
                return p;
        }
        return null;
    }

    GameObject GetRandomPlatformPrefab()
    {
        float rand = Random.value;
        if (rand < 0.6f)
            return normalPlatformPrefab;
        else if (rand < 0.8f)
            return movingPlatformPrefab;
        else
            return fallingPlatformPrefab;
    }

    public float GetMaxHeight()
    {
        return highestY;
    }
}