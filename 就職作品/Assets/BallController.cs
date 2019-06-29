using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private CircleCollider2D collider;
    private const float DEFAULT_MASS = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            rigid.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Thrown();
            }
        }
        
        if(rigid.velocity.x >= 100.0f || rigid.velocity.x <= -100.0f)
        {
            //ConfigureCollider(true);
        }  
    }

    /// <summary>
    /// 持たれた
    /// </summary>
    private void Held()
    {
        // 親オブジェクトの上に配置
        transform.localPosition = new Vector3(0.0f, 0.8f, 0.0f);
        // 投げられるように物理挙動を設定
        ConfigureRigidbody(RigidbodyType2D.Kinematic, 0.0f);
        //ConfigureCollider(false);
    }

    private void Thrown()
    {
        transform.parent = null;
        ConfigureRigidbody(RigidbodyType2D.Dynamic, DEFAULT_MASS);
        float angle = (float)transform.parent.GetComponent<PlayerMove>().GetDirection();
        Vector2 force = new Vector2(angle * 2000.0f, 0.0f);
        rigid.AddForce(force);
    }

    /// <summary>
    /// 物理挙動の切り替え
    /// </summary>
    /// <param name="rigidType">ボディタイプ</param>
    /// <param name="mass">質量</param>
    /// <param name="colliderEnable">コライダーのON/OFF</param>
    private void ConfigureRigidbody(RigidbodyType2D rigidType, float mass)
    {
        rigid.bodyType = rigidType;
        rigid.mass = mass;
    }

    private void ConfigureCollider(bool colliderEnable)
    {
        collider.enabled = colliderEnable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            transform.SetParent(collision.transform);
            Held();
        }
    }
}
