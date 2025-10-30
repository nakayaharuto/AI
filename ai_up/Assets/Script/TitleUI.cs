using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

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
            bestTimeText.text = $"Best Time: {best:F2} s";
        }
        else
        {
            bestTimeText.text = "Best Time: --.-- s";
        }
    }
    // Startボタンを押したときに呼ばれる
    public void OnClickStart()
    {
        Debug.Log("ゲーム開始！");
        SceneManager.LoadScene("GameScene");
    }
}
