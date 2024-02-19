using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Vector2 playerPosOnBoard;
    public Action playerMoveNotification;
    

    [SerializeField] int score;
    [SerializeField] Text txt_score;

    public bool isTestMode;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerPosOnBoard = Vector2.zero;
    }

    void Start()
    {
        score = 0;
        SetScoreUI(score);
    }

    void SetScoreUI(int score)
    {
        txt_score.text = "Score: " + score;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        SetScoreUI(score);
    }

    public void SetPlayerPos(Vector2 posOnBoard)
    {
        playerPosOnBoard = posOnBoard;
        playerMoveNotification?.Invoke();    // enemy가 플레이어를 향해 움직임
    }

    public void GameClear()
    {
        MySceneManager.Instance.ChangeScene(SceneIndex.GameClear);
    }

    public void GameOver()
    {
        SoundManager.Instance.PlaySFXAudio("Death");
        MySceneManager.Instance.ChangeScene(SceneIndex.Stage);
    }
}
