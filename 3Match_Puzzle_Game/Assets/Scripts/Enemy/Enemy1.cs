using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 3.0f;  // 적의 이동 속도
    public Transform player;    // 플레이어의 Transform
    public Vector3 offsetFromPlayer = new Vector3(0.01f, 0, 0); // 플레이어로부터 1cm 떨어진 위치 (1cm = 0.01 유니티 단위)
    public GameObject bulletPrefab;     // 발사할 총알 프리팹
    public Transform firePoint;         // 총알이 발사될 위치
    public float shootInterval = 1.0f;  // 총알 발사 간격
    private float nextShootTime = 0f;   // 다음 발사 시간
    private bool isMoving = true;       // 적이 이동 중인지 여부

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();     // 이동 중이라면 목표 위치로 이동
        }
        else
        {
            ShootAtPlayer();    // 목표 위치에 도달했다면 플레이어에게 총알 발사
        }
    }

    void MoveToTarget()
    {
        Vector3 targetPosition = player.position + offsetFromPlayer;    // 목표 위치 계산 (플레이어 위치 + 오프셋)
        float step = speed * Time.deltaTime;    // 프레임 당 이동할 거리 계산
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step); // 목표 위치로 이동

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)  // 목표 위치에 도달하면 이동 멈춤
        {
            isMoving = false;
        }
    }

    void ShootAtPlayer()
    {
        if (Time.time > nextShootTime)  // 현재 시간이 다음 발사 시간보다 크다면 총알 발사
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  // 총알 생성
            nextShootTime = Time.time + shootInterval;  // 다음 발사 시간 갱신
        }
    }
}
