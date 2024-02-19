using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 endPos;

    bool canInput = true;

    void Update()
    {
// #if UNITY_EDITOR
//         if (Input.GetKeyDown(KeyCode.W))
//         {
//             player.MoveSequence(Vector2.up);
//         }
//         else if (Input.GetKeyDown(KeyCode.A))
//         {
//             player.MoveSequence(Vector2.left);
//         }
//         else if (Input.GetKeyDown(KeyCode.S))
//         {
//             player.MoveSequence(Vector2.down);
//         }
//         else if (Input.GetKeyDown(KeyCode.D))
//         {
//             player.MoveSequence(Vector2.right);
//         }
// # else
        // 손가락 하나로 터치할 때만 움직이도록 적용
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    // Debug.Log($"startPos: {startPos.x}, {startPos.y}");
                    break;
                case TouchPhase.Moved:
                    // Report that the touch has ended when it ends
                    endPos = touch.position;
                    // Debug.Log($"endPos: {endPos.x}, {endPos.y}");

                    // 스와이프한 방향을 체크하여 움직임
                    Vector2 swipeVec = endPos - startPos; Vector2 moveVec;
                    if (Mathf.Abs(swipeVec.x) < Mathf.Abs(swipeVec.y))
                    {
                        if (swipeVec.y < 0) moveVec = Vector2.down;
                        else moveVec = Vector2.up;
                    }
                    else
                    {
                        if (swipeVec.x < 0) moveVec = Vector2.left;
                        else moveVec = Vector2.right;
                    }
                    
                    if (canInput)
                    {
                        canInput = false;
                        player.MoveSequence(moveVec);
                        StartCoroutine(InputInterval());
                    }
                    break;
            }

            // Debug.Log($"moveVec: {endPos.x-startPos.x}, {endPos.y-startPos.y}");
        }
        // test code
        else if (Input.touchCount > 1)
        {
            Debug.Log("Multiple Touch Detected");
        }
// #endif
    }

    WaitForSeconds interval = new WaitForSeconds(0.3f);
    IEnumerator InputInterval()
    {
        yield return interval;
        canInput = true;
        yield break;
    }
}
