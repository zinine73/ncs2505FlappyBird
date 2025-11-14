using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] Image medal;
    [SerializeField] TMP_Text scoreResult;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] Sprite[] medalSprite;
    public void UpdateResult()
    {
        // 3등 안이면
        if (ScoreManager.Instance.Rank < 3)
        {
            // 메달 표시
            medal.sprite = medalSprite[ScoreManager.Instance.Rank];
        }
        else
        {
            // 메달이미지 자체를 표시하지 않는다
            medal.gameObject.SetActive(false);
        }
        scoreResult.text = ScoreManager.Instance.Score.ToString();
        // 베스트 스코어는 최고스코어 값을 보여준다
        
    }
}
