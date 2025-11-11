using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 상태를 저장할 enum
    public enum State
    {
        TITLE,      // 0
        READY,      // 1
        PLAY,       // 2
        GAMEOVER,   // 3
        BESTSCORE   // 4
    }
    public static GameManager Instance;
    [SerializeField] GameObject gameOverUI;
    State gameState;    // 게임상태를 저장할 변수
    public State GameState => gameState;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        // 정상적인 게임의 시간이 흐르게
        Time.timeScale = 1.0f;
        // 게임의 시작은 타이틀에서
        GameTitle();
    }
    void ChangeState(State value)
    {
        gameState = value;
    }
    public void GameTitle() => ChangeState(State.TITLE);
    public void GameReady()
    {
        ChangeState(State.READY);
    }
    public void GamePlay() => ChangeState(State.PLAY);
    public void GameOver()
    {
        ChangeState(State.GAMEOVER);
        gameOverUI.SetActive(true);
        // 게임 시간을 멈춘다
        Time.timeScale = 0f;
    }
    public void GameBestScore() => ChangeState(State.BESTSCORE);

    // 현재 씬을 다시 불러오기
    public void RestartGame() => SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
}
