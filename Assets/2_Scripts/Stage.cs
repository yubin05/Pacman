using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageIndex
{
    Stage1
}

public enum StageDataElement    // Enemy는 안 쓸거 같음
{
    None, Wall, Coin, BigCoin, Enemy, Nothing
}

public static class Stage
{
    private static int[,] stage1Data = {  // 0:None, 1:Wall, 2:Coin, 3:BigCoin, 4:Enemy, 5:Nothing
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0},
        {0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0},
        {0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0},
        {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0},
        {0, 1, 1, 1, 1, 0, 1, 5, 5, 1, 0, 1, 1, 0, 1, 0},
        {0, 1, 1, 1, 1, 0, 1, 5, 5, 1, 0, 1, 0, 0, 1, 0},
        {0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0},
        {0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0},
        {0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0},
        {0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0},
        {0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0},
        {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0},
        {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0},
        {0, 1, 5, 5, 1, 0, 1, 5, 5, 1, 0, 1, 0, 1, 1, 0},
        {0, 1, 5, 5, 1, 0, 1, 5, 5, 1, 0, 1, 0, 0, 0, 0},
        {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };

    public static int[,] GetStageData(StageIndex stageIndex)
    {
        switch (stageIndex)
        {
            case StageIndex.Stage1:
                return stage1Data;
            default:
                Debug.LogError("해당 스테이지 데이터는 존재하지 않습니다.");
                return null;
        }
    }

    public static void InitCoin(int[,] stageData)
    {
        for (int i=0; i<stageData.GetLength(0); i++)
        {
            for (int j=0; j<stageData.GetLength(1); j++)
            {
                if (stageData[i,j] == 0) stageData[i,j] = (int)StageDataElement.Coin;
            }
        }
    }
}
