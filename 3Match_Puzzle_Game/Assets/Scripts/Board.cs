using System;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; } // Board Ŭ������ �̱��� �ν��Ͻ�

    [SerializeField] private AudioClip collectSound; // ������ ���� �� ����� ���� Ŭ��
    [SerializeField] private AudioSource audioSource; // ���� ����� ���� AudioSource
    
    public Row[] rows; // ���� ������ ���� ��Ÿ���� Row �迭

    public Tile[,] Tiles { get; private set; } // ���忡 ��ġ�� Ÿ���� ��Ÿ���� ������ �迭

    public int Width => Tiles.GetLength(dimension: 0); // ������ ���� ũ��
    public int Height => Tiles.GetLength(dimension: 1); // ������ ���� ũ��

    private readonly List<Tile> _selection = new List<Tile>(); // ���õ� Ÿ���� �����ϴ� ����Ʈ

    private const float TweenDuration = 0.25f; // Ÿ�� ��ȯ �ִϸ��̼� ���� �ð�

    private void Awake() => Instance = this; // �ν��Ͻ� �ʱ�ȭ �� �̱��� �ν��Ͻ� ����

    private void Start()
    {
        Tiles = new Tile[rows.Max(selector: row => row.tiles.Length), rows.Length]; // Ÿ�� �迭 �ʱ�ȭ �� ��ġ

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x; // Ÿ���� x ��ǥ ����
                tile.y = y; // Ÿ���� y ��ǥ ����

                tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)]; // Ÿ�Ͽ� ������ ����

                Tiles[x, y] = tile; // Ÿ���� �迭�� ��ġ
            }
        }
    }

    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) _selection.Add(tile); // ���õ� Ÿ���� ����Ʈ�� �߰�
            
        if (_selection.Count < 2) return; // ���õ� Ÿ���� 2�� �̸��̸� ����

        await Swap(_selection[0], _selection[1]); // ���õ� �� Ÿ�� ��ȯ

        if (CanPop()) // ��ġ�ϴ� Ÿ���� �����ϴ��� Ȯ��
        {
            Pop(); // ��ġ�ϴ� Ÿ�� ����
        }
        else 
        {
            await Swap(_selection[0], _selection[1]); // ��ġ���� ������ �ٽ� �� Ÿ���� ������� ��ȯ
        }

        _selection.Clear(); // ���õ� Ÿ�� �ʱ�ȭ
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence(); // DOTween ������ ����

        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration)) // ������ ��ȯ �ִϸ��̼�
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play() // �ִϸ��̼� ���
                      .AsyncWaitForCompletion(); // �ִϸ��̼��� �Ϸ�� ������ ���

        icon1Transform.SetParent(tile2.transform); // �������� �θ� ���� ����
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2; // Ÿ�� ������ ��ȯ
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item; // Ÿ���� ������ ��ȯ
        tile2.Item = tile1Item;
    }

    private bool CanPop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2) // ��ġ�ϴ� Ÿ���� �����ϴ��� Ȯ��
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

                var connectedTiles = tile.GetConnectedTiles(); // ����� Ÿ�� ��������

                if (connectedTiles.Skip(1).Count() < 2) continue; // ��ġ�ϴ� Ÿ���� 2�� �̻��� �ƴϸ� �������� �Ѿ

                var deflateSequence = DOTween.Sequence(); // Ÿ�� ���� �ִϸ��̼� ������ ����

                foreach (var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration)); // Ÿ�� ������ ũ�� ���
                }
                
                audioSource.PlayOneShot(collectSound); // ������ ���� ���� ���

                await deflateSequence.Play() // �ִϸ��̼� ���
                                     .AsyncWaitForCompletion(); // �ִϸ��̼��� �Ϸ�� ������ ���

                ScoreCounter.Instance.Score += tile.Item.value * connectedTiles.Count; // ���� ���
                
                var inflateSequence = DOTween.Sequence(); // Ÿ�� ���� �ִϸ��̼� ������ ����

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)]; // ���ο� ������ ����

                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration)); // Ÿ�� ������ ũ�� Ȯ��
                }

                await inflateSequence.Play() // �ִϸ��̼� ���
                                     .AsyncWaitForCompletion(); // �ִϸ��̼��� �Ϸ�� ������ ���

                // �ݺ��� ���� �ε��� �ʱ�ȭ
                x = 0;
                y = 0;
            }
        }
    }
}
