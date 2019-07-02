using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの向き
public enum Direction
{
    Left = -1, // 左向き
    Right = 1, // 右向き
}

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D    rigid;                  // リジッドボディ
    private bool           moveFlag;               // 移動可能フラグ
    private bool           isJump;                 // ジャンプ中のフラグ
    private Direction      direction;              // 向き

    // 定数
    private readonly float Speed          = 5.0f;  // 移動速度
    private readonly float JumpForce      = 7.0f;  // ジャンプ力
    private readonly float KnockBackForce = 30.0f; // ノックバックする力
    private readonly int   KnockBackTime  = 10;    // ノックバックの時間

    // Start is called before the first frame update
    void Start()
    {
        rigid     = GetComponent<Rigidbody2D>();
        moveFlag  = true;
        isJump    = false;
        direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveFlag) return;

        // 移動        
        rigid.velocity = Move();

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.velocity = Jump();
        }
    }

    /// <summary>
    /// プレイヤーの向きを設定
    /// </summary>
    /// <param name="direct">移動方向</param>
    private void TurnAround(float movement)
    {
        if (movement == 0) return;
        // プラス方向に動いていれば右,マイナス方向に動いていれば左
        direction = movement > 0 ? Direction.Right : Direction.Left;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private Vector2 Move()
    {
        // 「←」か「A」が押されたら"-1"「→」か「D」が押されたら"1"になる
        float moveX = Input.GetAxis("Horizontal");
        // 移動速度をかける
        moveX *= Speed;
        Vector2 move = new Vector2(moveX, rigid.velocity.y);

        // もしジャンプ中なら横移動速度を半減する
        if (isJump) move.x *= 0.5f;

        // 向きを設定する
        TurnAround(moveX);

        return move;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private Vector2 Jump()
    {
        // もしすでにジャンプしていたら処理しない
        if (isJump) return rigid.velocity;
        Vector2 jump = new Vector2(rigid.velocity.x, JumpForce);
        // ジャンプした
        isJump = true;
        return jump;
    }

    /// <summary>
    /// 敵に当たった処理
    /// </summary>
    /// <param name="enemyPos">敵の座標</param>
    public void HitEnemy(Vector3 enemyPos)
    {
        // ノックバックする処理
        moveFlag = false;
        Vector2 direction = transform.position - enemyPos;
        Vector2 force = direction.normalized * KnockBackForce;
        StartCoroutine(KnockBack(force));
    }

    /// <summary>
    /// ノックバック
    /// </summary>
    private IEnumerator KnockBack(Vector3 force)
    {
        for(int i = 0;i < KnockBackTime; i++)
        {
            rigid.AddForce(force);
            yield return null;
        }
        moveFlag = true;
    }    

    public void Landing()
    {
        isJump = false; // 着地した
    }

    public float GetDirection()
    {
        return (float)direction;
    }

    public void Freeze()
    {
        moveFlag = false;
    }
}
