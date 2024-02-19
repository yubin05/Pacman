using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int[,] stageData;
    [SerializeField] Transform coinRoot;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] Transform enemyRoot;
    [SerializeField] GameObject enemyPrefab;

    // KeyValuePair<int, int>: {xPos, yPos}
    Dictionary<KeyValuePair<int, int>, GameObject> coinObjectPosDict = new Dictionary<KeyValuePair<int, int>, GameObject>();
    // Dictionary<string, KeyValuePair<int, int>> enemyPosDict = new Dictionary<string, KeyValuePair<int, int>>();
    List<Enemy> enemyList = new List<Enemy>();
    int enemyCnt = 4; int minSpawnPos = 5;
    // List<KeyValuePair<int, int>> enemySpawnPosList = new List<KeyValuePair<int, int>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StageInit();
    }

    void StageInit()
    {
        // 코인 생성
        stageData = Stage.GetStageData(StageIndex.Stage1);
        Stage.InitCoin(stageData);
        stageData[0,0] = (int)StageDataElement.None;    // 플레이어 위치는 코인 X
        InstantiateCoin(stageData);

        enemyList.Clear();
        // 적 생성
        for (int i=0; i<enemyCnt; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefab, enemyRoot);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.InitData();
            KeyValuePair<int, int> xPos_yPos = GetRandomSpawnPos(stageData, minSpawnPos);
            enemy.Spawn(xPos_yPos.Key, xPos_yPos.Value);
            enemyList.Add(enemy);
        }
    }

    KeyValuePair<int, int> GetRandomSpawnPos(int[,] stageData, int minSpawnValue)
    {
        bool find = false;
        int x = -1; int y = -1;
        while (!find)
        {
            x = UnityEngine.Random.Range(minSpawnValue, stageData.GetLength(1));
            y = UnityEngine.Random.Range(minSpawnValue, stageData.GetLength(0));
            if (stageData[y, x] != (int)StageDataElement.Wall && stageData[y, x] != (int)StageDataElement.Nothing)
            {
                find = true;
            }
        }
        return new KeyValuePair<int, int>(x, y);
    }

    void InstantiateCoin(int[,] stageData)
    {
        for (int i=0; i<stageData.GetLength(0); i++)
        {
            for (int j=0; j<stageData.GetLength(1); j++)
            {
                if (stageData[i,j] == (int)StageDataElement.Coin)
                {
                    GameObject instantiateGo = Instantiate(coinPrefab, coinRoot);
                    instantiateGo.GetComponent<Coin>().SetCoinOnBoard(j, i);
                    coinObjectPosDict[new KeyValuePair<int, int>(j, i)] = instantiateGo;
                }
            }
        }
    }

    /// <summary>
    /// 플레이어가 지나간 위치에 코인이 있는 지 체크하는 함수
    /// </summary>
    /// <param name="posOnBoard">플레이어 위치</param>
    public void CheckCoinOnBoard(Vector2 posOnBoard)
    {
        int x = (int)posOnBoard.x;
        int y = (int)posOnBoard.y;

        if (stageData[y, x] == (int)StageDataElement.Coin) 
        {
            stageData[y, x] = (int)StageDataElement.None;            

            KeyValuePair<int, int> xPos_yPos = new KeyValuePair<int, int>(x, y);
            // add score
            GameManager.Instance.AddScore(
                coinObjectPosDict[xPos_yPos].GetComponent<Coin>().score
            );
            Destroy(coinObjectPosDict[xPos_yPos]);
            coinObjectPosDict.Remove(xPos_yPos);

            SoundManager.Instance.PlaySFXAudio("Coin");
        }

        if (coinObjectPosDict.Count == 0) GameManager.Instance.GameClear();
    }
    /// <summary>
    /// 플레이어 위치에 적이 있는 지 체크하는 함수
    /// </summary>
    /// <param name="posOnBoard">플레이어 위치</param>
    public void CheckEnemyOnBoard(Vector2 posOnBoard)
    {
        // int x = (int)posOnBoard.x;
        // int y = (int)posOnBoard.y;

        // enemyList.ForEach((Enemy enemy) => {
        //     if (posOnBoard == enemy.GetPosVec2()) GameOver();
        // });
        bool find = false;
        for (int i=0; i<enemyList.Count; i++)
        {
            if (posOnBoard == enemyList[i].GetPosVec2())
            {
                find = true;
                break;
            }
        }
        if (find) GameManager.Instance.GameOver();
    }    
}
