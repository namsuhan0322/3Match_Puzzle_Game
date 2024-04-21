using TMPro; // TextMeshProUGUI를 사용하기 위한 네임스페이스
using UnityEngine;

public sealed class ScoreCounter : MonoBehaviour
{
    // ScoreCounter 클래스의 인스턴스를 가져오기 위한 정적 속성
    public static ScoreCounter Instance { get; private set; }

    // 게임에서 사용될 점수를 나타내는 변수
    private int _score;

    // 점수를 나타내는 속성
    public int Score
    {
        get => _score; // 현재 점수를 반환

        set
        {
            if (_score == value) return; // 현재 점수와 변경될 점수가 같으면 아무것도 하지 않음

            _score = value; // 점수를 설정

            // TMPro TextMeshProUGUI 객체에 현재 점수를 표시
            scoreText.SetText($"{_score}/100");
        }
    }
    
    // Unity Inspector에서 할당할 점수를 표시할 TMPro TextMeshProUGUI 변수
    [SerializeField] private TextMeshProUGUI scoreText;

    // ScoreCounter 인스턴스를 설정하는 Awake 메서드
    private void Awake() => Instance = this;
}