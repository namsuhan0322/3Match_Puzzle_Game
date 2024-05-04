using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;


    void Start()
    {
        // ���� �ð� �Ŀ� ���� ���� ����
        Invoke("StartMonsterSpawning", 1f);
    }

    void StartMonsterSpawning()
    {
        monsterSpawner.gameObject.SetActive(true); // ���� ������ Ȱ��ȭ
    }
}