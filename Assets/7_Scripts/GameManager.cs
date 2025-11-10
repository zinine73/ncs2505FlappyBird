using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject gameOverUI;
    void Awake()
    {
        if (Instance == null) Instance = this;
        Time.timeScale = 1.0f;
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        // 현재 씬을 다시 불러오기
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}
