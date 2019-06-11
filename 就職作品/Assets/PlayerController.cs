using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Light light;
    private PlayerMove move;
    private Life life;
    private float contactTime;

    public const int KNOCK_BACK_TIME = 10;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        life = GetComponent<Life>();
        contactTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(life.IsDie())
        {
            //Debug.Log("GameOver");
        }
    }

    /// <summary>
    /// ライトエネルギー取得
    /// </summary>
    private void LightPowerUp()
    {
        // ライトが照らす範囲を広げて少し離す
        light.range += 2;        
        light.transform.position += new Vector3(0.0f, 0.0f, -0.5f);
    }

    private IEnumerator Blink()
    {
        for(int i = 0;i < KNOCK_BACK_TIME;i++)
        {
            float alpha = Mathf.Sin(i);
            Debug.Log(alpha);
            yield return null;
        }        
    }

    private bool CheckFrameTime()
    {
        if (contactTime == Time.time) return true;
        contactTime = Time.time;
        return false;
    }

    /// <summary>
    /// トリガーとの衝突判定
    /// </summary>
    /// <param name="trigger">衝突したトリガー</param>
    void OnTriggerEnter2D(Collider2D trigger)
    {
        // フレーム時間のチェック
        if (CheckFrameTime()) return;

        // 着地
        if (trigger.tag.Equals("Ground")) move.Landing();

        // ライトエネルギー
        if (trigger.tag.Equals("Light")) LightPowerUp();

        // ライフエネルギー
        if (trigger.tag.Equals("Heal")) life.Increase();

    }

    /// <summary>
    /// コリジョンとの衝突判定
    /// </summary>
    /// <param name="collision">衝突したコリジョン</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // フレーム時間のチェック
        if (CheckFrameTime()) return;

        // 敵
        if (collision.gameObject.tag.Equals("Enemy"))
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
