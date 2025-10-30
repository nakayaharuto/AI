using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool cleared = false;

    private void OnTriggerEnter(Collider other)
    {
        if (cleared) return;

        if (other.CompareTag("Player"))
        {
            cleared = true;
            Debug.Log("ゴール到達！");
            GameManager.Instance.StageClear();
        }
    }
}
