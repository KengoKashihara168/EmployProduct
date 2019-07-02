using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Enemy
{
    [SerializeField] private float moveSpeed; // 移動速度

    // Update is called once per frame
    void Update()
    {
        // 徘徊
        Wandering();
    }

    /// <summary>
    /// 徘徊
    /// </summary>
    private void Wandering()
    {
        Vector2 move = new Vector2(moveSpeed, rigid.velocity.y);
        rigid.velocity = move;
    }

    /// <summary>
    /// 進行方向の反転
    /// </summary>
    private void InvertSpeed()
    {
        moveSpeed *= -1.0f;
    }

    /// <summary>
    /// 衝突終了判定(トリガー)
    /// </summary>
    /// <param name="trigger"></param>
    private void OnTriggerExit2D(Collider2D trigger)
    {
        // 足(子オブジェクト)が地面から離れたら反転する
        if(trigger.tag.Equals("Ground"))
        {
            InvertSpeed();
        }
    }
}
