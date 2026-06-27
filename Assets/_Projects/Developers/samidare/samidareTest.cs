using UnityEngine;
using TMPro;


public class SamidareTest : MonoBehaviour
{
    // テキスト操作の練習用
    [SerializeField] DifficultyViewer difficultyViewer;
    [SerializeField] GameData gameData;

    void Start()
    {
        // テキストの表示を変更する
        difficultyViewer.SetDifficulty(gameData.difficulty);
    }
}
