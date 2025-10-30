using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [Header("プレハブ設定")]
    [SerializeField] private GameObject[] characterPrefabs; // プレハブ配列
    private GameObject currentCharacter;
    private int currentIndex = 0;

    [Header("表示位置")]
    [SerializeField] private Transform spawnPoint; // キャラ表示位置

    void Start()
    {
        // GameManagerがまだ生成されていない場合、再生成される
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManagerが存在しないため再生成します");
            new GameObject("GameManager").AddComponent<GameManager>();
        }

        float best = GameManager.Instance.BestTime;
        if (best < float.MaxValue)
        {
            // 秒数をTimeSpanに変換
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(best);
            bestTimeText.text = $"Best Time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds / 10:00}";
        }
        else
        {
            bestTimeText.text = "Best Time: 00:00.00";
        }
    }

    // 左矢印ボタン
    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characterPrefabs.Length - 1;
        ShowCharacter(currentIndex);
    }

    // 右矢印ボタン
    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characterPrefabs.Length) currentIndex = 0;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        // 古いキャラを削除
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // 新しいキャラを生成
        currentCharacter = Instantiate(characterPrefabs[index], spawnPoint.position, spawnPoint.rotation);
    }


    // Startボタンを押したときに呼ばれる
    public void OnClickStart()
    {
        // 選択中のキャラを保持
        if (characterPrefabs.Length > 0)
        {
            GameManager.Instance.SelectedCharacterPrefab = characterPrefabs[currentIndex];
        }

        SceneManager.LoadScene("GameScene"); // ゲームシーン名に変更
    }
}
