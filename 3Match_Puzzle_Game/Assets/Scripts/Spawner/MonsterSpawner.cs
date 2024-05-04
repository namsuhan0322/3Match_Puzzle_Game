using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 여러 종류의 몬스터 프리팹을 저장할 배열
    public Transform spawnPosition; // 몬스터가 생성될 위치
    
    private int currentMonsterIndex = 0; // 현재 스폰할 몬스터의 인덱스
    private GameObject currentSpawnedMonster;

    public float spawnInterval = 3.0f;

    void Start()
    {
        SpawnNextMonster();
    }

    public void SpawnNextMonster()
    {
        if (currentMonsterIndex < monsterPrefabs.Length)
        {
            // 현재 인덱스에 해당하는 몬스터 프리팹을 스폰
            GameObject nextMonsterPrefab = monsterPrefabs[currentMonsterIndex];
            currentSpawnedMonster = Instantiate(nextMonsterPrefab, spawnPosition.position, spawnPosition.rotation);

            // 몬스터가 사망할 때의 이벤트에 대한 구독
            Enemy enemyScript = currentSpawnedMonster.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.OnDeath += HandleMonsterDeath;
            }

            currentMonsterIndex++; // 다음 몬스터 인덱스로 이동
        }
        else
        {
            // 모든 몬스터가 스폰된 경우 보스 몬스터 스폰
            SpawnBoss();
        }
    }

    private void HandleMonsterDeath()
    {
        // 현재 스폰된 몬스터 사망 시 다음 몬스터 스폰
        StartCoroutine(WaitAndSpawnNextMonster());
    }

    private IEnumerator WaitAndSpawnNextMonster()
    {
        yield return new WaitForSeconds(spawnInterval); // 일정 시간 대기 후 다음 몬스터 스폰
        SpawnNextMonster();
    }

    private void SpawnBoss()
    {
        // 보스 몬스터를 스폰하는 로직을 여기에 구현
        Debug.Log("보스 몬스터 등장!");
    }
}