using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;


    void Start()
    {
        // 일정 시간 후에 몬스터 스폰 시작
        Invoke("StartMonsterSpawning", 1f);
    }

    void StartMonsterSpawning()
    {
        monsterSpawner.gameObject.SetActive(true); // 몬스터 스포너 활성화
    }
}