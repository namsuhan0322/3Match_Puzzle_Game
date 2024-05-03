using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 여러 종류의 몬스터 프리팹을 저장할 배열
    public Transform spawnPosition; // 몬스터가 생성될 위치

    public float spawnInterval = 0.2f;

    void Start()
    {
        StartCoroutine(SpawnMonsterRoutine());
    }

    IEnumerator SpawnMonsterRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 랜덤하게 몬스터 프리팹을 선택하여 스폰
            GameObject randomMonsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            Instantiate(randomMonsterPrefab, spawnPosition.position, spawnPosition.rotation);
        }
    }
}
