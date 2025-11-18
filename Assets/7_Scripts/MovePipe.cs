using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] float speed = 0.65f;
    [SerializeField] BoxCollider2D upPipe; // 위 파이프
    [SerializeField] BoxCollider2D downPipe; // 아래 파이프
    public bool Moving {get;set;} // 오브젝트풀링을 위해 파이프가 움직일지말지
    void Update()
    {
        // 게임 상태가 PLAY일때만 움직이도록
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            if (Moving) // 값이 true일때만 움직이게
            {
                // 파이프의 위치를 speed만큼 좌로 이동
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else if (GameManager.Instance.GameState == GameManager.State.GAMEOVER)
        {
            // 게임오버 상태에서는 파이프의 충돌이 안 일어나게
            upPipe.enabled = downPipe.enabled = false;
        }
    }
}
