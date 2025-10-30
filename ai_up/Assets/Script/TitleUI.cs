using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

    void Start()
    {
        // GameManager���܂���������Ă��Ȃ��ꍇ�A�Đ��������
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager�����݂��Ȃ����ߍĐ������܂�");
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
    // Start�{�^�����������Ƃ��ɌĂ΂��
    public void OnClickStart()
    {
        Debug.Log("�Q�[���J�n�I");
        SceneManager.LoadScene("GameScene");
    }
}
