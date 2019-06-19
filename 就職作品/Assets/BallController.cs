using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Transform playerTransform; // プレイヤーのトランスフォーム
    private new CircleCollider2D collider;
    private bool      isCaught;        // キャッチされたフラグ

    // Start is called before the first frame update
    void Start()
    {
        isCaught = false;
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Thrown(int angle)
    {
        if (!isCaught) return;

        collider.enabled = true;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.mass = 1.0f;
        transform.parent = null;
        Vector2 force = new Vector2(angle * 200, 0.0f);
        rigid.AddForce(force);
        isCaught = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collider.enabled = false;
            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.mass = 0.0f;
            transform.parent = collision.transform;
            transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);

            isCaught = true;
        }

        if(collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("敵に当たった");
        }
    }
}
