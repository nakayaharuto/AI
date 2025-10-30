using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.SelectedCharacterPrefab != null)
        {
            Instantiate(GameManager.Instance.SelectedCharacterPrefab,
                        spawnPoint.position,
                        spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("GameManager�܂��͑I���L���������ݒ�ł��B");
        }
    }
}
