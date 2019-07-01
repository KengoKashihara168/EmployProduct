using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField] private float   waveInterval;  // １カーブする時間(フレーム数)
    [SerializeField] private bool    isPositive;    // 正数フラグ
                     private float   angle;         // sinの角度
                     private Vector3 startPosition; // 初期座標

    private void Start()
    {
        angle = 0.0f;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 座標の更新
        transform.position = Froating();
    }

    /// <summary>
    /// 浮遊
    /// </summary>
    /// <returns>移動座標</returns>
    private Vector2 Froating()
    {
        // 初期位置にサイン波分足す
        Vector3 pos = startPosition;
        // 正数フラグがtrueなら上昇,falseなら下降
        float direction = isPositive ? 1.0f : -1.0f;
        pos.y += SineWave() * direction;
        return pos;
    }

    /// <summary>
    /// サイン波の計算
    /// </summary>
    /// <returns></returns>
    private float SineWave()
    {
        float sin = Unsign(Mathf.Sin(angle * Mathf.Deg2Rad));
        angle += 180.0f / waveInterval;
        return sin;
    }

    /// <summary>
    /// 負数を正数にする
    /// </summary>
    /// <param name="f">値</param>
    /// <returns>正数にした値</returns>
    private float Unsign(float f)
    {
        if(f < 0.0f)
        {
            f *= -1.0f;
        }
        return f;
    }
}
