using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private Rigidbody2D     rigid;                 // リジッドボディ
    private AlphaController alpha;                 // 透明度
    private readonly float  DefaultGravity = 4.0f; // 落下するときの重力

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        alpha = GetComponent<AlphaController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 落下
    /// </summary>
    /// <returns></returns>
    private IEnumerator Falling()
    {
        // 揺れてる子オブジェクトに切り替える
        ChangeObject(0.0f, true);
        yield return new WaitForSeconds(1.0f);
        // 元に戻して落下させる
        ChangeObject(1.0f, false);
        rigid.gravityScale = DefaultGravity;
    }

    /// <summary>
    /// 親オブジェクトと子オブジェクトの切替
    /// </summary>
    /// <param name="alpha">親オブジェクトの透明度</param>
    /// <param name="active">子オブジェクトのアクティブ</param>
    private void ChangeObject(float alpha,bool active)
    {
        this.alpha.ChangeAlpha(alpha);
        transform.GetChild(0).gameObject.SetActive(active);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="trigger"></param>
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag.Equals("Player"))
        {
            // 落下処理
            StartCoroutine(Falling());
        }
    }
}
