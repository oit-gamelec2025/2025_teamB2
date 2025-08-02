using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/// <summary>
/// スコアUIを管理するクラス
/// </summary>
public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI player1ScoreText;

    [SerializeField]
    private TextMeshProUGUI player2ScoreText;

    private int _player1Score;
    private int _player2Score;

    private void Start()
    {
        InitScoreUI();
        //AddScore(1);  デモで1点加算
    }

    /// <summary>
    /// UIを初期化
    /// </summary>
    public void InitScoreUI()
    {
        _player1Score = 0;
        _player2Score = 0;
        UpdateScoreText();
    }

    /// <summary>
    /// プレイヤーのスコアを加算し、UIを更新
    /// </summary>
    /// <param name="playerNumber">1 または 2</param>
    public void AddScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            _player1Score++;
        }
        else if (playerNumber == 2)
        {
            _player2Score++;
        }

        UpdateScoreText();
    }

    /// <summary>
    /// スコアのテキストUIを更新する
    /// </summary>
    private void UpdateScoreText()
    {
        player1ScoreText.text = $"P1: {_player1Score}";
        player2ScoreText.text = $"P2: {_player2Score}";
    }
    //UI要素は TextMeshProUGUI を使用（Textは古い）

    //スコア更新処理は AddScore() に集約（再利用性◎）

    //命名はルールに沿って統一済み（Pascal, camel, _privateField）

    
}