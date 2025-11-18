using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using을 사용해서 자주 쓰는 namespace, enum을 줄여 쓸 수 있다
using GMState = GameManager.State;

public class BirdControl : MonoBehaviour
{
    [SerializeField] float velocity = 1.5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float hitHeight = -0.2f;
    [SerializeField] Animator flapAnim;
    [SerializeField] Animator birdAnim;
    [SerializeField] AudioClip acWing;
    [SerializeField] AudioClip acDie;
    Rigidbody2D rb;
    GameManager gmi;

    void Start()
    {
        // 자주 쓰려고 만든 instance 연결
        gmi = GameManager.Instance;
        // Rigidbody2D 연결
        rb = GetComponent<Rigidbody2D>();
        // 처음 시작시 안 떨어지게 중력 값 조정
        rb.gravityScale = 0f;
    }

    void Update()
    {
        // 마우스클릭(화면터치)를 하면 위로 움직이게
        if (Input.GetMouseButtonDown(0))
        {
            // 게임상태가 READY면
            if (gmi.GameState == GMState.READY)
            {
                // 게임상태를 PLAY로 바꾸고
                gmi.GamePlay();
                // Bird가 떨어지도록 중력 값 조정
                rb.gravityScale = 1.0f;
            }
            else if (gmi.GameState == GMState.PLAY) // 게임상태가 PLAY면 
            {
                // 나는 소리
                gmi.PlayAudio(acWing);
                // 위로 이동
                rb.velocity = Vector2.up * velocity;
            }
        }
    }

    void FixedUpdate()
    {
        // update에서 변경된 velocity.y 값만큼 회전
        transform.rotation = Quaternion.Euler
            (0, 0, rb.velocity.y * rotateSpeed);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 게임 PLAY 일때만 충돌 감지
        if (gmi.GameState != GMState.PLAY) return;

        // 게임 오버
        gmi.GameOver();
        // 새의 Flap 애니메이션을 멈춘다
        //GetComponent<Animator>().enabled = false;
        flapAnim.enabled = false;
        // 새의 y좌표에 따라 소리 재생
        if (transform.position.y > hitHeight)
        {
            // 새가 떨어지는 소리
            gmi.PlayAudio(acDie);
        }
    }
    public void BirdReady()
    {
        // 새를 뒤로 움직인다
        birdAnim.SetTrigger("Ready");
    }
    public void OffBirdAnimator()
    {
        // 새의 애니메이터를 끈다
        birdAnim.enabled = false;
    }
}
