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

    // 근접 공격 호출 메서드
    public void MeleeAttack()
    {
        PerformAttack(meleeAttackCost, "근접");
    }

    // 불 공격 호출 메서드
    public void FireAttack()
    {
        PerformAttack(fireAttackCost, "불");
    }

    // 물 공격 호출 메서드
    public void IceAttack()
    {
        PerformAttack(iceAttackCost, "얼음");
    }

    // 번개 공격 호출 메서드
    public void LightningAttack()
    {
        PerformAttack(lightningAttackCost, "번개");
    }

    // 활 공격 호출 메서드
    public void BowAttack()
    {
        PerformAttack(bowAttackCost, "활");
    }

    // 각 공격에 대한 호출 메서드
    private void PerformAttack(int cost, string attackType)
    {
        // 플레이어의 점수가 공격에 필요한 점수보다 충분한지 확인
        if (_scoreCounter.Score >= cost)
        {
            // 공격 로직을 구현
            Debug.Log($"코스트 {cost} 의 {attackType} 공격을 실행합니다!");

            // 점수를 감소시킴
            _scoreCounter.Score -= cost;

            // 총알 생성
            bulletSpawner.SpawnBullet(); // BulletSpawner의 SpawnBullet() 메서드 호출
        }
        else
        {
            // 플레이어의 점수가 부족할 때 처리할 내용 추가 가능
            Debug.Log($"코스트 {cost} 의 {attackType} 공격의 코스트가 부족하여 공격할 수 없습니다.");
        }
    }
}