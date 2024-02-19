using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField] Vector2 finishPosVec2;
    IEnumerator IE_Move;
    WaitForSeconds moveDelay = new WaitForSeconds(0.23f);

    // float standardPosX; float standardPosY;
    // [SerializeField] Vector2 posOnBoard;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // standardPosX = transform.position.x;
        // standardPosY = transform.position.y;
        // posOnBoard = Vector2.zero;
        IE_Move = Move();
    }

    void Start()
    {
        // GameManager.Instance.enemyMoveNotification += () => StartCoroutine(Move());
        GameManager.Instance.playerMoveNotification += SetFinishPos;
        finishPosVec2 = GameManager.Instance.playerPosOnBoard;
        StartCoroutine(IE_Move);

        InvokeRepeating("CheckPlayerPos", 0f, 0.1f);
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    // public GameObject InstantiateEnemy(int enemyIndex = -1)
    // {
    //     InitData(enemyIndex);

    //     // Enemy 생성
    //     return gameObject;
    // }

    public void InitData(int enemyIndex = -1)
    {
        if (enemyIndex == -1 || enemyIndex >= TableManager.Instance.GetEnemyResourceCnt())   // random select
        {
            enemyIndex = UnityEngine.Random.Range(0, TableManager.Instance.GetEnemyResourceCnt());
        }
        spriteRenderer.sprite = TableManager.Instance.GetSprite((SpriteSequence)enemyIndex);
        animator.runtimeAnimatorController = TableManager.Instance.GetAnimator((AnimatorSequence)enemyIndex);
    }
    /// <summary>
    /// 지정 위치에 적 스폰하는 함수
    /// </summary>
    /// <param name="xPos">스폰 위치 x좌표</param>
    /// <param name="yPos">스폰 위치 y좌표</param>
    public void Spawn(int xPos, int yPos)
    {   
        transform.position = new Vector2(
            transform.position.x + xPos, transform.position.y + yPos
        );
        posOnBoard = new Vector2(xPos, yPos);
    }

    List<Vector2> moveVec2List = new List<Vector2>() {Vector2.up, Vector2.left, Vector2.down, Vector2.right};
    
    // 플레이어를 향해 움직임 - 길찾기 알고리즘 활용
    public IEnumerator Move()
    {
        int moveVecIndex;
        do
        {
            Vector2 moveVec2;
            Vector2 moveDir = finishPosVec2-posOnBoard;
            // Debug.Log($"moveDir.x: {moveDir.x}, moveDir.y: {moveDir.y}");
            if (Mathf.Abs(moveDir.x) < Mathf.Abs(moveDir.y))
            {
                if (moveDir.y < 0)
                {
                    moveVecIndex = 2;
                }   
                else
                {
                    moveVecIndex = 0;
                }
            }
            else
            {
                if (moveDir.x < 0)
                {
                    moveVecIndex = 1;
                }   
                else
                {
                    moveVecIndex = 3;
                }
            }

            bool finding = false;
            // 벽이나 프레임 바깥으로 통과하지 않도록 예외 처리
            do
            {
                moveVec2 = moveVec2List[moveVecIndex];
                Vector2 nextPos = posOnBoard + moveVec2;
                int offset = UnityEngine.Random.Range(1, moveVec2List.Count);

                if (nextPos.x < 0 || nextPos.x >= StageManager.Instance.stageData.GetLength(1)
                || nextPos.y < 0 || nextPos.y >= StageManager.Instance.stageData.GetLength(0))
                {
                    finding = true;
                    moveVecIndex = (moveVecIndex + offset) < moveVec2List.Count ? moveVecIndex+offset : moveVecIndex+offset-moveVec2List.Count;
                }
                else
                {
                    if (StageManager.Instance.stageData[(int)nextPos.y, (int)nextPos.x] == (int)StageDataElement.Wall)   // wall
                    {
                        finding = true;
                        moveVecIndex = (moveVecIndex + offset) < moveVec2List.Count ? moveVecIndex+offset : moveVecIndex+offset-moveVec2List.Count;    
                    }
                    else
                    {
                        finding = false;
                    }
                }
                
            }
            while (finding);
            
            transform.position += (Vector3)moveVec2;
            posOnBoard += moveVec2;

            yield return moveDelay;
        }
        while (true);
        // InitShortPath();
        // yield return null;
    }

    // List<Vector2> moveVecList = new List<Vector2>();
    // 최단 경로 알고리즘 설정하는 함수
    // void InitShortPath()
    // {
    //     moveVec2List.Clear();
    //     int row = StageManager.Instance.stageData.GetLength(0);
    //     int col = StageManager.Instance.stageData.GetLength(1);

    //     int[,] stageDistanceData = new int[row,col];
    //     for (int i=0; i<row; i++)
    //     {
    //         for (int j=0; j<col; j++)
    //         {
    //             if (StageManager.Instance.stageData[i,j] == (int)StageDataElement.Wall)
    //             {
    //                 stageDistanceData[i,j] = -1;
    //             } 
    //             else
    //             {
    //                 stageDistanceData[i,j] = 0;
    //             }
    //         }
    //     }

    //     // 최단 경로 찾기 - BFS 알고리즘 활용
    //     Vector2 startPos = posOnBoard; Vector2 finishPos = finishPosVec2;
    //     Vector2 currentPos = new Vector2(startPos.x, startPos.y);
    //     // stageData[(int)currentPos.y, (int)currentPos.x] = -1;

    //     Queue<KeyValuePair<int,int>> searchPosQueue = new Queue<KeyValuePair<int, int>>();   // xPos, yPos
    //     searchPosQueue.Enqueue(new KeyValuePair<int, int>((int)currentPos.x, (int)currentPos.y));

    //     while (searchPosQueue.Count > 0)
    //     {
    //         var xPos_yPos = searchPosQueue.Dequeue();
    //         int x = xPos_yPos.Key; int y = xPos_yPos.Value;

    //         int distance = stageDistanceData[y, x] + 1;
    //         // 상
    //         if (y+1 < stageDistanceData.GetLength(0) && stageDistanceData[y+1, x] == 0)
    //         {
    //             stageDistanceData[y+1, x] = distance;
    //             searchPosQueue.Enqueue(new KeyValuePair<int, int>(x, y+1));
    //         }
    //         // 하
    //         if (y-1 >= 0 && stageDistanceData[y-1, x] == 0)
    //         {
    //             stageDistanceData[y-1, x] = distance;
    //             searchPosQueue.Enqueue(new KeyValuePair<int, int>(x, y-1));
    //         }
    //         // 좌
    //         if (x-1 >= 0 && stageDistanceData[y, x-1] == 0)
    //         {
    //             stageDistanceData[y, x-1] = distance;
    //             searchPosQueue.Enqueue(new KeyValuePair<int, int>(x-1, y));
    //         }
    //         // 우
    //         if (x+1 < stageDistanceData.GetLength(1) && stageDistanceData[y, x+1] == 0)
    //         {
    //             stageDistanceData[y, x+1] = distance;
    //             searchPosQueue.Enqueue(new KeyValuePair<int, int>(x+1, y));
    //         }
    //     }
    //     // PrintStage(stageDistanceData);
    //     // MoveStage(startPos, finishPos);
    // }
    // void MoveStage(Vector2 startPos, Vector2 finishPos)
    // {
        
    // }
    // // 모든 경로를 탐색했는 지 체크하여 bool 변수를 return하는 함수
    // bool CheckStage(int[,] stageData)
    // {
    //     for (int i=0; i<stageData.GetLength(0); i++)
    //     {
    //         for (int j=0; j<stageData.GetLength(1); j++)
    //         {
    //             if (stageData[i,j] == 0) return true;
    //         }
    //     }
    //     return false;
    // }
    // void PrintStage(int[,] stageData)
    // {
    //     string printStr = "";
    //     for (int i=stageData.GetLength(0)-1; i>=0; i--)
    //     {
    //         for (int j=0; j<stageData.GetLength(1); j++)
    //         {
    //             printStr += string.Format("{0}", stageData[i,j]) + " ";
    //         }
    //         printStr += "\n";
    //     }
    //     Debug.Log(printStr);
    // }

    // 플레이어 위치(목적지) 변경 시 실행되는 함수
    void SetFinishPos()
    {
        StopCoroutine(IE_Move);
        // Debug.Log("플레이어 움직임 감지");
        finishPosVec2 = GameManager.Instance.playerPosOnBoard;
        StartCoroutine(IE_Move);
    }

    void CheckPlayerPos()
    {
        if (posOnBoard == GameManager.Instance.playerPosOnBoard)
            GameManager.Instance.GameOver();
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
