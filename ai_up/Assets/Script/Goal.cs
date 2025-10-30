using UnityEngine;
using UnityEngine.UI; // ← Imageを使う場合必要

public class Goal : MonoBehaviour
{
    private bool cleared = false;

    [SerializeField] private GameObject goalImage; // Canvas上のImageをアサイン

    private void OnTriggerEnter(Collider other)
    {
        if (cleared) return;

        if (other.CompareTag("Player"))
        {
            cleared = true;
            Debug.Log("ゴール到達！");

            // ゴール画像を表示
            if (goalImage != null)
                goalImage.SetActive(true);

            GameManager.Instance.StageClear();
        }
    }
}
