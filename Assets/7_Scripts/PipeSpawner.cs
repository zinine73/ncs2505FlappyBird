using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] float maxTime = 1.5f;      // 몇 초마다 생성할건지
    [SerializeField] float heightRange = 0.5f;  // 생성위치 y의 랜덤한 범위
    [SerializeField] GameObject pipePrefab;     // 파이프의 프레팝 연결
    float timer;
    void Update()
    {
        // timer가 maxTime을 넘으면
        if (timer > maxTime)
        {
            // pipe 만드는 함수 호출
            SpawnPipe();
            // timer는 0으로
            timer = 0;
        }
        // timer에 deltaTime 더해주기
        timer += Time.deltaTime;
    }
    void SpawnPipe()
    {
        // 랜덤으로 y값을 정해서, 생성될 파이프의 위치 정하기
        Vector3 spawnPos = transform.position + new Vector3(
            0, Random.Range(-heightRange, heightRange)
        );
        // Instantiate로 생성, 생성된 객체는 pipe라는 GameObject에 할당
        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);
        // 5초 뒤에 pipe 객체 파괴
        Destroy(pipe, 5f);
    }
}
