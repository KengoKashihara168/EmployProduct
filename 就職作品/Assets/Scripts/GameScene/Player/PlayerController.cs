using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AlphaController alpha;
    private PlayerMove move;
    private Life life;
    private LightController myLight;
    private float contactTime;
    private int ballIndex;

    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponent<AlphaController>();
        move = GetComponent<PlayerMove>();
        life = GetComponent<Life>();
        myLight = GetComponent<LightController>();
        contactTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(life.IsDie())
        {
            //Debug.Log("GameOver");
        }

        // スペースキーが押されたら
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // ボールを投げる
            Throw();
        }
    }

    /// <summary>
    /// ボールを投げる
    /// </summary>
    private void Throw()
    {
        if (!CanThrow()) return;
        Ball ball = transform.GetChild(ballIndex).GetComponent<Ball>();
        ball.Thrown(move.GetDirection());
    }

    /// <summary>
    /// 点滅
    /// </summary>
    /// <returns></returns>
    private IEnumerator Blink()
    {
        // プレイヤーを透明にしてライトの光量を下げる
        alpha.ChangeAlpha(0.0f);
        myLight.ChangeIntensity(1.0f);

        // コルーチンで0.2秒待つ
        float invisibleTime = 0.2f;
        yield return new WaitForSeconds(invisibleTime);

        // プレイヤーを元に戻す
        alpha.ChangeAlpha(1.0f);
        myLight.RestoreIntensity();
    }

    /// <summary>
    /// 衝突したときの時間が同じか？
    /// </summary>
    /// <returns>true:同じ時間/false:時間が違う</returns>
    private bool IsSameTime()
    {
        if (contactTime == Time.time) return true;
        // 現在の時間を新たに衝突した時間とする
        contactTime = Time.time;
        return false;
    }

    /// <summary>
    /// 投げられる？
    /// </summary>
    /// <returns>true:投げられる/false:投げられない</returns>
    private bool CanThrow()
    {
        // 子オブジェクトのタグを一つずつ比較
        for(int i = 0;i < transform.childCount;i++)
        {
            if(transform.GetChild(i).tag.Equals("Ball"))
            {
                // "Ball"があればインデックスを保存
                ballIndex = i;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// トリガーとの衝突判定
    /// </summary>
    /// <param name="trigger">衝突したトリガー</param>
    void OnTriggerEnter2D(Collider2D trigger)
    {
        // フレーム時間のチェック
        if (IsSameTime()) return;

        // 着地
        if (trigger.tag.Equals("Ground")) move.Landing();

        // ライトエネルギー
        if (trigger.tag.Equals("Light")) myLight.PowerUp();

        // ライフエネルギー
        if (trigger.tag.Equals("Heal")) life.Increase();

    }

    /// <summary>
    /// コリジョンとの衝突判定
    /// </summary>
    /// <param name="collision">衝突したコリジョン</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        // フレーム時間のチェック
        if (IsSameTime()) return;

        // 敵
        if (collisionTag.Equals("Enemy"))
        {
            // ダメージを食らう
            life.Damage();
            // 敵に当たった処理
            move.HitEnemy(collision.transform.position);
            // 点滅
            StartCoroutine(Blink());
        }
    }
}
