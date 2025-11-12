using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] float speed = 0.65f;

    void Update()
    {
        // 게임 상태가 PLAY일때만 움직이도록
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            // 파이프의 위치를 speed만큼 좌로 이동
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
