using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public const int MAX_RANK = 5; // 최대 랭크 보여줄 개수
    // DateTime을 string형식으로 바꿀 때 쓸 패턴
    public static string DTPattern = @"yyMMddhhmmss";
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameOverCanvas goCanvas;
    int score = 0;
    int rank = 0;
    public int Score => score;
    public int Rank => rank;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    public void CheckBestScore()
    {
        // 현재 저장된 1~5 순위 값을 딕셔너리를 사용해 저장
        var rankDic = new Dictionary<string, int>();
        for (int i = 0; i < MAX_RANK; i++)
        {
            string key = PlayerPrefs.GetString($"RANKDATE{i}", 
                $"25111712000{i}");
            int value = PlayerPrefs.GetInt($"RANKSCORE{i}", 0);
            rankDic.Add(key, value);
        }
        // 현재 일시를 패턴을 이용해 키값으로 만들고
        string nowKey = DateTime.Now.ToString(DTPattern);
        // 딕셔너리에 저장 => 총 개수가 MAX_RANK + 1
        rankDic.Add(nowKey, score);
        // 내림차순으로 정렬한 값을 새로운 딕셔너리에 저장
        var newDic = rankDic.OrderByDescending(x => x.Value);
        // 랭크값으로 최대값을 설정하고
        rank = MAX_RANK;
        // 인덱스는 0으로 시작
        int index = 0;
        foreach (var item in newDic)
        {
            // 1~5등까지 값을 저장
            PlayerPrefs.SetString($"RANKDATE{index}", item.Key);
            PlayerPrefs.SetInt($"RANKSCORE{index}", item.Value);
            // 현재 item이 nowKey값과 같으면 그 때 인덱스가 랭크 값
            if (item.Key.Equals(nowKey))
            {
                // 랭크 값 정하기
                rank = index;
            }
            // 최대 랭크 수만큼 돌았으면 나가기
            if (++index == MAX_RANK) break;
        }
        // GameOverCanvas를 업데이트한다
        goCanvas.UpdateResult();
    }
#if UNITY_EDITOR
    // 베스트 스코어 리셋
    [MenuItem("FlappyBird/Reset BestScore")]
    public static void ResetBestScore()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset best score...done");
    }
#endif
}
