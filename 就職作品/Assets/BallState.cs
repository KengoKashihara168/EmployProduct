using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ボールの状態を表す基底クラス
public abstract class BallState : MonoBehaviour
{                                                                    
    protected BallState state;
    protected Rigidbody2D rigid;
    protected new CircleCollider2D collider;

    private void Start()
    {
        //state = GetComponent<Moved>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    public abstract void Execute();

    public abstract void HitObject(GameObject obj);

    protected void ConfigureRigidbody(RigidbodyType2D rigidType, float mass, bool colliderEnable)
    {
        rigid.bodyType = rigidType;
        rigid.mass = mass;
        collider.enabled = colliderEnable;
    }
}

// 止まった
public class Stopped : BallState
{
    private Moved moved;
    private PickedUp pickedUp;

    public override void Execute()
    {
        // 動いている
        if (rigid.velocity != Vector2.zero) state = state = GetComponent<Moved>();
    }

    public override void HitObject(GameObject obj)
    {
        if(obj.tag.Equals("Player"))
        {
            // 拾われた
            state = GetComponent<PickedUp>();
        }
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
        ConfigureRigidbody(RigidbodyType2D.Kinematic, 0.0f, false);
        transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
    }

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space)) state = GetComponent<Thrown>();
    }

    public override void HitObject(GameObject obj)
    {
        // 何もしない
    }
}

// 投げられた
public class Thrown : BallState
{
    private const float DEFAULT_MASS = 1.0f;
    private PlayerMove player;

    private void Start()
    {
        transform.parent = null;
        ConfigureRigidbody(RigidbodyType2D.Dynamic, DEFAULT_MASS, true);
        Throw();
    }

    public override void Execute()
    {
        // 何もしない
    }

    private void Throw()
    {
        float angle = (float)player.GetDirection();
        Vector2 force = new Vector2(angle * 200.0f, 0.0f);
        rigid.AddForce(force);
    }

    public override void HitObject(GameObject obj)
    {
        if (obj.tag.Equals("Player"))
        {
            player = obj.GetComponent<PlayerMove>();
        }

        state = GetComponent<Moved>();
    }
}


// 動いた
public class Moved : BallState
{
    public override void HitObject(GameObject obj)
    {
        // 拾われた
        if (obj.tag.Equals("Player"))
        {
            transform.parent = obj.transform;
            state = GetComponent<PickedUp>();
        }

    }

    public override void Execute()
    {
        // 止まっている
        if (rigid.velocity == Vector2.zero) state = GetComponent<Stopped>();
    }
}