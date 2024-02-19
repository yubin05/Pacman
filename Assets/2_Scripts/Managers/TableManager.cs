using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum AnimatorSequence
{
    blueGhost, greenGhost, orangeGhost, redGhost, yellowGhost
}

public enum SpriteSequence
{
    blueGhost, greenGhost, orangeGhost, redGhost, yellowGhost
}

public enum CreateAssetMenuSequence
{
    GoogleSheetLoader=10000
}

public class TableManager : MonoBehaviour
{
    int enemyResourceCnt = 5;

    private static TableManager instance;
    public static TableManager Instance
    {
        get
        {
            return instance;
        }
    }

    readonly string animatorPath = "Animators/";
    readonly string spritePath = "Sprites/";

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

    public RuntimeAnimatorController GetAnimator(AnimatorSequence animatorSequence)
    {
        RuntimeAnimatorController animator;
        switch (animatorSequence)
        {
            case AnimatorSequence.blueGhost:
                animator = Resources.Load<RuntimeAnimatorController>(animatorPath + "blueGhost_0");
                break;
            case AnimatorSequence.greenGhost:
                animator = Resources.Load<RuntimeAnimatorController>(animatorPath + "greenGhost_0");
                break;
            case AnimatorSequence.orangeGhost:
                animator = Resources.Load<RuntimeAnimatorController>(animatorPath + "orangeGhost_0");
                break;
            case AnimatorSequence.redGhost:
                animator = Resources.Load<RuntimeAnimatorController>(animatorPath + "redGhost_0");
                break;
            case AnimatorSequence.yellowGhost:
                animator = Resources.Load<RuntimeAnimatorController>(animatorPath + "yellowGhost_0");
                break;
            default:
                Debug.LogError("존재하지 않는 Animator입니다");
                animator = null;
                break;
        }
        return animator;
    }

    public Sprite GetSprite(SpriteSequence spriteSequence)
    {
        Sprite sprite;
        switch (spriteSequence)
        {
            case SpriteSequence.blueGhost:
                sprite = Resources.Load<Sprite>(spritePath + "blueGhost_0");
                break;
            case SpriteSequence.greenGhost:
                sprite = Resources.Load<Sprite>(spritePath + "greenGhost_0");
                break;
            case SpriteSequence.orangeGhost:
                sprite = Resources.Load<Sprite>(spritePath + "orangeGhost_0");
                break;
            case SpriteSequence.redGhost:
                sprite = Resources.Load<Sprite>(spritePath + "redGhost_0");
                break;
            case SpriteSequence.yellowGhost:
                sprite = Resources.Load<Sprite>(spritePath + "yellowGhost_0");
                break;
            default:
                Debug.LogError("존재하지 않는 sprite입니다");
                sprite = null;
                break;
        }
        return sprite;
    }

    public int GetEnemyResourceCnt()
    {
        return enemyResourceCnt;
    }

}
