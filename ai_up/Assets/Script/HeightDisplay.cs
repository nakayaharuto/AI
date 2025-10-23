using UnityEngine;
using TMPro;

public class HeightDisplay : MonoBehaviour
{
    [Header("�Q�Ɛݒ�")]
    public Transform player;                     // �v���C���[��Transform
    public TextMeshProUGUI heightText;           // �\���pText

    [Header("�I�v�V����")]
    public float startHeightOffset = 0f;         // �X�^�[�g�ʒu��0m�Ƃ��邽�߂̃I�t�Z�b�g
    public string unit = " m";                   // �P�ʁim �Ȃǁj
    public bool showBestHeight = true;           // �ō����B���x��\�����邩�ǂ���

    private float bestHeight = 0f;               // �ō����B�_���L�^

    void Start()
    {
        if (player != null)
            startHeightOffset = player.position.y; // �X�^�[�g�n�_�����0m��
    }

    void Update()
    {
        if (player == null || heightText == null) return;

        // ���ݍ����i�X�^�[�g�ʒu��0�Ƃ��Čv�Z�j
        float currentHeight = player.position.y - startHeightOffset;

        // �ō����x���X�V
        if (currentHeight > bestHeight)
            bestHeight = currentHeight;

        // �\���e�L�X�g�X�V
        if (showBestHeight)
            heightText.text = $"Height: {Mathf.FloorToInt(currentHeight)}{unit}\nBest: {Mathf.FloorToInt(bestHeight)}{unit}";
        else
            heightText.text = $"Height: {Mathf.FloorToInt(currentHeight)}{unit}";
    }
}