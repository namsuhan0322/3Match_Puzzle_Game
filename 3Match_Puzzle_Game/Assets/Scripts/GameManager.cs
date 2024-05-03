using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;


    void Start()
    {
        // ���� �ð� �Ŀ� ���� ���� ����
        Invoke("StartMonsterSpawning", 0);
    }

    void StartMonsterSpawning()
    {
        monsterSpawner.enabled = true; // ���� ������ Ȱ��ȭ
    }
}
