using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;

    [SerializeField] private TilemapCollider2D tilemap;

    private bool isThrown;
    private const float ThrownForce = 400.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Thrown(float direction)
    {
        isThrown = true;
        rigid.isKinematic = false;
        transform.parent = null;
        Vector2 force = new Vector2(ThrownForce * direction, 0.0f);
        rigid.AddForce(force);


    }

    public bool IsThrown()
    {
        return isThrown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rigid.isKinematic = true;
            rigid.velocity = Vector2.zero;
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector3(0.0f, 0.55f, 0.0f);
        }

        if(!collision.gameObject.tag.Equals("Enemy"))
        {
            isThrown = false;
        }

        if (collision.gameObject.tag.Equals("Ground"))
        {
            CircleCollider2D circle = GetComponent<CircleCollider2D>();

            //Debug.Log(circle.ClosestPoint(collision.transform.position));
            //Debug.Log(transform.position);
        }
    }
}
