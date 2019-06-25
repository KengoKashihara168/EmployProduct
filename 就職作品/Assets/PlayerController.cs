using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private new Light light;
    private PlayerMove move;
    private Life life;
    private float contactTime;
    private BallController ball;

    public const int KNOCK_BACK_TIME = 10;
    private const float DEFOULT_LIGHT_INTENSITY = 10.0f;

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ball.Thrown((int)move.GetDirection());
            //Debug.Log(move.GetDirection());
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

    private void ChangeLightIntensity(float intensity)
    {
        light.intensity = intensity;
    }

    private IEnumerator Blink()
    {
        renderer.material.color = ChangeAlpha(0.0f);
        ChangeLightIntensity(1.0f);

        float invisibleTime = 0.2f;
        yield return new WaitForSeconds(invisibleTime);        

        renderer.material.color = ChangeAlpha(1.0f);
        ChangeLightIntensity(DEFOULT_LIGHT_INTENSITY);
    }

    Color ChangeAlpha(float alpha)
    {
        Color color = renderer.material.color;
        color.a = alpha;
        return color;
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
        string collisionTag = collision.gameObject.tag;

        // フレーム時間のチェック
        if (CheckFrameTime()) return;

        // ボール
        if(collisionTag.Equals("Ball"))
        {            
            ball = collision.gameObject.GetComponent<BallController>();                       
        }

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
