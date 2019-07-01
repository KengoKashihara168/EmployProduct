using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    private Vector2 startPosition; // 初期位置

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 左右に揺れ続ける
        Vector2 pos = startPosition;
        pos.x += Mathf.Sin(Time.time * 20) * 0.1f;
        transform.localPosition = pos;
    }
}
