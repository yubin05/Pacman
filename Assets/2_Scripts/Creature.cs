using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected float standardPosX; protected float standardPosY;
    [SerializeField] protected Vector2 posOnBoard; // 보드 위에서의 위치값 (벽, 코인 등을 체크하기 위한 위치 값)

    protected virtual void Awake()
    {
        standardPosX = transform.position.x;
        standardPosY = transform.position.y;

        posOnBoard = Vector2.zero;
    }

    // protected abstract void Update();
    // protected abstract IEnumerator Move(Vector2 moveVec2);

    public virtual KeyValuePair<int, int> GetPos()
    {
        return new KeyValuePair<int, int>((int)posOnBoard.x, (int)posOnBoard.y);
    }

    public virtual Vector2 GetPosVec2()
    {
        return posOnBoard;
    }
}
