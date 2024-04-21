using UnityEngine;

public class Attack : MonoBehaviour
{
    // ScoreCounter의 인스턴스
    private ScoreCounter _scoreCounter;
    
    // BulletSpawner 인스턴스
    private BulletSpawner bulletSpawner; 

    // 플레이어 공격에 필요한 점수
    public int meleeAttackCost = 50;
    public int fireAttackCost = 100;
    public int iceAttackCost = 70;
    public int lightningAttackCost = 150;
    public int bowAttackCost = 30;

    void Awake()
    {
        // ScoreCounter의 인스턴스를 가져옴
        _scoreCounter = ScoreCounter.Instance;

        // 버튼에 클릭 이벤트 리스너 추가는 Start에서 수행
        
        bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    // 각 공격에 대한 호출 메서드
    public void PerformAttack()
    {
        // 해당 공격에 필요한 점수를 가져옴
        int cost = 0;
        string attackType = "";

        // 해당 버튼에 따라 공격 종류와 점수를 설정
        if (gameObject.name == "MeleeButton")
        {
            cost = meleeAttackCost;
            attackType = "근접";
        }
        else if (gameObject.name == "FireButton")
        {
            cost = fireAttackCost;
            attackType = "불";
        }
        else if (gameObject.name == "IceButton")
        {
            cost = iceAttackCost;
            attackType = "얼음";
        }
        else if (gameObject.name == "LightningButton")
        {
            cost = lightningAttackCost;
            attackType = "번개";
        }
        else if (gameObject.name == "BowButton")
        {
            cost = bowAttackCost;
            attackType = "활";
        }

        // 플레이어의 점수가 공격에 필요한 점수보다 충분한지 확인
        if (_scoreCounter.Score >= cost)
        {
            // 공격 로직을 구현
            Debug.Log($"{attackType} 공격을 실행합니다!");

            // 점수를 감소시킴
            _scoreCounter.Score -= cost;

            // 총알 생성
            bulletSpawner.SpawnBullet(); // BulletSpawner의 SpawnBullet() 메서드 호출
        }
        else
        {
            // 플레이어의 점수가 부족할 때 처리할 내용 추가 가능
            Debug.Log("점수가 부족하여 공격할 수 없습니다.");
        }
    }
}