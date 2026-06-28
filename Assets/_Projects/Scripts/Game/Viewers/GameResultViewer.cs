using UnityEngine;

/// <summary>GameScene_moti上のゲーム終了オーバーレイを管理する。</summary>
public sealed class GameResultViewer : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject clearImage;
    [SerializeField] private GameObject failedImage;

    public void Hide()
    {
        if (resultPanel != null) resultPanel.SetActive(false);
    }

    public void Show(ResultType resultType)
    {
        bool isClear = resultType == ResultType.Clear;
        if (clearImage != null) clearImage.SetActive(isClear);
        if (failedImage != null) failedImage.SetActive(!isClear);
        if (resultPanel != null) resultPanel.SetActive(true);
    }
}
