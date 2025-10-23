using UnityEngine;
using TMPro;

public class HeightDisplay : MonoBehaviour
{
    [Header("参照設定")]
    public Transform player;                     // プレイヤーのTransform
    public TextMeshProUGUI heightText;           // 表示用Text

    [Header("オプション")]
    public float startHeightOffset = 0f;         // スタート位置を0mとするためのオフセット
    public string unit = " m";                   // 単位（m など）
    public bool showBestHeight = true;           // 最高到達高度を表示するかどうか

    private float bestHeight = 0f;               // 最高到達点を記録

    void Start()
    {
        if (player != null)
            startHeightOffset = player.position.y; // スタート地点を基準に0m化
    }

    void Update()
    {
        if (player == null || heightText == null) return;

        // 現在高さ（スタート位置を0として計算）
        float currentHeight = player.position.y - startHeightOffset;

        // 最高高度を更新
        if (currentHeight > bestHeight)
            bestHeight = currentHeight;

        // 表示テキスト更新
        if (showBestHeight)
            heightText.text = $"Height: {Mathf.FloorToInt(currentHeight)}{unit}\nBest: {Mathf.FloorToInt(bestHeight)}{unit}";
        else
            heightText.text = $"Height: {Mathf.FloorToInt(currentHeight)}{unit}";
    }
}