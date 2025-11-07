using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    [SerializeField] float velocity = 1.5f;
    [SerializeField] float rotateSpeed = 10f;
    Rigidbody2D rb;

    void Start()
    {
        // Rigidbody2D 연결
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 마우스클릭(화면터치)를 하면 위로 움직이게
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * velocity;
        }
    }

    void FixedUpdate()
    {
        // update에서 변경된 velocity.y 값만큼 회전
        transform.rotation = Quaternion.Euler
            (0, 0, rb.velocity.y * rotateSpeed);
    }
}
