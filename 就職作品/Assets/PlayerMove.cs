using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D                      rigid;     // Rigidbody2D
    [SerializeField] private float   speed;     // 移動速度
    [SerializeField] private float   jumpForce; // ジャンプ力
    private bool                     moveFlag;  // 移動可能フラグ
    private bool                     isJump;    // ジャンプ中のフラグ

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveFlag = true;
        isJump = false;        
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        rigid.velocity = Move();

        // ジャンプ
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.velocity = Jump();
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private Vector2 Move()
    {
        if (!moveFlag) return Vector2.zero;
        // 「←」か「A」が押されたら"-1"「→」か「D」が押されたら"1"になる
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), rigid.velocity.y);
        // 移動速度をかける
        move.x *= speed;

        // もしジャンプ中なら横移動速度を半減する
        if (isJump) move.x *= 0.5f;

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
    
    public void HitEnemy(Vector3 otherPos)
    {
        moveFlag = false;
        Vector2 direction = transform.position - otherPos;
        float knockForce = 200.0f;
        Vector2 force = direction.normalized * knockForce;
        StartCoroutine(KnockBack(force));
    }

    /// <summary>
    /// ノックバック
    /// </summary>
    private IEnumerator KnockBack(Vector3 force)
    {      
        for(int i = 0;i < 10.0f;i++)
        {
            rigid.AddForce(force);
            yield return null;
        }
        moveFlag = true;
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {  
        isJump = false; // 着地した
    }
}
