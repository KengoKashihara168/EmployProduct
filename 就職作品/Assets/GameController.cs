using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float limitTime;

    // Start is called before the first frame update
    void Start()
    {
        limitTime = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime(limitTime - Time.time);
    }

    void CheckTime(float time)
    {
        if (time >= 0.0f) return;

        Debug.Log("ゲームオーバー");
    }
}
