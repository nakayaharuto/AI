using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [Header("�v���n�u�ݒ�")]
    [SerializeField] private GameObject[] characterPrefabs; // �v���n�u�z��
    private GameObject currentCharacter;
    private int currentIndex = 0;

    [Header("�\���ʒu")]
    [SerializeField] private Transform spawnPoint; // �L�����\���ʒu

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
            // �b����TimeSpan�ɕϊ�
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(best);
            bestTimeText.text = $"Best Time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds / 10:00}";
        }
        else
        {
            bestTimeText.text = "Best Time: 00:00.00";
        }
    }

    // �����{�^��
    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characterPrefabs.Length - 1;
        ShowCharacter(currentIndex);
    }

    // �E���{�^��
    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characterPrefabs.Length) currentIndex = 0;
        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        // �Â��L�������폜
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // �V�����L�����𐶐�
        currentCharacter = Instantiate(characterPrefabs[index], spawnPoint.position, spawnPoint.rotation);
    }


    // Start�{�^�����������Ƃ��ɌĂ΂��
    public void OnClickStart()
    {
        // �I�𒆂̃L������ێ�
        if (characterPrefabs.Length > 0)
        {
            GameManager.Instance.SelectedCharacterPrefab = characterPrefabs[currentIndex];
        }

        SceneManager.LoadScene("GameScene"); // �Q�[���V�[�����ɕύX
    }
}
