using UnityEngine;
using TMPro; // TextMeshPro���g���ꍇ

public class HeightDisplay : MonoBehaviour
{
    public InfiniteStageGenerator stageGenerator; // InfiniteStageGenerator���Z�b�g
    public TextMeshProUGUI heightText;           // �\���pText

    void Update()
    {
        if (stageGenerator == null || heightText == null) return;

        float maxHeight = stageGenerator.GetMaxHeight();
        heightText.text = "Height: " + Mathf.FloorToInt(maxHeight) + " m";
    }
}
