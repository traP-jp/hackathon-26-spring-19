using UnityEngine;
using TMPro;


public class samidareTest : MonoBehaviour
{
    // テキスト操作の練習用
    [SerializeField] DifficultyViewer difficultyViewer;

    void Start()
    {
        // テキストの表示を変更する
        difficultyViewer.SetDifficulty(DifficultyType.Easy);
    }
}
