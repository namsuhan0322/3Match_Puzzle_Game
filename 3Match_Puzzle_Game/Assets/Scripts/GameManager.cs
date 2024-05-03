using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;


    void Start()
    {
        // 일정 시간 후에 몬스터 스폰 시작
        Invoke("StartMonsterSpawning", 0);
    }

    void StartMonsterSpawning()
    {
        monsterSpawner.enabled = true; // 몬스터 스포너 활성화
    }
}
