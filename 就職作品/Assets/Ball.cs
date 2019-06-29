using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigid.isKinematic = false;
            Vector2 force = new Vector2(400.0f, 0.0f);
            force *= (float)player.GetComponent<PlayerMove>().GetDirection();
            rigid.AddForce(force);
        }
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
    }
}
