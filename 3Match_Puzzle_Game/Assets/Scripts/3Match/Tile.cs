using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Tile : MonoBehaviour
{

    public int x;
    public int y;

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
    
    public Tile Left
    {
        get
        {
            if (x > 0)
            {
                return Board.Instance.Tiles[x - 1, y];
            }
            else
            {
                return null;
            }
        }
    }
    
    public Tile Top
    {
        get
        {
            if (y > 0)
            {
                return Board.Instance.Tiles[x, y - 1];
            }
            else
            {
                return null;
            }
        }
    }
    
    public Tile Right
    {
        get
        {
            if (x < Board.Instance.Width - 1)
            {
                return Board.Instance.Tiles[x + 1, y];
            }
            else
            {
                return null;
            }
        }
    }

    public Tile Bottom
    {
        get
        {
            if (y < Board.Instance.Height - 1)
            {
                return Board.Instance.Tiles[x, y + 1];
            }
            else
            {
                return null;
            }
        }
    }

    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
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
