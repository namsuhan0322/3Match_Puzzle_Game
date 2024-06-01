using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnDelay = 3f;
    public Transform spawnPoint;

    private bool areMonstersAlive;
    private int maxStages = 3; // 최대 스테이지 수
    
    public GameObject gameClear;

    void Start()
    {
        areMonstersAlive = true;
        Invoke("SpawnMonster", spawnDelay);
    }

    void Update()
    {
        if (!areMonstersAlive)
        {
            LoadNextStage();
        }
    }

    void SpawnMonster()
    {
        Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void SetMonstersAlive(bool alive)
    {
        areMonstersAlive = alive;
    }
    
    public void EnemyKilled()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        if (monsters.Length == 0)
        {
            SetMonstersAlive(false);
        }
    }
    
    void LoadNextStage()
    {
        StartCoroutine(TransitionToNextStage());
    }

    private IEnumerator TransitionToNextStage()
    {
        // 페이드 아웃 효과
        FadeEffect fadeEffect = FindObjectOfType<FadeEffect>();
        if (fadeEffect != null)
        {
            yield return StartCoroutine(fadeEffect.Fade(0f, 1f)); // 페이드 아웃
        }

        // 현재 씬의 이름을 가져옴
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 스테이지의 번호를 추출
        int currentStageNumber = int.Parse(currentSceneName.Substring("Stage".Length));

        if (currentStageNumber < maxStages)
        {
            // 다음 스테이지로 이동
            int nextStageNumber = currentStageNumber + 1;
            string nextStageName = "Stage" + nextStageNumber;
            SceneManager.LoadScene(nextStageName);
        }

        GameManager gameManager = FindObjectOfType<GameManager>();
        
        if (currentStageNumber == maxStages)
        {
            gameManager.EnemyKilled();
            
            Time.timeScale = 0;
            
            gameClear.SetActive(true);
        }

        // 페이드 인 효과
        if (fadeEffect != null)
        {
            yield return StartCoroutine(fadeEffect.Fade(1f, 0f)); // 페이드 인 (시간은 0.5초로 설정)
        }
    }
}