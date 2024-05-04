using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ���� ������ ���� �������� ������ �迭
    public Transform spawnPosition; // ���Ͱ� ������ ��ġ
    
    private int currentMonsterIndex = 0; // ���� ������ ������ �ε���
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
            // ���� �ε����� �ش��ϴ� ���� �������� ����
            GameObject nextMonsterPrefab = monsterPrefabs[currentMonsterIndex];
            currentSpawnedMonster = Instantiate(nextMonsterPrefab, spawnPosition.position, spawnPosition.rotation);

            // ���Ͱ� ����� ���� �̺�Ʈ�� ���� ����
            Enemy enemyScript = currentSpawnedMonster.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.OnDeath += HandleMonsterDeath;
            }

            currentMonsterIndex++; // ���� ���� �ε����� �̵�
        }
        else
        {
            // ��� ���Ͱ� ������ ��� ���� ���� ����
            SpawnBoss();
        }
    }

    private void HandleMonsterDeath()
    {
        // ���� ������ ���� ��� �� ���� ���� ����
        StartCoroutine(WaitAndSpawnNextMonster());
    }

    private IEnumerator WaitAndSpawnNextMonster()
    {
        yield return new WaitForSeconds(spawnInterval); // ���� �ð� ��� �� ���� ���� ����
        SpawnNextMonster();
    }

    private void SpawnBoss()
    {
        // ���� ���͸� �����ϴ� ������ ���⿡ ����
        Debug.Log("���� ���� ����!");
    }
}