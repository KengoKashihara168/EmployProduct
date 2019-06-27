using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ボールの状態を表す基底クラス
public class BallState : MonoBehaviour
{
    [SerializeField] protected PlayerMove player;
    protected BallState state;
    protected Rigidbody2D rigid;
    protected new CircleCollider2D collider;

    private void Awake()
    {
        state = GetComponent<BallState>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        state = GetComponent<Moved>();
    }

    protected virtual void OnCollision(GameObject obj) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }
}

// 止まった
public class Stopped : BallState
{
    protected override void OnCollision(GameObject obj)
    {
        if(obj.tag.Equals("Player"))
        {
            // 拾われた
            state = GetComponent<Stopped>();
            return;
        }

        // 動いている
        state = GetComponent<Moved>();
    }
}

// 拾われた
public class PickedUp : BallState
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    //public PickedUp(GameObject parent)
    //{
    //    Debug.Assert(parent.tag.Equals("Player"));
    //    transform.parent = parent.transform;
    //}

    private void Start()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
        rigid.mass = 0.0f;
        collider.enabled = false;
        transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) state = GetComponent<Thrown>();
    }

    //protected void SetParent(GameObject parent)
    //{
    //    Debug.Assert(parent.tag.Equals("Player"));
    //    transform.parent = parent.transform;
    //}


}

// 投げられた
public class Thrown : BallState
{
    private const float DEFAULT_MASS = 1.0f;

    private void Start()
    {
        transform.parent = null;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.mass = DEFAULT_MASS;
        collider.enabled = true;

        float angle = (float)player.GetDirection();
        Vector2 force = new Vector2(angle * 200.0f, 0.0f);
        rigid.AddForce(force);
    }

    protected override void OnCollision(GameObject obj)
    {
        if (obj.tag.Equals("Player")) return;

        if(obj.tag.Equals("Enemy"))
        {
            // 敵にダメージを与える
        }

        state = GetComponent<Moved>();
    }
}


// 動いた
public class Moved : BallState
{
    protected override void OnCollision(GameObject obj)
    {
        // 拾われた
        if (obj.tag.Equals("Player"))
        {
            transform.parent = obj.transform;
            state = GetComponent<PickedUp>();
        }

    }

    private void Update()
    {
        Debug.Log(state);
        // 止まっている
        if (rigid.velocity == Vector2.zero) state = GetComponent<Stopped>();
    }
}
