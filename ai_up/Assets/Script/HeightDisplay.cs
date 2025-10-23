using UnityEngine;
using TMPro; // TextMeshProを使う場合

public class HeightDisplay : MonoBehaviour
{
    public InfiniteStageGenerator stageGenerator; // InfiniteStageGeneratorをセット
    public TextMeshProUGUI heightText;           // 表示用Text

    void Update()
    {
        if (stageGenerator == null || heightText == null) return;

        float maxHeight = stageGenerator.GetMaxHeight();
        heightText.text = "Height: " + Mathf.FloorToInt(maxHeight) + " m";
    }
}
