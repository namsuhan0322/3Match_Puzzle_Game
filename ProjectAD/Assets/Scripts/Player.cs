using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // ���� ī�޶� ��������
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // ���콺 ���� ��ư�� ���ȴ��� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ�� ȭ�� ��ǥ�� ��������
            Vector3 mousePosition = Input.mousePosition;
            // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // ��ȯ�� ���� ��ǥ�� ������ Ÿ�� ��ġ�� ����
            Tile tile = GetTileAtPosition(worldPosition);
            if (tile != null)
            {
                // ������ Select �޼��� ȣ��
                Board.Instance.Select(tile);
            }
        }
    }

    private Tile GetTileAtPosition(Vector3 position)
    {
        // �ȼ� ��ǥ�� ������ Ÿ�� ��ǥ�� ��ȯ
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        // ��ȯ�� Ÿ�� ��ǥ�� ���� ���� �ִ��� Ȯ��
        if (x >= 0 && x < Board.Instance.Width && y >= 0 && y < Board.Instance.Height)
        {
            // �ش� Ÿ�� ��ȯ
            return Board.Instance.Tiles[x, y];
        }
        else
        {
            // ���� ������ ��� ��� null ��ȯ
            return null;
        }
    }
}
