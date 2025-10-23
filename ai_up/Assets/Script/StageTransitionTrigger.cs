using UnityEngine;

public class StageTransitionTrigger : MonoBehaviour
{
    [Header("生成制御")]
    public InfiniteStageGenerator stageGenerator; // 既存のステージ生成スクリプトを参照
    public Transform triggerMoveTarget;           // トリガーを次に移動させる位置（再利用したい場合）
    public float moveOffsetY = 100f;              // トリガーを上にどのくらい移動するか

    [Header("背景などの演出")]
    public Material[] backgroundMaterials;        // 背景用マテリアル（段階で切替）
    public Renderer backgroundRenderer;           // 背景のRendererを指定

    private int stageCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageCount++;

            Debug.Log($"[StageTransition] プレイヤーが{stageCount}回目の上限に到達！");

            // ✅ 新しい床をまとめて生成
            for (int i = 0; i < 15; i++)
                stageGenerator.SendMessage("SpawnPlatform", false);

            // ✅ 背景を切り替える（配列の範囲内なら）
            if (backgroundRenderer != null && backgroundMaterials.Length > 0)
            {
                int matIndex = stageCount % backgroundMaterials.Length;
                backgroundRenderer.material = backgroundMaterials[matIndex];
            }

            // ✅ トリガーを上に移動（再利用）
            if (triggerMoveTarget != null)
                transform.position = triggerMoveTarget.position;
            else
                transform.position += new Vector3(0, moveOffsetY, 0);
        }
    }
}