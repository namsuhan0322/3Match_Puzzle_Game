using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private MonsterSpawner monsterSpawner;
    private PlayerHealth playerHealth;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        monsterSpawner = FindObjectOfType<MonsterSpawner>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void EnemyKilled()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        if (monsters.Length == 0)
        {
            monsterSpawner.SetMonstersAlive(false);
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    public void HealPlayer(int healAmount)
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}