using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float ElapsedTime { get; private set; }
    public bool IsPlaying { get; private set; }
    public float BestTime { get; private set; }

    // ★ 選択中のキャラプレハブを保持する変数
    public GameObject SelectedCharacterPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 起動時にベストタイムを読み込む
            BestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (IsPlaying)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        ElapsedTime = 0f;
        IsPlaying = true;
    }

    public void StopTimer()
    {
        IsPlaying = false;
    }

    public void StageClear()
    {
        StopTimer();
        Debug.Log($"🎉 ステージクリア！ タイム: {ElapsedTime:F2} 秒");

        // ベストタイム更新処理
        if (ElapsedTime < BestTime)
        {
            BestTime = ElapsedTime;
            PlayerPrefs.SetFloat("BestTime", BestTime);
            PlayerPrefs.Save();
            Debug.Log("🏆 ベストタイム更新！");
        }

        Invoke(nameof(LoadResultScene), 2f);
    }

    void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
