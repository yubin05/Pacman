using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    // float standardPosX; float standardPosY;
    // [SerializeField] Vector2 posOnBoard; // 보드 위에서의 위치값 (벽, 코인 등을 체크하기 위한 위치 값)

    IEnumerator IE_Move;
    WaitForSeconds moveDelay = new WaitForSeconds(0.2f);

    // Start is called before the first frame update
    protected override void Awake()
    {
        // standardPosX = transform.position.x;
        // standardPosY = transform.position.y;

        // posOnBoard = Vector2.zero;
        base.Awake();
        IE_Move = Move(Vector2.zero);
    }
    
    public void MoveSequence(Vector2 moveDir)
    {
        StopCoroutine(IE_Move);
        IE_Move = Move(moveDir);
        StartCoroutine(IE_Move);
    }
    IEnumerator Move(Vector2 moveVec2)
    {
        // Rotate
        if (moveVec2 == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (moveVec2 == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (moveVec2 == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else if (moveVec2 == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            Debug.LogError("잘못된 방향입니다");
            yield break;
        }

        do
        {
            Vector2 nextPos = posOnBoard + moveVec2;
            // 벽이나 프레임 바깥으로 통과하지 않도록 예외 처리
            if (nextPos.x < 0 || nextPos.x >= StageManager.Instance.stageData.GetLength(1)
            || nextPos.y < 0 || nextPos.y >= StageManager.Instance.stageData.GetLength(0))
            {
                yield break;
            }
            if (StageManager.Instance.stageData[(int)nextPos.y, (int)nextPos.x] == (int)StageDataElement.Wall)   // wall
            {
                yield break;
            }

            // StageManager.Instance.CheckEnemyOnBoard(nextPos);
            StageManager.Instance.CheckCoinOnBoard(nextPos);
            transform.position += (Vector3)moveVec2;
            posOnBoard += moveVec2;
            GameManager.Instance.SetPlayerPos(posOnBoard);  // 실제 움직임 + 게임 매니저에 알림
            
            yield return moveDelay;
        }
        while (true);
    }

    // public KeyValuePair<int, int> GetPos()
    // {
    //     return new KeyValuePair<int, int>((int)posOnBoard.x, (int)posOnBoard.y);
    // }

    // public Vector2 GetPosVec2()
    // {
    //     return posOnBoard;
    // }
}
