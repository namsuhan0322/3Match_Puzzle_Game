using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Tile : MonoBehaviour
{
    public string iconObjectName; // 타일에 해당하는 아이콘 게임 오브젝트의 이름

    public int x;
    public int y;

    // 아이템 이름으로 타일을 찾는 메서드
    public static Tile FindTileByIconObjectName(string iconObjectName)
    {
        // 모든 타일을 찾아서 검사
        foreach (Tile tile in Board.Instance.Tiles)
        {
            // 현재 타일의 아이콘 게임 오브젝트의 이름이 찾고자 하는 아이콘 이름과 일치하는지 확인
            if (tile.iconObjectName == iconObjectName)
            {
                // 일치하는 타일 반환
                return tile;
            }
        }

        // 일치하는 타일이 없는 경우 null 반환
        return null;
    }

    private Item _item;

    public Item Item
    {
        get => _item;

        set
        {
            if (_item == value) return;

            _item = value;

            icon.sprite = _item.sprite;
        }
    }

    public Image icon;

    public Button button;

    public Tile Left => x > 0 ? Board.Instance.Tiles[x - 1, y] : null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1] : null;
    public Tile RIght => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null;
    public Tile Bottom => y < Board.Instance.Height - 1 ? Board.Instance.Tiles[x, y + 1] : null;

    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        RIght,
        Bottom,
    };

    private void Start() => button.onClick.AddListener(call: () => Board.Instance.Select(tile: this));

    public List<Tile> GetConnectedTiles(List<Tile> exclude = null)
    {
        var result = new List<Tile> { this, };

        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }
}
