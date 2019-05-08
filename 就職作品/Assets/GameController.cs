using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float limitTime;
<<<<<<< HEAD
=======
    public Sprite sprite;
>>>>>>> eeea10468d7dab342c2864f296158833c193dd11

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
