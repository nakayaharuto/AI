using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        // �Q�[���J�n���Ƀ^�C�}�[���X�^�[�g
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartTimer();
        }
    }
}
