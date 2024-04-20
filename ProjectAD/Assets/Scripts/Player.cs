using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // 메인 카메라 가져오기
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸는지 확인
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치를 화면 좌표로 가져오기
            Vector3 mousePosition = Input.mousePosition;
            // 화면 좌표를 월드 좌표로 변환
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // 변환된 월드 좌표를 보드의 타일 위치로 매핑
            Tile tile = GetTileAtPosition(worldPosition);
            if (tile != null)
            {
                // 보드의 Select 메서드 호출
                Board.Instance.Select(tile);
            }
        }
    }

    private Tile GetTileAtPosition(Vector3 position)
    {
        // 픽셀 좌표를 보드의 타일 좌표로 변환
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        // 변환된 타일 좌표가 보드 내에 있는지 확인
        if (x >= 0 && x < Board.Instance.Width && y >= 0 && y < Board.Instance.Height)
        {
            // 해당 타일 반환
            return Board.Instance.Tiles[x, y];
        }
        else
        {
            // 보드 범위를 벗어난 경우 null 반환
            return null;
        }
    }
}
