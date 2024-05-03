using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ���� ������ ���� �������� ������ �迭
    public Transform spawnPosition; // ���Ͱ� ������ ��ġ

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

            // �����ϰ� ���� �������� �����Ͽ� ����
            GameObject randomMonsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            Instantiate(randomMonsterPrefab, spawnPosition.position, spawnPosition.rotation);
        }
    }
}
