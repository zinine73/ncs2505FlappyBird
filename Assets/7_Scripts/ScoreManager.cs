using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] TMP_Text scoreText;
    int score = 0;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
