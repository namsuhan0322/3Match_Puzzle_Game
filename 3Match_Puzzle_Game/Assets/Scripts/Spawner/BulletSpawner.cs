using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet_prefab;        // 생성할 탄알의 원본 프리팹
    public float spqwn_rate_min = 0.5f;      // 최소 생성 주기
    public float spqwn_rate_max = 1.5f;      // 최대 생성 주기

    Transform target;   // 발사할 대상
    float spawn_rate;   // 생성 주기
    float time_after_spawn; // 최근 생성 시점에서 지난 시간

    void Start()
    {
        // 최근 생성 이후의 누적 시간을 0으로 초기화
        time_after_spawn = 0;

        // 탄알 생성 간격을 spqwn_rate_min과 spqwn_rate_max 사이에서 랜덤 지정  
        spawn_rate = Random.Range(spqwn_rate_min, spqwn_rate_max);

        // Enemy 컴포넌트를 가진 게임 오브젝트를 찾아 조준 대상으로 설정
        target = FindObjectOfType<MonsterSpawner>().transform;
    }

    void Update()
    {
        // time_after_spawn 갱신
        time_after_spawn += Time.deltaTime;

        // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (time_after_spawn > spawn_rate)
        {
            // 누적된 시간을 리셋
            time_after_spawn = 0;
        }
    }
    
    public void SpawnBullet()
    {
        // 총알 생성
        GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);

        // 총알 방향 설정
        Vector2 direction = (target.position - transform.position).normalized;
    
        // 총알 Rigidbody2D 컴포넌트 설정
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * bullet.GetComponent<Bullet>().speed;
    
        // 총알이 적(Enemy)을 향하도록 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            // 배열에서 랜덤하게 적(Enemy)을 선택하여 대상으로 설정
            target = enemies[Random.Range(0, enemies.Length)].transform;
        }
    }
}
