using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Left = -1,
    Right = 1,
}

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D                      rigid;     // Rigidbody2D
    [SerializeField] private float   speed;     // 移動速度
    [SerializeField] private float   jumpForce; // ジャンプ力
    private bool                     moveFlag;  // 移動可能フラグ
    private bool                     isJump;    // ジャンプ中のフラグ
    private Direction                direction; // 向き

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveFlag = true;
        isJump = false;
        direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(movement > 0)
        {
            // 右向き
            direction = Direction.Right;
        }
        else if(movement < 0)
        {
            // 左向き
            direction = Direction.Left;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private Vector2 Move()
    {
        if (!moveFlag) return Vector2.zero;
        // 「←」か「A」が押されたら"-1"「→」か「D」が押されたら"1"になる
        float moveX = Input.GetAxis("Horizontal");
        // 移動速度をかける
        moveX *= speed;
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
        Vector2 jump = new Vector2(rigid.velocity.x, jumpForce);
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
        float knockForce = 300.0f;
        Vector2 force = direction.normalized * knockForce;
        StartCoroutine(KnockBack(force));
    }

    /// <summary>
    /// ノックバック
    /// </summary>
    private IEnumerator KnockBack(Vector3 force)
    {
        for(int i = 0;i < PlayerController.KNOCK_BACK_TIME; i++)
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

    public Direction GetDirection()
    {
        return direction;
    }
}
