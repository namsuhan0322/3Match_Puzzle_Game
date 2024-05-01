using System;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; } // Board 클래스의 싱글톤 인스턴스

    [SerializeField] private AudioClip collectSound; // 아이템 제거 시 재생할 사운드 클립
    [SerializeField] private AudioSource audioSource; // 사운드 재생을 위한 AudioSource
    
    public Row[] rows; // 게임 보드의 행을 나타내는 Row 배열

    public Tile[,] Tiles { get; private set; } // 보드에 배치된 타일을 나타내는 이차원 배열

    public int Width => Tiles.GetLength(dimension: 0); // 보드의 가로 크기
    public int Height => Tiles.GetLength(dimension: 1); // 보드의 세로 크기

    private readonly List<Tile> _selection = new List<Tile>(); // 선택된 타일을 저장하는 리스트

    private const float TweenDuration = 0.25f; // 타일 교환 애니메이션 지속 시간

    private void Awake() => Instance = this; // 인스턴스 초기화 및 싱글톤 인스턴스 설정

    private void Start()
    {
        Tiles = new Tile[rows.Max(selector: row => row.tiles.Length), rows.Length]; // 타일 배열 초기화 및 배치

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x; // 타일의 x 좌표 설정
                tile.y = y; // 타일의 y 좌표 설정

                tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)]; // 타일에 아이템 설정

                Tiles[x, y] = tile; // 타일을 배열에 배치
            }
        }
    }

    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) _selection.Add(tile); // 선택된 타일을 리스트에 추가
            
        if (_selection.Count < 2) return; // 선택된 타일이 2개 미만이면 리턴

        await Swap(_selection[0], _selection[1]); // 선택된 두 타일 교환

        if (CanPop()) // 일치하는 타일이 존재하는지 확인
        {
            Pop(); // 일치하는 타일 제거
        }
        else 
        {
            await Swap(_selection[0], _selection[1]); // 일치하지 않으면 다시 두 타일을 원래대로 교환
        }

        _selection.Clear(); // 선택된 타일 초기화
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence(); // DOTween 시퀀스 생성

        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration)) // 아이콘 교환 애니메이션
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play() // 애니메이션 재생
                      .AsyncWaitForCompletion(); // 애니메이션이 완료될 때까지 대기

        icon1Transform.SetParent(tile2.transform); // 아이콘의 부모 설정 변경
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2; // 타일 아이콘 교환
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item; // 타일의 아이템 교환
        tile2.Item = tile1Item;
    }

    private bool CanPop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2) // 일치하는 타일이 존재하는지 확인
                    return true;
            }
        }
        return false;
    }

    private async void Pop() 
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++) 
            { 
                var tile = Tiles[x, y];

                var connectedTiles = tile.GetConnectedTiles(); // 연결된 타일 가져오기

                if (connectedTiles.Skip(1).Count() < 2) continue; // 일치하는 타일이 2개 이상이 아니면 다음으로 넘어감

                var deflateSequence = DOTween.Sequence(); // 타일 제거 애니메이션 시퀀스 생성

                foreach (var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration)); // 타일 아이콘 크기 축소
                }
                
                audioSource.PlayOneShot(collectSound); // 아이템 수집 사운드 재생

                await deflateSequence.Play() // 애니메이션 재생
                                     .AsyncWaitForCompletion(); // 애니메이션이 완료될 때까지 대기

                ScoreCounter.Instance.Score += tile.Item.value * connectedTiles.Count; // 점수 계산
                
                var inflateSequence = DOTween.Sequence(); // 타일 생성 애니메이션 시퀀스 생성

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)]; // 새로운 아이템 설정

                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration)); // 타일 아이콘 크기 확대
                }

                await inflateSequence.Play() // 애니메이션 재생
                                     .AsyncWaitForCompletion(); // 애니메이션이 완료될 때까지 대기

                // 반복을 위한 인덱스 초기화
                x = 0;
                y = 0;
            }
        }
    }
}
