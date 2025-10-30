using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStageGenerator : MonoBehaviour
{
    [Header("Platform Prefabs")]
    public GameObject normalPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject verticalMovingPlatformPrefab; // 上下移動床
    public GameObject fallingPlatformPrefab;
    public GameObject trapPlatformPrefab;       // トラップ（落とすギミック）
    public GameObject goalPrefab;       //ゴール

    [Header("Settings")]
    public Transform player;
    public float spawnY = 0f;
    public float platformSpacingMin = 2.5f; // ← 間隔を小さく
    public float platformSpacingMax = 4f;   // ← 間隔を小さく
    public float xRange = 5f;
    public int poolSize = 40;               // ← 床の数も少し増加
    public float playerSafeZone = 6f;
    public float initialHeight = 300f;      // 初期生成高さ
    //public float extendHeight = 1000f;       // 拡張生成高さ

    private List<GameObject> platforms = new List<GameObject>();
    private float highestY;
    private bool generatedInitial = false;

    void Start()
    {
        highestY = spawnY;

        // プール作成
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = GetRandomPlatformPrefab(0);
            GameObject platform = Instantiate(prefab, transform);
            platform.SetActive(false);
            platforms.Add(platform);
        }

        // ⭐ プレイヤー足元〜少し上まで即生成
        for (int i = 0; i < 10; i++)
            SpawnPlatform(ignoreSafeZone: true);

        // ⭐ 最初から500まで生成して終了
        StartCoroutine(GenerateInitialStageCoroutine(initialHeight));

    }

    void Update()
    {
        if (player == null) return;

        // もう300まで生成したら追加しない
        if (generatedInitial) return;

        // 通常更新（プレイヤーが上昇し続ける場合）
        if (player.position.y + 15f > highestY)
        {
            for (int i = 0; i < 5; i++)
                SpawnPlatform();
        }
    }

    IEnumerator GenerateInitialStageCoroutine(float targetHeight)
    {
        int count = 0;

        while (highestY < targetHeight)
        {
            SpawnPlatform(ignoreSafeZone: true);
            count++;

            // 毎 10 枚ごとに 1 フレーム待機
            if (count % 10 == 0)
                yield return null;
        }

        generatedInitial = true;

        //ゴール生成
        SpawnGoal();
    }

    void SpawnPlatform(bool ignoreSafeZone = false)
    {
        GameObject platform = GetInactivePlatform();
        if (platform == null) return;

        float difficulty = Mathf.Clamp(highestY / 2000f, 0f, 1.5f);
        float spacingMin = Mathf.Lerp(platformSpacingMin, 4.5f, difficulty);
        float spacingMax = Mathf.Lerp(platformSpacingMax, 6.0f, difficulty);

        float y = highestY + Random.Range(spacingMin, spacingMax);
        float x = Random.Range(-xRange, xRange);

        if (!ignoreSafeZone && Mathf.Abs(y - player.position.y) < playerSafeZone)
            y += playerSafeZone;

        // ランダムな床タイプ取得
        GameObject prefab = GetRandomPlatformPrefab(difficulty);

        // 新しい床をプール再利用で更新
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

    GameObject GetRandomPlatformPrefab(float difficulty)
    {
        // 難易度で確率を制御
        float normalRate = Mathf.Clamp01(0.5f - 0.3f * difficulty);
        float movingRate = Mathf.Clamp01(0.2f + 0.1f * difficulty);
        float verticalRate = Mathf.Clamp01(0.1f + 0.15f * difficulty);
        float fallingRate = Mathf.Clamp01(0.1f + 0.2f * difficulty);
        float trapRate = Mathf.Clamp01(0.1f + 0.25f * difficulty);

        float total = normalRate + movingRate + verticalRate + fallingRate + trapRate;
        float rand = Random.value * total;

        if (rand < normalRate)
            return normalPlatformPrefab;
        rand -= normalRate;

        if (rand < movingRate)
            return movingPlatformPrefab;
        rand -= movingRate;

        if (rand < verticalRate)
            return verticalMovingPlatformPrefab;
        rand -= verticalRate;

        if (rand < fallingRate)
            return fallingPlatformPrefab;

        return trapPlatformPrefab;
    }

    public float GetMaxHeight()
    {
        return highestY;
    }
    void SpawnGoal()
    {
        Vector3 goalPos = new Vector3(0f, highestY + 3f, 0f); // 床より少し上
        GameObject goal = Instantiate(goalPrefab, goalPos, Quaternion.identity);
        Debug.Log("ゴール出現！");
    }
}

