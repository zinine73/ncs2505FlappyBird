using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] int scoreValue; // 파이프 종류별로 얻게 되는 점수 지정
    [SerializeField] AudioClip acPoint;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.UpdateScore(scoreValue);
            GameManager.Instance.PlayAudio(acPoint);
        }
    }
}
