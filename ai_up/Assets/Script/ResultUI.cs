using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTimeText;

    void Start()
    {
        float time = GameManager.Instance.ElapsedTime;
        currentTimeText.text = $"Your Time: {time:F2} s";
    }

    public void OnClickReturnTitle()
    {
        GameManager.Instance.ReturnToTitle();
    }
}
