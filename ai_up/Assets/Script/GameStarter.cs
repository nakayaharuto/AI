using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        // ゲーム開始時にタイマーをスタート
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartTimer();
        }
    }
}
