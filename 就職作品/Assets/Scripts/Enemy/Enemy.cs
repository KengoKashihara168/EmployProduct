using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Life life;
    private AlphaController alpha;

    readonly int RiseTime = 30;
    readonly float RiseLength = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        life = GetComponent<Life>();
        alpha = GetComponent<AlphaController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Die()
    {
        // レイヤーを「Die」に変える
        gameObject.layer = life.DieLayer;        
        for (int i = 0;i < RiseTime;i++)
        {
            // 上昇
            Rise();
            // フェードアウト
            Fade(i);
            yield return null;
        }
        // 消滅
        Destroy(gameObject);
    }

    /// <summary>
    /// 上昇
    /// </summary>
    private void Rise()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
        // 現在の座標から0.05ずつ上昇
        Vector2 risePosition = transform.position;
        risePosition.y += RiseLength;
        transform.position = risePosition;
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    private void Fade(int frame)
    {
        // 上昇時間の間減少していく
        float fade = 1.0f - (1.0f / RiseTime * frame);
        alpha.ChangeAlpha(fade);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ball"))
        {
            // ダメージを受ける
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if(ball.IsThrown())
            {
                life.Damage();
            }

            // 撃破演出
            if (life.IsDie())
            {
                StartCoroutine(Die());
            }
        }
    }
}
