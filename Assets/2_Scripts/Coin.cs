using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float standardPosX; float standardPosY;
    [SerializeField] Vector2 posOnBoard;
    public int score = 100;

    void Awake()
    {
        standardPosX = transform.position.x;
        standardPosY = transform.position.y;

        posOnBoard = Vector2.zero;
    }

    /// <summary>
    /// 코인 위치 값을 받아 보드 위에 배치하는 함수
    /// </summary>
    /// <param name="x">x 좌표</param>
    /// <param name="y">y 좌표</param>
    public void SetCoinOnBoard(int x, int y)
    {
        posOnBoard = new Vector2(x, y);
        transform.position = new Vector2(standardPosX, standardPosY) + posOnBoard;
    }
}
