using UnityEngine;
using System.Collections.Generic;

public class Infinite : MonoBehaviour
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

        // 初期生成（プレイヤー近くにもOK）
        for (int i = 0; i < 10; i++)
        {
            bool ignoreSafeZone = i < 3;
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

        if (!ignoreSafeZone && Mathf.Abs(y - player.position.y) < playerSafeZone)
            y += playerSafeZone;

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
        // プレイヤーの高さから難易度係数を計算（0〜1.5くらいまで上昇）
        float difficulty = Mathf.Clamp(player.position.y / 200f, 0f, 1.5f);

        // 基本確率
        float normalRate = Mathf.Clamp01(0.6f - 0.4f * difficulty);  // 上昇で減少
        float movingRate = Mathf.Clamp01(0.25f + 0.15f * difficulty); // 上昇で増加
        float fallingRate = Mathf.Clamp01(0.15f + 0.25f * difficulty); // 上昇で増加

        float rand = Random.value;

        if (rand < normalRate)
            return normalPlatformPrefab;
        else if (rand < normalRate + movingRate)
            return movingPlatformPrefab;
        else
            return fallingPlatformPrefab;
    }

    public float GetMaxHeight()
    {
        return highestY;
    }
}